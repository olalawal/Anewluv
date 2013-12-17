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
using System.ServiceModel.Activation;
using Nmedia.DataAccess.Interfaces;
using LoggingLibrary;
//using Nmedia.Infrastructure.Domain.errorlog;
using Anewluv.Domain.Data.ViewModels;
using Shell.MVC2.Infrastructure;
using Anewluv.Lib;
using Anewluv.DataExtentionMethods;
using Nmedia.Infrastructure.Domain.Data.errorlog;
using Anewluv.Domain;
using Anewluv.Services.Members;
using Anewluv.Services.Mapping;

namespace Anewluv.Services.MemberActions
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MemberActionsService : IMemberActionsService
    {

        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        IUnitOfWork _unitOfWork;
        private LoggingLibrary.ErroLogging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public MemberActionsService(IUnitOfWork unitOfWork)
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

        //BEfore unit of work contrcutor
        //public MemberActionsService(IMemberActionsRepository memberactionsrepository)
        //    {
        //        _memberactionsrepository = memberactionsrepository;
        //       // _apikey  = HttpContext.Current.Request.QueryString["apikey"];
        //      //  throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        //    }


        //TO DO come back to this
        public List<MemberSearchViewModel> getmyrelationshipsfiltered(string profileid, List<profilefiltertypeEnum> types)
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {

                    //return _memberactionsrepository.getmyrelationshipsfiltered(Convert.ToInt32(profileid), types);
                    return null;

                }
                catch (Exception ex)
                {
                    logger = new ErroLogging(logapplicationEnum.MemberActionsService);
                    //int profileid = Convert.ToInt32(viewerprofileid);

                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(profileid)); logger.Dispose();
                    
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
        }

        //#region "Private methods used internally to this repo"
        //private IQueryable<block> activeblocksbyprofileid(int profileid)
        //{
        //    //_unitOfWork.DisableProxyCreation = true;
        //    using (var db = _unitOfWork)
        //    {
        //        try
        //        {
        //            IRepository<block> repo = db.GetRepository<block>();
        //            //filter out blocked profiles 
        //            return repo.Find().OfType<block>().Where(p => p.profile_id == id && p.removedate != null);
        //        }
        //        catch (Exception ex)
        //        {
        //            //instantiate logger here so it does not break anything else.
        //            new ErroLogging(logapplicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, profileid, null);
        //            //logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, profileid, null);
        //            //log error mesasge
        //            //handle logging here
        //            var message = ex.Message;
        //            throw;
        //        }
        //    }
        //}
     
        //private List<MemberSearchViewModel> getunpagedwhoisinterestedinme(int profileid, IQueryable<block> MyActiveblocks)
        //{


        //       _unitOfWork.DisableProxyCreation = true;
        // using (var db = _unitOfWork)
        // {
        //     try
        //     {
        //         return (from p in  db.GetRepository<interest>().Find().Where(p => p.interestprofile_id == id & p.deletedbymemberdate == null)
        //                 join f in  db.GetRepository<profiledata>().Find() on p.profile_id equals f.profile_id
        //                 join z in  db.GetRepository<profile>().Find() on p.profile_id equals z.id
        //                 where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
        //                 select new MemberSearchViewModel
        //                 {
        //                     interestdate = p.creationdate,
        //                     id = f.profile_id
        //                     // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
        //                 }).ToList();




        //     }
        //     catch (Exception ex)
        //     {

        //           new ErroLogging(logapplicationEnum.MemberActionsService);
        //            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
        //            //can parse the error to build a more custom error mssage and populate fualt faultreason
        //            FaultReason faultreason = new FaultReason("Error in member actions service");
        //            string ErrorMessage = "";
        //            string ErrorDetail = "ErrorMessage: " + ex.Message;
        //            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
        //     }

        // }


           

        //}
       
        //private List<MemberSearchViewModel> getunpagedwhoiaminsterestedin(int profileid, IQueryable<block> MyActiveblocks)
        //{

        //       _unitOfWork.DisableProxyCreation = true;
        // using (var db = _unitOfWork)
        // {
        //     try
        //     {
        //           //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
        //        //rematerialize on the back end.
        //        //final query to send back only the profile datatas of the interests we want
        //        return (from p in  db.GetRepository<interest>().Find().Where(p => p.profile_id == id & p.deletedbymemberdate == null)
        //                join f in  db.GetRepository<profiledata>().Find() on p.interestprofile_id equals f.profile_id
        //                join z in  db.GetRepository<profile>().Find() on p.interestprofile_id equals z.id
        //                where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
        //                select new MemberSearchViewModel
        //                {
        //                    interestdate = p.creationdate,
        //                    id = f.profile_id
        //                    // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
        //                }).ToList();
        //    }
             
        //     catch (Exception ex)
        //     {

        //           new ErroLogging(logapplicationEnum.MemberActionsService);
        //            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
        //            //can parse the error to build a more custom error mssage and populate fualt faultreason
        //            FaultReason faultreason = new FaultReason("Error in member actions service");
        //            string ErrorMessage = "";
        //            string ErrorDetail = "ErrorMessage: " + ex.Message;
        //            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
        //     }

        // }

         

        //}
        //private List<MemberSearchViewModel> getunpagedwhopeekedatme(int profileid, IQueryable<block> MyActiveblocks)
        //{

        //       _unitOfWork.DisableProxyCreation = true;
        // using (var db = _unitOfWork)
        // {
        //     try
        //     {
        //         //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
        //         //rematerialize on the back end.
        //         //final query to send back only the profile datatas of the interests we want
        //         return (from p in  db.GetRepository<peek>().Find().Where(p => p.peekprofile_id == id && p.deletedbymemberdate == null)
        //                 join f in  db.GetRepository<profiledata>().Find() on p.profile_id equals f.profile_id
        //                 join z in  db.GetRepository<profile>().Find() on p.profile_id equals z.id
        //                 where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
        //                 select new MemberSearchViewModel
        //                 {
        //                     peekdate = p.creationdate,
        //                     id = f.profile_id
        //                     // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
        //                 }).ToList();

        //     }
        //     catch (Exception ex)
        //     {

        //           new ErroLogging(logapplicationEnum.MemberActionsService);
        //            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
        //            //can parse the error to build a more custom error mssage and populate fualt faultreason
        //            FaultReason faultreason = new FaultReason("Error in member actions service");
        //            string ErrorMessage = "";
        //            string ErrorDetail = "ErrorMessage: " + ex.Message;
        //            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
        //     }

        // }

           



        //}

        //private List<MemberSearchViewModel> getunpagedwhoipeekedat(int profileid, IQueryable<block> MyActiveblocks)
        //{

        //       _unitOfWork.DisableProxyCreation = true;
        // using (var db = _unitOfWork)
        // {
        //     try
        //     {

        //         //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
        //         //rematerialize on the back end.
        //         //final query to send back only the profile datatas of the interests we want
        //         return (from p in  db.GetRepository<peek>().Find().Where(p => p.profile_id == id && p.deletedbymemberdate == null)
        //                 join f in  db.GetRepository<profiledata>().Find() on p.peekprofile_id equals f.profile_id
        //                 join z in  db.GetRepository<profile>().Find() on p.peekprofile_id equals z.id
        //                 where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
        //                 select new MemberSearchViewModel
        //                 {
        //                     peekdate = p.creationdate,
        //                     id = f.profile_id
        //                 }).ToList();

        //     }
        //     catch (Exception ex)
        //     {

        //           new ErroLogging(logapplicationEnum.MemberActionsService);
        //            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
        //            //can parse the error to build a more custom error mssage and populate fualt faultreason
        //            FaultReason faultreason = new FaultReason("Error in member actions service");
        //            string ErrorMessage = "";
        //            string ErrorDetail = "ErrorMessage: " + ex.Message;
        //            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
        //     }

        // }

           


        //}
        //private List<MemberSearchViewModel> getunpagedblocks(int profileid)
        //{

        //       _unitOfWork.DisableProxyCreation = true;
        // using (var db = _unitOfWork)
        // {
        //     try
        //     {
        //         return (from p in  db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate == null)
        //                 join f in  db.GetRepository<profiledata>().Find() on p.blockprofile_id equals f.profile_id
        //                 join z in  db.GetRepository<profile>().Find() on p.blockprofile_id equals z.id
        //                 where (f.profile.status.id < 3)
        //                 orderby (p.creationdate) descending
        //                 select new MemberSearchViewModel
        //                 {
        //                     blockdate = p.creationdate,
        //                     id = f.profile_id
        //                 }).ToList();


        //     }
        //     catch (Exception ex)
        //     {

        //           new ErroLogging(logapplicationEnum.MemberActionsService);
        //            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
        //            //can parse the error to build a more custom error mssage and populate fualt faultreason
        //            FaultReason faultreason = new FaultReason("Error in member actions service");
        //            string ErrorMessage = "";
        //            string ErrorDetail = "ErrorMessage: " + ex.Message;
        //            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
        //     }

        // }

          

        //}
        //private List<MemberSearchViewModel> getunpagedwholikesme(int profileid, IQueryable<block> MyActiveblocks)
        //{

        //       _unitOfWork.DisableProxyCreation = true;
        // using (var db = _unitOfWork)
        // {
        //     try
        //     {
        //         //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
        //         //rematerialize on the back end.
        //         //final query to send back only the profile datatas of the interests we want
        //         return (from p in  db.GetRepository<like>().Find().Where(p => p.likeprofile_id == id && p.deletedbylikedate == null)
        //                 join f in  db.GetRepository<profiledata>().Find() on p.profile_id equals f.profile_id
        //                 join z in  db.GetRepository<profile>().Find() on p.profile_id equals z.id
        //                 where (f.profile.status_id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
        //                 orderby (p.creationdate) descending
        //                 select new MemberSearchViewModel
        //                 {
        //                     likedate = p.creationdate,
        //                     id = f.profile_id
        //                 }).ToList();

        //     }
        //     catch (Exception ex)
        //     {

        //           new ErroLogging(logapplicationEnum.MemberActionsService);
        //            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
        //            //can parse the error to build a more custom error mssage and populate fualt faultreason
        //            FaultReason faultreason = new FaultReason("Error in member actions service");
        //            string ErrorMessage = "";
        //            string ErrorDetail = "ErrorMessage: " + ex.Message;
        //            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
        //     }

        // }

       


        //}
        //private List<MemberSearchViewModel> getunpagedwhoilike(int profileid, IQueryable<block> MyActiveblocks)
        //{

        //       _unitOfWork.DisableProxyCreation = true;
        // using (var db = _unitOfWork)
        // {
        //     try
        //     {
        //         //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
        //         //rematerialize on the back end.
        //         //final query to send back only the profile datatas of the interests we want
        //         return (from p in  db.GetRepository<like>().Find().Where(p => p.profile_id == id && p.deletedbymemberdate == null)
        //                 join f in  db.GetRepository<profiledata>().Find() on p.likeprofile_id equals f.profile_id
        //                 join z in  db.GetRepository<profile>().Find() on p.likeprofile_id equals z.id
        //                 where (f.profile.status_id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
        //                 orderby (p.creationdate) descending
        //                 select new MemberSearchViewModel
        //                 {
        //                     likedate = p.creationdate,
        //                     id = f.profile_id
        //                 }).ToList();


        //     }
        //     catch (Exception ex)
        //     {

        //           new ErroLogging(logapplicationEnum.MemberActionsService);
        //            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
        //            //can parse the error to build a more custom error mssage and populate fualt faultreason
        //            FaultReason faultreason = new FaultReason("Error in member actions service");
        //            string ErrorMessage = "";
        //            string ErrorDetail = "ErrorMessage: " + ex.Message;
        //            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
        //     }

        // }

        

        //}

        ////MAke an anon method to handle this paging and apply to all paged methods 
        ////private int setpaging (MemberSearchViewModel models,int? page,int? numberperpage)
        ////{

        ////    bool allowpaging = (models.Count >= (page.GetValueOrDefault()  * numberperpage.GetValueOrDefault())) ? true : false);
        ////    var pageData = page.GetValueOrDefault() > 1 & allowpaging ?

        ////    new PaginatedList<MemberSearchViewModel>().GetCurrentPages(models, page ?? 1, numberperpage ?? 4) : models.Take(numberperpage.GetValueOrDefault());

        ////}
        //#endregion

        #region "Interest Methods"

        //added 1/29/2010 ola lawal
        //no checks i.e invokes to test values here only updates deletes etc , for now on the MVC side
        // they are prevalidated on the model and the booleans are checked on the view  view possible if and if statement exists
        //    remeber emails must be sent on the client side since they user shared reference files    

        //interest methods

        #region "Count methods"

        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public int getwhoiaminterestedincount(string profileid)
        {
          
               _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 count = (
            from f in db.GetRepository<interest>().Find()
            where (f.profile_id ==   id && f.deletedbymemberdate == null)
            select f).Count();
                 // ?? operator example.
                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;
                 return defaultvalue;

             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

         
        }

        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public int getwhoisinterestedinmecount(string profileid)
        {

               _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 //filter out blocked profiles 
                 var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate != null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 count = (
                    from p in  db.GetRepository<interest>().Find()
                    where (p.interestprofile_id == id)
                    join f in  db.GetRepository<profile>().Find() on p.profile_id equals f.id
                    where (f.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                    select f).Count();

                 // ?? operator example.


                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;

                 return defaultvalue;

             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

       
        }

        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public int getwhoisinterestedinmenewcount(string profileid)
        {

               _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 //filter out blocked profiles 
                 var MyActiveblocks = from c in  db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate != null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 count = (
                    from p in  db.GetRepository<interest>().Find()
                    where (p.interestprofile_id == id && p.viewdate == null)
                    join f in  db.GetRepository<profile>().Find() on p.profile_id equals f.id
                    where (f.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                    select f).Count();

                 // ?? operator example.


                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;

                 return defaultvalue;

             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

       
        }

        #endregion


        /// <summary>
        /// //gets list of all the profiles I am interested in
        /// </summary 
        public List<MemberSearchViewModel> getwhoiaminterestedin(string profileid, string page, string numberperpage)
        {




            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

                    int id = Convert.ToInt32(profileid);

                    var MyActiveblocks = from c in db.GetRepository<block>().Find().OfType<block>().Where(p => p.profile_id == id && p.removedate == null)
                                         select new
                                         {
                                             ProfilesBlockedId = c.blockprofile_id
                                         };

                    //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                    //rematerialize on the back end.
                    //final query to send back only the profile datatas of the interests we want
                    var interests = (from p in db.GetRepository<interest>().Find().OfType<interest>().Where(p => p.profile_id == id && p.deletedbymemberdate == null)                              
                                     join f in  db.GetRepository<profiledata>().Find() on p.interestprofile_id equals f.profile_id
                                     join z in  db.GetRepository<profile>().Find() on p.interestprofile_id equals z.id
                                     where (f.profile.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                                     select new MemberSearchViewModel
                                     {
                                         interestdate = p.creationdate,
                                         id = f.profile_id
                                         // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                     }).ToList();
                    // var dd2 = 0;
                    //var dd = 2 /  dd2;

                    int? pageint = Convert.ToInt32(page);
                    int?  numberperpageint =  Convert.ToInt32(numberperpage);

                    bool allowpaging = (interests.Count >= (pageint * numberperpageint) ? true : false);
                    var pageData = pageint > 1 & allowpaging ?
                        new PaginatedList<MemberSearchViewModel>().GetCurrentPages(interests, pageint ?? 1, numberperpageint ?? 4) : interests.Take(numberperpageint.GetValueOrDefault());
                    //this.AddRange(pageData.ToList());
                    // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                    //return interests.ToList();
                    List<MemberSearchViewModel> results;
                    AnewluvContext AnewluvContext  = new AnewluvContext();
                    using (var tempdb = AnewluvContext)
                    {
                        MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                        results = MemberMapperService.mapmembersearchviewmodels(profileid, pageData.ToList(), "false").OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                    }
                    // return data2.OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();;
                    //.OrderByDescending(f => f.interestdate ?? DateTime.MaxValue).ToList();
                    return results;
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    new ErroLogging(logapplicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid), null);
                    //logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, profileid, null);
                    //log error mesasge
                    //handle logging here
                    var message = ex.Message;
                    // throw;
                    //pass back the fault
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
                finally
                {
                    //Api.DisposeMemberMapperService();
                }



            }
        }

        //1/18/2011 modifed results to use correct ordering
        /// <summary>
        /// //gets all the members who are interested in me
        /// </summary 
        public List<MemberSearchViewModel> getwhoisinterestedinme(string profileid, string page, string numberperpage)
        {
            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

          _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 var id = Convert.ToInt32(profileid);

                 var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate == null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                 //rematerialize on the back end.
                 //final query to send back only the profile datatas of the interests we want
                 var whoisinterestedinme = (from p in db.GetRepository<interest>().Find().Where(p => p.interestprofile_id == id && p.deletedbymemberdate == null)
                                            join f in db.GetRepository<profiledata>().Find() on p.profile_id equals f.profile_id
                                            join z in db.GetRepository<profile>().Find() on p.profile_id equals z.id
                                            where (f.profile.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                                            select new MemberSearchViewModel
                                            {
                                                interestdate = p.creationdate,
                                                id = f.profile_id
                                                // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                            }).ToList();

                 int? pageint = Convert.ToInt32(page);
                 int? numberperpageint = Convert.ToInt32(numberperpage);

                 bool allowpaging = (whoisinterestedinme.Count >= (pageint * numberperpageint) ? true : false);
                 var pageData = pageint > 1 & allowpaging ?
                     new PaginatedList<MemberSearchViewModel>().GetCurrentPages(whoisinterestedinme, pageint ?? 1, numberperpageint ?? 4) : whoisinterestedinme.Take(numberperpageint.GetValueOrDefault());
                 //this.AddRange(pageData.ToList());
                 // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                 //return interests.ToList();
                 List<MemberSearchViewModel> results;
                 AnewluvContext AnewluvContext = new AnewluvContext();
                 using (var tempdb = AnewluvContext)
                 {
                     MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                    results = MemberMapperService.mapmembersearchviewmodels(profileid, pageData.ToList(), "false").OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                 }

                 return results;
             }
             catch (Exception ex)
             {

                new ErroLogging(logapplicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member actions service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }
             finally
             {
               //  Api.DisposeMemberMapperService();
             }

         }



   
        }

        /// <summary>
        /// //gets all the members who are interested in me, that ive not viewd yet
        /// </summary 
        public List<MemberSearchViewModel> getwhoisinterestedinmenew(string profileid, string page, string numberperpage)
        {
            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

               _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 var id = Convert.ToInt32(profileid);

                 var MyActiveblocks = from c in  db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate != null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                 //rematerialize on the back end.
                 //final query to send back only the profile datatas of the interests we want
                 var whoisinterestedinmenew = (from p in  db.GetRepository<interest>().Find().Where(p => p.interestprofile_id == id && p.viewdate == null)
                                               join f in  db.GetRepository<profiledata>().Find() on p.profile_id equals f.profile_id
                                               join z in  db.GetRepository<profile>().Find() on p.profile_id equals z.id
                                               where (f.profile.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))

                                               select new MemberSearchViewModel
                                               {
                                                   interestdate = p.creationdate,
                                                   id = f.profile_id
                                                   // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                               }).ToList();

                 int? pageint = Convert.ToInt32(page);
                 int? numberperpageint = Convert.ToInt32(numberperpage);

                 bool allowpaging = (whoisinterestedinmenew.Count >= (pageint * numberperpageint) ? true : false);
                 var pageData = pageint > 1 & allowpaging ?
                     new PaginatedList<MemberSearchViewModel>().GetCurrentPages(whoisinterestedinmenew, pageint ?? 1, numberperpageint ?? 4) : whoisinterestedinmenew.Take(numberperpageint.GetValueOrDefault());
                 //this.AddRange(pageData.ToList());
                 // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                 //return interests.ToList();
                   AnewluvContext AnewluvContext = new AnewluvContext();
                   using (var tempdb = AnewluvContext)
                   {
                       MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                       return MemberMapperService.mapmembersearchviewmodels(profileid, pageData.ToList(), "false").OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                   }


             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

             finally
             {
                // Api.DisposeMemberMapperService();
             }

         }


     
        }

        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both interest 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public List<MemberSearchViewModel> getmutualinterests(string profileid, string targetprofileid)
        {

               _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 IEnumerable<MemberSearchViewModel> mutualinterests = default(IEnumerable<MemberSearchViewModel>);
                 return mutualinterests.ToList();

             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

         

        }
        /// <summary>
        /// //checks if you already sent and interest to the target profile
        /// </summary        
        public bool checkinterest(string profileid, string targetprofileid)
        {

               _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 var id = Convert.ToInt32(profileid);
                 var targetid = Convert.ToInt32(targetprofileid);
                 return  db.GetRepository<interest>().Find().Any(r => r.profile_id == id && r.interestprofile_id == targetid);

             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

      
        }

        /// <summary>
        /// Adds a New interest
        /// </summary>
        public bool addinterest(string profileid, string targetprofileid)
        {

            //create new inetrest object
            interest interest = new interest();
            //make sure you are not trying to interest at yourself
            if (profileid == targetprofileid) return false;


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        
                         var id = Convert.ToInt32(profileid);
                         var targetid = Convert.ToInt32(targetprofileid);

                       //check  interest first  
                         //if this was a interest being restored just do that part
                        var existinginterest = db.GetRepository<interest>().FindSingle(r => r.profile_id == id && r.interestprofile_id == targetid);

                        //just  update it if we have one already
                        if (existinginterest != null)
                        {
                            existinginterest.deletedbymemberdate = null; ;
                            existinginterest.modificationdate = DateTime.Now;
                            db.Update(existinginterest);

                        }
                        else
                        {
                            //interest = this. db.GetRepository<interest>().Find().Where(p => p.profileid == profileid).FirstOrDefault();
                            //update the profile status to 2
                            interest.profile_id = id;
                            interest.interestprofile_id = targetid;
                            interest.mutual = false;  // not dealing with this calulatin yet
                            interest.creationdate = DateTime.Now;
                            //handele the update using EF
                            // this. db.GetRepository<profile>().Find().AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                            db.Add(interest);
                            
                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


        


        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool removeinterestbyprofileid(string profileid, string interestprofile_id)
        {


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(interestprofile_id);

                        var interest = db.GetRepository<interest>().Find().Where(p => p.profile_id == id && p.interestprofile_id == targetid).FirstOrDefault();
                        //update the profile status to 2

                        interest.deletedbymemberdate = DateTime.Now;
                        interest.modificationdate = DateTime.Now;
                        db.Update(interest);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool removeinterestbyinterestprofileid(string interestprofile_id, string profileid)
        {


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(interestprofile_id);

                       var interest = db.GetRepository<interest>().Find().Where(p => p.profile_id == targetid && p.interestprofile_id == id).FirstOrDefault();
                        //update the profile status to 2

                        interest.deletedbyinterestdate = DateTime.Now;
                        interest.modificationdate = DateTime.Now;
                        db.Update(interest);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

       
        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool restoreinterestbyprofileid(string profileid, string interestprofile_id)
        {


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(interestprofile_id);

                      var  interest =  db.GetRepository<interest>().Find().Where(p => p.profile_id == id && p.interestprofile_id == targetid).FirstOrDefault();
                        //update the profile status to 2

                        interest.deletedbymemberdate = null;
                        interest.modificationdate = DateTime.Now;
                        db.Update(interest);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

          
        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool restoreinterestbyinterestprofileid(string interestprofile_id, string profileid)
        {


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(interestprofile_id);

                       var interest =  db.GetRepository<interest>().Find().Where(p => p.profile_id == targetid && p.interestprofile_id == id).FirstOrDefault();
                        //update the profile status to 2

                        interest.deletedbyinterestdate = null;
                        interest.modificationdate = DateTime.Now;
                        db.Update(interest);


                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

     

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removeinterestsbyprofileidandscreennames(string profileid, List<String> screennames)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        // interests = this. db.GetRepository<interest>().Find().Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                        //update the profile status to 2
                        interest interest = new interest();
                        foreach (string value in screennames)
                        {

                          
          

                           int? interestprofile_id = db.GetRepository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                           var  currentinterest = db.GetRepository<interest>().Find().Where(p => p.profile_id == id && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                            interest.deletedbymemberdate = DateTime.Now;
                            interest.modificationdate = DateTime.Now;
                            db.Update(currentinterest);
                           
                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

       
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restoreinterestsbyprofileidandscreennames(string profileid, List<String> screennames)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        // interests = this. db.GetRepository<interest>().Find().Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                        //update the profile status to 2
                        interest interest = new interest();
                        foreach (string value in screennames)
                        {
                            int? interestprofile_id = db.GetRepository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                            var currentinterest = db.GetRepository<interest>().Find().Where(p => p.profile_id == id && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                            interest.deletedbymemberdate = null;
                            interest.modificationdate = DateTime.Now;
                            db.Update(currentinterest);

                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


        

        }

        /// <summary>
        ///  Update interest with a view     
        /// </summary 
        public bool updateinterestviewstatus(string profileid, string targetprofileid)
        {
            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(targetprofileid);

                        var interest =  db.GetRepository<interest>().Find().Where(p => p.interestprofile_id == targetid && p.profile_id == id).FirstOrDefault();
                        //update the profile status to 2            
                        if (interest.viewdate == null)
                        {
                            interest.viewdate = DateTime.Now;
                            interest.modificationdate = DateTime.Now;
                            db.Update(interest);

                            int i = db.Commit();
                            transaction.Commit();
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }
        }

        #endregion


        #endregion

        //end of Inteerst methods
        //********************************************************

        //profile views in DB= peeks on UI
        #region "peek methods"

        //count methods first

        #region "Count methods"

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public int getwhoipeekedatcount(string profileid)
        {

               _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 count = (
            from f in db.GetRepository<peek>().Find()
            where (f.profile_id == id && f.deletedbymemberdate == null)
            select f).Count();
                 // ?? operator example.
                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;
                 return defaultvalue;

             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

        }

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public int getwhopeekedatmecount(string profileid)
        {

               _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 //filter out blocked profiles 
                 var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate != null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 count = (
                    from p in db.GetRepository<peek>().Find()
                    where (p.peekprofile_id == id)
                    join f in db.GetRepository<profile>().Find() on p.profile_id equals f.id
                    where (f.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                    select f).Count();

                 // ?? operator example.


                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;

                 return defaultvalue;
             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

          
        }

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public int getwhopeekedatmenewcount(string profileid)
        {

               _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 //filter out blocked profiles 
                 var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate != null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 count = (
                    from p in db.GetRepository<peek>().Find()
                    where (p.peekprofile_id == id && p.viewdate == null)
                    join f in db.GetRepository<profile>().Find() on p.profile_id equals f.id
                    where (f.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                    select f).Count();

                 // ?? operator example.


                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;

                 return defaultvalue;
             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

         
        }

        #endregion


        /// <summary>
        /// //gets all the members who are interested in me
        /// //TODO add filtering for blocked members that you blocked and system blocked
        /// </summary 
        public List<MemberSearchViewModel> getwhopeekedatme(string profileid, string page, string numberperpage)
        {

            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

               _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 var id = Convert.ToInt32(profileid);

                 var MyActiveblocks = from c in  db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate == null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                 //rematerialize on the back end.
                 //final query to send back only the profile datatas of the interests we want
                 var peeks = (from p in  db.GetRepository<peek>().Find().Where(p => p.profile_id == id && p.deletedbymemberdate == null)
                              join f in  db.GetRepository<profiledata>().Find() on p.peekprofile_id equals f.profile_id
                              join z in  db.GetRepository<profile>().Find() on p.peekprofile_id equals z.id
                              where (f.profile.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                              select new MemberSearchViewModel
                              {
                                  peekdate = p.creationdate,
                                  id = f.profile_id
                                  // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                              }).ToList();

                 int? pageint = Convert.ToInt32(page);
                 int? numberperpageint = Convert.ToInt32(numberperpage);

                 bool allowpaging = (peeks.Count >= (pageint * numberperpageint) ? true : false);
                 var pageData = pageint > 1 & allowpaging ?
                     new PaginatedList<MemberSearchViewModel>().GetCurrentPages(peeks, pageint ?? 1, numberperpageint ?? 4) : peeks.Take(numberperpageint.GetValueOrDefault());
                 //this.AddRange(pageData.ToList());
                 // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                 //return interests.ToList();
                   AnewluvContext AnewluvContext = new AnewluvContext();
                   using (var tempdb = AnewluvContext)
                   {
                       MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                       return MemberMapperService.mapmembersearchviewmodels(profileid, pageData.ToList(), "false").OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

                   }
             }
             catch (Exception ex)
             {

                new ErroLogging(logapplicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member actions service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }
             finally
             {
                // Api.DisposeMemberMapperService();
             }

         }

           
        }
        
        /// <summary>
        /// return all  new  Peeks as an object
        /// </summary>
        public List<MemberSearchViewModel> getwhopeekedatmenew(string profileid, string page, string numberperpage)
        {

            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

               _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 var id = Convert.ToInt32(profileid);

                 var MyActiveblocks = from c in  db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate == null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                 //rematerialize on the back end.
                 //final query to send back only the profile datatas of the interests we want
                 var WhoPeekedAtMe = (from p in  db.GetRepository<peek>().Find().Where(p => p.peekprofile_id == id && p.deletedbymemberdate == null)
                                      join f in  db.GetRepository<profiledata>().Find() on p.profile_id equals f.profile_id
                                      join z in  db.GetRepository<profile>().Find() on p.profile_id equals z.id
                                      where (f.profile.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                                      select new MemberSearchViewModel
                                      {
                                          peekdate = p.creationdate,
                                          id = f.profile_id
                                          // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                      }).ToList();

                 int? pageint = Convert.ToInt32(page);
                 int? numberperpageint = Convert.ToInt32(numberperpage);

                 bool allowpaging = (WhoPeekedAtMe.Count >= (pageint * numberperpageint) ? true : false);
                 var pageData = pageint > 1 & allowpaging ?
                     new PaginatedList<MemberSearchViewModel>().GetCurrentPages(WhoPeekedAtMe, pageint ?? 1, numberperpageint ?? 4) : WhoPeekedAtMe.Take(numberperpageint.GetValueOrDefault());
                 //this.AddRange(pageData.ToList());
                 // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                 //return interests.ToList();
                  List<MemberSearchViewModel> results;
                    AnewluvContext AnewluvContext  = new AnewluvContext();
                    using (var tempdb = AnewluvContext)
                    {
                        MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                        results = MemberMapperService.mapmembersearchviewmodels(profileid, pageData.ToList(), "false").OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                    }
                    return results;

             }
             catch (Exception ex)
             {

                new ErroLogging(logapplicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member actions service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }
             finally
             {
                // Api.DisposeMemberMapperService();
             }

         }

       
        }

        /// <summary>
        /// //gets list of all the profiles I Peeked at in
        /// </summary 
        public List<MemberSearchViewModel> getwhoipeekedat(string profileid, string page, string numberperpage)
        {

            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

              _unitOfWork.DisableProxyCreation = false;
         using (var db = _unitOfWork)
         {
             try
             {
                 var id = Convert.ToInt32(profileid);
                 // IEnumerable<MemberSearchViewModel> PeekNew = default(IEnumerable<MemberSearchViewModel>);

                 //gets all  interestets from the interest table based on if the person's profiles are stil lvalid tho


                 var MyActiveblocks = (from c in  db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate == null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      }).ToList();

                 //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                 //rematerialize on the back end.
                 //final query to send back only the profile datatas of the interests we want
                 var peeknew = (from f in  db.GetRepository<peek>().Find().Where(p => p.peekprofile_id == id )
                               // join f in  db.GetRepository<profiledata>().Find() on p.profile_id equals f.profile_id
                              //  join z in  db.GetRepository<profile>().Find() on p.profile_id equals z.id
                                where (f.profilemetadata1.profile.status_id < 3 && (MyActiveblocks !=null && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profilemetadata1.profile_id)))
                                select new MemberSearchViewModel
                                {
                                    peekdate = f.creationdate,
                                    id = f.profilemetadata1.profile_id //,
                                    // profiledata = f.profilemetadata1.profile.profiledata
                                    // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                }).ToList();


                 int? pageint = Convert.ToInt32(page);
                 int? numberperpageint = Convert.ToInt32(numberperpage);

                 bool allowpaging = (peeknew.Count >= (pageint * numberperpageint) ? true : false);
                 var pageData = pageint > 1 & allowpaging ?
                     new PaginatedList<MemberSearchViewModel>().GetCurrentPages(peeknew, pageint ?? 1, numberperpageint ?? 4) : peeknew.Take(numberperpageint.GetValueOrDefault());
                 //this.AddRange(pageData.ToList());
                 // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                 //return interests.ToList();
                 List<MemberSearchViewModel> results;
                 //AnewluvContext AnewluvContext = new AnewluvContext();
                 using (var tempdb = new AnewluvContext())
                 {
                     MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                     results = MemberMapperService.mapmembersearchviewmodels(profileid, pageData.ToList(), "false").OrderByDescending(f => f.peekdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                 }
                 return results;
                

             }
             catch (Exception ex)
             {

                new ErroLogging(logapplicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member actions service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }
             finally
             {
               //  Api.DisposeMemberMapperService();
             }


         }

         
        }




        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both peek 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public List<MemberSearchViewModel> getmutualpeeks(string profileid, string targetprofileid)
        {

                 _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {

                 IEnumerable<MemberSearchViewModel> mutualinterests = default(IEnumerable<MemberSearchViewModel>);
                 return mutualinterests.ToList();
                 
             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }


        }
        /// <summary>
        /// //checks if you already sent and peek to the target profile
        /// </summary        
        public bool checkpeek(string profileid, string targetprofileid)
        {

                 _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {

                 var id = Convert.ToInt32(profileid);
                 var targetid = Convert.ToInt32(targetprofileid);
                 return  db.GetRepository<peek>().Find().Any(r => r.profile_id == id && r.peekprofile_id == targetid);

             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

        

        }

        /// <summary>
        /// Adds a New peek
        /// </summary>
        public bool addpeek(string profileid, string targetprofileid)
        {
            //create new inetrest object
            peek peek = new peek();
            //make sure you are not trying to peek at yourself
            if (profileid == targetprofileid) return false;

        
            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(targetprofileid);

                        //check  peek first  
                        //if this was a peek being restored just do that part
                        var existingpeek = db.GetRepository<peek>().FindSingle(r => r.profile_id == id && r.peekprofile_id == targetid);

                        //just  update it if we have one already
                        if (existingpeek != null)
                        {
                            existingpeek.deletedbymemberdate = null; ;
                            existingpeek.modificationdate = DateTime.Now;
                            db.Update(existingpeek);

                        }
                        else
                        {
                            //peek = this. db.GetRepository<peek>().Find().Where(p => p.profileid == profileid).FirstOrDefault();
                            //update the profile status to 2
                            peek.profile_id = id;
                            peek.peekprofile_id = targetid;
                            peek.mutual = false;  // not dealing with this calulatin yet
                            peek.creationdate = DateTime.Now;
                            //handele the update using EF
                            // this. db.GetRepository<profile>().Find().AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                            db.Add(peek);

                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

        

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool removepeekbyprofileid(string profileid, string peekprofile_id)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(peekprofile_id);

                        var peek = db.GetRepository<peek>().Find().Where(p => p.profile_id == id && p.peekprofile_id == targetid).FirstOrDefault();
                        //update the profile status to 2

                        peek.deletedbymemberdate = DateTime.Now;
                        peek.modificationdate = DateTime.Now;
                        db.Update(peek);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

      

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool removepeekbypeekprofileid(string peekprofile_id, string profileid)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(peekprofile_id);

                        var peek = db.GetRepository<peek>().Find().Where(p => p.profile_id == targetid && p.peekprofile_id == id).FirstOrDefault();
                        //update the profile status to 2

                        peek.deletedbypeekdate = DateTime.Now;
                        peek.modificationdate = DateTime.Now;
                        db.Update(peek);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

      
        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool restorepeekbyprofileid(string profileid, string peekprofile_id)
        {
            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {


                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(peekprofile_id);

                        var peek = db.GetRepository<peek>().FindSingle(p => p.profile_id == id && p.peekprofile_id == targetid);
                        //update the profile status to 2

                        peek.deletedbymemberdate = null;
                        peek.modificationdate = DateTime.Now;

                        db.Update(peek);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


         

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool restorepeekbypeekprofileid(string peekprofile_id, string profileid)
        {
            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(peekprofile_id);

                        var peek = db.GetRepository<peek>().FindSingle(p => p.profile_id == targetid && p.peekprofile_id == id);
                        //update the profile status to 2
                        //update the profile status to 2


                        peek.deletedbypeekdate = null;
                        peek.modificationdate = DateTime.Now;

                        db.Update(peek);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

        

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removepeeksbyprofileidandscreennames(string profileid, List<String> screennames)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                              var id = Convert.ToInt32(profileid);
                        // peeks = this. db.GetRepository<peek>().Find().Where(p => p.profileid == profileid && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                        //update the profile status to 2
                        peek peek = new peek();
                        foreach (string value in screennames)
                        {

                           int? peekprofile_id = db.GetRepository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                           var  currentpeek = db.GetRepository<peek>().Find().Where(p => p.profile_id == id && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                           peek.deletedbymemberdate = null;
                            peek.modificationdate = DateTime.Now;
                            db.Update(currentpeek);
                           
                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

        }

        /// <summary>
        ///  //Removes a peek i.e makes is seem like you never peeeked at  anymore
        ///  //removed multiples 
        /// </summary 
        public bool restorepeeksbyprofileidandscreennames(string profileid, List<String> screennames)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var id = Convert.ToInt32(profileid);
                        // peeks = this. db.GetRepository<peek>().Find().Where(p => p.profileid == profileid && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                        //update the profile status to 2
                        peek peek = new peek();
                        foreach (string value in screennames)
                        {

                            int? peekprofile_id = db.GetRepository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                            var currentpeek = db.GetRepository<peek>().Find().Where(p => p.profile_id == id && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                            peek.deletedbymemberdate = null;
                            peek.modificationdate = DateTime.Now;
                            db.Update(currentpeek);

                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }
         
        }

        /// <summary>
        ///  Update peek with a view     
        /// </summary 
        public bool updatepeekviewstatus(string profileid, string targetprofileid)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(targetprofileid);

                        var peek = db.GetRepository<peek>().Find().Where(p => p.peekprofile_id == targetid && p.profile_id == id).FirstOrDefault();
                        //update the profile status to 2            
                        if (peek.viewdate == null)
                        {
                            peek.viewdate = DateTime.Now;
                            peek.modificationdate = DateTime.Now;
                            db.Update(peek);

                            int i = db.Commit();
                            transaction.Commit();
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

       
        }

        #endregion


        #endregion

        // MailboxLock methods , right now if you block user from mail they are blocked from your site on everything
        #region "block methods"


        //count methods first
        /// <summary>
        /// count all total blocks
        /// </summary>

        public int getwhoiblockedcount(string profileid)
        {

                _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {

                 int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 count = (
                   from f in  db.GetRepository<block>().Find()
                   where (f.profile_id == id && f.removedate == null)
                   select f).Count();

                 // ?? operator example.


                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;



                 return defaultvalue;

             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

       
        }

        /// <summary>
        /// return all    block as an object
        /// </summary>
        public List<MemberSearchViewModel> getwhoiblocked(string profileid, string page, string numberperpage)
        {

            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

                _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 var id = Convert.ToInt32(profileid);
                 var blocknew = (from p in  db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate == null)
                                 join f in  db.GetRepository<profiledata>().Find() on p.blockprofile_id equals f.profile_id
                                 join z in  db.GetRepository<profile>().Find() on p.blockprofile_id equals z.id
                                 where (f.profile.status_id < 3)
                                 orderby (p.creationdate) descending
                                 select new MemberSearchViewModel
                                 {
                                     blockdate = p.creationdate,
                                     id = f.profile_id
                                     // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                 }).ToList();

                 int? pageint = Convert.ToInt32(page);
                 int? numberperpageint = Convert.ToInt32(numberperpage);

                 bool allowpaging = (blocknew.Count >= (pageint * numberperpageint) ? true : false);
                 var pageData = pageint > 1 & allowpaging ?
                     new PaginatedList<MemberSearchViewModel>().GetCurrentPages(blocknew, pageint ?? 1, numberperpageint ?? 4) : blocknew.Take(numberperpageint.GetValueOrDefault());
                 //this.AddRange(pageData.ToList());
                 // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                 List<MemberSearchViewModel> results;
                 AnewluvContext AnewluvContext = new AnewluvContext();
                 using (var tempdb = AnewluvContext)
                 {
                     MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                     results = MemberMapperService.mapmembersearchviewmodels(profileid, pageData.ToList(), "false").OrderByDescending(f => f.blockdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                 }
                 return results;

             }
             catch (Exception ex)
             {

                new ErroLogging(logapplicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member actions service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }
             finally
             {
                // Api.DisposeMemberMapperService();
             }


         }

           
        }

        /// <summary>
        /// //gets all the members who areblocked in me
        /// </summary 
        public List<MemberSearchViewModel> getwhoblockedme(string profileid, string page, string numberperpage)
        {

            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

                _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 var id = Convert.ToInt32(profileid);

                 var whoblockedme = (from p in  db.GetRepository<block>().Find().Where(p => p.blockprofile_id == id && p.removedate == null)
                                     join f in  db.GetRepository<profiledata>().Find() on p.profile_id equals f.profile_id
                                     join z in  db.GetRepository<profile>().Find() on p.profile_id equals z.id
                                     where (f.profile.status_id < 3)
                                     orderby (p.creationdate) descending
                                     select new MemberSearchViewModel
                                     {
                                         blockdate = p.creationdate,
                                         id = f.profile_id
                                         // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                     }).ToList();

                 int? pageint = Convert.ToInt32(page);
                 int? numberperpageint = Convert.ToInt32(numberperpage);

                 bool allowpaging = (whoblockedme.Count >= (pageint * numberperpageint) ? true : false);
                 var pageData = pageint > 1 & allowpaging ?
                     new PaginatedList<MemberSearchViewModel>().GetCurrentPages(whoblockedme, pageint ?? 1, numberperpageint ?? 4) : whoblockedme.Take(numberperpageint.GetValueOrDefault());
                 //this.AddRange(pageData.ToList());
                 // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                 List<MemberSearchViewModel> results;
                 AnewluvContext AnewluvContext = new AnewluvContext();
                 using (var tempdb = AnewluvContext)
                 {
                     MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                     results = MemberMapperService.mapmembersearchviewmodels(profileid, pageData.ToList(), "false").OrderByDescending(f => f.blockdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                 }
                 return results;
             }
             catch (Exception ex)
             {

                new ErroLogging(logapplicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member actions service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }
             finally
             {
                // Api.DisposeMemberMapperService();
             }


         }

        }


        #region "update/check/reomve methods"

        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public List<MemberSearchViewModel> getmutualblocks(string profileid, string targetprofileid)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {


                        IEnumerable<MemberSearchViewModel> mutualblocks = default(IEnumerable<MemberSearchViewModel>);
                        return mutualblocks.ToList();


                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

        }
        /// <summary>
        /// //checks if you already sent and block to the target profile
        /// </summary        
        public bool checkblock(string profileid, string targetprofileid)
        {


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {


                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(targetprofileid);

                        return db.GetRepository<block>().Find().Any(r => r.profile_id == id && r.blockprofile_id == targetid);


                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

        }

        /// <summary>
        /// Adds a New block
        /// </summary>
        public bool addblock(string profileid, string targetprofileid)
        {
                //create new inetrest object
            block block = new block();
            //make sure you are not trying to block at yourself
            if (profileid == targetprofileid) return false;

           

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(targetprofileid);

                        //check  block first  
                        //if this was a block being restored just do that part
                        var existingblock = db.GetRepository<block>().FindSingle(r => r.profile_id == id && r.blockprofile_id == targetid);

                        //just  update it if we have one already
                        if (existingblock != null)
                        {
                            existingblock.removedate = null; ;
                            existingblock.modificationdate = DateTime.Now;
                            db.Update(existingblock);

                        }
                        else
                        {
                            //block = this. db.GetRepository<block>().Find().Where(p => p.profileid == profileid).FirstOrDefault();
                            //update the profile status to 2
                            block.profile_id = id;
                            block.blockprofile_id = targetid;
                             // not dealing with this calulatin yet                           
                            block.creationdate = DateTime.Now;
                            //handele the update using EF
                            // this. db.GetRepository<profile>().Find().AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                            db.Add(block);

                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;


                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

         

        }

        /// <summary>
        ///  //Removes an block i.e changes the block to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool removeblock(string profileid, string blockprofile_id)
        {
            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(blockprofile_id);

                        var block = db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.blockprofile_id == targetid).FirstOrDefault();
                        //update the profile status to 2

                        block.removedate = DateTime.Now;
                        block.modificationdate = DateTime.Now;
                        db.Update(block);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


        }

        /// <summary>
        ///  //Removes an block i.e changes the block to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool restoreblock(string profileid, string blockprofile_id)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(blockprofile_id);

                        var block = db.GetRepository<block>().FindSingle(p => p.profile_id == id && p.blockprofile_id == targetid);
                        //update the profile status to 2

                        block.removedate = null;
                        block.modificationdate = DateTime.Now;

                        db.Update(block);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

       
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removeblocksbyscreennames(string profileid, List<String> screennames)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        // blocks = this. db.GetRepository<block>().Find().Where(p => p.profileid == profileid && p.blockprofile_id == blockprofile_id).FirstOrDefault();
                        //update the profile status to 2
                        block block = new block();
                        foreach (string value in screennames)
                        {


                            int? blockprofile_id = db.GetRepository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                            var currentblock = db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.blockprofile_id == blockprofile_id).FirstOrDefault();
                            block.removedate = null;
                            block.modificationdate = DateTime.Now;
                            db.Update(currentblock);

                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

       

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restoreblocksbyscreennames(string profileid, List<String> screennames)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        // blocks = this. db.GetRepository<block>().Find().Where(p => p.profileid == profileid && p.blockprofile_id == blockprofile_id).FirstOrDefault();
                        //update the profile status to 2
                        block block = new block();
                        foreach (string value in screennames)
                        {
                            
                            int? blockprofile_id = db.GetRepository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                            var currentblock = db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.blockprofile_id == blockprofile_id).FirstOrDefault();
                            block.removedate = null;
                            block.modificationdate = DateTime.Now;
                            db.Update(currentblock);

                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


      

        }

        //TO DO this needs to me reviewed , all blocks need notes  if reviewed otherwise nothing
        /// <summary>
        ///  Update block with a view     
        /// </summary 
        /// 
        public bool updateblockreviewstatus(string profileid, string targetprofileid, string reviewerid)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                      

                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(targetprofileid);

                        var block = db.GetRepository<block>().Find().Where(p => p.blockprofile_id == targetid && p.profile_id == id).FirstOrDefault();
                        //update the profile status to 2            
                     
                            block.modificationdate = DateTime.Now;
                          
                            db.Update(block);

                            int i = db.Commit();
                            transaction.Commit();
                        
                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

         
        }
        #endregion

        #endregion

        #region "Like methods"

        //count methods first

        #region "Count methods"

        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
        public int getwhoilikecount(string profileid)
        {

                _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 count = (
            from f in db.GetRepository<like>().Find()
            where (f.profile_id == id && f.deletedbymemberdate == null)
            select f).Count();
                 // ?? operator example.
                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;
                 return defaultvalue;



             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

        }

        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
        public int getwholikesmecount(string profileid)
        {

                _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {

                 int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 //filter out blocked profiles 
                 var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate != null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 count = (
                    from p in db.GetRepository<like>().Find()
                    where (p.likeprofile_id == id)
                    join f in db.GetRepository<profile>().Find() on p.profile_id equals f.id
                    where (f.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                    select f).Count();

                 // ?? operator example.


                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;

                 return defaultvalue;

             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

          
        }

        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
        public int getwhoislikesmenewcount(string profileid)
        {

                _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 int? count = null;
                 int defaultvalue = 0;
                 var id = Convert.ToInt32(profileid);

                 //filter out blocked profiles 
                 var MyActiveblocks = from c in db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate != null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 count = (
                    from p in db.GetRepository<like>().Find()
                    where (p.likeprofile_id == id && p.viewdate == null)
                    join f in db.GetRepository<profile>().Find() on p.profile_id equals f.id
                    where (f.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.id)) //filter out banned profiles or deleted profiles            
                    select f).Count();

                 // ?? operator example.


                 // y = x, unless x is null, in which case y = -1.
                 defaultvalue = count ?? 0;

                 return defaultvalue;


             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

       
        }

        #endregion



        /// <summary>
        /// return all  new  likes as an object
        /// </summary>
        public List<MemberSearchViewModel> getwholikesmenew(string profileid, string page, string numberperpage)
        {
            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

                _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 var id = Convert.ToInt32(profileid);
                 
                 var MyActiveblocks = (from c in  db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate == null)
                                       select new
                                       {
                                           ProfilesBlockedId = c.blockprofile_id
                                       }).ToList();

                 //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                 //rematerialize on the back end.
                 //final query to send back only the profile datatas of the interests we want
                 var likenew = (from p in  db.GetRepository<like>().Find().Where(p => p.likeprofile_id == id && p.viewdate == null).ToList()
                                join f in  db.GetRepository<profiledata>().Find() on p.profile_id equals f.profile_id
                                join z in  db.GetRepository<profile>().Find() on p.profile_id equals z.id
                                where (f.profile.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                                orderby (p.creationdate) descending
                                select new MemberSearchViewModel
                                {
                                    likedate = p.creationdate,
                                    id = f.profile_id
                                    // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                }).ToList();


                 int? pageint = Convert.ToInt32(page);
                 int? numberperpageint = Convert.ToInt32(numberperpage);

                 bool allowpaging = (likenew.Count >= (pageint * numberperpageint) ? true : false);
                 var pageData = pageint > 1 & allowpaging ?
                     new PaginatedList<MemberSearchViewModel>().GetCurrentPages(likenew, pageint ?? 1, numberperpageint ?? 4) : likenew.Take(numberperpageint.GetValueOrDefault());
                 //this.AddRange(pageData.ToList());
                 // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                 //return interests.ToList();
                 List<MemberSearchViewModel> results;
                 AnewluvContext AnewluvContext = new AnewluvContext();
                 using (var tempdb = AnewluvContext)
                 {
                     MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                     results = MemberMapperService.mapmembersearchviewmodels(profileid, pageData.ToList(), "false").OrderByDescending(f => f.likedate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                 }
                 return results;

             }
             catch (Exception ex)
             {

                new ErroLogging(logapplicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member actions service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }
             finally
             {
                // Api.DisposeMemberMapperService();
             }


         }

     
        }

        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public List<MemberSearchViewModel> getwholikesme(string profileid, string page, string numberperpage)
        {

            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

                _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 var id = Convert.ToInt32(profileid);

                 var MyActiveblocks = from c in  db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate != null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                 //rematerialize on the back end.
                 //final query to send back only the profile datatas of the interests we want
                 var wholikesme = (from p in  db.GetRepository<like>().Find().Where(p => p.likeprofile_id == id && p.deletedbylikedate == null)
                                   join f in  db.GetRepository<profiledata>().Find() on p.profile_id equals f.profile_id
                                   join z in  db.GetRepository<profile>().Find() on p.profile_id equals z.id
                                   where (f.profile.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                                   orderby (p.creationdate) descending
                                   select new MemberSearchViewModel
                                   {
                                       likedate = p.creationdate,
                                       id = f.profile_id
                                       // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                   }).ToList();

                 int? pageint = Convert.ToInt32(page);
                 int? numberperpageint = Convert.ToInt32(numberperpage);

                 bool allowpaging = (wholikesme.Count >= (pageint * numberperpageint) ? true : false);
                 var pageData = pageint > 1 & allowpaging ?
                     new PaginatedList<MemberSearchViewModel>().GetCurrentPages(wholikesme, pageint ?? 1, numberperpageint ?? 4) : wholikesme.Take(numberperpageint.GetValueOrDefault());
                 //this.AddRange(pageData.ToList());
                 // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                 //return interests.ToList();
                 List<MemberSearchViewModel> results;
                 AnewluvContext AnewluvContext = new AnewluvContext();
                 using (var tempdb = AnewluvContext)
                 {
                     MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                     results = MemberMapperService.mapmembersearchviewmodels(profileid, pageData.ToList(), "false").OrderByDescending(f => f.likedate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                 }
                 return results;

             }
             catch (Exception ex)
             {

                new ErroLogging(logapplicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member actions service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }
             finally
             {
                // Api.DisposeMemberMapperService();
             }


         }

     
        }


        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public List<MemberSearchViewModel> getwhoilike(string profileid, string page, string numberperpage)
        {
            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

                _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 var id = Convert.ToInt32(profileid);

                 var MyActiveblocks = from c in  db.GetRepository<block>().Find().Where(p => p.profile_id == id && p.removedate != null)
                                      select new
                                      {
                                          ProfilesBlockedId = c.blockprofile_id
                                      };

                 //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                 //rematerialize on the back end.
                 //final query to send back only the profile datatas of the interests we want
                 var whoilike = (from p in  db.GetRepository<like>().Find().Where(p => p.profile_id == id && p.deletedbymemberdate == null)
                                 join f in  db.GetRepository<profiledata>().Find() on p.likeprofile_id equals f.profile_id
                                 join z in  db.GetRepository<profile>().Find() on p.likeprofile_id equals z.id
                                 where (f.profile.status_id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                                 orderby (p.creationdate) descending
                                 select new MemberSearchViewModel
                                 {
                                     likedate = p.creationdate,
                                     id = f.profile_id
                                     // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                 }).ToList();

                 int? pageint = Convert.ToInt32(page);
                 int? numberperpageint = Convert.ToInt32(numberperpage);

                 bool allowpaging = (whoilike.Count >= (pageint * numberperpageint) ? true : false);
                 var pageData = pageint > 1 & allowpaging ?
                     new PaginatedList<MemberSearchViewModel>().GetCurrentPages(whoilike, pageint ?? 1, numberperpageint ?? 4) : whoilike.Take(numberperpageint.GetValueOrDefault());
                 //this.AddRange(pageData.ToList());
                 // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                 //return interests.ToList();
                 List<MemberSearchViewModel> results;
                 AnewluvContext AnewluvContext = new AnewluvContext();
                 using (var tempdb = AnewluvContext)
                 {
                     MembersMapperService MemberMapperService = new MembersMapperService(tempdb);
                     results = MemberMapperService.mapmembersearchviewmodels(profileid, pageData.ToList(), "false").OrderByDescending(f => f.likedate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                 }
                 return results;

             }
             catch (Exception ex)
             {

                new ErroLogging(logapplicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member actions service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }
             finally
             {
                // Api.DisposeMemberMapperService();
             }


         }

      
        }


        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public List<MemberSearchViewModel> getmutuallikes(string profileid, string targetprofileid)
        {
                _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                 IEnumerable<MemberSearchViewModel> mutuallikes = default(IEnumerable<MemberSearchViewModel>);
                 return mutuallikes.ToList();


             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

        

        }
        /// <summary>
        /// //checks if you already sent and like to the target profile
        /// </summary        
        public bool checklike(string profileid, string targetprofileid)
        {

                _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {

                 var id = Convert.ToInt32(profileid);
                 var targetid = Convert.ToInt32(targetprofileid);

                 return db.GetRepository<like>().Find().Any(r => r.profile_id == id && r.likeprofile_id == targetid);


             }
             catch (Exception ex)
             {

                   new ErroLogging(logapplicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }

         }

        
        }

        /// <summary>
        /// Adds a New like
        /// </summary>
        public bool addlike(string profileid, string targetprofileid)
        {

            //create new inetrest object
            like like = new like();
            //make sure you are not trying to like at yourself
            if (profileid == targetprofileid) return false;


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(targetprofileid);

                        //check  like first  
                        //if this was a like being restored just do that part
                        var existinglike = db.GetRepository<like>().FindSingle(r => r.profile_id == id && r.likeprofile_id == targetid);

                        //just  update it if we have one already
                        if (existinglike != null)
                        {
                            existinglike.deletedbymemberdate = null; ;
                            existinglike.modificationdate = DateTime.Now;
                            db.Update(existinglike);

                        }
                        else
                        {
                            //like = this. db.GetRepository<like>().Find().Where(p => p.profileid == profileid).FirstOrDefault();
                            //update the profile status to 2
                            like.profile_id = id;
                            like.likeprofile_id = targetid;
                            like.mutual = false;  // not dealing with this calulatin yet
                            like.creationdate = DateTime.Now;
                            //handele the update using EF
                            // this. db.GetRepository<profile>().Find().AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                            db.Add(like);

                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


 

        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool removelikebyprofileid(string profileid, string likeprofile_id)
        {


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(likeprofile_id);

                        var like = db.GetRepository<like>().Find().Where(p => p.profile_id == id && p.likeprofile_id == targetid).FirstOrDefault();
                        //update the profile status to 2

                        like.deletedbymemberdate = DateTime.Now;
                        like.modificationdate = DateTime.Now;
                        db.Update(like);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool removelikebylikeprofileid(string likeprofile_id, string profileid)
        {


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(likeprofile_id);

                        var like = db.GetRepository<like>().Find().Where(p => p.profile_id == targetid && p.likeprofile_id == id).FirstOrDefault();
                        //update the profile status to 2

                        like.deletedbylikedate = DateTime.Now;
                        like.modificationdate = DateTime.Now;
                        db.Update(like);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;


                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool restorelikebyprofileid(string profileid, string likeprofile_id)
        {


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(likeprofile_id);

                        var like = db.GetRepository<like>().FindSingle(p => p.profile_id == id && p.likeprofile_id == targetid);
                        //update the profile status to 2

                        like.deletedbymemberdate = null;
                        like.modificationdate = DateTime.Now;

                        db.Update(like);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


          
        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool restorelikebylikeprofileid(string likeprofile_id, string profileid)
        {

            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(likeprofile_id);

                        var like = db.GetRepository<like>().FindSingle(p => p.profile_id == targetid && p.likeprofile_id == id);
                        //update the profile status to 2
                        //update the profile status to 2


                        like.deletedbylikedate = null;
                        like.modificationdate = DateTime.Now;

                        db.Update(like);

                        int i = db.Commit();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }




        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removelikesbyprofileidandscreennames(string profileid, List<String> screennames)
        {


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        // likes = this. db.GetRepository<like>().Find().Where(p => p.profileid == profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                        //update the profile status to 2
                        like like = new like();
                        foreach (string value in screennames)
                        {

                            int? likeprofile_id = db.GetRepository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                            var currentlike = db.GetRepository<like>().Find().Where(p => p.profile_id == id && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                            like.deletedbymemberdate = null;
                            like.modificationdate = DateTime.Now;
                            db.Update(currentlike);

                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


        
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restorelikesbyprofileidandscreennames(string profileid, List<String> screennames)
        {


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        // likes = this. db.GetRepository<like>().Find().Where(p => p.profileid == profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                        //update the profile status to 2
                        like like = new like();
                        foreach (string value in screennames)
                        {

                            int? likeprofile_id = db.GetRepository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                            var currentlike = db.GetRepository<like>().Find().Where(p => p.profile_id == id && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                            like.deletedbymemberdate = null;
                            like.modificationdate = DateTime.Now;
                            db.Update(currentlike);

                        }

                        int i = db.Commit();
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }


          
        }

        /// <summary>
        ///  Update like with a view     
        /// </summary 
        public bool updatelikeviewstatus(string profileid, string targetprofileid)
        {


            //update method code
            using (var db = _unitOfWork)
            {
                db.IsAuditEnabled = false; //do not audit on adds
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var id = Convert.ToInt32(profileid);
                        var targetid = Convert.ToInt32(targetprofileid);

                        var like = db.GetRepository<like>().Find().Where(p => p.likeprofile_id == targetid && p.profile_id == id).FirstOrDefault();
                        //update the profile status to 2            
                        if (like.viewdate == null)
                        {
                            like.viewdate = DateTime.Now;
                            like.modificationdate = DateTime.Now;
                            db.Update(like);

                            int i = db.Commit();
                            transaction.Commit();
                        }
                        return true;
              
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                       new ErroLogging(logapplicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(profileid));
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in member actions service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                    }

                }
            }

          

        }

        #endregion


        #endregion

        #region "Search methods"



        #endregion


    }
}
