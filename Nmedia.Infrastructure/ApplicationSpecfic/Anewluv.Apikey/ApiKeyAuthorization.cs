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
//using Nmedia.Services.Authorization;
using Anewluv.Domain;
using Anewluv.Domain.Data.ViewModels;
using System.Threading.Tasks;
using Nmedia.Infrastructure.Domain.Data.Apikey;
using System.Xml;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.Anewluv.ViewModels;
using Nmedia.Infrastructure.Utils;
using System.ServiceModel.Dispatcher;
//using Anewluv.Lib;
//using Nmedia.Services.Authorization;
//using Anewluv.Domain;
//using Anewluv.Services.Authentication;

namespace Anewluv.Apikey
{
    //implement this using the api key servbice key service i.e , which is referenced in this API as well
    //sample 
    //http://stackoverflow.com/questions/11192610/inject-repository-into-serviceauthorizationmanager-using-unity
    public class ApikeyAuthorization : ServiceAuthorizationManager
    {

        //TO do add this to web config as a key so each service that calls this can be granualry controlled.
        int MaxBuffersize = 2147483647;
                 
        //public APIKeyAuthorization(IAPIkeyRepository apikeyrepository)
        //{
        //    _apikeyrepository = apikeyrepository;
        //}
        //had to set up a parameterless contrycture due to WCF restructtions 
        //its not too bad since this only seems to be called on the first instantiaion
        public ApikeyAuthorization()
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


                //TO do might need to raise this or set to a static value on the max buffer size for larger posts !
                //store the message here so we can parse body for things like geodata profile id etc
                MessageBuffer buffer = operationContext.RequestContext.RequestMessage.CreateBufferedCopy(MaxBuffersize);
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
                nonauthenticatedURLS.Add("Nmedia.Infrastructure.Web.Services.Authorization");
                nonauthenticatedURLS.Add("membersservice.svc/rest/updateuserlogintimebyprofileid");

                //for testing non auth on this
               // nonauthenticatedURLS.Add("Anewluv.Web.Services.Members");
               // nonauthenticatedURLS.Add("Anewluv.Web.Services.MemberActions");

                //these methods use internal rest calls so need to be excluded
                //nonauthenticatedURLS.Add("updateuserlogintimebyprofileidandsessionid");  // do this this way until we determine how to add headers
                //nonauthenticatedURLS.Add("validateuserandgettoken");  //internal call no auth required
               // nonauthenticatedURLS.Add("validateuserandgettoken");  //quick search is not authenticated since its used on main page


                //allow calls to API key auth only i.e we dont validate profile ID is tied to a valid apikey for these calls since they are generic calls
                //TO do we need to create a server side method that generates or randomizes the apikey so folks cannot use our calls for other things
                //withoute permission.  For mobile a permemant apikey can be embeded in the code, for web the web server has a call to allow the client to grab the IP 
                //do not expose it in the client somehow.
                apikeyonlyURLS.Add("anewluv.web.services.authentication");                                  
                apikeyonlyURLS.Add("anewluv.web.services.common");
                apikeyonlyURLS.Add("anewluv.web.services.spatial");
                apikeyonlyURLS.Add("/anewluv.web.services.media/photoservice.svc/rest/addphotos");  //everyone can call addphotos



                //TO DO add special handling to the service side to disallow the pulling on anyones basic search settings even tho it cant be changed.
                //added quick search to this apikey only to allow for quick search to 
                //to bypass userid and password auth
                apikeyonlyURLS.Add("/anewluv.web.services.membermapper/membersmapperservice.svc/rest/getquicksearch");                                   
                apikeyonlyURLS.Add("/anewluv.web.services.edit/searcheditservice.svc/rest/getbasicsearchsettings");
                
               

                             
                //allow all photo uploads
                //TO DO add code to  call membership service and make sure the requestor has rights to view the data they are requesting
                //TO DO List the Service URLS that and handle differing security for each 

                string path = OperationContext.Current.IncomingMessageHeaders.To.AbsolutePath;
                string[] urisegments = OperationContext.Current.IncomingMessageHeaders.To.Segments;
                string helpsegment = "help"; //this is the thing we are checking   
                string restsegment = "rest"; //this is the thing we are checking 
                string operationssegment = "operations/"; //this is the thing we are checking 
                string soapsegment = "soap"; //this is the thing we are checking 


                
                //allow soap access to add photos tempoary soad access 
               // if (urisegments.Last().Replace("/", "").ToLower() == soapsegment)
                  //  return true;

                //allow access to help page, even if they added help to a wrong URL they could not get in
                //if last segment is rest no operation is getting activated so ok to display help service page
                //if we have help in the url dont do api key verifcation

            
                var segmentcount = urisegments.Count();

                
                //if last segment is rest or help dont do anything we are fine
                if (urisegments[segmentcount - 1].ToLower().Contains(restsegment.ToLower()) || urisegments[segmentcount - 1].ToLower().Contains(helpsegment.ToLower()) || urisegments.ToList().Contains(operationssegment)) return true; 

                //if (urisegments.ToList().Contains(restsegment) && (urisegments.ToList().Contains(helpsegment) | urisegments.ToList().Contains(helpsegment.Replace("/",""))))
                 //   return true;

                //check if we are looking at the URLS or specific methods that allow Anonymoys access
                //seecon part checks the end of the URL i.e updateuserlogintimebyprofileidandsessionid since it could be called from somehwere else
                if (nonauthenticatedURLS.Contains(urisegments[1].ToLower().ToString().Replace("/", ""))) return true;
                
                //flag the API key only auth URLS
                if ((apikeyonlyURLS.Contains(urisegments[1].ToString().ToLower().Replace("/", "")) || apikeyonlyURLS.Contains(path.ToLower()))) apikeyauthonly = true; ;
                                

                //allows service to be discovereable with no api key
                if (OperationContext.Current.IncomingMessageHeaders.To.Segments.Last().Replace("/", "") != "$metadata")
                {
                     Guid? key = WCFContextParser.GetAPIKey(operationContext);   
              
                   // ProfileModel ProfileModel = new ProfileModel(); 
                    //if there is no API key nothing happens
                    if (key != null )
                    {  
                      
                                
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
                        
                           var ProfileModel = WCFMessageInpectors.getprofileidmodelfrombody(ref internalCopy);

                           if (ProfileModel.profileid != null)
                           {
                               //check if the passed apikey matches the profile ID that was passed in and is also active on the apikservice side of things.
                               validrequest = Api.AsyncCalls.isvalidapikeyanduserasync(new
                               apikey { application_id = (int)applicationenum.anewluv, keyvalue = key.Value, user = new user { useridentifier = ProfileModel.profileid.Value} }).Result;
                               //  (key, (int)applicationenum.anewluv, );
                               //log activity and geodata if it exists
                              // Utilities.LogProfileActivity(ProfileModel, path, apiKey);
                           }
                           else
                           {
                               //invlaid request if we have no profile id
                               validrequest = false; 
                           }

                           if (!validrequest)  CreateAccsessExiredReply(operationContext, key.Value.ToString());

                       }
                       else
                       {
                           //just validate the api key
                          // var dd = Api.ApiKeyService.IsValidAPIKey(new apikey { key = apiKey }).Result;
                           validrequest = Api.AsyncCalls.isvalidapikeyasync(new apikey { keyvalue = key.Value, application_id = (int)applicationenum.anewluv }).Result;//; Utilities.ValidateApiKey(key).Result;
                           
                       }
                        //create well formatted reply for UI
                       if (!validrequest) CreateApiKeyErrorReply(operationContext, key.Value.ToString());
                        //Api.DisposeApiKeyService();
                        //   return validrequest;                        
                    }
                    else
                    {
                        // Send back an HTML reply
                        CreateNoApikeyReply(operationContext);
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
                throw ex;  //TO DO send a generic error message for falures here

            }
            finally
            {
              // Api.DisposeAuthenticationService();
              //   Api.DisposeApiKeyService();
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



        private static void CreateNoApikeyReply(OperationContext operationContext)
        {
            // The error message is padded so that IE shows the response by default
            using (var sr = new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + APIErrorHTML))
            {
                XElement response = XElement.Load(sr);
                using (Message reply = Message.CreateMessage(MessageVersion.None, null, response))
                {
                    HttpResponseMessageProperty responseProp = new HttpResponseMessageProperty() { StatusCode = HttpStatusCode.Unauthorized, StatusDescription = "No valid Token/Key !" };
                    responseProp.Headers[HttpResponseHeader.ContentType] = "text/html";
                    reply.Properties[HttpResponseMessageProperty.Name] = responseProp;
                    operationContext.RequestContext.Reply(reply);
                    // set the request context to null to terminate processing of this request
                    operationContext.RequestContext = null;
                }
            }
        }

        private static void CreateAccsessExiredReply(OperationContext operationContext, string key)
        {
            // The error message is padded so that IE shows the response by default
            using (var sr = new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + APIErrorHTML))
            {
                XElement response = XElement.Load(sr);
                using (Message reply = Message.CreateMessage(MessageVersion.None, null, response))
                {
                    HttpResponseMessageProperty responseProp = new HttpResponseMessageProperty() { StatusCode = HttpStatusCode.Unauthorized, StatusDescription = "Token has Expired or is Invaid!" };
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


        //private static void PreflightRequestAprovalOld(OperationContext operationContext)
        //{
        //    // The error message is padded so that IE shows the response by default
        //    using (var sr = new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + PreflightRequestApprovalHTML))
        //    {
        //        XElement response = XElement.Load(sr);
        //        using (Message reply = Message.CreateMessage(MessageVersion.None, null, response))
        //        {
        //            HttpResponseMessageProperty responseProp = new HttpResponseMessageProperty() { StatusCode = HttpStatusCode.NoContent, StatusDescription = String.Format("AnewLuv Says :Preflightrequest allowed") };
        //            responseProp.Headers[HttpResponseHeader.ContentType] = "text/html";
        //            reply.Properties[HttpResponseMessageProperty.Name] = responseProp;
        //            operationContext.RequestContext.Reply(reply);
        //            // set the request context to null to terminate processing of this request
        //            operationContext.RequestContext = null;
        //        }
        //    }
        //}


        private static void PreflightRequestAproval(OperationContext operationContext)
        {

            //these are added in web config so dont need to add 
            //var requiredHeaders = new Dictionary<string, string>();
            //requiredHeaders.Add("Access-Control-Allow-Origin", "*");
            //requiredHeaders.Add("Access-Control-Request-Method", "POST,GET,PUT,DELETE,OPTIONS");
            //requiredHeaders.Add("Access-Control-Allow-Headers", "X-Requested-With,Content-Type,apikey,Authorization ,,Accept");


            var incmessage = operationContext.RequestContext.RequestMessage;
            // The error message is padded so that IE shows the response by default
            using (var sr = new StringReader("<?xml version=\"1.0\" encoding=\"utf-8\"?>" + PreflightRequestApprovalHTML))
            {
                XElement response = XElement.Load(sr);
                using (Message reply = Message.CreateMessage(incmessage.Version, FindReplyAction(incmessage.Headers.Action), response))
                {
                    HttpRequestMessageProperty httpRequestHeader = (HttpRequestMessageProperty)operationContext.RequestContext.RequestMessage.Properties[HttpRequestMessageProperty.Name];
                    HttpResponseMessageProperty responseProp = new HttpResponseMessageProperty() { StatusCode = HttpStatusCode.NoContent, StatusDescription = String.Format("AnewLuv Says :Preflightrequest allowed") };
                   
                    //these are added via web config
                    //foreach (KeyValuePair<string, string> item in requiredHeaders)
                   //     responseProp.Headers.Add(item.Key, item.Value);
                    responseProp.Headers[HttpResponseHeader.ContentType] = "text/html";
                    reply.Properties[HttpResponseMessageProperty.Name] = responseProp;

                    operationContext.RequestContext.Reply(reply);
                    // set the request context to null to terminate processing of this request
                    operationContext.RequestContext = null;
                }
            }
        }

        /// <summary>
        /// Finds the reply action based on the supplied request action
        /// </summary>
        /// <param name="requestAction">The request action for witch the reply action should be found.</param>
        /// <returns></returns>
        public static string FindReplyAction(string requestAction)
        {
            OperationContext ctx = OperationContext.Current;
            EndpointDispatcher epd = ctx.EndpointDispatcher;

            // var dd = OperationContext.Current.EndpointDispatcher.Op;
            foreach (var operation in epd.DispatchRuntime.Operations)
            {
                //if (operation.Messages[0].Action == requestAction)
                //{
                //    return operation.Messages[1].Action;
                //}
                if (operation.Action == requestAction) return operation.Action;
            }
            return null;
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
