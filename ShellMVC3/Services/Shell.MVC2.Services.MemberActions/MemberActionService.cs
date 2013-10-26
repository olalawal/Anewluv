using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Services.Contracts;

using System.Web;
using System.Net;

using Shell.MVC2.Interfaces;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using System.ServiceModel.Activation;
using Anewluv.DataAccess.Interfaces;
using LoggingLibrary;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;

namespace Shell.MVC2.Services.Actions
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

                    //   return _memberactionsrepository.getmyrelationshipsfiltered(Convert.ToInt32(profileid), types);
                    return null;

                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
        }

        #region "Private methods used internally to this repo"
        private IQueryable<block> activeblocksbyprofileid(int profileid)
        {
            //_unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {
                try
                {
                    IRepository<block> repo = db.GetRepository<block>();
                    //filter out blocked profiles 
                    return repo.Find().OfType<block>().Where(p => p.profile_id == profileid && p.removedate != null);
                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                    //logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                    //log error mesasge
                    //handle logging here
                    var message = ex.Message;
                    throw;
                }
            }
        }
        private List<MemberSearchViewModel> getunpagedwhoisinterestedinme(int profileid, IQueryable<block> MyActiveblocks)
        {



            try
            {
                //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                //rematerialize on the back end.
                //final query to send back only the profile datatas of the interests we want
                return (from p in _datingcontext.interests.Where(p => p.interestprofile_id == profileid & p.deletedbymemberdate == null)
                        join f in _datingcontext.profiledata on p.profile_id equals f.profile_id
                        join z in _datingcontext.profiles on p.profile_id equals z.id
                        where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
                        select new MemberSearchViewModel
                        {
                            interestdate = p.creationdate,
                            id = f.profile_id
                            // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                        }).ToList();

            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;
            }

        }
        private List<MemberSearchViewModel> getunpagedwhoiaminsterestedin(int profileid, IQueryable<block> MyActiveblocks)
        {


            try
            {
                //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                //rematerialize on the back end.
                //final query to send back only the profile datatas of the interests we want
                return (from p in _datingcontext.interests.Where(p => p.profile_id == profileid & p.deletedbymemberdate == null)
                        join f in _datingcontext.profiledata on p.interestprofile_id equals f.profile_id
                        join z in _datingcontext.profiles on p.interestprofile_id equals z.id
                        where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
                        select new MemberSearchViewModel
                        {
                            interestdate = p.creationdate,
                            id = f.profile_id
                            // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                        }).ToList();
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;
            }


        }
        private List<MemberSearchViewModel> getunpagedwhopeekedatme(int profileid, IQueryable<block> MyActiveblocks)
        {


            try
            {
                //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                //rematerialize on the back end.
                //final query to send back only the profile datatas of the interests we want
                return (from p in _datingcontext.peeks.Where(p => p.peekprofile_id == profileid && p.deletedbymemberdate == null)
                        join f in _datingcontext.profiledata on p.profile_id equals f.profile_id
                        join z in _datingcontext.profiles on p.profile_id equals z.id
                        where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
                        select new MemberSearchViewModel
                        {
                            peekdate = p.creationdate,
                            id = f.profile_id
                            // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                        }).ToList();
            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;
            }



        }
        private List<MemberSearchViewModel> getunpagedwhoipeekedat(int profileid, IQueryable<block> MyActiveblocks)
        {

            try
            {

                //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                //rematerialize on the back end.
                //final query to send back only the profile datatas of the interests we want
                return (from p in _datingcontext.peeks.Where(p => p.profile_id == profileid && p.deletedbymemberdate == null)
                        join f in _datingcontext.profiledata on p.peekprofile_id equals f.profile_id
                        join z in _datingcontext.profiles on p.peekprofile_id equals z.id
                        where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
                        select new MemberSearchViewModel
                        {
                            peekdate = p.creationdate,
                            id = f.profile_id
                        }).ToList();


            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;
            }


        }
        private List<MemberSearchViewModel> getunpagedblocks(int profileid)
        {

            try
            {


                return (from p in _datingcontext.blocks.Where(p => p.profile_id == profileid && p.removedate == null)
                        join f in _datingcontext.profiledata on p.blockprofile_id equals f.profile_id
                        join z in _datingcontext.profiles on p.blockprofile_id equals z.id
                        where (f.profile.status.id < 3)
                        orderby (p.creationdate) descending
                        select new MemberSearchViewModel
                        {
                            blockdate = p.creationdate,
                            id = f.profile_id
                        }).ToList();


            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;
            }

        }
        private List<MemberSearchViewModel> getunpagedwholikesme(int profileid, IQueryable<block> MyActiveblocks)
        {

            try
            {


                //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                //rematerialize on the back end.
                //final query to send back only the profile datatas of the interests we want
                return (from p in _datingcontext.likes.Where(p => p.likeprofile_id == profileid && p.deletedbylikedate == null)
                        join f in _datingcontext.profiledata on p.profile_id equals f.profile_id
                        join z in _datingcontext.profiles on p.profile_id equals z.id
                        where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
                        orderby (p.creationdate) descending
                        select new MemberSearchViewModel
                        {
                            likedate = p.creationdate,
                            id = f.profile_id
                        }).ToList();


            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;
            }


        }
        private List<MemberSearchViewModel> getunpagedwhoilike(int profileid, IQueryable<block> MyActiveblocks)
        {

            try
            {


                //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                //rematerialize on the back end.
                //final query to send back only the profile datatas of the interests we want
                return (from p in _datingcontext.likes.Where(p => p.profile_id == profileid && p.deletedbymemberdate == null)
                        join f in _datingcontext.profiledata on p.likeprofile_id equals f.profile_id
                        join z in _datingcontext.profiles on p.likeprofile_id equals z.id
                        where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
                        orderby (p.creationdate) descending
                        select new MemberSearchViewModel
                        {
                            likedate = p.creationdate,
                            id = f.profile_id
                        }).ToList();


            }
            catch (Exception ex)
            {
                //instantiate logger here so it does not break anything else.
                new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
                //log error mesasge
                //handle logging here
                var message = ex.Message;
                throw;
            }


        }

        //MAke an anon method to handle this paging and apply to all paged methods 
        //private int setpaging (MemberSearchViewModel models,int? page,int? numberperpage)
        //{

        //    bool allowpaging = (models.Count >= (page.GetValueOrDefault()  * numberperpage.GetValueOrDefault())) ? true : false);
        //    var pageData = page.GetValueOrDefault() > 1 & allowpaging ?

        //    new PaginatedList<MemberSearchViewModel>().GetCurrentPages(models, page ?? 1, numberperpage ?? 4) : models.Take(numberperpage.GetValueOrDefault());

        //}
        #endregion

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

            try
            {
                return _memberactionsrepository.getwhoiaminterestedincount(Convert.ToInt32(profileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public int getwhoisinterestedinmecount(string profileid)
        {

            try
            {
                return _memberactionsrepository.getwhoisinterestedinmecount(Convert.ToInt32(profileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        //count methods first
        /// <summary>
        /// count all total interests
        /// </summary>       
        public int getwhoisinterestedinmenewcount(string profileid)
        {

            try
            {
                return _memberactionsrepository.getwhoisinterestedinmenewcount(Convert.ToInt32(profileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
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
                                    .Where(p => p.profileme)
                                     join f in _datingcontext.profiledata on p.interestprofile_id equals f.profile_id
                                     join z in _datingcontext.profiles on p.interestprofile_id equals z.id
                                     where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profile_id))
                                     select new MemberSearchViewModel
                                     {
                                         interestdate = p.creationdate,
                                         id = f.profile_id
                                         // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                     }).ToList();
                    // var dd2 = 0;
                    //var dd = 2 /  dd2;

                    bool allowpaging = (interests.Count >= (Page.GetValueOrDefault() * NumberPerPage.GetValueOrDefault()) ? true : false);
                    var pageData = Page.GetValueOrDefault() > 1 & allowpaging ?
                        new PaginatedList<MemberSearchViewModel>().GetCurrentPages(interests, Page ?? 1, NumberPerPage ?? 4) : interests.Take(NumberPerPage.GetValueOrDefault());
                    //this.AddRange(pageData.ToList());
                    // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                    //return interests.ToList();
                    return _membermapperrepository.mapmembersearchviewmodels(profileid, pageData.ToList(), false).OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

                    // return data2.OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();;
                    //.OrderByDescending(f => f.interestdate ?? DateTime.MaxValue).ToList();

                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    new ErroLogging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(profileid), null);
                    //logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, profileid, null);
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

            try
            {

                return _memberactionsrepository.getwhoisinterestedinme(Convert.ToInt32(profileid), Convert.ToInt32(page), Convert.ToInt32(numberperpage));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        /// //gets all the members who are interested in me, that ive not viewd yet
        /// </summary 
        public List<MemberSearchViewModel> getwhoisinterestedinmenew(string profileid, string page, string numberperpage)
        {
            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

            try
            {
                return _memberactionsrepository.getwhoisinterestedinmenew(Convert.ToInt32(profileid), Convert.ToInt32(page), Convert.ToInt32(numberperpage)); ;

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
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

            try
            {
                return _memberactionsrepository.getmutualinterests(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }
        /// <summary>
        /// //checks if you already sent and interest to the target profile
        /// </summary        
        public bool checkinterest(string profileid, string targetprofileid)
        {

            try
            {
                return _memberactionsrepository.checkinterest(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        /// Adds a New interest
        /// </summary>
        public bool addinterest(string profileid, string targetprofileid)
        {



            try
            {
                return _memberactionsrepository.addinterest(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }


        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool removeinterestbyprofileid(string profileid, string interestprofile_id)
        {

            try
            {
                return _memberactionsrepository.removeinterestbyinterestprofileid(Convert.ToInt32(profileid), Convert.ToInt32(interestprofile_id));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool removeinterestbyinterestprofileid(string interestprofile_id, string profileid)
        {

            try
            {

                return _memberactionsrepository.removeinterestbyinterestprofileid(Convert.ToInt32(interestprofile_id), Convert.ToInt32(profileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool restoreinterestbyprofileid(string profileid, string interestprofile_id)
        {


            try
            {
                return _memberactionsrepository.restoreinterestbyprofileid(Convert.ToInt32(profileid), Convert.ToInt32(interestprofile_id));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public bool restoreinterestbyinterestprofileid(string interestprofile_id, string profileid)
        {

            try
            {
                return _memberactionsrepository.restoreinterestbyinterestprofileid(Convert.ToInt32(interestprofile_id), Convert.ToInt32(profileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removeinterestsbyprofileidandscreennames(string profileid, List<String> screennames)
        {

            try
            {
                return _memberactionsrepository.removeinterestsbyprofileidandscreennames(Convert.ToInt32(profileid), screennames);

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restoreinterestsbyprofileidandscreennames(string profileid, List<String> screennames)
        {



            try
            {
                return _memberactionsrepository.restoreinterestsbyprofileidandscreennames(Convert.ToInt32(profileid), screennames);

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        ///  Update interest with a view     
        /// </summary 
        public bool updateinterestviewstatus(string profileid, string targetprofileid)
        {


            try
            {
                return _memberactionsrepository.updatelikeviewstatus(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
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

            try
            {
                return _memberactionsrepository.getwhoipeekedatcount(Convert.ToInt32(profileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public int getwhopeekedatmecount(string profileid)
        {

            try
            {
                return _memberactionsrepository.getwhopeekedatmecount(Convert.ToInt32(profileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public int getwhopeekedatmenewcount(string profileid)
        {

            try
            {
                return _memberactionsrepository.getwhopeekedatmenewcount(Convert.ToInt32(profileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
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

            try
            {
                return _memberactionsrepository.getwhopeekedatme(Convert.ToInt32(profileid), Convert.ToInt32(page), Convert.ToInt32(numberperpage));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }



        /// <summary>
        /// return all  new  Peeks as an object
        /// </summary>
        public List<MemberSearchViewModel> getwhopeekedatmenew(string profileid, string page, string numberperpage)
        {

            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

            try
            {
                return _memberactionsrepository.getwhopeekedatmenew(Convert.ToInt32(profileid), Convert.ToInt32(page), Convert.ToInt32(numberperpage));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }



        /// <summary>
        /// //gets list of all the profiles I Peeked at in
        /// </summary 
        public List<MemberSearchViewModel> getwhoipeekedat(string profileid, string page, string numberperpage)
        {

            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

            try
            {
                return _memberactionsrepository.getwhoipeekedat(Convert.ToInt32(profileid), Convert.ToInt32(page), Convert.ToInt32(numberperpage));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
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

            try
            {
                return _memberactionsrepository.getmutualpeeks(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }
        /// <summary>
        /// //checks if you already sent and peek to the target profile
        /// </summary        
        public bool checkpeek(string profileid, string targetprofileid)
        {

            try
            {
                return _memberactionsrepository.checkpeek(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        /// Adds a New peek
        /// </summary>
        public bool addpeek(string profileid, string targetprofileid)
        {


            try
            {
                return _memberactionsrepository.addpeek(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool removepeekbyprofileid(string profileid, string peekprofile_id)
        {


            try
            {
                return _memberactionsrepository.removepeekbypeekprofileid(Convert.ToInt32(profileid), Convert.ToInt32(peekprofile_id));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool removepeekbypeekprofileid(string peekprofile_id, string profileid)
        {


            try
            {
                return _memberactionsrepository.removepeekbypeekprofileid(Convert.ToInt32(peekprofile_id), Convert.ToInt32(profileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool restorepeekbyprofileid(string profileid, string peekprofile_id)
        {



            try
            {
                return _memberactionsrepository.restorepeekbypeekprofileid(Convert.ToInt32(profileid), Convert.ToInt32(peekprofile_id));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        ///  //Removes an peek i.e changes the peek to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can peek u
        ///  //not inmplemented
        /// </summary 
        public bool restorepeekbypeekprofileid(string peekprofile_id, string profileid)
        {

            try
            {
                return _memberactionsrepository.restorepeekbypeekprofileid(Convert.ToInt32(peekprofile_id), Convert.ToInt32(profileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removepeeksbyprofileidandscreennames(string profileid, List<String> screennames)
        {

            try
            {
                return _memberactionsrepository.removepeeksbyprofileidandscreennames(Convert.ToInt32(profileid), screennames);

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        ///  //Removes a peek i.e makes is seem like you never peeeked at  anymore
        ///  //removed multiples 
        /// </summary 
        public bool restorepeeksbyprofileidandscreennames(string profileid, List<String> screennames)
        {


            try
            {
                return _memberactionsrepository.restorepeeksbyprofileidandscreennames(Convert.ToInt32(profileid), screennames);

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        ///  Update peek with a view     
        /// </summary 
        public bool updatepeekviewstatus(string profileid, string targetprofileid)
        {


            try
            {
                return _memberactionsrepository.updatepeekviewstatus(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
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


            try
            {
                return _memberactionsrepository.getwhoiblockedcount(Convert.ToInt32(profileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        /// return all    block as an object
        /// </summary>
        public List<MemberSearchViewModel> getwhoiblocked(string profileid, string page, string numberperpage)
        {

            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";


            try
            {
                return _memberactionsrepository.getwhoiblocked(Convert.ToInt32(profileid), Convert.ToInt32(page), Convert.ToInt32(numberperpage));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        /// //gets all the members who areblocked in me
        /// </summary 
        public List<MemberSearchViewModel> getwhoblockedme(string profileid, string page, string numberperpage)
        {

            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

            try
            {

                return _memberactionsrepository.getwhoblockedme(Convert.ToInt32(profileid), Convert.ToInt32(page), Convert.ToInt32(numberperpage));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
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

            try
            {
                return _memberactionsrepository.getmutualblocks(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }
        /// <summary>
        /// //checks if you already sent and block to the target profile
        /// </summary        
        public bool checkblock(string profileid, string targetprofileid)
        {
            try
            {
                return _memberactionsrepository.checkblock(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));


            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        /// Adds a New block
        /// </summary>
        public bool addblock(string profileid, string targetprofileid)
        {


            try
            {
                return _memberactionsrepository.addblock(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        ///  //Removes an block i.e changes the block to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool removeblock(string profileid, string blockprofile_id)
        {

            try
            {
                return _memberactionsrepository.removeblock(Convert.ToInt32(profileid), Convert.ToInt32(blockprofile_id));


            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        ///  //Removes an block i.e changes the block to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can block u
        ///  //not inmplemented
        /// </summary 
        public bool restoreblock(string profileid, string blockprofile_id)
        {
            try
            {
                return _memberactionsrepository.restoreblock(Convert.ToInt32(profileid), Convert.ToInt32(blockprofile_id));


            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removeblocksbyscreennames(string profileid, List<String> screennames)
        {


            try
            {
                return _memberactionsrepository.removeblocksbyscreennames(Convert.ToInt32(profileid), screennames);

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restoreblocksbyscreennames(string profileid, List<String> screennames)
        {



            try
            {
                return _memberactionsrepository.restoreblocksbyscreennames(Convert.ToInt32(profileid), screennames);

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        //TO DO this needs to me reviewed , all blocks need notes  if reviewed otherwise nothing
        /// <summary>
        ///  Update block with a view     
        /// </summary 
        /// 
        public bool updateblockreviewstatus(string profileid, string targetprofileid, string reviewerid)
        {

            try
            {
                return _memberactionsrepository.updateblockreviewstatus(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid), Convert.ToInt32(reviewerid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
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

            try
            {
                return _memberactionsrepository.getwhoilikecount(Convert.ToInt32(profileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
        public int getwholikesmecount(string profileid)
        {
            try
            {

                return _memberactionsrepository.getwholikesmecount(Convert.ToInt32(profileid));


            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        //count methods first
        /// <summary>
        /// count all total likes
        /// </summary>       
        public int getwhoislikesmenewcount(string profileid)
        {
            try
            {
                return _memberactionsrepository.getwholikesmenewcount(Convert.ToInt32(profileid));


            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
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


            try
            {
                return _memberactionsrepository.getwholikesmenew(Convert.ToInt32(profileid), Convert.ToInt32(page), Convert.ToInt32(numberperpage));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public List<MemberSearchViewModel> getwholikesme(string profileid, string page, string numberperpage)
        {

            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

            try
            {
                return _memberactionsrepository.getwholikesme(Convert.ToInt32(profileid), Convert.ToInt32(page), Convert.ToInt32(numberperpage));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }


        /// <summary>
        /// //gets all the members who Like Me
        /// </summary 
        public List<MemberSearchViewModel> getwhoilike(string profileid, string page, string numberperpage)
        {
            if (page == "" | page == "0") page = "1";
            if (numberperpage == "" | numberperpage == "0") numberperpage = "4";

            try
            {
                return _memberactionsrepository.getwhoilike(Convert.ToInt32(profileid), Convert.ToInt32(page), Convert.ToInt32(numberperpage));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
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


            try
            {
                return _memberactionsrepository.getmutuallikes(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));
            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }
        /// <summary>
        /// //checks if you already sent and like to the target profile
        /// </summary        
        public bool checklike(string profileid, string targetprofileid)
        {


            try
            {
                return _memberactionsrepository.checklike(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));
            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        /// Adds a New like
        /// </summary>
        public bool addlike(string profileid, string targetprofileid)
        {




            try
            {
                return _memberactionsrepository.addlike(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));
            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }


        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool removelikebyprofileid(string profileid, string likeprofile_id)
        {



            try
            {
                return _memberactionsrepository.removelikebylikeprofileid(Convert.ToInt32(profileid), Convert.ToInt32(likeprofile_id));
            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool removelikebylikeprofileid(string likeprofile_id, string profileid)
        {




            try
            {
                return _memberactionsrepository.removelikebylikeprofileid(Convert.ToInt32(likeprofile_id), Convert.ToInt32(profileid));
            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool restorelikebyprofileid(string profileid, string likeprofile_id)
        {


            try
            {
                return _memberactionsrepository.restorelikebyprofileid(Convert.ToInt32(profileid), Convert.ToInt32(likeprofile_id));
            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        ///  //Removes an like i.e changes the like to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can like u
        ///  //not inmplemented
        /// </summary 
        public bool restorelikebylikeprofileid(string likeprofile_id, string profileid)
        {




            try
            {
                return _memberactionsrepository.restorelikebylikeprofileid(Convert.ToInt32(likeprofile_id), Convert.ToInt32(profileid));
            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool removelikesbyprofileidandscreennames(string profileid, List<String> screennames)
        {


            try
            {
                return _memberactionsrepository.removelikesbyprofileidandscreennames(Convert.ToInt32(profileid), screennames);
            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
        public bool restorelikesbyprofileidandscreennames(string profileid, List<String> screennames)
        {



            try
            {
                return _memberactionsrepository.restorelikesbyprofileidandscreennames(Convert.ToInt32(profileid), screennames);

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }

        /// <summary>
        ///  Update like with a view     
        /// </summary 
        public bool updatelikeviewstatus(string profileid, string targetprofileid)
        {

            try
            {
                return _memberactionsrepository.updatelikeviewstatus(Convert.ToInt32(profileid), Convert.ToInt32(targetprofileid));

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in member actions service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }

        }

        #endregion


        #endregion

        #region "Search methods"



        #endregion


    }
}
