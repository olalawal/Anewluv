using Anewluv.Domain.Data.ViewModels;
using Nmedia.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.Apikey
{
    public class CustomHeaderMessageInspector : IDispatchMessageInspector
    {
        Dictionary<string, string> requiredHeaders;
        public CustomHeaderMessageInspector(Dictionary<string, string> headers)
        {
            requiredHeaders = headers ?? new Dictionary<string, string>();
        }

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            var httpHeader = reply.Properties["httpResponse"] as HttpResponseMessageProperty;
            foreach (var item in requiredHeaders)
            {
                httpHeader.Headers.Add(item.Key, item.Value);
            }
        }
    }

    public class EnableCrossOriginResourceSharingBehavior : BehaviorExtensionElement, IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {

        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {

        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            var requiredHeaders = new Dictionary<string, string>();

            requiredHeaders.Add("Access-Control-Allow-Origin", "*");
            requiredHeaders.Add("Access-Control-Request-Method", "POST,GET,PUT,DELETE,OPTIONS");
            requiredHeaders.Add("Access-Control-Allow-Headers", "X-Requested-With,Content-Type");

            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new CrossOriginResourceSharingMessageInspector(requiredHeaders));
        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }

        public override Type BehaviorType
        {
            get { return typeof(EnableCrossOriginResourceSharingBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new EnableCrossOriginResourceSharingBehavior();
        }
    }


    public class CrossOriginResourceSharingMessageInspector : IDispatchMessageInspector
    {
        private Dictionary<string, string> requiredHeaders;

        public CrossOriginResourceSharingMessageInspector(Dictionary<string, string> requiredHeaders)
        {
            this.requiredHeaders = requiredHeaders ?? new Dictionary<string, string>();
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            HttpRequestMessageProperty httpRequestHeader = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;

            if (httpRequestHeader.Method.ToUpper() == "OPTIONS")
                instanceContext.Abort();

            //else if (httpRequestHeader.Headers[HttpRequestHeader.Authorization] == null)
            //    instanceContext.Abort();

            // Check to see if there is an Authorization in the header, otherwise throw a 401
            //if (WebOperationContext.Current.IncomingRequest.Headers["Apikey"] == null)
            //{
            //    WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"myrealm\"");
            //    throw new WebFaultException<string>("Authorization has not been set.", HttpStatusCode.Unauthorized);
            //}
            //else // Decode the header, check password
            //{
            //   // string encodedUnamePwd = GetEncodedCredentialsFromHeader();
            //   // if (!string.IsNullOrEmpty(encodedUnamePwd))
            // //   {
            //        //// Decode the credentials
            //        //byte[] decodedBytes = null;
            //        //try
            //        //{
            //        //    decodedBytes = Convert.FromBase64String(encodedUnamePwd);
            //        //}
            //        //catch (FormatException)
            //        //{
            //        //    return false;
            //        //}

            //        //string credentials = ASCIIEncoding.ASCII.GetString(decodedBytes);

            //        //// Validate User and Password
            //        //string[] authParts = credentials.Split(':');
            //        //CustomUserNameValidator oCustomUserNameValidator = new CustomUserNameValidator();
            //        //oCustomUserNameValidator.Validate(authParts[0], authParts[1]);


            //  //  }

            //}
            return null;


            return httpRequestHeader;
        }




        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            HttpRequestMessageProperty httpRequestHeader = correlationState as HttpRequestMessageProperty;
            HttpResponseMessageProperty httpResponseHeader = reply.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;

            foreach (KeyValuePair<string, string> item in this.requiredHeaders)
                httpResponseHeader.Headers.Add(item.Key, item.Value);

            string origin = httpRequestHeader.Headers["origin"];
            if (origin != null)
                httpResponseHeader.Headers.Add("Access-Control-Allow-Origin", origin);

            string method = httpRequestHeader.Method;
            if (method.ToUpper() == "OPTIONS")
            {
                httpResponseHeader.StatusCode = HttpStatusCode.NoContent;
            }

            //else if (httpRequestHeader.Headers[HttpRequestHeader.Authorization] == null)
            //{
            //    httpResponseHeader.StatusDescription = "Unauthorized";
            //    httpResponseHeader.StatusCode = HttpStatusCode.Unauthorized;
            //}
        }

    }  
}
