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
using LoggingLibrary;

using System.Web;
using System.IO;
using System.Xml;
using Nmedia.Infrastructure.Domain.Data.ApiKey;
using Nmedia.Infrastructure.Domain.Data.log;

using Nmedia.Infrastructure.Domain.Data;

using System.Threading.Tasks;
using Nmedia.Infrastructure.Domain;
//using Nmedia.Infrastructure.WCF;
//using Anewluv.Lib;
//using Anewluv.Lib;

namespace Nmedia.Services.ApikeyAuthorization
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

     //  private static  IUnitOfWork _unitOfWork;
        private  IUnitOfWork _unitOfWork;
        private Logging logger;

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

        //TO DO use the Initial Catalog= context wehre API key is stored 

        const string APIKEYLIST = "APIKeyList";

        public async Task<bool> IsValidAPIKey(apikey model)
        {
            // TODO: Implement IsValidAPI Key using your repository

            //Guid apiKey;

            // _unitOfWork.DisableProxyCreation = true;
         
               Boolean result = false;
                try
                {

                    //Convert the string into a Guid and validate it
                    // not validating against a list anymore

                    var task = Task.Factory.StartNew(() =>
                    {
                        _unitOfWork.DisableProxyCreation = true;
                        _unitOfWork.DisableLazyLoading = true;
                         using (var db = _unitOfWork)
                         {
                            result =  db.GetRepository<apikey>().Find().Any(p => p.key == model.key & p.active == true); 
                         }
                        return result;
                    });
                    return await task.ConfigureAwait(false);
                }               
                   

                
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new  Logging(applicationEnum.MemberService);
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
        public async Task<Guid> GenerateAPIkey(string service, string username ,string application)
        {
           
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {                   
              

                        Guid key = new Guid();
                       
                        
                    


                        return key;
                    });
                    return await task.ConfigureAwait(false);

                }
                catch (Exception ex)
                {

                    //instantiate logger here so it does not break anything else.
                    logger = new  Logging(applicationEnum.MemberService);
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

       
    }
}
