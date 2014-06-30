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

//using Anewluv.Domain.Data.ViewModels;
using System.Web;
using Nmedia.Infrastructure;
using Nmedia.Infrastructure.Domain.Data.errorlog;
using Nmedia.Infrastructure.Domain;
using Nmedia.Infrastructure.Domain.Data;
using Nmedia.Services.ApikeyAuthorization;
using Anewluv.Domain;
using Anewluv.Domain.Data.ViewModels;
using Anewluv.Services.Authentication;
using System.Threading.Tasks;
using Nmedia.Infrastructure.Domain.Data.ApiKey;
//using Anewluv.Lib;
//using Nmedia.Services.Authorization;
//using Anewluv.Domain;
//using Anewluv.Services.Authentication;

namespace Anewluv.Api
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
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
           // string[] authinfo;


            try
            {
                bool validrequest = true;
                bool apikeyauthonly = false;

                //if its preflight allow all
                //if (OperationContext.Current.RequestContext  == "OPTIONS")

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


                    //   using (Message reply = Message.CreateMessage(MessageVersion.None, null, ""))
                    // {
                    //  HttpResponseMessageProperty responseProp = new HttpResponseMessageProperty() { StatusCode = HttpStatusCode.Accepted , StatusDescription = String.Format("Preflight Request Allowed") };
                    //  responseProp.Headers[HttpResponseHeader.ContentType] = "text/html";
                    //  reply.Properties[HttpResponseMessageProperty.Name] = responseProp;
                    //  operationContext.RequestContext.Reply(reply);
                    //  // set the request context to null to terminate processing of this request
                    //  operationContext.RequestContext = null;
                    //}
                    //   validrequest = true;
                    //   return validrequest;
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
                nonauthenticatedURLS.Add("Nmedia.Infrastructure.Web.Services.Authorization");
                nonauthenticatedURLS.Add("updateuserlogintimebyprofileidandsessionid");
                //allow calls to API key auth
                apikeyonlyURLS.Add("Anewluv.Web.Services.Authentication");
                                   //"Anewluv.Web.Services.Authentication"
                apikeyonlyURLS.Add("Anewluv.Web.Services.Common");
                apikeyonlyURLS.Add("Anewluv.Web.Services.Spatial");
                apikeyonlyURLS.Add("Anewluv.Web.Services.Media/PhotoService.svc/Rest/addphotos");  //everyone can call addphotos
                apikeyonlyURLS.Add("Anewluv.Web.Services.Members/MembersMapperService.svc/Rest/getquicksearch");  //added quick search to this apikey only to allow for quick search to 
                //to bypass userid and password auth
               

                             
                //allow all photo uploads
                //TO DO add code to  call membership service and make sure the requestor has rights to view the data they are requesting
                //TO DO List the Service URLS that and handle differing security for each 

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
                if (apikeyonlyURLS.Contains(urisegments[1].ToString().Replace("/", ""))) apikeyauthonly = true; ;




                //allows service to be discovereable with no api key
                if (OperationContext.Current.IncomingMessageHeaders.To.Segments.Last().Replace("/", "") != "$metadata")
                {
                    string key = GetAPIKey(operationContext);                                 
                 
                    
                        //AsyncCallback callback = result =>
                        //{
                        //    var value = Api.ApiKeyService.EndIsValidAPIKey(result);
                        //    Api.DisposeApiKeyService(); //clean up
                        //    if (value)
                        //    {

                        //        //now validate the username password info if required 
                        //        //TO DO determine which URLS need validation of this i.e personal data only
                        //        if (ValidateUser(operationContext)) validrequest = true;
                        //        else 
                        //        {
                        //              validrequest= false;
                        //              CreateUserNamePasswordErrorReply(operationContext);
                        //              return true ;
                        //        }
                        //    }
                        //    else
                        //    {
                        //        // Send back an HTML reply
                        //          validrequest = false;
                        //        CreateApiKeyErrorReply(operationContext, key);
                              
                        //    }
                        //    Api.DisposeApiKeyService();
                            
                        //};

                    if (key != null)
                    {


                        var IsApiKeyValid =    CallValidateApiKey(key);
                        if (IsApiKeyValid.Result) //(Api.ApiKeyService.NonAysncIsValidAPIKey(key))
                        {

                            if (!apikeyauthonly)
                            {
                                //now validate the username password info if required 
                                //TO DO determine which URLS need validation of this i.e personal data only
                                if (ValidateUser(operationContext).Result)
                                {
                                    validrequest = true;
                                }
                                else
                                {
                                    validrequest = false;
                                    CreateUserNamePasswordErrorReply(operationContext);
                                }
                            }
                           // Api.DisposeApiKeyService();
                            return validrequest;
                        }
                    }
                    else
                    {
                        // Send back an HTML reply
                        CreateApiKeyErrorReply(operationContext, key);
                        validrequest = false;
                    }
                        // oLogEntry.id = d.Endsenderrormessage(result);
                        //d.senderrormessage(oLogEntry, addresstypeenum.Developer.ToString());
                }
                else
                {
                    validrequest = true;
                }

                              

                return validrequest;
            }
            catch (Exception ex)
            {
                // var profileinfo = authinfo[0] ?? "";
                //instantiate logger here so it does not break anything else.
                //TO DO get HTTP context to replace the null

                new ErroLogging(logapplicationEnum.UserAuthorizationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex,1,null,true);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, profileid, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;

            }
            finally
            {
              // Api.DisposeAuthenticationService();
              //   Api.DisposeApiKeyService();
            }
        }

        static async Task<bool> CallValidateApiKey(string key)
        {
            try
            {
                ApiKeyContext ApiKeyContext = new ApiKeyContext();
                
                using (var db = ApiKeyContext)
                {
                    Guid apiKey;
                    Guid.TryParse(key.ToString(), out apiKey);
                    ApiKeyService ApiKeyService = new ApiKeyService(db);
                    Task<bool> returnedTaskTResult = ApiKeyService.NonAysncIsValidAPIKey(new apikey { key = apiKey });
                    bool result = await returnedTaskTResult;
                 
                    // IsApiKeyValid = await 
                    return result;
                    // IsApiKeyValid = result.Result;
                }

            }
            catch (Exception ex )
            {
                Console.Error.WriteLine("Error: " + ex.Message);
                return false;
            }
        }


        static async Task<bool> ValidateUser(OperationContext operationContext)
        {
            var authinfo = GetUserNamePassword(operationContext);
            if (authinfo != null)
            {
                //TO DO convert to asynch
               // AnewluvContext AnewluvContext = new AnewluvContext();
                var IsUserValidated = false;
                using (var tempdb = new  AnewluvContext())
                {
                    AuthenticationService AuthenticationService = new AuthenticationService(tempdb);
                    Task<bool> returnedTaskTResult =  AuthenticationService.validateuserbyusernamepassword(new ProfileModel { username = authinfo[0], password = authinfo[1] });
                    bool result = await returnedTaskTResult;

                    // IsApiKeyValid = await 
                    return result;
                   
                }

                //var result = Api.AuthenticationService.validateuserbyusernamepassword(new ProfileModel { username = authinfo[0], password = authinfo[1] });
               //.0 Api.DisposeAuthenticationService();
                return IsUserValidated;
            }
            return false;

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

        public static string[] GetUserNamePassword(OperationContext operationContext)
        {

            try
            {
                var message = operationContext.RequestContext.RequestMessage;
                var request = (HttpRequestMessageProperty)message.Properties[HttpRequestMessageProperty.Name];
                string authorization = request.Headers[HttpRequestHeader.Authorization];
                if (authorization != null && authorization != "")
                    return Encryption.DecodeBasicAuthenticationString(authorization.Replace("Basic", "").Trim());

                return null;

                // string username = operationContext.IncomingMessageHeaders.GetHeader<string>("username","");
                //  string password = operationContext.IncomingMessageHeaders.GetHeader <string>("password", string.Empty);
                // return _membmbershipprovider.ValidateUser(username, password);
            }
            catch (Exception ex)
            {
                //return false;
                throw;

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
