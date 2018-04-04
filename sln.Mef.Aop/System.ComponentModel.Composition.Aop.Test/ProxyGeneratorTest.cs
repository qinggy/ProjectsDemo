using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace System.ComponentModel.Composition.Aop.Test
{
    [TestClass]
    public class ProxyGeneratorTest
    {
        [TestMethod]
        public void TestMethod01()
        {
            var objTarget = new MockTarget();
            var objProxy = ProxyGenerator.CreateProxy(objTarget) as MockTarget;

            Assert.IsNotNull(objProxy);

            Assert.AreEqual("Intercepted", objProxy.GetString(1, true, "3"));
        }

        [TestMethod]
        public void TestMethod02()
        {
            var objTarget = new MockTarget();
            var objProxy = ProxyGenerator.CreateProxy(objTarget) as MockTarget;

            Assert.IsNotNull(objProxy);

            Assert.AreEqual("A", objProxy.Get("A"));
            Assert.AreEqual(12, objProxy.Get(12));
        }
    }



    [Interceptor(typeof(MockInterceptor))]
    public class MockTarget
    {
        public virtual string GetString(int a, bool b, string c)
        {
            return c;
        }

        public virtual T Get<T>(T a)
        {
            return a;
        }

    }


    public class MockInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name == "GetString")
                invocation.ReturnValue = "Intercepted";
            else
                invocation.Proceed();

        }

        public int Order
        {
            get { return 0; }
        }
    }


}
