using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Nmedia.Services.Contracts;
//using Nmedia.DataAccess.Interfaces;
using LoggingLibrary;

using System.Web;
using System.IO;
using System.Xml;
using Nmedia.Infrastructure.Domain.Data.Apikey;
using Nmedia.Infrastructure.Domain.Data.log;

using Nmedia.Infrastructure.Domain.Data;

using System.Threading.Tasks;
using Nmedia.Infrastructure.Domain;
using Nmedia.Infrastructure.Domain.Data.Apikey.DTOs;
using Repository.Pattern.UnitOfWork;
//using Nmedia.Infrastructure.ExceptionHandling;
//using Nmedia.Infrastructure.WCF;
//using Anewluv.Lib;
//using Anewluv.Lib;

namespace Nmedia.Services.Authorization
{

    [DataContract]
    public class AnewluvMessages
    {
        [DataMember]
        public List<string> messages { get; set; }
        [DataMember]
        public List<string> errormessages { get; set; }
        //TO DO allow for objects to comeback


        public AnewluvMessages()
        {
            this.messages = new List<string>();
            this.errormessages = new List<string>();
        }


    }
    //TO do with this new patter either find a way to make extention methods use the generic repo dict in EFUnitOfWork or 
    //MOve the remaining ext method methods into this service layer that uses the EF unit of work
    //TO do also make a single Update method generic and implement that for the rest of the Repo saves.
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ApikeyService : IApikeyService
    {


        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        //  private static    private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private   readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IApiKeyStoredProcedures _storedProcedures;
        private Logging logger;

        public ApikeyService(IUnitOfWorkAsync unitOfWork, IApiKeyStoredProcedures storedProcedures)
        {

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "unitOfWork cannot be null");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("dataContext", "dataContext cannot be null");
            }

            //promotionrepository = _promotionrepository;
             _unitOfWorkAsync = unitOfWork;
             _storedProcedures = storedProcedures;
            //disable proxy stuff by default
            //_unitOfWork.DisableProxyCreation = true;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
        }

        //TO DO use the Initial Catalog= context wehre API key is stored 

        const string APIKEYLIST = "APIKeyList";

        public async Task<bool> IsValidAPIKey(apikey model)
        {
            // TODO: Implement IsValidAPI Key using your repository

           // return true;
            //Guid apiKey;

            // _unitOfWork.DisableProxyCreation = true;

            Boolean result = false;
            try
            {

                //Convert the string into a Guid and validate it
                // not validating against a list anymore

                var task = Task.Factory.StartNew(() =>
                {

                   // .DisableProxyCreation = true;
                   // _unitOfWork.DisableLazyLoading = true;
                  //    using (var db = _unitOfWork)
                    {
                        result = _unitOfWorkAsync.Repository<apikey>().Queryable().Any(p => p.keyvalue == model.keyvalue & p.application_id == model.application_id & p.active == true);
                    }
                    return result;
                });
                return await task.ConfigureAwait(false);
            }



            catch (Exception ex)
            {

                //instantiate logger here so it does not break anything else.
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member Apikey service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
              throw ex;

                //throw convertedexcption;
            }


        }


        /// <summary>
        /// Make sure the profileID matches the API key (user identifer and also the API key is active as well)
        /// TO DO check last activity period if less than 30 mins 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> IsValidAPIKeyAndUser(apikey model)
        {
            Boolean isvalid = false;
            try
            {
              //  var task = Task.Factory.StartNew(() =>
              //  {             
                  //    using (var db = _unitOfWork)
                    //var apikeys = _unitOfWorkAsync.Repository<apikey>().Queryable();
                    //var users = _unitOfWorkAsync.Repository<user>().Queryable();

                   
                       
                    //    // .DisableLazyLoading = false;
                    //var result = (from c in apikeys
                    //            join o in users on new { a = c.user_id.GetValueOrDefault()}
                    //            equals new { a = o.id }
                    //            where (c.keyvalue == model.keyvalue && c.application_id == model.application_id & c.active == true)                                    
                    //            select new apikey
                    //            { 
                    //                user = o
                                  
                    //            }).FirstOrDefault();
                
                   
                        var result =  await _unitOfWorkAsync.RepositoryAsync<apikey>()
                        .Query(p => p.keyvalue == model.keyvalue && p.application_id == model.application_id & p.active == true).Include(x=>x.user).SelectAsync();
                        apikey  keydata = result.FirstOrDefault();
                     

                        if (keydata != null && keydata.user != null && keydata.user.useridentifier == model.user.useridentifier)
                        {
                             var currenttime = DateTime.Now;
                             //default expiration is 90 mins so if the current time is greater than 90 mins from last access time the token is invalid
                             var apikeyexpirationtime = keydata.lastaccesstime.GetValueOrDefault().AddMinutes(90);                         
                             //var dif = (apikeyexpirationtime - currenttime).TotalMinutes;
                             //var test 
                            //check the time diff
                             int value = DateTime.Compare(currenttime, apikeyexpirationtime);

                             if (value > 0) isvalid = false;
                             else
                             {
                                 isvalid = true;
                             }
                             //update the last accessed key
                             await UpdateApiKeyActivity(model.keyvalue, _unitOfWorkAsync);
                             return isvalid;
                            
                        }                        
                    
                        return isvalid;
              //  });
              //  return await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                //instantiate logger here so it does not break anything else.
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member Apikey service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
              throw ex;

                //throw convertedexcption;
            }


        }



        /// <summary>
        /// make sure 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> ValidateApiKeyByUseridentifier(ApiKeyValidationModel  model)
        {
            Boolean isvalid = false;
            try
            {
               


                var result = await _unitOfWorkAsync.RepositoryAsync<apikey>()
                .Query(p => p.keyvalue == model.keyvalue && p.application_id == model.application_id & p.active == true).Include(x => x.user).SelectAsync();
                apikey keydata = result.FirstOrDefault();


                if (keydata != null && keydata.user != null && keydata.user.useridentifier == model.useridentifier) isvalid = true;
                
               
            }
            catch (Exception ex)
            {

                //instantiate logger here so it does not break anything else.
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member Apikey service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw ex;

                //throw convertedexcption;
            }
            return isvalid;


        }

        public async Task<Guid> ValidateOrGenerateNewApiKey(ApiKeyValidationModel model)
        {

            try
            {

                //Convert the string into a Guid and validate it
                // not validating against a list anymore
                
                var task = Task.Factory.StartNew(() =>
                {
                   // _unitOfWork.DisableProxyCreation = false;
                   // _unitOfWork.DisableLazyLoading = false;
                  //    using (var db = _unitOfWork)
                    {
                        var result = _unitOfWorkAsync.Repository<apikey>().Queryable().Where(p => p.keyvalue == model.keyvalue & p.active == true).FirstOrDefault();
                       if (result == null) 
                       {
                        var newKey=   GenerateUserAPIkey(model,_unitOfWorkAsync);
                        model.keyvalue = newKey;            

                        return newKey;
                       }
                      return result.keyvalue;
                    }
                   
                });
                return await task.ConfigureAwait(false);
            }



            catch (Exception ex)
            {

                //instantiate logger here so it does not break anything else.
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member Apikey service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
              throw ex;

                //throw convertedexcption;
            }


        }

        public async Task InValidateUserApiKey(ApiKeyValidationModel model)
        {

            try
            {

                //Convert the string into a Guid and validate it
                // not validating against a list anymore



                   var application = await _unitOfWorkAsync.RepositoryAsync<lu_application>().Query(p => p.id == model.application_id).SelectAsync();
                   var validapplication = application.FirstOrDefault();

                   if (validapplication != null)
                   {
                       
                       //user handling   
                       //check for existing user that is tied to this application, search the apikey table
                       var result = await _unitOfWorkAsync.RepositoryAsync<user>()
                           .Query(p => p.username.ToUpper() == model.username.ToUpper() && p.useridentifier == model.useridentifier && p.apikeys.Any(z => z.application_id == validapplication.id)).SelectAsync();

                       var existinguser = result.FirstOrDefault();
                       if (existinguser != null)
                           DeactivateAllApiKeysByUserIdentifierAndKeyAndApplication(existinguser.id, validapplication.id);
                   }

               
            }



            catch (Exception ex)
            {

                //instantiate logger here so it does not break anything else.
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member Apikey service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw ex;

                //throw convertedexcption;
            }


        }

        /// <summary>
        /// invalidate all other API keys , its part of the retival of a new key so its not asycn, should be fast becuase its a store cammand in any case
        /// </summary>
        /// <param name="model"></param>
        private  void DeactivateOldApiKeysByUserIdentifierAndKeyAndApplication(int userid,Guid keyvalue, int application_id)
        {

            try
            {

                //Convert the string into a Guid and validate it
                // not validating against a list anymore

               
                
             //   var task = Task.Factory.StartNew(() =>
              //  {      
                _storedProcedures.ResetApplicationtUserApiKeys(userid.ToString(),keyvalue.ToString(), application_id.ToString());
                        //return result.key;

              //  });
              // await task.ConfigureAwait(false);
            }



            catch (Exception ex)
            {

                //instantiate logger here so it does not break anything else.
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, ex, null);
               
                //throw convertedexcption;
                //log the error and do nothing for for now
            }


        }

        private void DeactivateAllApiKeysByUserIdentifierAndKeyAndApplication(int userid,int application_id)
        {

            try
            {

                //Convert the string into a Guid and validate it
                // not validating against a list anymore



                //   var task = Task.Factory.StartNew(() =>
                //  {      
                _storedProcedures.DeactivateApplicationUserApiKeys(userid.ToString(), application_id.ToString());
                //return result.key;

                //  });
                // await task.ConfigureAwait(false);
            }



            catch (Exception ex)
            {

                //instantiate logger here so it does not break anything else.
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, ex, null);

                //throw convertedexcption;
                //log the error and do nothing for for now
            }


        }

        private async Task UpdateApiKeyActivity(Guid keyvalue, IUnitOfWorkAsync db)
        {

            try
            {

                //Convert the string into a Guid and validate it
                // not validating against a list anymore



                //   var task = Task.Factory.StartNew(() =>
                //  {      
               await _storedProcedures.UpdateApiKeyActivity(keyvalue.ToString());
                //return result.key;

                //  });
                // await task.ConfigureAwait(false);
            }



            catch (Exception ex)
            {

                //instantiate logger here so it does not break anything else.
                logger = new Logging(applicationEnum.MemberService);
                //int profileid = Convert.ToInt32(viewerprofileid);
                logger.WriteSingleEntry(logseverityEnum.Warning, globals.getenviroment, ex, null);

                //throw convertedexcption;
                //log the error and do nothing for for now
            }


        }        


        public async Task<Guid> GenerateAPIkey(ApiKeyValidationModel model)
        {
            apikey newkey = new apikey();

         //   _unitOfWork.DisableProxyCreation = false;
          //    using (var db = _unitOfWork)
            {
                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {
                        return GenerateUserAPIkey(model,_unitOfWorkAsync);
                    });
                    return await task.ConfigureAwait(false);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member Apikey service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                  throw ex;

                    //throw convertedexcption;
                }
            } 
        }


        #region "private methods for re-use"

        /// <summary>
        /// genereates a new API key for a user by application
        /// it also creates a user account if one is not present using the unuqie indentifer for that user that ties to thier app
        /// finally it disables all other keys since the new key should be the only actibe one
        /// </summary>
        /// <param name="model"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private Guid GenerateUserAPIkey(ApiKeyValidationModel model,IUnitOfWorkAsync db)
        {
            apikey newkey = new apikey();

           
                try
                {

                        int userid = 0;
                        newkey.timestamp = DateTime.Now;
                        int applicationid = model.application_id.GetValueOrDefault();
                        //use this for the application descritipoin
                        var application = _unitOfWorkAsync.Repository<lu_application>().Queryable().Where(p => p.id == applicationid ).FirstOrDefault();
                        newkey.application_id = applicationid;
                        newkey.accesslevel_id = (int)accesslevelsenum.user;
                        //user handling   
                        //check for existing user that is tied to this application, search the apikey table
                        var existinguser = _unitOfWorkAsync.Repository<user>().Queryable().Where
                            (p => p.username.ToUpper() == model.username.ToUpper() && p.useridentifier == model.useridentifier && p.apikeys.Any(z=>z.application_id == model.application_id)).FirstOrDefault();


                        if (existinguser == null  && application !=null)
                        {
                            //generate a new user and assign the id                            
                            userid = AddAPIKeyUser(model.username, model.useridentifier, application.description, db);
                        }
                        else if (application == null)
                        {

                             //CustomExceptionTypes("Invalid application id");
                        
                        }
                        else
                        {
                            userid = existinguser.id;
                        }


                        // newkey.key = new Guid();
                        newkey.user_id = userid;
                        newkey.active = true;
                        newkey.lastaccesstime = DateTime.Now;
//return newkey.key;
                        _unitOfWorkAsync.Repository<apikey>().Insert(newkey);
                        var j = _unitOfWorkAsync.SaveChanges();
                       
                        //disable all other keys for this user since we have made a new one
                        //async call
                        DeactivateOldApiKeysByUserIdentifierAndKeyAndApplication(userid, newkey.keyvalue, applicationid);

                        return newkey.keyvalue;
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new Logging(applicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member Apikey service");                    
                   
                    throw ex;

                    //throw convertedexcption;
                }
            
        }


        /// <summary>
        /// create a new user in the API key database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="useridentifier"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private int  AddAPIKeyUser(string username, int useridentifier,string registringapplication, IUnitOfWorkAsync db)
        {
            
             ////   using (var transaction = db.BeginTransaction())
             //   {

                    try
                    {
                           user user = new user();
                           user.timestamp = DateTime.Now;
                           user.useridentifier = useridentifier;
                           user.username = username;
                           user.active = true;
                           user.registeringapplication = registringapplication;

                           _unitOfWorkAsync.Repository<user>().Insert(user);
                        //only use commit if we have a transaction
                           var j = _unitOfWorkAsync.SaveChanges();
                          //  db.Add(user);
                            //save all changes bro
                           // int i = db.Commit();
                          // // transaction.Commit();

                            return user.id;

                        
                    }
                    catch (Exception ex)
                    {
                      // // transaction.Rollback();
                        //instantiate logger here so it does not break anything else.
                        logger = new Logging(applicationEnum.MemberService);
                        //int profileid = Convert.ToInt32(viewerprofileid);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(useridentifier));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member service");
                     
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw ex;

                        //throw convertedexcption;
                    }
                
         //   }

        }
        
        #endregion

        //public List<Guid> APIKeys()
        //{
        //    try
        //    {
        //        // Get from the cache
        //        // Could also use AppFabric cache for scalability
        //        var keys = HttpContext.Current.Cache[APIKEYLIST] as List<Guid>;
        //        if (keys == null)
        //            keys = PopulateAPIKeys();
        //        return keys;
        //    }
        //    catch (Exception ex)
        //    {

        //        //instantiate logger here so it does not break anything else.
        //        logger = new  Logging(applicationEnum.MemberService);
        //        //int profileid = Convert.ToInt32(viewerprofileid);
        //        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null);
        //        //can parse the error to build a more custom error mssage and populate fualt faultreason
        //        FaultReason faultreason = new FaultReason("Error in member Apikey service");
        //        string ErrorMessage = "";
        //        string ErrorDetail = "ErrorMessage: " + ex.Message;
        //      throw ex;

        //        //throw convertedexcption;
        //    }
        //}

        //private List<Guid> PopulateAPIKeys()
        //{
        //    List<Guid> keyList;

        //    DataContractSerializer dcs = new DataContractSerializer(typeof(List<Guid>));
        //    var server = HttpContext.Current.Server;
        //    using (FileStream fs = new FileStream(server.MapPath("~/ApiKeys/APIKeys.xml"), FileMode.Open))
        //    using (XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas()))
        //    {
        //        keyList = (List<Guid>)dcs.ReadObject(reader);
        //    }

        //    // Save it in the cache
        //    // Could be saved in AppFabric Cache for scalability across a farm
        //    HttpContext.Current.Cache[APIKEYLIST] = keyList;

        //    return keyList;
        //}

        //TO DO use enum maybe
    }
}
