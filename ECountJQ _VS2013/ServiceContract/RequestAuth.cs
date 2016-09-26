using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SGM.ECount.Contract.Service
{
    public class RequestAuth : IClientMessageInspector, IEndpointBehavior
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public RequestAuth(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string hName = "UserName";
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string hNameSpace = "http://Gardon";

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private string hPwd = "Password";

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            return;
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            MessageHeader<string> headerUser = new MessageHeader<string>();
            headerUser.Actor = "anyone";
            headerUser.Content = UserName;
            MessageHeader unTypedHeaderUser = headerUser.GetUntypedHeader(hName, hNameSpace);
            request.Headers.Add(unTypedHeaderUser);

            MessageHeader<string> headerPwd = new MessageHeader<string>();
            headerPwd.Actor = "anyone";
            headerPwd.Content = Password;
            MessageHeader unTypedHeaderPwd = headerPwd.GetUntypedHeader(hPwd, hNameSpace);
            request.Headers.Add(unTypedHeaderPwd);


            return null;
        }

        #endregion

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            return;
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            return;
        }

        #endregion
    }
}
