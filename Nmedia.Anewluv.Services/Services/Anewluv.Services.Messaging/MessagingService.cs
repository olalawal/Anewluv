using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Anewluv.Services.Contracts;
using System.Web;
using System.Net;

using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using System.ServiceModel.Activation;
using LoggingLibrary;

using Anewluv.Services.Contracts.ServiceResponse;

using System.IO;

//using Nmedia.DataAccess.Interfaces;
using System.Drawing;

using Nmedia.Infrastructure.Domain.Data.log;
using Nmedia.Infrastructure.Domain.Data;
using System.Threading.Tasks;
using Anewluv.Domain;
using Repository.Pattern.UnitOfWork;

namespace Anewluv.Services.Media
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
   [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]  
    public class MessagingService : IMessagingService 
    {


        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private LoggingLibrary.Logging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public MessagingService(IUnitOfWorkAsync unitOfWork)
        {

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork", "unitOfWork cannot be null");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("dataContext", "dataContext cannot be null");
            }

         
            _unitOfWorkAsync = unitOfWork;
         

        }

       



        public bool sendmessage(int? id)
         {
            return true;
         }     
       
         public void addmailfoxfolder()
         {


         }  
        
        public int getmailcountbyfolder(int folderid, int profileid)   
       {
            return 0;
       }   
     
       
         public List<mailviewmodel> getmailpagedbyfolder(int folderid, int profileid)
         {
             List<mailviewmodel> model = new List<mailviewmodel>();
             return model;


         }
        



              


           

        }


}

