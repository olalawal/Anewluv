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
using Nmedia.Infrastructure.Domain.Data.errorlog;
using Shell.MVC2.Infrastructure.WCF;

namespace Nmedia.Services.Logging
{
    //TO do with this new patter either find a way to make extention methods use the generic repo dict in EFUnitOfWork or 
    //MOve the remaining ext method methods into this service layer that uses the EF unit of work
    //TO do also make a single Update method generic and implement that for the rest of the Repo saves.
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ErrorLoggingService : IErrorLoggingService
    {
        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        IUnitOfWork _unitOfWork;


        public ErrorLoggingService(IUnitOfWork unitOfWork)
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

        public IAsyncResult BeginWriteCompleteLogEntry(errorlog logEntry, AsyncCallback callback, object asyncState)
        {

            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        IRepository<lu_logapplication> applicationrepo = db.GetRepository<lu_logapplication>();
                        IRepository<lu_logseverity> logseverityrepo = db.GetRepository<lu_logseverity>();
                        IRepository<lu_logenviroment> logenviromentrepo = db.GetRepository<lu_logenviroment>();

                        //make sure we valid desc and log severites so we are not adding new ones
                        lu_logapplication application = applicationrepo.FindSingle(p => p.id == logEntry.application.id);
                        lu_logseverity logseverity = logseverityrepo.FindSingle(p => p.id == logEntry.logseverity.id);
                        lu_logenviroment enviroment = logenviromentrepo.FindSingle(p => p.id == logEntry.enviroment.id);

                        //set as default error messages if blank
                        logEntry.application = application != null ? application : applicationrepo.FindSingle(p => p.id == (int)logapplicationEnum.GeneralApplicationError);
                        logEntry.logseverity = logseverity != null ? logseverity : logseverityrepo.FindSingle(p => p.id == (int)logseverityEnum.Warning);
                        logEntry.enviroment = enviroment != null ? enviroment : logenviromentrepo.FindSingle(p => p.id == (int)logenviromentEnum.dev); 


                        db.Add(logEntry);
                        int i = db.Commit();
                        transaction.Commit();


                        // return i;
                        return new CompletedAsyncResult<int>(i);
                        //return i;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        //log to eventlog and notify
                        //TO DO notifiy or Supress ?
                        //supress.  we can't risk have an endless loop of exceptions here.
                        WriteToEventLog(ex.Message, EventLogEntryType.Error);
                        //throw new InvalidOperationException("Failed to update the log. Try your request again.", generalexception);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Generic Error");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                       
                    }
                }
            }

            return new CompletedAsyncResult<int>(0);
        }

        public int EndWriteCompleteLogEntry(IAsyncResult r)
        {
            CompletedAsyncResult<int> result = r as CompletedAsyncResult<int>;
            Console.WriteLine("EndServiceAsyncMethod called with: \"{0}\"", result.Data);
            return result.Data;
        }

        public int TranslateLogSeverity(logseverityEnum logseverityvalue)
        {
            try
            {
                return Convert.ToInt32(LogSeverityUtil.TranslateLogSeverity(logseverityvalue));
                //return 1;
            }
            catch (Exception ex)
            {
                //handle logger error 

            }
            return 0;
        }

        /// <summary>
        /// Writes to Windows event log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="entryType">Type of the entry.</param>
        private void WriteToEventLog(string message, EventLogEntryType entryType)
        {
            try
            {
                string sourceName = "Logger";
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }
                EventLog.WriteEntry(sourceName, message, entryType);
            }
            catch (Exception ex)
            {
                //supress.  we can't risk have an endless loop of exceptions here.
            }
        }

        //main promotionobject service impelematations
    }
}
