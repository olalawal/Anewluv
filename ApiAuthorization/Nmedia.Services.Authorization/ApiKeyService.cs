using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;

using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Nmedia.Services.Contracts;
using Nmedia.DataAccess.Interfaces;
using Nmedia.Infrastructure.Domain.Data.ApiKey;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data.errorlog;
using System.Web;
using System.IO;
using System.Xml;
using Shell.MVC2.Infrastructure.WCF;
using Anewluv.Lib;

namespace Nmedia.Services.Authorization
{
    //TO do with this new patter either find a way to make extention methods use the generic repo dict in EFUnitOfWork or 
    //MOve the remaining ext method methods into this service layer that uses the EF unit of work
    //TO do also make a single Update method generic and implement that for the rest of the Repo saves.
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ApiKeyService : IApiKeyService
    {
        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        IUnitOfWork _unitOfWork;
        private ErroLogging logger;

        public ApiKeyService(IUnitOfWork unitOfWork)
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
            _unitOfWork = unitOfWork;
            //disable proxy stuff by default
            //_unitOfWork.DisableProxyCreation = true;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
        }

        //TO DO use the database context wehre API key is stored 

        const string APIKEYLIST = "APIKeyList";

        public bool NonAysncIsValidAPIKey(string key)
        {
            // TODO: Implement IsValidAPI Key using your repository

            Guid apiKey;

            // _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                    //Convert the string into a Guid and validate it
                    // not validating against a list anymore
                    if (Guid.TryParse(key, out apiKey) && db.GetRepository<apikey>().Find().Any(p => p.key == apiKey))     //APIKeys.Contains(apiKey))
                    {
                        //  return true;
                        return true;
                    }
                    else
                    {
                        //  return false;
                        return false;

                    }

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member Apikey service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }
        }

        public IAsyncResult BeginIsValidAPIKey(string key, AsyncCallback callback, object asyncState)
        {
            // TODO: Implement IsValidAPI Key using your repository

            Guid apiKey;

           // _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                    //Convert the string into a Guid and validate it
                    // not validating against a list anymore
                    if (Guid.TryParse(key, out apiKey) && db.GetRepository<apikey>().Find().Any(p => p.key == apiKey))     //APIKeys.Contains(apiKey))
                    {
                      //  return true;
                        return new CompletedAsyncResult<bool>(true);
                    }
                    else
                    {
                      //  return false;
                        return new CompletedAsyncResult<bool>(false);

                    }

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member Apikey service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }

            }
        }

        public bool EndIsValidAPIKey(IAsyncResult r)
        {
            CompletedAsyncResult<bool> result = r as CompletedAsyncResult<bool>;
           // Console.WriteLine("EndServiceAsyncMethod called with: \"{0}\"", result.Data);
            return result.Data;
        }

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
        //        logger = new ErroLogging(logapplicationEnum.MemberService);
        //        //int profileid = Convert.ToInt32(viewerprofileid);
        //        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null);
        //        //can parse the error to build a more custom error mssage and populate fualt faultreason
        //        FaultReason faultreason = new FaultReason("Error in member Apikey service");
        //        string ErrorMessage = "";
        //        string ErrorDetail = "ErrorMessage: " + ex.Message;
        //        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

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
        public IAsyncResult BegingenerateAPIkey(string service, AsyncCallback callback, object asyncState)
        {
           
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    Guid key = new Guid();
                    //store to DB
                    return new CompletedAsyncResult<Guid>(key);
                    //return key;
                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new ErroLogging(logapplicationEnum.MemberService);
                    //int profileid = Convert.ToInt32(viewerprofileid);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null);
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member Apikey service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                    //throw convertedexcption;
                }
            }

        }

        public Guid EndgenerateAPIkey(IAsyncResult r)
        {
            CompletedAsyncResult<Guid> result = r as CompletedAsyncResult<Guid>;
            //Console.WriteLine("EndServiceAsyncMethod called with: \"{0}\"", result.Data);
            return result.Data;
        }

    }
}
