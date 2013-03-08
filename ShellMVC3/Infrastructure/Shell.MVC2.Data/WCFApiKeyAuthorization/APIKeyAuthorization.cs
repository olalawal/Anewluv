using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Collections.Specialized;
using System.Web;
using System.IO;
using System.Xml.Linq;
using System.Net;
using Shell.MVC2.Infrastructure.Interfaces;
using Shell.MVC2.Infrastructure.Entities.ApiKeyModel;
using Shell.MVC2.Data.AuthenticationAndMembership;
using Shell.MVC2.Interfaces;
using Shell.MVC2.Domain.Entities.Anewluv;
using Dating.Server.Data.Models;
using Shell.MVC2.Infastructure;


//code sample of header and how to get it from this code

//IT SHOULD APPEAR AS BELOW IN HEADER:

//POST /MyURL/ HTTP/1.1 
//Host hostname 
//Content-Length 396 
//Origin chrome-extension://cokgbflfommojglbmbpenpphppikmonn 
//UserID 12345 
//Password 98765abc 
//User-Agent Mozilla/5.0 
//AppleWebKit/535.11 (KHTML, like Gecko) Chrome/17.0.963.56 Safari/535.11 
//Content-Type application/json 
//Accept / 
//Accept-Encoding gzip,deflate,sdch 
//Accept-Language en-US,en;q=0. 

//code below shows how to add headers to request
//        using (ChannelFactory<IMyServiceChannel> factory = 
//   new ChannelFactory<IMyServiceChannel>(new NetTcpBinding()))
//  {
//   using (IMyServiceChannel proxy = factory.CreateChannel(...))
//   {
//      using ( OperationContextScope scope = new OperationContextScope(proxy) )
//      {
//         Guid apiKey = Guid.NewGuid();
//         MessageHeader<Guid> mhg = new MessageHeader<Guid>(apiKey);
//         MessageHeader untyped = mhg.GetUntypedHeader("apiKey", "ns");
//         OperationContext.Current.OutgoingMessageHeaders.Add(untyped);

//         proxy.DoOperation(...);
//      }
//   }                    
//}

//Retriving the header value
//     Guid apiKey =
//OperationContext.Current.IncomingMessageHeaders.GetHeader<Guid>("apiKey", "ns");




namespace Shell.MVC2.Data
{
    public class APIKeyAuthorization : ServiceAuthorizationManager
    {
        IAPIkeyRepository _apikeyrepository;
        AnewLuvMembershipProvider _membmbershipprovider;
        private IGeoRepository _georepository;
        private IMemberRepository _memberepository;
        private AnewluvContext _datingcontext;
        private IPhotoRepository _photorepository;

        //public APIKeyAuthorization(IAPIkeyRepository apikeyrepository)
        //{
        //    _apikeyrepository = apikeyrepository;
        //}
        //had to set up a parameterless contrycture due to WCF restructtions 
        //its not too bad since this only seems to be called on the first instantiaion
        public APIKeyAuthorization()
        {
             ApiKeyContext context = new ApiKeyContext();
            _apikeyrepository = new APIkeyRepository(context);
         //   PostalData2Entities postalcontext = new PostalData2Entities();
        //   _georepository = new GeoRepository(postalcontext);
              AnewluvContext  datingcontext = new AnewluvContext();
              _datingcontext = datingcontext;
              _memberepository = new MemberRepository(datingcontext);
             _photorepository = new PhotoRepository(datingcontext, _memberepository);

            _membmbershipprovider = new AnewLuvMembershipProvider(_datingcontext, _georepository, _memberepository, _photorepository);

        }


        //TO DO re-activate this code when you complete basic testing
        //2-15-2013 olawal validate username password for some URI's
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            bool validrequest = false;


            //for now while testing ignore api key
            //Inject this somewhere or add to API key repo
            List<string> nonauthenticatedservices = new List<string>();
            List<string> nonauthenticatedURLS = new List<string>();

            //TO DO this list needs to be more broken down
            nonauthenticatedservices.Add("Shell.MVC2.Web.NotificationService");
            nonauthenticatedservices.Add("Shell.MVC2.Web.AuthenticationService");
            nonauthenticatedservices.Add("Shell.MVC2.Web.Common");
            nonauthenticatedservices.Add("Shell.MVC2.Web.GeoService");
            nonauthenticatedservices.Add("Shell.MVC2.Web.MediaService");
            //TO DO add code to  call membership service and make sure the requestor has rights to view the data they are requesting
            //TO DO List the Service URLS that and handle differing security for each 

            string[] urisegments = OperationContext.Current.IncomingMessageHeaders.To.Segments;
            string helpsegment = "help"; //this is the thing we are checking   
            string restsegment = "rest"; //this is the thing we are checking 
           
            //allow access to help page, even if they added help to a wrong URL they could not get in
            //if last segment is rest no operation is getting activated so ok to display help service page
            //if we have help in the url dont do api key verifcation
            if (urisegments.Last().Replace("/", ""  ).ToLower() == restsegment ||
                urisegments[4].Replace("/", ""  ) == helpsegment 
                )
            return true;
            
            //check if we are looking at the URLS or specific methods that allow Anonymoys access
            if (nonauthenticatedservices.Contains(urisegments[1].ToString())) return true;
            //look at the urls for specicif URLS that allow anonymous
            if (nonauthenticatedURLS.Contains(urisegments[2].ToString())) return true;


              //allows service to be discovereable with no api key
              if (OperationContext.Current.IncomingMessageHeaders.To.Segments.Last().Replace("/","") != "$metadata") 
              {
                  string key = GetAPIKey(operationContext);

                  validrequest = true;
                  //TO DO re-implect api key
                  //if (_apikeyrepository.IsValidAPIKey(key))
                  //{
                  //    validrequest = true;
                  //}
                  //else
                  //{
                  //    // Send back an HTML reply
                  //    CreateErrorReply(operationContext, key);
                  //    validrequest = false;
                  //}
              }
              else
              {
                  validrequest = true;
              }
           
           
             //get username and password from request stuff if API key was valid
            if (validrequest == true)
            {
                var authinfo = GetUserNamePassword(operationContext);
                if (authinfo != null)
                {
                    return _membmbershipprovider.ValidateUser(authinfo[0], authinfo[1]);
                }
                else
                {
                    CreateUserNamePasswordErrorReply(operationContext);
                }
            }
              
             return validrequest;
        }

        public string GetAPIKey(OperationContext operationContext)
        {
            // Get the request message
            var request = operationContext.RequestContext.RequestMessage;

            // Get the HTTP Request
            var requestProp = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];

            // Get the query string
            NameValueCollection queryParams = HttpUtility.ParseQueryString(requestProp.QueryString);

            // Return the API key (if present, null if not)
            return queryParams[APIKEY];
        }

        public string[] GetUserNamePassword(OperationContext operationContext )
        {

            try
            {
                var message = operationContext.RequestContext.RequestMessage;
                var request = (HttpRequestMessageProperty)message.Properties[HttpRequestMessageProperty.Name];
                string authorization = request.Headers[HttpRequestHeader.Authorization];
                if ( authorization !=null && authorization  != "" ) 
                return Encryption.DecodeBasicAuthenticationString(authorization);
                
                return  null;

               // string username = operationContext.IncomingMessageHeaders.GetHeader<string>("username","");
              //  string password = operationContext.IncomingMessageHeaders.GetHeader <string>("password", string.Empty);
               // return _membmbershipprovider.ValidateUser(username, password);
            }
            catch (Exception ex)
            {
                //return false;
                throw ex;

            }
   
        }

        private static void CreateApiKeyErrorReply(OperationContext operationContext, string key)
        {
            // The error message is padded so that IE shows the response by default
            using (var sr = new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + APIErrorHTML))
            {
                XElement response = XElement.Load(sr);
                using (Message reply = Message.CreateMessage(MessageVersion.None, null, response))
                {
                    HttpResponseMessageProperty responseProp = new HttpResponseMessageProperty() { StatusCode = HttpStatusCode.Unauthorized, StatusDescription = String.Format("'{0}' is an invalid API key", key) };
                    responseProp.Headers[HttpResponseHeader.ContentType] = "text/html";
                    reply.Properties[HttpResponseMessageProperty.Name] = responseProp;
                    operationContext.RequestContext.Reply(reply);
                    // set the request context to null to terminate processing of this request
                    operationContext.RequestContext = null;
                }
            }
        }

        private static void CreateUserNamePasswordErrorReply(OperationContext operationContext)
        {
            // The error message is padded so that IE shows the response by default
            using (var sr = new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + UserNamePasswordErrorHTML ))
            {
                XElement response = XElement.Load(sr);
                using (Message reply = Message.CreateMessage(MessageVersion.None, null, response))
                {
                    HttpResponseMessageProperty responseProp = new HttpResponseMessageProperty() { StatusCode = HttpStatusCode.Unauthorized, StatusDescription = String.Format("Username and password is invlaid") };
                    responseProp.Headers[HttpResponseHeader.ContentType] = "text/html";
                    reply.Properties[HttpResponseMessageProperty.Name] = responseProp;
                    operationContext.RequestContext.Reply(reply);
                    // set the request context to null to terminate processing of this request
                    operationContext.RequestContext = null;
                }
            }
        }

        const string APIKEY = "APIKey";
        const string APIErrorHTML = @"
<html>
<head>
    <title>Request Error - No API Key</title>
    <style type=""text/css"">
        body
        {
            font-family: Verdana;
            font-size: large;
        }
    </style>
</head>
<body>
    <h1>
        Request Error
    </h1>
    <p>
        A valid API key needs to be included using the apikey query string parameter
    </p>
</body>
</html>
";

        const string UserNameAndPassword = "UserNamePassword";
        const string UserNamePasswordErrorHTML = @"
<html>
<head>
    <title>Request Error - No username and password auth info in request header</title>
    <style type=""text/css"">
        body
        {
            font-family: Verdana;
            font-size: large;
        }
    </style>
</head>
<body>
    <h1>
        Request Error
    </h1>
    <p>
        The request you made requires further authorization ! please include the username and password of the requestor 
        requests to this service.
    </p>
</body>
</html>
";

    }

   

}
