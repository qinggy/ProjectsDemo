using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using System.Net;
using System.ServiceModel.Channels;
using DinoTechDataSyncService.Domain;
using DinoTechDataSyncService.Business;


namespace DinoTechDataSyncService.WCF
{
    public class TokenValidation : Attribute, IOperationBehavior, IOperationInvoker
    {

        #region Private Fields

        private IOperationInvoker _invoker;

        #endregion

        #region IOperationBehavior Members

        public void ApplyDispatchBehavior(OperationDescription operationDescription,
                                          DispatchOperation dispatchOperation)
        {
            _invoker = dispatchOperation.Invoker;
            dispatchOperation.Invoker = this;
        }

        public void ApplyClientBehavior(OperationDescription operationDescription,
                                        ClientOperation clientOperation)
        {
        }

        public void AddBindingParameters(OperationDescription operationDescription,
                                         BindingParameterCollection bindingParameters)
        {
        }

        public void Validate(OperationDescription operationDescription)
        {
            //   Authenticate("Client Name here");
        }

        #endregion

        #region IOperationInvoker Members

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

        public object[] AllocateInputs()
        {
            return _invoker.AllocateInputs();
        }

        public IAsyncResult InvokeBegin(object instance, object[] inputs,
                                        AsyncCallback callback, object state)
        {
            throw new NotSupportedException();
        }

        public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
        {
            throw new NotSupportedException();
        }

        public bool IsSynchronous
        {
            get
            {
                return true;
            }
        }

        #endregion

        private bool Authenticate()
        {
            string UserToken = GetTokens(WebOperationContext.Current.IncomingRequest.Headers);

            if (!String.IsNullOrEmpty(UserToken))
            {

                UserBusiness userBusiness = new UserBusiness();
                if (!userBusiness.ValidateUserToken(UserToken))
                {
                    WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;
                }
                else
                    return true;
            }
            WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.Unauthorized;

            return false;
        }

        private string GetTokens(WebHeaderCollection headers)
        {
            //   string userToken = WebOperationContext.Current.IncomingRequest.Headers["token"];
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
