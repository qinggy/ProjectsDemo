using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace System.ComponentModel.Composition.Aop
{

    static class ProxyGenerator
    {
        /// <summary>
        /// 代理对象的一个简单缓存
        /// </summary>
        private static readonly ConcurrentDictionary<Type, Type> _proxyTypeCache = new ConcurrentDictionary<Type, Type>();

        /// <summary>
        /// 创建对象的代理
        /// </summary>
        /// <param name="obj">要创建代理的对象</param>
        /// <returns>创建完成的代理</returns>
        public static object CreateProxy(AOPCompositionContainer container, object obj)
        {
            var attrs = obj.GetType().GetCustomAttributes(typeof(IInterceptor), true);

            if (attrs == null || attrs.Length == 0) return obj;

            var interceptors = attrs.Select(x => { container.ComposeParts(x); return (IInterceptor)x; })
                .OrderBy(x => x.Order)
                .ToArray();

            return CreateProxy(obj, interceptors);
        }

        public static object CreateProxy(object obj)
        {
            var attrs = obj.GetType().GetCustomAttributes(typeof(IInterceptor), true);

            if (attrs == null || attrs.Length == 0) return obj;

            var interceptors = attrs.Select(x => (IInterceptor)x)
                .OrderBy(x => x.Order)
                .ToArray();

            return CreateProxy(obj, interceptors);
        }

        private static object CreateProxy(object target, IInterceptor[] interceptors)
        {
            var targetType = target.GetType();

            if (!_proxyTypeCache.ContainsKey(targetType))
                _proxyTypeCache[targetType] = GenerateProxyType(targetType);

            return Activator.CreateInstance(_proxyTypeCache[targetType], target, interceptors);
        }


        /// <summary>
        /// 根据目标类型，生成代理类型
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        private static Type GenerateProxyType(Type targetType)
        {
            var assemblyName = new AssemblyName("System.ComponentModel.Composition.Aop.Proxies");
#if DEBUG
            var assemblyDef = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            var moduleDef = assemblyDef.DefineDynamicModule(assemblyName.Name, "System.ComponentModel.Composition.Aop.Proxies.dll");
#else

            var assemblyDef = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleDef = assemblyDef.DefineDynamicModule(assemblyName.Name);
#endif

            // 继承至 targetType 的一个代理类型
            var typeDef = moduleDef.DefineType(targetType.FullName + "__Proxy", TypeAttributes.Public, targetType);

            var context = new EmitContext(targetType, typeDef);

            EmitProxyTypeCustomeAttributes(context);
            EmitProxyTypeFields(context);
            EmitProxyTypeConstructor(context);

            var targetMethods = targetType.GetMethods();

            foreach (var method in targetMethods)
            {
                if (method.IsFinal) continue;
                if (!method.IsVirtual && !method.IsAbstract) continue;
                if (method.Name == "ToString") continue;
                if (method.Name == "GetHashCode") continue;
                if (method.Name == "Equals") continue;

                EmitProxyTypeMethod(context, method);
            }


            var result = typeDef.CreateType();
#if DEBUG
            assemblyDef.Save("System.ComponentModel.Composition.Aop.Proxies.dll");
#endif
            return result;
        }

        // 给 Emit 出的类型增加 CustomeAttributes
        private static void EmitProxyTypeCustomeAttributes(EmitContext context)
        {
            var constructorInfo = typeof(System.ComponentModel.Composition.PartNotDiscoverableAttribute)
                .GetConstructor(Type.EmptyTypes);

            var customAttributeBuilder = new CustomAttributeBuilder(constructorInfo, new object[] { });

            // 设置 PartNotDiscoverableAttribute， 使得代理不会被 MEF 找到
            context.TypeBuilder.SetCustomAttribute(customAttributeBuilder);

        }

        // Emit 成员字段
        private static void EmitProxyTypeFields(EmitContext context)
        {
            context.FieldBuilders["__obj"] = context.TypeBuilder.DefineField("__obj", typeof(object), FieldAttributes.Private);
            context.FieldBuilders["__interceptors"] = context.TypeBuilder.DefineField("__interceptors", typeof(IInterceptor[]), FieldAttributes.Private);
        }

        // Emit 构造函数
        private static void EmitProxyTypeConstructor(EmitContext context)
        {
            var ctorDef = context.TypeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard,
                new Type[] { context.BaseType, typeof(IInterceptor[]) });

            var il = ctorDef.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, context.BaseType.GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Stfld, context.FieldBuilders["__obj"]);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_2);
            il.Emit(OpCodes.Stfld, context.FieldBuilders["__interceptors"]);

            il.Emit(OpCodes.Ret);
        }

        private static readonly Type VoidType = Type.GetType("System.Void");
        // Emit 拦截的方法，必须为 virtual 或 abstract
        private static void EmitProxyTypeMethod(EmitContext context, MethodInfo method)
        {
            var parameterInfos = method.GetParameters();
            var parameterTypes = parameterInfos.Select(p => p.ParameterType).ToArray();
            var parameterLength = parameterTypes.Length;

            var hasResult = method.ReturnType != VoidType;

            var methodBuilder = context.TypeBuilder.DefineMethod(method.Name,
                MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual,
                method.ReturnType, parameterTypes);

            if (method.IsGenericMethod)
            {
                // 简单支持泛型，未配置泛型约束
                methodBuilder.DefineGenericParameters(method.GetGenericArguments().Select(x => x.Name).ToArray());
            }

            var il = methodBuilder.GetILGenerator();

            il.DeclareLocal(typeof(object[]));   // arguments
            il.DeclareLocal(typeof(Type[]));     // argumentTypes
            il.DeclareLocal(typeof(MethodInfo)); // method
            il.DeclareLocal(typeof(Invocation)); // invocation

            if (method.IsGenericMethod) // 定义泛型参数的实际类型
            {
                il.DeclareLocal(typeof(Type[]));     // genericTypes

                var genericArguments = methodBuilder.GetGenericArguments();

                il.Emit(OpCodes.Ldc_I4, genericArguments.Length);
                il.Emit(OpCodes.Newarr, typeof(Type));
                il.Emit(OpCodes.Stloc_S, 4);

                for (int i = 0; i < genericArguments.Length; i++)
                {
                    il.Emit(OpCodes.Ldloc_S, 4);
                    il.Emit(OpCodes.Ldc_I4, i);
                    il.Emit(OpCodes.Ldtoken, genericArguments[i]);
                    il.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle", new Type[] { typeof(RuntimeTypeHandle) }));
                    il.Emit(OpCodes.Stelem_Ref);
                }
            }


            // arguments = new object[parameterLength]
            il.Emit(OpCodes.Ldc_I4, parameterLength);
            il.Emit(OpCodes.Newarr, typeof(object));
            il.Emit(OpCodes.Stloc_0);

            //argumentTypes = new Type[parameterLength]
            il.Emit(OpCodes.Ldc_I4, parameterLength);
            il.Emit(OpCodes.Newarr, typeof(Type));
            il.Emit(OpCodes.Stloc_1);

            for (int i = 0; i < parameterLength; i++)
            {
                // arguments[i] = arg[i]
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg, i + 1);  // arg[0] == this
                if (parameterTypes[i].IsValueType || parameterTypes[i].IsGenericParameter)
                    il.Emit(OpCodes.Box, parameterTypes[i]);
                il.Emit(OpCodes.Stelem_Ref);

                // argumentTypes[i] = arg[i].GetType()
                il.Emit(OpCodes.Ldloc_1);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg, i + 1);  // arg[0] == this
                if (parameterTypes[i].IsValueType || parameterTypes[i].IsGenericParameter)
                    il.Emit(OpCodes.Box, parameterTypes[i]);
                il.Emit(OpCodes.Callvirt, typeof(object).GetMethod("GetType"));
                il.Emit(OpCodes.Stelem_Ref);

            }

            // method =  ReflectionHelper.GetMethod(this.__obj.GetType(), "TestMethod", array2, true);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, context.FieldBuilders["__obj"]);
            il.Emit(OpCodes.Callvirt, context.BaseType.GetMethod("GetType"));
            il.Emit(OpCodes.Ldstr, method.Name);
            il.Emit(OpCodes.Ldloc_1);
            if (method.IsGenericMethod)
                il.Emit(OpCodes.Ldc_I4_1);
            else
                il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Call, typeof(ReflectionHelper).GetMethod("GetMethod"));

            if (method.IsGenericMethod)
            {
                il.Emit(OpCodes.Ldloc_S, 4);
                il.Emit(OpCodes.Callvirt, typeof(MethodInfo).GetMethod("MakeGenericMethod"));
            }

            il.Emit(OpCodes.Stloc_2);


            // invocation = new Invocation(_obj, this, method, arguments, _interceptors)
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, context.FieldBuilders["__obj"]);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_2);
            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldfld, context.FieldBuilders["__interceptors"]);
            il.Emit(OpCodes.Newobj, typeof(Invocation).GetConstructor(new Type[] { typeof(object), typeof(object), typeof(MethodInfo), typeof(object[]), typeof(IInterceptor[]) }));
            il.Emit(OpCodes.Stloc_3);

            // invocation.Proceed()
            il.Emit(OpCodes.Ldloc_3);
            il.Emit(OpCodes.Callvirt, typeof(Invocation).GetMethod("Proceed"));

            // 返回值
            if (hasResult)
            {
                il.Emit(OpCodes.Ldloc_3);
                il.Emit(OpCodes.Callvirt, typeof(Invocation).GetMethod("get_ReturnValue"));
                if (method.ReturnType.IsValueType || method.ReturnType.IsGenericParameter)
                    il.Emit(OpCodes.Unbox_Any, method.ReturnType);
            }

            il.Emit(OpCodes.Ret);

        }

    }


    class EmitContext
    {
        public EmitContext(Type baseType, TypeBuilder typeBuilder)
        {
            this.BaseType = baseType;
            this.TypeBuilder = typeBuilder;
        }

        public Type BaseType { get; protected set; }

        public TypeBuilder TypeBuilder { get; protected set; }


        private readonly Dictionary<string, FieldBuilder> _fields = new Dictionary<string, FieldBuilder>();

        public Dictionary<string, FieldBuilder> FieldBuilders { get { return _fields; } }

    }

}
