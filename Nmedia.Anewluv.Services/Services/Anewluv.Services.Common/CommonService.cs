using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Anewluv.Services.Contracts;




using System.Web;
using System.Net;


using System.ServiceModel.Activation;
using Anewluv.Domain.Data.ViewModels;
//using Nmedia.DataAccess.Interfaces;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data.log;
using System.Threading.Tasks;
using Repository.Pattern.UnitOfWork;


namespace Anewluv.Services.Common
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CommonService : ICommonService
    {


       
        IUnitOfWorkAsync _unitOfWorkAsync;
        private LoggingLibrary.Logging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public CommonService(IUnitOfWorkAsync unitOfWork)
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
            //disable proxy stuff by default
            //_unitOfWorkAsync.DisableProxyCreation = true;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        }

        public async Task<string> getNETJSONdatefromISO(DateValidateModel date)
        {


            try
            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {
                        var test = Serialization.datetimetojson(DateTime.Parse(date.IsoDate));
                        return test;
                    });
                    return await task.ConfigureAwait(false);
              
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    //new  Logging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null, null,false);
                    //logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, profileid, null);
                    //log error mesasge
                    //handle logging here
                    var message = ex.Message;
                    throw;
                }

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Generic Error");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }




    }
     
     
     
    

}
