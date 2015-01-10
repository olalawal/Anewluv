using Anewluv.Domain.Data;
using Anewluv.Domain.Data.Anewluv.ViewModels;
using Anewluv.Domain.Data.ViewModels;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data;
using Nmedia.Infrastructure.Domain.Data.Apikey;
using Nmedia.Infrastructure.Domain.Data.log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Anewluv.Api;

namespace Anewluv.Apikey
{
    public static class  Utilities
    {

        #region "validation Utilities"

        //validate of api key only not profile id if its not passed
       //public static Task<bool> ValidateApiKey(string key)
       // {
       //     try
       //     {
       //         bool result = false;
       //         Guid apiKey;
       //         Guid.TryParse(key.ToString(), out apiKey);
       //         // ApiKeyService ApiKeyService = new ApiKeyService(db);
       //         // Task<bool> returnedTaskTResult = ApiKeyService.IsValidAPIKey(new apikey { key = apiKey });
       //         Task<bool> returnedTaskTResult = Anewluv.Api.AsyncCalls.isvalidapikeyasync(new apikey { key = apiKey });

       //         return returnedTaskTResult;

       //         // IsApiKeyValid = await 
       //        // return result;
       //         // IsApiKeyValid = result.Result;


       //     }
       //     catch (Exception ex)
       //     {
       //         Console.Error.WriteLine("Error: " + ex.Message);
       //        // return false;
       //     }
       //     return null;
       // }

     
        //old way of validating, now we will use a new method so we can get the profile ID and validate it against the message body JSON
        public static bool ValidateUser(OperationContext operationContext)
        {
            var authinfo = Utilities.GetUserNamePassword(operationContext);

            if (authinfo != null)
            {
                //TO DO convert to asynch
                // AnewluvContext AnewluvContext = new AnewluvContext();
                //  var IsUserValidated = false;
                ////  using (var tempdb = new  AnewluvContext())
                //  {
                //  AuthenticationService AuthenticationService = new AuthenticationService(tempdb);
                var dd = AsyncCalls.validateuserbyusernamepasswordasync(authinfo[0], authinfo[1]);
                return dd.Result;
                // bool result = await returnedTaskTResult;
                // IsApiKeyValid = await 
                // return result;                   

            }
            return false;
            //var result = Api.AuthenticationService.validateuserbyusernamepassword(new ProfileModel { username = authinfo[0], password = authinfo[1] });
            //.0 Api.DisposeAuthenticationService();
            // return IsUserValidated;
        }

        #endregion


        public static bool LogProfileActivity(ProfileModel ProfileModel, string path, Guid apiKey)
        {
            
            try
            {
                //log the activity since we have a profileID that is valid 
                //TO DO find a way to get the geo data and the activity type from the URL probbaly dictorronay or class to do that.
                var actvitymodel = new ActivityModel();

                var activitybase = new profileactivity
                {
                    profile_id = ProfileModel.profileid.GetValueOrDefault(),
                    creationdate = DateTime.Now,
                    routeurl = path,
                    sessionid = OperationContext.Current.SessionId,
                    ipaddress = Utilities.GetRequestIP(OperationContext.Current),
                    useragent = Utilities.GetUserAgent(OperationContext.Current),
                    //TO do have an index of enums that parses the acity type by url
                    activitytype_id = (int)activitytypeEnum.NotSet,
                    apikey = apiKey,
                };

                //handle the geo data stuff

                var activitygeodata = new profileactivitygeodata
                {
                    city = ProfileModel.city,
                    countryname = ProfileModel.Countryname,
                    lattitude = ProfileModel.lattitude.GetValueOrDefault(),
                    longitude = ProfileModel.longitude.GetValueOrDefault(),
                    regionname = ProfileModel.stateprovince,
                    creationdate = activitybase.creationdate,

                };


                actvitymodel.activitybase = activitybase;
                actvitymodel.activitygeodata = activitygeodata;

                AsyncCalls.addprofileactvity(actvitymodel);

                return true;
            }
            catch (Exception ex)
            { 
            //do nothing in this case since we dont want to affect authentication based on activity logging. 
            //errors should be logged downstream as well so just log.
                new Logging(applicationEnum.UserAuthorizationService).WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null, true);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, profileid, null);
                return false;
            }

           
        }

        public static string GetRequestIP(OperationContext context)
        {
            try
            {
                MessageProperties prop = context.IncomingMessageProperties;
                RemoteEndpointMessageProperty endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                string ip = endpoint.Address;

                return ip;
            }
            catch (Exception ex)
            {
               
                new Logging(applicationEnum.UserAuthorizationService).WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null, true);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, profileid, null);
                throw ex;
            }
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

        public static string  GetUserAgent(OperationContext operationContext)
        {

            try
            {
                var message = operationContext.RequestContext.RequestMessage;
                var request = (HttpRequestMessageProperty)message.Properties[HttpRequestMessageProperty.Name];
                string agent = request.Headers[HttpRequestHeader.UserAgent];

                return agent;

              

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

        public static string MessageToString( Message message)
        {
            WebContentFormat messageFormat = GetMessageContentFormat(message);
            MemoryStream ms = new MemoryStream();
            XmlDictionaryWriter writer = null;
            switch (messageFormat)
            {
                case WebContentFormat.Default:
                case WebContentFormat.Xml:
                    writer = XmlDictionaryWriter.CreateTextWriter(ms);
                    break;
                case WebContentFormat.Json:
                    writer = JsonReaderWriterFactory.CreateJsonWriter(ms);
                    break;
                case WebContentFormat.Raw:
                    // special case for raw, easier implemented separately
                    return ReadRawBody(ref message);
            }

            message.WriteMessage(writer);
            writer.Flush();
            string messageBody = Encoding.UTF8.GetString(ms.ToArray());

            // Here would be a good place to change the message body, if so desired.

            // now that the message was read, it needs to be recreated.
            ms.Position = 0;

            // if the message body was modified, needs to reencode it, as show below
            // ms = new MemoryStream(Encoding.UTF8.GetBytes(messageBody));

            XmlDictionaryReader reader;
            if (messageFormat == WebContentFormat.Json)
            {
                reader = JsonReaderWriterFactory.CreateJsonReader(ms, XmlDictionaryReaderQuotas.Max);
            }
            else
            {
                reader = XmlDictionaryReader.CreateTextReader(ms, XmlDictionaryReaderQuotas.Max);
            }

            Message newMessage = Message.CreateMessage(reader, int.MaxValue, message.Version);
            newMessage.Properties.CopyProperties(message.Properties);
            message = newMessage;

            return messageBody;
        }

        private static WebContentFormat GetMessageContentFormat(Message message)
        {
            WebContentFormat format = WebContentFormat.Default;
            if (message.Properties.ContainsKey(WebBodyFormatMessageProperty.Name))
            {
                WebBodyFormatMessageProperty bodyFormat;
                bodyFormat = (WebBodyFormatMessageProperty)message.Properties[WebBodyFormatMessageProperty.Name];
                format = bodyFormat.Format;
            }

            return format;
        }     

        private static string ReadRawBody(ref Message message)
        {
            XmlDictionaryReader bodyReader = message.GetReaderAtBodyContents();
            bodyReader.ReadStartElement("Binary");
            byte[] bodyBytes = bodyReader.ReadContentAsBase64();
            string messageBody = Encoding.UTF8.GetString(bodyBytes);

            // Now to recreate the message
            MemoryStream ms = new MemoryStream();
            XmlDictionaryWriter writer = XmlDictionaryWriter.CreateBinaryWriter(ms);
            writer.WriteStartElement("Binary");
            writer.WriteBase64(bodyBytes, 0, bodyBytes.Length);
            writer.WriteEndElement();
            writer.Flush();
            ms.Position = 0;
            XmlDictionaryReader reader = XmlDictionaryReader.CreateBinaryReader(ms, XmlDictionaryReaderQuotas.Max);
            Message newMessage = Message.CreateMessage(reader, int.MaxValue, message.Version);
            newMessage.Properties.CopyProperties(message.Properties);
            message = newMessage;

            return messageBody;
        }


    }
}
