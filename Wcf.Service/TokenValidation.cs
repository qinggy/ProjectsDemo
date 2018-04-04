using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Web;

namespace Wcf.Service
{
    public class TokenValidation : Attribute, IOperationBehavior, IOperationInvoker
    {
        private IOperationInvoker _invoker;

        #region IOperationInvoker Members

        public object[] AllocateInputs()
        {
            return _invoker.AllocateInputs();
        }

        public object Invoke(object instance, object[] inputs, out object[] outputs)
        {
            if (Authenticate())
                return _invoker.Invoke(instance, inputs, out outputs);
            else
            {
                outputs = null;
                return null;
            }
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronous
        {
            get { return true; }
        }

        #endregion

        #region IOperationBehavior Members

        public void AddBindingParameters(OperationDescription operationDescription, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {

        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            _invoker = dispatchOperation.Invoker;
            dispatchOperation.Invoker = this;
        }

        public void Validate(OperationDescription operationDescription)
        {

        }

        #endregion

        private bool Authenticate()
        {
            string UserToken = GetTokens();

            if (!String.IsNullOrEmpty(UserToken))
            {
                //读取数据库校验当前请求Api的用户是否存在调用该Api的权限 etc.
                //UserBusiness userBusiness = new UserBusiness();
                //if (!userBusiness.ValidateUserToken(UserToken))
                //{
                //    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
                //}
                //else
                return true;
            }

            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;

            return false;
        }

        private string GetTokens()
        {
            //WebOperationContext.Current.IncomingRequest.Headers 注意IncomingRequest.Headers和IncomingMessageHeaders的区别
            //前者获取http请求报文Header中的内容，后者获取http请求报文中body中的Header下内容，相比后者的安全性更高
            string userToken = Convert.ToString(OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("TokenHeader", "TokenNameSpace"));
            if (userToken != null)
                userToken = userToken.Trim();
            if (!string.IsNullOrEmpty(userToken))
            {
                return userToken;
            }

            return null;
        }
    }
}