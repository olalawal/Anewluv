using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Net;
using System.Xml.Linq;
using System.IO;
using System.Collections.Specialized;
using LoggingLibrary;

using System.Runtime.Serialization.Json;

//using Anewluv.Domain.Data.ViewModels;
using System.Web;
using Nmedia.Infrastructure;
using Nmedia.Infrastructure.Domain.Data.log;
using Nmedia.Infrastructure.Domain;
using Nmedia.Infrastructure.Domain.Data;
//using Nmedia.Services.ApikeyAuthorization;
using Anewluv.Domain;
using Anewluv.Domain.Data.ViewModels;
using System.Threading.Tasks;
using Nmedia.Infrastructure.Domain.Data.ApiKey;
using System.Xml;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.Anewluv.ViewModels;
//using Anewluv.Lib;
//using Nmedia.Services.Authorization;
//using Anewluv.Domain;
//using Anewluv.Services.Authentication;

namespace Anewluv.Apikey
{
    //implement this using the api key servbice key service i.e , which is referenced in this API as well
    //sample 
    //http://stackoverflow.com/questions/11192610/inject-repository-into-serviceauthorizationmanager-using-unity
    public class ApiKeyAuthorization : ServiceAuthorizationManager
    {
       

        //public APIKeyAuthorization(IAPIkeyRepository apikeyrepository)
        //{
        //    _apikeyrepository = apikeyrepository;
        //}
        //had to set up a parameterless contrycture due to WCF restructtions 
        //its not too bad since this only seems to be called on the first instantiaion
        public ApiKeyAuthorization()
        {
         

        }


        //TO DO re-activate this code when you complete basic testing
        //2-15-2013 olawal validate username password for some URI's
        public override bool CheckAccess(OperationContext operationContext, ref Message message)
        {
           // string[] authinfo;


            try
            {
                bool validrequest = true;
                bool apikeyauthonly = false;  //items that only require a valid api key to make the request

                //if its preflight allow all
                //if (OperationContext.Current.RequestContext  == "OPTIONS")


                //store the message here so we can parse body for things like geodata profile id etc
                MessageBuffer buffer = operationContext.RequestContext.RequestMessage.CreateBufferedCopy(8192);
                message = buffer.CreateMessage();
                Message internalCopy = buffer.CreateMessage();
                buffer.Close();

                if (OperationContext.Current != null)
                {
                    const String HttpRequestKey = "httpRequest";
                    const String MethodName = "OPTIONS"; 
                    MessageProperties messageProperties = OperationContext.Current.IncomingMessageProperties;

                    // get http request
                    var httpRequest = (HttpRequestMessageProperty)OperationContext.Current.IncomingMessageProperties[HttpRequestKey];

                    // extract credentials
                    // var username =  
                    if (httpRequest.Method.Contains(MethodName))
                    {
                        PreflightRequestAproval(operationContext);
                        return true;
                    }
                    // [MethodName];
                   
                }


                //for now while testing ignore api key
                //Inject this somewhere or add to API key repo
               // List<string> nonauthenticatedservices = new List<string>();
                List<string> apikeyonlyURLS = new List<string>();  //need apikey authorization only
                List<string> nonauthenticatedURLS = new List<string>();  //urals that are not authenticated at all

                //everything else needs api and user auth

                //TO DO this list needs to be more broken down
                nonauthenticatedURLS.Add("Nmedia.Infrastructure.Web.Services.Logging");
                nonauthenticatedURLS.Add("Nmedia.Infrastructure.Web.Services.Notification");
                //nonauthenticatedURLS.Add("Nmedia.Infrastructure.Web.Services.Authorization");
                //these methods use internal rest calls so need to be excluded
                //nonauthenticatedURLS.Add("updateuserlogintimebyprofileidandsessionid");  // do this this way until we determine how to add headers
                //nonauthenticatedURLS.Add("validateuserandgettoken");  //internal call no auth required
               // nonauthenticatedURLS.Add("validateuserandgettoken");  //quick search is not authenticated since its used on main page


                //allow calls to API key auth only i.e we dont validate profile ID is tied to a valid apikey for these calls since they are generic calls
                //TO do we need to create a server side method that generates or randomizes the apikey so folks cannot use our calls for other things
                //withoute permission.  For mobile a permemant apikey can be embeded in the code, for web the web server has a call to allow the client to grab the IP 
                //do not expose it in the client somehow.
                apikeyonlyURLS.Add("Anewluv.Web.Services.Authentication");                                  
                apikeyonlyURLS.Add("Anewluv.Web.Services.Common");
                apikeyonlyURLS.Add("Anewluv.Web.Services.Spatial");
                apikeyonlyURLS.Add("/Anewluv.Web.Services.Media/PhotoService.svc/Rest/addphotos");  //everyone can call addphotos
                //added quick search to this apikey only to allow for quick search to 
                //to bypass userid and password auth
                apikeyonlyURLS.Add("/Anewluv.Web.Services.Members/MembersMapperService.svc/Rest/getquicksearch"); 
               

                             
                //allow all photo uploads
                //TO DO add code to  call membership service and make sure the requestor has rights to view the data they are requesting
                //TO DO List the Service URLS that and handle differing security for each 

                string path = OperationContext.Current.IncomingMessageHeaders.To.AbsolutePath;
                string[] urisegments = OperationContext.Current.IncomingMessageHeaders.To.Segments;
                string helpsegment = "help"; //this is the thing we are checking   
                string restsegment = "rest"; //this is the thing we are checking 

                //allow access to help page, even if they added help to a wrong URL they could not get in
                //if last segment is rest no operation is getting activated so ok to display help service page
                //if we have help in the url dont do api key verifcation
                if (urisegments.Last().Replace("/", "").ToLower() == restsegment ||
                    urisegments[4].Replace("/", "") == helpsegment
                    )
                    return true;

                //check if we are looking at the URLS or specific methods that allow Anonymoys access
                //seecon part checks the end of the URL i.e updateuserlogintimebyprofileidandsessionid since it could be called from somehwere else
                if (nonauthenticatedURLS.Contains(urisegments[1].ToString().Replace("/", "")) | (nonauthenticatedURLS.Contains(urisegments[4].ToString()))) return true;
                
                //flag the API key only auth URLS
                if ((apikeyonlyURLS.Contains(urisegments[1].ToString().Replace("/", "")) || apikeyonlyURLS.Contains(path))) apikeyauthonly = true; ;
                                

                //allows service to be discovereable with no api key
                if (OperationContext.Current.IncomingMessageHeaders.To.Segments.Last().Replace("/", "") != "$metadata")
                {
                    string key = GetAPIKey(operationContext);   
              
                   // ProfileModel ProfileModel = new ProfileModel(); 
                    //if there is no API key nothing happens
                    if (key != null)
                    {  
                       Guid apiKey;
                       Guid.TryParse(key.ToString(), out apiKey);
                                
                        //separate calls that need to verify the IPkey is tied to a profileID
                       if (!apikeyauthonly)
                       {
                           ////TO DO check if the call body has a profileid in the request body to make sure the user can access the revevant data being called for.
                           ////if so return use a diffeernt validation call that returns the profileID so we can match against the passed on.
                           //// Message msg = OperationContext.Current.RequestContext.RequestMessage.CreateBufferedCopy(Int32.MaxValue).CreateMessage();
                           ////if we have a body look for the profileid
                           //if (!internalCopy.IsEmpty)
                           //{
                           //    var dd = Utilities.MessageToString(internalCopy);
                           //    //get the profile id and map and other values as needed to the model if it exists otherwise no nothing
                           //    if (dd != "" && dd.Contains("profileid"))
                           //    {
                           //        ProfileModel = JsonExtentionsMethods.Deserialize<ProfileModel>(dd);
                           //    }
                           //    // msg.Close();  //kill this since we need it no more.
                           //}

                           var ProfileModel = Inspectors.getprofileidmodelfrombody(ref internalCopy);

                           if (ProfileModel.profileid != null)
                           {
                               validrequest = Utilities.VallidateApiKeyAndUserId(key, (int)applicationenum.anewluv, ProfileModel.profileid.GetValueOrDefault());

                               //log activity and geodata if it exists
                               Utilities.LogProfileActivity(ProfileModel, path, apiKey);
                           }



                       }
                       else
                       {
                           //just validate the api key
                           validrequest = Utilities.ValidateApiKey(key).Result;
                       }
                        //Api.DisposeApiKeyService();
                        //   return validrequest;                        
                    }
                    else
                    {
                        // Send back an HTML reply
                        CreateApiKeyErrorReply(operationContext, key);
                        validrequest = false;
                    }  
               
                }
                return validrequest;  //default is false btw
            }
            catch (Exception ex)
            {
                // var profileinfo = authinfo[0] ?? "";
                //instantiate logger here so it does not break anything else.
                //TO DO get HTTP context to replace the null

                new  Logging(applicationEnum.UserAuthorizationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex,null,null,true);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, profileid, null);
                //log error mesasge
                //handle logging here
                var message2 = ex.Message;
                throw;

            }
            finally
            {
              // Api.DisposeAuthenticationService();
              //   Api.DisposeApiKeyService();
            }
        }        

        

        public string GetAPIKey(OperationContext operationContext)
        {


            // Get the request message
            var request = operationContext.RequestContext.RequestMessage;


            // Get the HTTP Request
            var requestProp = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];
            // Get the query string
           // NameValueCollection queryParams = HttpUtility.get(requestProp.Headers);

            var prop = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];

            return prop.Headers[APIKEY];

            

           //var dd = operationContext.IncomingMessageHeaders.Where(p=>p.Name == APIKEY);

           // if (operationContext.IncomingMessageHeaders.FindHeader(APIKEY, "") != -1)
           // {
           //     MessageHeaders headers = operationContext.IncomingMessageHeaders;
           //     string apikey = headers.GetHeader<string>(APIKEY, "");
           //     return apikey;
           // }
            
            
            // Return the API key (if present, null if not)
           // return queryParams[APIKEY];
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
            using (var sr = new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + UserNamePasswordErrorHTML))
            {
                XElement response = XElement.Load(sr);
                using (Message reply = Message.CreateMessage(MessageVersion.None, null, response))
                {
                    HttpResponseMessageProperty responseProp = new HttpResponseMessageProperty() { StatusCode = HttpStatusCode.Unauthorized, StatusDescription = String.Format("AnewLuv Says :Username and password is invalid") };
                    responseProp.Headers[HttpResponseHeader.ContentType] = "text/html";
                    reply.Properties[HttpResponseMessageProperty.Name] = responseProp;
                    operationContext.RequestContext.Reply(reply);
                    // set the request context to null to terminate processing of this request
                    operationContext.RequestContext = null;
                }
            }
        }


        private static void PreflightRequestAproval(OperationContext operationContext)
        {
            // The error message is padded so that IE shows the response by default
            using (var sr = new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + PreflightRequestApprovalHTML))
            {
                XElement response = XElement.Load(sr);
                using (Message reply = Message.CreateMessage(MessageVersion.None, null, response))
                {
                    HttpResponseMessageProperty responseProp = new HttpResponseMessageProperty() { StatusCode = HttpStatusCode.OK, StatusDescription = String.Format("AnewLuv Says :Preflightrequest allowed") };
                    responseProp.Headers[HttpResponseHeader.ContentType] = "text/html";
                    reply.Properties[HttpResponseMessageProperty.Name] = responseProp;
                    operationContext.RequestContext.Reply(reply);
                    // set the request context to null to terminate processing of this request
                    operationContext.RequestContext = null;
                }
            }
        }


        const string PreflightRequestApprovalHTML = @"
<html>
<head>
    <title>PreflightRequest Approval </title>
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
        Request Approved
    </h1>
    <p>
        Cross server request approved 
    </p>
</body>
</html>
";


        const string APIKEY = "apikey";
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
