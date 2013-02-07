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

namespace Shell.MVC2.Data
{
    public class APIKeyAuthorization : ServiceAuthorizationManager
    {
        IAPIkeyRepository _apikeyrepository;

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

        }


        //TO DO re-activate this code when you complete basic testing
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            //for now while testing ignore api key
            return true;


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
         
              //allows service to be discovereable with no api key
              if (OperationContext.Current.IncomingMessageHeaders.To.Segments.Last().Replace("/","") != "$metadata") 
              {
                  string key = GetAPIKey(operationContext);

                  // _apikeyrepository.IsValidAPIKey(key);

                  if (_apikeyrepository.IsValidAPIKey(key))
                  {
                      return true;
                  }
                  else
                  {
                      // Send back an HTML reply
                      CreateErrorReply(operationContext, key);
                      return false;
                  }
              }
              else
              {
                  return true;
              }
           

          
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

        private static void CreateErrorReply(OperationContext operationContext, string key)
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
    }

   

}
