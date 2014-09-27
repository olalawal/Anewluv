using Anewluv.Domain.Data.ViewModels;
using GeoData.Domain.Models.ViewModels;
using Nmedia.Infrastructure.Domain.Data.ApiKey;
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

            Task<bool> returnedTaskTResult = Api.ApiKeyService.IsValidAPIKey(model);
            bool  result = await returnedTaskTResult;

            return result;

            // IsApiKeyValid = await 


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
        #endregion

        #region "Media API asyc calls"
        public static async Task<bool> checkforuploadedphotobyprofileidasync(string profileid)
        {

            Task<bool> returnedTaskTResult = Api.PhotoService.checkforuploadedphotobyprofileid(profileid);
            bool result = await returnedTaskTResult;
            return result;
            // IsApiKeyValid = await 
        }

        public static async Task<AnewluvMessages>addphotosasync(PhotoUploadViewModel model)
        {

            Task<AnewluvMessages> returnedTaskTResult = Api.PhotoService.addphotos(model);
            AnewluvMessages result = await returnedTaskTResult;
            return result;
            // IsApiKeyValid = await 
        }


        public static async Task<string> getimageb64stringfromurlasync(string imageurl,string source)
        {

            Task<string> returnedTaskTResult = Api.PhotoService.getimageb64stringfromurl(imageurl, source);
            string result = await returnedTaskTResult;
            return result;
            // IsApiKeyValid = await 
        }


        #endregion

        #region "Auth API asynch or other calls"
        public static async Task<bool> validateuserbyusernamepasswordasync(string username,string password)
        {
         Task<bool> returnedTaskTResult =  Api.AuthenticationService.validateuserbyusernamepassword(new ProfileModel { username = username, password = password });
                    bool result = await returnedTaskTResult;
                    // IsApiKeyValid = await 
                    return result;

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


        public static async Task<bool> createmailboxfoldersasync(ProfileModel model)
        {
         Task<bool> returnedTaskTResult =  Api.MemberService.createmailboxfolders(model);
                    bool result = await returnedTaskTResult;
                    // IsApiKeyValid = await 
                    return result;
        }

        public static async Task<bool> checkifmailboxfoldersarecreatedasync(ProfileModel model)
        {
            Task<bool> returnedTaskTResult = Api.MemberService.checkifmailboxfoldersarecreated(model);
            bool result = await returnedTaskTResult;
            // IsApiKeyValid = await 
            return result;
        }
        


        #endregion

    }
}
