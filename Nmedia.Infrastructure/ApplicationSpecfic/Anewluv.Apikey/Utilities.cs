using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
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

      


    }
}
