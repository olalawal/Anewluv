using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using Anewluv.Domain.Data.ViewModels;
using GeoData.Domain.Models.ViewModels;
using GeoData.Domain.ViewModels;
using Nmedia.Infrastructure.Domain.Data.Apikey;
using Nmedia.Infrastructure.Domain.Data.Apikey.DTOs;
using Nmedia.Infrastructure.Domain.Data.CustomClaimToken;
using Nmedia.Infrastructure.Domain.Data.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Anewluv.Api
{
    public static class AsyncCalls
    {


        #region "API key auth asyc calls"

        public static async Task<bool> isvalidapikeyasync(apikey model)
        {

            try
            {
                Task<bool> returnedTaskTResult = Api.ApiKeyService.IsValidAPIKey(model);
                bool result = await returnedTaskTResult;

                return result;
            }
            catch (Exception ex)
            {
                return false;
            
            }

            // IsApiKeyValid = await 


        }

        public static async Task<bool> isvalidapikeyanduserasync(apikey model)
        {

            Task<bool> returnedTaskTResult = Api.ApiKeyService.IsValidAPIKeyAndUser(model);
            bool result = await returnedTaskTResult;

            return result;

            // IsApiKeyValid = await 


        }
        public static async Task<Guid> validateorgetapikeyasync(ApiKeyValidationModel model)
        {

            Task<Guid> returnedTaskTResult = Api.ApiKeyService.ValidateOrGenerateNewApiKey(model);
            Guid result = await returnedTaskTResult;

            return result;

            // IsApiKeyValid = await 


        }

        public static async Task<bool> validateapikeybyuseridentifierasync(ApiKeyValidationModel model)
        {

            Task<bool> returnedTaskTResult = Api.ApiKeyService.ValidateApiKeyByUseridentifier(model);
            bool result = await returnedTaskTResult;

            return result;

            // IsApiKeyValid = await 


        }

        public static async Task invalidateuserapikey(ApiKeyValidationModel model)
        {

            try
            {


                await Api.ApiKeyService.InValidateUserApiKey(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
     

     
            

        


        }
        

        #endregion

        #region "Spatial API asyc calls"
        public static async Task<List<countrypostalcode>> getcountryandpostalcodestatuslistasync()
        {

            Task<List<countrypostalcode>> returnedTaskTResult = Api.GeoService.getcountrypostalcodestatuslist();
            List<countrypostalcode> result = await returnedTaskTResult;

            return result;

            // IsApiKeyValid = await 


        }    
         public static async Task<int> getcountryidbycountryname(string countryname)
                {

                    Task<int> returnedTaskTResult = Api.GeoService.getcountryidbycountryname(new GeoModel {  country = countryname} );
                    int result = await returnedTaskTResult;

                    return result;

                    // IsApiKeyValid = await 


                }
        #endregion

        #region "Media API asyc calls"
    
        public static async Task<bool> checkforuploadedphotobyprofileidasync(int profileid)
        {

            Task<bool> returnedTaskTResult = Api.PhotoService.checkforuploadedphotobyprofileid(new PhotoModel { profileid = profileid });
            bool result = await returnedTaskTResult;
            return result;
            // IsApiKeyValid = await 
        }
        public static async Task<AnewluvMessages>addphotosasync(PhotoModel model)
        {

            Task<AnewluvMessages> returnedTaskTResult = Api.PhotoService.addphotos(model);
            AnewluvMessages result = await returnedTaskTResult;
            return result;
            // IsApiKeyValid = await 
        }
        public static async Task<string> getimageb64stringfromurlasync(PhotoModel model)
        {

            Task<string> returnedTaskTResult = Api.PhotoService.getimageb64stringfromurl(model);
               
            string result = await returnedTaskTResult;
            return result;
            // IsApiKeyValid = await 
        }


        #endregion

        #region "Auth API asynch or other calls"


        public static async Task<NmediaToken> validateuserandgettoken(string username, string password)
        {

            Task<NmediaToken> returnedTaskTResult = Api.AuthenticationService.validateuserandgettoken(new ProfileModel { username = username, password = password });
            NmediaToken result = await returnedTaskTResult;
            // IsApiKeyValid = await 
            return result;

        }


        //old method that returned bool, now we want profileid to validate agains body
        public static async Task<bool> validateuserbyusernamepasswordasync(string username,string password)
        {
         //Task<bool> returnedTaskTResult =  Api.AuthenticationService.validateuserbyusernamepassword(new ProfileModel { username = username, password = password });
         //           bool result = await returnedTaskTResult;
         //           // IsApiKeyValid = await 
         //           return result;
            return false;

        }
      
        //public static async Task addprofileactvity(ActivityModel activitymodel)
        //{
        //   // Api.MemberService.addprofileactvity(activity);
        //   //  result = await returnedTaskTResult;
        //   // IsApiKeyValid = await 
        //  //  return result;

        //   await  Api.MemberService.addprofileactvity(activitymodel);
        //}

        public static async Task addprofileactivities(List<ActivityModel> activitymodels)
        {
            // Api.MemberService.addprofileactvity(activity);
            //  result = await returnedTaskTResult;
            // IsApiKeyValid = await 
            //  return result;

            await Api.MemberService.addprofileactivities(activitymodels);
        }

        public static async Task updateuserlogintimeasync(ProfileModel model)
        {
            // Api.MemberService.addprofileactvity(activity);
            //  result = await returnedTaskTResult;
            // IsApiKeyValid = await 
            //  return result;

            await Api.MemberService.updateuserlogintimebyprofileid(model);
        }

        public static async Task updateuserlogintimebyprofileidandsessionidasync(ProfileModel model)
        {

          await Api.MemberService.updateuserlogintimebyprofileidandsessionid(model);
        
        }

        public static async Task updateuserlogouttimebyprofileidasync(ProfileModel model)
        {
            await Api.MemberService.updateuserlogouttimebyprofileid(model);
            
        }

        public static async Task<bool> checkifprofileisactivatedasync(ProfileModel model)
        {

         Task<bool> returnedTaskTResult =  Api.MemberService.checkifprofileisactivated(model);
                    bool result = await returnedTaskTResult;
                    // IsApiKeyValid = await 
                    return result;

        }
        public static async Task<bool> activateprofileasync(ProfileModel model)
        {
         Task<bool> returnedTaskTResult =  Api.MemberService.activateprofile(model);
                    bool result = await returnedTaskTResult;
                    // IsApiKeyValid = await 
                    return result;

        }
        public static void createmailboxfoldersasync(ProfileModel model)
        {

            Task.Run(() => Api.MemberService.createmailboxfolders(model));

         //Task<bool> returnedTaskTResult =  Api.MemberService.createmailboxfolders(model);
         //           bool result = await returnedTaskTResult;
         //           // IsApiKeyValid = await 
         //           return result;
        }
        public static async Task<bool> checkifmailboxfoldersarecreatedasync(ProfileModel model)
        {
            Task<bool> returnedTaskTResult = Api.MemberService.checkifmailboxfoldersarecreated(model);
            bool result = await returnedTaskTResult;
            // IsApiKeyValid = await 
            return result;
        }
        


        #endregion



        #region "Notification Service calls"
        public static string  sendmessagesbytemplate(List<EmailModel> model)
        {
            try
            {


              var  returnedTaskTResult = Api.NotificationService.sendmessagesbytemplate(model).Result;
                //string result = await returnedTaskTResult;
                // IsApiKeyValid = await 
               // return result;
              return returnedTaskTResult;


               // Task.Run(() => Api.NotificationService.sendmessagebytemplate(model));

                //Task<bool> returnedTaskTResult =  Api.MemberService.createmailboxfolders(model);
                //           bool result = await returnedTaskTResult;
                //           // IsApiKeyValid = await 
                //           return result;
            }
            catch (Exception ex)
            { 
            
            }
            return "";
        }
        

        #endregion

    }
}
