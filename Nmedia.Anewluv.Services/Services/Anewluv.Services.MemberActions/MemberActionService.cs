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
//using Nmedia.DataAccess.Interfaces;
using LoggingLibrary;
//using Nmedia.Infrastructure;.Domain.log;
using Anewluv.Domain.Data.ViewModels;
using Nmedia.Infrastructure;

using Anewluv.DataExtentionMethods;
using Nmedia.Infrastructure.Domain.Data.log;
using Anewluv.Domain;

using Anewluv.Services.Mapping;
using Nmedia.Infrastructure.Domain.Data;
using System.Threading.Tasks;
using Nmedia.Infrastructure.DependencyInjection;
using Repository.Pattern.UnitOfWork;
using GeoData.Domain.Models;

namespace Anewluv.Services.MemberActions
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple )]
    public class MemberActionsService : IMemberActionsService
    {

        //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
       // private readonly IUnitOfWorkAsync _spatial_unitOfWorkAsync;
        private readonly IGeoDataStoredProcedures _storedProcedures;
        private LoggingLibrary.Logging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public MemberActionsService([IAnewluvEntitesScope]IUnitOfWorkAsync unitOfWork, [InSpatialEntitesScope]IGeoDataStoredProcedures storedProcedures)
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
            //_unitOfWorkAsync.DisableProxyCreation = true;
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
        public async Task<SearchResultsViewModel> getmyrelationshipsfiltered(ProfileModel model)
        {

            ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
             var db = _unitOfWorkAsync;
            {
                try
                {

                  
                    var task = Task.Factory.StartNew(() =>
                    {

                        //return _memberactionsrepository.getmyrelationshipsfiltered(Convert.ToInt32(model.profileid), types);
                        return new SearchResultsViewModel();

                    });

                    return await task.ConfigureAwait(false);


                }
                catch (Exception ex)
                {
                    logger = new  Logging(applicationEnum.MemberActionsService);
                    //int profileid = Convert.ToInt32(viewerprofileid);

                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid)); logger.Dispose();
                    
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
        }

        

        

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
        public async Task<int> getwhoiaminterestedincount(ProfileModel model)
        {
          
               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {

                  return   memberactionsextentions.getwhoiaminterestedincount(model, db);

                 });

                return await task.ConfigureAwait(false);

                

             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<int> getwhoisinterestedinmecount(ProfileModel model)
        {

               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {

                 var task = Task.Factory.StartNew(() =>
                 {


                     return memberactionsextentions.getwhoisinterestedinmecount(model, db);


                 });

                return await task.ConfigureAwait(false);

                

             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<int> getwhoisinterestedinmenewcount(ProfileModel model)
        {

               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {
                     return memberactionsextentions.getwhoisinterestedinmenewcount(model, db);


                 });

                return await task.ConfigureAwait(false);

             ;
             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<SearchResultsViewModel> getwhoiaminterestedin(ProfileModel model)
        {





            ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
           
             var db = _unitOfWorkAsync;
            {

                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {

                        if (model.page == null | model.page == 0) model.page = 1;
                        if (model.numberperpage == null | model.numberperpage == 0) model.numberperpage = 4;

                      //  int id = Convert.ToInt32(model.profileid);

                        var MyActiveblocks = (from c in db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.removedate == null)
                                              select new
                                              {
                                                  ProfilesBlockedId = c.blockprofile_id
                                              }).ToList();

                        //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                        //rematerialize on the back end.
                        //final query to send back only the profile datatas of the interests we want
                        var interests = (from f in db.Repository<interest>().Queryable().OfType<interest>().Where(p => p.profile_id == model.profileid && p.deletedbymemberdate == null)
                                         where (f.profilemetadata1.profile.status_id < 3 && !(MyActiveblocks.Count != 0 && f.profilemetadata1 != null))//&& !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profilemetadata1.profile_id)))
                                         select new MemberSearchViewModel
                                         {
                                             interestdate = f.creationdate,
                                             id = f.profilemetadata1.profile_id
                                             // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                         }).ToList();
                        // var dd2 = 0;
                        //var dd = 2 /  dd2;

                        int? pageint = model.page;
                        int? numberperpageint = model.numberperpage;

                        bool allowpaging = (interests.Count >= (pageint * numberperpageint) ? true : false);
                        var pageData = pageint > 1 & allowpaging ?
                            new PaginatedList<MemberSearchViewModel>().GetCurrentPages(interests, pageint ?? 1, numberperpageint ?? 4) : interests.Take(numberperpageint.GetValueOrDefault());
                        //this.AddRange(pageData.ToList());
                        // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                        //return interests.ToList();
                        List<MemberSearchViewModel> results;



                        results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList(), allphotos = false }, db, _storedProcedures).OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                        
                        // return data2.OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();;
                        //.OrderByDescending(f => f.interestdate ?? DateTime.MaxValue).ToList();

                        return new SearchResultsViewModel { results = results, totalresults = interests.Count() };
                      //  return results;


                    });

                   return await task.ConfigureAwait(false);



                }
                catch (Exception ex)
                {
                    //instantiate logger here so it does not break anything else.
                    new  Logging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid), null);
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
        public async Task<SearchResultsViewModel> getwhoisinterestedinme(ProfileModel model)
        {
       
       

          ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
         
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {

                     if (model.page == null | model.page == 0) model.page = 1;
                     if (model.numberperpage == null | model.numberperpage == 0) model.numberperpage = 4;

                     var MyActiveblocks = (from c in db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.removedate == null)
                                           select new
                                           {
                                               ProfilesBlockedId = c.blockprofile_id
                                           }).ToList();


                     //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                     //rematerialize on the back end.
                     //final query to send back only the profile datatas of the interests we want
                     var whoisinterestedinme = (from f in db.Repository<interest>().Queryable().Where(p => p.interestprofile_id == model.profileid && p.deletedbymemberdate == null)
                                                //join f in db.Repository<profiledata>().Queryable() on p.profile_id equals f.profile_id
                                                // join z in db.Repository<profile>().Queryable() on p.profile_id equals z.id
                                                where (f.profilemetadata1.profile.status_id < 3 && !(MyActiveblocks.Count != 0 && f.profilemetadata1 != null))//&& !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profilemetadata1.profile_id)))
                                                select new MemberSearchViewModel
                                                {
                                                    interestdate = f.creationdate,
                                                    id = f.profilemetadata1.profile_id
                                                    // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                                }).ToList();

                     int? pageint = Convert.ToInt32(model.page);
                     int? numberperpageint = Convert.ToInt32(model.numberperpage);

                     bool allowpaging = (whoisinterestedinme.Count >= (pageint * numberperpageint) ? true : false);
                     var pageData = pageint > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(whoisinterestedinme, pageint ?? 1, numberperpageint ?? 4) : whoisinterestedinme.Take(numberperpageint.GetValueOrDefault());
                     //this.AddRange(pageData.ToList());
                     // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                     //return interests.ToList();
                     List<MemberSearchViewModel> results;
                     results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList(), allphotos = false }, db, _storedProcedures).OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

                     return new SearchResultsViewModel { results = results, totalresults = whoisinterestedinme.Count() };

                 });

                return await task.ConfigureAwait(false);


             }
             catch (Exception ex)
             {

                new  Logging(applicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<SearchResultsViewModel> getwhoisinterestedinmenew(ProfileModel model)
        {
            

               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
              
          var db = _unitOfWorkAsync;
         {
             try
             {

                 var task = Task.Factory.StartNew(() =>
                 {
                     if (model.page == null | model.page == 0) model.page = 1;
                     if (model.numberperpage == null | model.numberperpage == 0) model.numberperpage = 4;


                     var MyActiveblocks = (from c in db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.removedate == null)
                                           select new
                                           {
                                               ProfilesBlockedId = c.blockprofile_id
                                           }).ToList();

                     //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                     //rematerialize on the back end.
                     //final query to send back only the profile datatas of the interests we want
                     var whoisinterestedinmenew = (from f in db.Repository<interest>().Queryable().Where(p => p.interestprofile_id == model.profileid && p.viewdate == null)
                                                   where (f.profilemetadata1.profile.status_id < 3 && !(MyActiveblocks.Count != 0 && f.profilemetadata1 != null))//&& !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profilemetadata1.profile_id)))                
                                                   select new MemberSearchViewModel
                                                   {
                                                       interestdate = f.creationdate,
                                                       id = f.profilemetadata1.profile_id
                                                       // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                                   }).ToList();

                     int? pageint = model.page;
                     int? numberperpageint = model.numberperpage;

                     bool allowpaging = (whoisinterestedinmenew.Count >= (pageint * numberperpageint) ? true : false);
                     var pageData = pageint > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(whoisinterestedinmenew, pageint ?? 1, numberperpageint ?? 4) : whoisinterestedinmenew.Take(numberperpageint.GetValueOrDefault());
                     //this.AddRange(pageData.ToList());
                     // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                     //return interests.ToList();
                     List<MemberSearchViewModel> results;
                     results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList(), allphotos = false }, db, _storedProcedures).OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

                     return new SearchResultsViewModel { results = results, totalresults = whoisinterestedinmenew.Count() };


                 });

                return await task.ConfigureAwait(false);



             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<SearchResultsViewModel> getmutualinterests(ProfileModel model)
        {

               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {


                     IEnumerable<MemberSearchViewModel> source = default(IEnumerable<MemberSearchViewModel>);
                  
                     if (model.page == null || model.page == 0) model.page = 1;
                     if (model.numberperpage == null || model.numberperpage == 0) model.numberperpage = 4;

                     bool allowpaging = (source.Count() >= (model.page * model.numberperpage) ? true : false);
                     var pageData = model.page > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(source.ToList(), model.page ?? 1, model.numberperpage ?? 20) : source.Take(model.numberperpage.GetValueOrDefault());

                     //TO do pagge this
                     return new SearchResultsViewModel { results = source.ToList(), totalresults = source.Count() };
                     // return mutualblocks.ToList();


                 });

                return await task.ConfigureAwait(false);




             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<bool> checkinterest(ProfileModel model)
        {

               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {

                     return db.Repository<interest>().Queryable().Any(r => r.profile_id == model.profileid && r.interestprofile_id == model.targetprofileid);



                 });

                return await task.ConfigureAwait(false);

         
             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task addinterest(ProfileModel model)
        {


            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {


                            //create new inetrest object
                            interest interest = new interest();
                            //make sure you are not trying to interest at yourself
                            if (model.profileid.GetValueOrDefault().ToString() == model.targetprofileid.ToString()) return ;


                            //check  interest first  
                            //if this was a interest being restored just do that part
                            var existinginterest = db.Repository<interest>().Queryable().Where (r => r.profile_id == model.profileid && r.interestprofile_id == model.targetprofileid).FirstOrDefault();

                            //just  update it if we have one already
                            if (existinginterest != null)
                            {
                                existinginterest.deletedbymemberdate = null; ;
                                existinginterest.modificationdate = DateTime.Now;
                                db.Repository<interest>().Update(existinginterest);

                            }
                            else
                            {
                                //interest = this. db.Repository<interest>().Queryable().Where(p => p.profileid == profileid).FirstOrDefault();
                                //update the profile status to 2
                                interest.profile_id = model.profileid.GetValueOrDefault();
                                interest.interestprofile_id = model.targetprofileid.GetValueOrDefault();
                                interest.mutual = false;  // not dealing with this calulatin yet
                                interest.creationdate = DateTime.Now;
                                //handele the update using EF
                                // this. db.Repository<profile>().Queryable().AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                                db.Repository<interest>().Insert(interest);

                            }

                          var i =db.SaveChanges();
                           // transaction.Commit();

                          //  return true;


                        });
                        await task.ConfigureAwait(false);

                        
                       

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task removeinterestbyprofileid(ProfileModel model)
        {


            //update method code  return awa
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {



                         //   var targetid = Convert.ToInt32(interestprofile_id);

                            var interest = db.Repository<interest>().Queryable().Where(p => p.profile_id == model.profileid && p.interestprofile_id == model.targetprofileid).FirstOrDefault();
                            //update the profile status to 2

                            interest.deletedbymemberdate = DateTime.Now;
                            interest.modificationdate = DateTime.Now;
                           db.Repository<interest>().Update(interest);

                          var i =db.SaveChanges();
                           // transaction.Commit();

                        });

                      await task.ConfigureAwait(false);


                     

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
         public async Task removeinterestbyinterestprofileid(ProfileModel model)
        {


            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            //  var targetid = Convert.ToInt32(interestprofile_id);

                            var interest = db.Repository<interest>().Queryable().Where(p => p.profile_id == model.targetprofileid && p.interestprofile_id == model.profileid).FirstOrDefault();
                            //update the profile status to 2

                            interest.deletedbyinterestdate = DateTime.Now;
                            interest.modificationdate = DateTime.Now;
                            db.Repository<interest>().Update(interest);

                           var i =db.SaveChanges();
                           // transaction.Commit();

                            return true;

                        });

                      await task.ConfigureAwait(false);

                       
                   

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
         public async Task restoreinterestbyprofileid(ProfileModel model)
        {


            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {


                           // var targetid = Convert.ToInt32(interestprofile_id);

                            var interest = db.Repository<interest>().Queryable().Where(p => p.profile_id == model.profileid && p.interestprofile_id == model.targetprofileid).FirstOrDefault();
                            //update the profile status to 2

                            interest.deletedbymemberdate = null;
                            interest.modificationdate = DateTime.Now;
                           db.Repository<interest>().Update(interest);

                          var i =db.SaveChanges();
                           // transaction.Commit();

                           // return true;

                        });

                      await task.ConfigureAwait(false);


                     
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
         public async Task restoreinterestbyinterestprofileid(ProfileModel model)
        {


            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {


                          //  var targetid = Convert.ToInt32(interestprofile_id);

                            var interest = db.Repository<interest>().Queryable().Where(p => p.profile_id == model.targetprofileid && p.interestprofile_id == model.profileid).FirstOrDefault();
                            //update the profile status to 2

                            interest.deletedbyinterestdate = null;
                            interest.modificationdate = DateTime.Now;
                           db.Repository<interest>().Update(interest);


                          var i =db.SaveChanges();
                           // transaction.Commit();

                            return true;

                        });

                      await task.ConfigureAwait(false);

                       
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
         public async Task removeinterestsbyprofileidandscreennames(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            // interests = this. db.Repository<interest>().Queryable().Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                            //update the profile status to 2
                            interest interest = new interest();
                            foreach (string value in model.targetscreennames)
                            {




                                int? interestprofile_id = db.Repository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                                var currentinterest = db.Repository<interest>().Queryable().Where(p => p.profile_id == model.profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                                interest.deletedbymemberdate = DateTime.Now;
                                interest.modificationdate = DateTime.Now;
                               db.Repository<interest>().Update(currentinterest);

                            }

                          var i =db.SaveChanges();
                           // transaction.Commit();


                        });

                      await task.ConfigureAwait(false);

                     

                      //  return true;
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
         public async Task restoreinterestsbyprofileidandscreennames(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {


                            // interests = this. db.Repository<interest>().Queryable().Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                            //update the profile status to 2
                            interest interest = new interest();
                            foreach (string value in model.targetscreennames)
                            {
                                int? interestprofile_id = db.Repository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                                var currentinterest = db.Repository<interest>().Queryable().Where(p => p.profile_id == model.profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                                interest.deletedbymemberdate = null;
                                interest.modificationdate = DateTime.Now;
                               db.Repository<interest>().Update(currentinterest);

                            }

                          var i =db.SaveChanges();
                           // transaction.Commit();

                          //  return true;

                        });

                        await task.ConfigureAwait(false);

                      

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
         public async Task updateinterestviewstatus(ProfileModel model)
        {
            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {


                            var interest = db.Repository<interest>().Queryable().Where(p => p.interestprofile_id == model.targetprofileid && p.profile_id == model.profileid).FirstOrDefault();
                            //update the profile status to 2            
                            if (interest.viewdate == null)
                            {
                                interest.viewdate = DateTime.Now;
                                interest.modificationdate = DateTime.Now;
                               db.Repository<interest>().Update(interest);

                              var i =db.SaveChanges();
                               // transaction.Commit();
                            }
                       //     return true;

                        });

                      await task.ConfigureAwait(false);
 
                      

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<int> getwhoipeekedatcount(ProfileModel model)
        {

               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {

                 var task = Task.Factory.StartNew(() =>
                 {

                    return memberactionsextentions.getwhoipeekedatcount(model, db);


                 });

                return await task.ConfigureAwait(false);

                


             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<int> getwhopeekedatmecount(ProfileModel model)
        {

               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {

                 var task = Task.Factory.StartNew(() =>
                 {

                     return memberactionsextentions.getwhopeekedatmecount(model, db);

                 });

                return await task.ConfigureAwait(false);

                
               
             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<int> getwhopeekedatmenewcount(ProfileModel model)
        {

               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {


                     return memberactionsextentions.getwhopeekedatmenewcount(model, db);

                 });

                return await task.ConfigureAwait(false);

                

             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<SearchResultsViewModel> getwhopeekedatme(ProfileModel model)
        {

          

               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
              
               var db = _unitOfWorkAsync;
         {
             try
             {

                 var task = Task.Factory.StartNew(() =>
                 {
                     if (model.page == null | model.page == 0) model.page = 1;
                     if (model.numberperpage == null | model.numberperpage == 0) model.numberperpage = 4;

                     var MyActiveblocks = (from c in db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.removedate == null)
                                           select new
                                           {
                                               ProfilesBlockedId = c.blockprofile_id
                                           }).ToList();

                     //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                     //rematerialize on the back end.
                     //final query to send back only the profile datatas of the interests we want
                     var peeks = (from f in db.Repository<peek>().Queryable().Where(p => p.profile_id == model.profileid && p.deletedbymemberdate == null)
                                  where (f.profilemetadata1.profile.status_id < 3 && !(MyActiveblocks.Count != 0 && f.profilemetadata1 != null))//&& !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profilemetadata1.profile_id)))
                                  select new MemberSearchViewModel
                                  {
                                      peekdate = f.creationdate,
                                      id = f.profilemetadata1.profile_id
                                      // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                  }).ToList();

                     int? pageint = model.page;
                     int? numberperpageint = model.numberperpage;

                     bool allowpaging = (peeks.Count >= (pageint * numberperpageint) ? true : false);
                     var pageData = pageint > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(peeks, pageint ?? 1, numberperpageint ?? 4) : peeks.Take(numberperpageint.GetValueOrDefault());
                     //this.AddRange(pageData.ToList());
                     // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                     List<MemberSearchViewModel> results;


                     results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList(), allphotos = false }, db, _storedProcedures).OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

                     
                      return new SearchResultsViewModel { results = results, totalresults = peeks.Count() };

                 });

                return await task.ConfigureAwait(false);


            
      
             }
             catch (Exception ex)
             {

                new  Logging(applicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<SearchResultsViewModel> getwhopeekedatmenew(ProfileModel model)
        {

          
               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
              
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {
                     if (model.page == null | model.page == 0) model.page = 1;
                     if (model.numberperpage == null | model.numberperpage == 0) model.numberperpage = 4;

                     var MyActiveblocks = (from c in db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.removedate == null)
                                           select new
                                           {
                                               ProfilesBlockedId = c.blockprofile_id
                                           }).ToList();

                     //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                     //rematerialize on the back end.
                     //final query to send back only the profile datatas of the interests we want
                     var WhoPeekedAtMe = (from f in db.Repository<peek>().Queryable().Where(p => p.peekprofile_id == model.profileid && p.deletedbymemberdate == null)
                                          where (f.profilemetadata1.profile.status_id < 3 && !(MyActiveblocks.Count != 0 && f.profilemetadata1 != null))//&& !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profilemetadata1.profile_id)))
                                          select new MemberSearchViewModel
                                          {
                                              peekdate = f.creationdate,
                                              id = f.profilemetadata1.profile_id
                                              // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                          }).ToList();

                     int? pageint = model.page;
                     int? numberperpageint = model.numberperpage;

                     bool allowpaging = (WhoPeekedAtMe.Count >= (pageint * numberperpageint) ? true : false);
                     var pageData = pageint > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(WhoPeekedAtMe, pageint ?? 1, numberperpageint ?? 4) : WhoPeekedAtMe.Take(numberperpageint.GetValueOrDefault());
                     //this.AddRange(pageData.ToList());
                     // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                     //return interests.ToList();
                     List<MemberSearchViewModel> results;
                     results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList(), allphotos = false }, db, _storedProcedures).OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

                    return new SearchResultsViewModel { results = results, totalresults = WhoPeekedAtMe.Count() };

                 });

                return await task.ConfigureAwait(false);


            

               

             }
             catch (Exception ex)
             {

                new  Logging(applicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<SearchResultsViewModel> getwhoipeekedat(ProfileModel model)
        {

        

            ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
             
              var db = _unitOfWorkAsync;
         //using (var db = _unitOfWorkAsync)
         //{
             try
             {

                 var task = Task.Factory.StartNew(() =>
                 {
                     if (model.page == null | model.page == 0) model.page = 1;
                     if (model.numberperpage == null | model.numberperpage == 0) model.numberperpage = 4;

                     // IEnumerable<MemberSearchViewModel> PeekNew = default(IEnumerable<MemberSearchViewModel>);

                     //gets all  interestets from the interest table based on if the person's profiles are stil lvalid tho


                     var MyActiveblocks = (from c in db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.removedate == null)
                                           select new
                                           {
                                               ProfilesBlockedId = c.blockprofile_id
                                           }).ToList();

                     //if (MyActiveblocks.Count != 0 && !MyActiveblocks.Any(b => b.ProfilesBlockedId == ))
                     // {
                     //     var test = "";
                     // }


                     //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                     //rematerialize on the back end.
                     //final query to send back only the profile datatas of the interests we want
                     var peeknew = (from f in db.Repository<peek>().Queryable().Where(p => p.peekprofile_id == model.profileid)
                                    // join f in  db.Repository<profiledata>().Queryable() on p.profile_id equals f.profile_id
                                    //  join z in  db.Repository<profile>().Queryable() on p.profile_id equals z.id
                                    where (f.profilemetadata1.profile.status_id < 3 && !(MyActiveblocks.Count != 0 && f.profilemetadata1 != null))//&& !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profilemetadata1.profile_id)))
                                    select new MemberSearchViewModel
                                    {
                                        peekdate = f.creationdate,
                                        id = f.profilemetadata1.profile_id //,
                                        // profiledata = f.profilemetadata1.profile.profiledata
                                        // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                    }).ToList();


                     int? pageint = model.page;
                     int? numberperpageint = model.numberperpage;

                     bool allowpaging = (peeknew.Count >= (pageint * numberperpageint) ? true : false);
                     var pageData = pageint > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(peeknew, pageint ?? 1, numberperpageint ?? 4) : peeknew.Take(numberperpageint.GetValueOrDefault());
                     //this.AddRange(pageData.ToList());
                     // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                     //return interests.ToList();

                     var results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList(), allphotos = false }, db, _storedProcedures).OrderByDescending(f => f.peekdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();


                      // db.DisableProxyCreation = true;
                      return new SearchResultsViewModel { results = results, totalresults = peeknew.Count() };
                

                 });
                return await task.ConfigureAwait(false);

            
                
             }
             catch (Exception ex)
             {

                new  Logging(applicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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


     //    }

         
        }




        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both peek 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public async Task<SearchResultsViewModel> getmutualpeeks(ProfileModel model)
        {

                 ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {

                     IEnumerable<MemberSearchViewModel> source = default(IEnumerable<MemberSearchViewModel>);
                     if (model.page == null || model.page == 0) model.page = 1;
                     if (model.numberperpage == null || model.numberperpage == 0) model.numberperpage = 4;

                     bool allowpaging = (source.Count() >= (model.page * model.numberperpage) ? true : false);
                     var pageData = model.page > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(source.ToList(), model.page ?? 1, model.numberperpage ?? 20) : source.Take(model.numberperpage.GetValueOrDefault());

                     //TO do pagge this
                     return new SearchResultsViewModel { results = source.ToList(), totalresults = source.Count() };
                     // return mutualblocks.ToList();


                 });

                return await task.ConfigureAwait(false);

            
                 
             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<bool> checkpeek(ProfileModel model)
        {

                 ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {

                 var task = Task.Factory.StartNew(() =>
                 {



                     return db.Repository<peek>().Queryable().Any(r => r.profile_id == model.profileid && r.peekprofile_id == model.targetprofileid);

         

                 });

                return await task.ConfigureAwait(false);
               
    }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task addpeek(ProfileModel model)
        {
            //create new inetrest object
            peek peek = new peek();
            //make sure you are not trying to peek at yourself
            if (model.profileid.ToString() == model.targetprofileid.ToString()) return ;

        
            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {


                            // var targetid = Convert.ToInt32(model.targetprofileid.ToString());

                            //check  peek first  
                            //if this was a peek being restored just do that part
                            var existingpeek = db.Repository<peek>().Queryable().Where(r => r.profile_id == model.profileid && r.peekprofile_id == model.targetprofileid).FirstOrDefault();

                            //just  update it if we have one already
                            if (existingpeek != null)
                            {
                                existingpeek.deletedbymemberdate = null; ;
                                existingpeek.modificationdate = DateTime.Now;
                               db.Repository<peek>().Update(existingpeek);

                            }
                            else
                            {
                                //peek = this. db.Repository<peek>().Queryable().Where(p => p.profileid == profileid).FirstOrDefault();
                                //update the profile status to 2
                                peek.profile_id = model.profileid.GetValueOrDefault();
                                peek.peekprofile_id = model.targetprofileid.GetValueOrDefault();
                                peek.mutual = false;  // not dealing with this calulatin yet
                                peek.creationdate = DateTime.Now;
                                //handele the update using EF
                                // this. db.Repository<profile>().Queryable().AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                               db.Repository<peek>().Insert(peek);

                            }

                          var i =db.SaveChanges();
                           // transaction.Commit();

                            return true;

                        });

                      await task.ConfigureAwait(false);



                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task removepeekbyprofileid(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {


                          //  var targetid = Convert.ToInt32(peekprofile_id);

                            var peek = db.Repository<peek>().Queryable().Where(p => p.profile_id == model.profileid && p.peekprofile_id == model.targetprofileid).FirstOrDefault();
                            //update the profile status to 2

                            peek.deletedbymemberdate = DateTime.Now;
                            peek.modificationdate = DateTime.Now;
                           db.Repository<peek>().Update(peek);

                          var i =db.SaveChanges();
                           // transaction.Commit();

                            return true;

                        });

                      await task.ConfigureAwait(false);


                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task removepeekbypeekprofileid(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                         //   var targetid = Convert.ToInt32(peekprofile_id);

                            var peek = db.Repository<peek>().Queryable().Where(p => p.profile_id == model.targetprofileid && p.peekprofile_id == model.profileid).FirstOrDefault();
                            //update the profile status to 2

                            peek.deletedbypeekdate = DateTime.Now;
                            peek.modificationdate = DateTime.Now;
                           db.Repository<peek>().Update(peek);

                          var i =db.SaveChanges();
                           // transaction.Commit();

                            return true;


                        });

                      await task.ConfigureAwait(false);

                       
                     

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task restorepeekbyprofileid(ProfileModel model)
        {
            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                         //   var targetid = Convert.ToInt32(peekprofile_id);

                            var peek = db.Repository<peek>().Queryable().Where(p => p.profile_id == model.profileid && p.peekprofile_id == model.targetprofileid).FirstOrDefault();
                            //update the profile status to 2

                            peek.deletedbymemberdate = null;
                            peek.modificationdate = DateTime.Now;

                           db.Repository<peek>().Update(peek);

                          var i =db.SaveChanges();
                           // transaction.Commit();

                            return true;

                        });

                      await task.ConfigureAwait(false);

                       
                      
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task restorepeekbypeekprofileid( ProfileModel model)
        {
            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {

                         //   var targetid = Convert.ToInt32(peekprofile_id);

                            var peek = db.Repository<peek>().Queryable().Where(p => p.profile_id == model.targetprofileid && p.peekprofile_id == model.profileid).FirstOrDefault();
                            //update the profile status to 2
                            //update the profile status to 2


                            peek.deletedbypeekdate = null;
                            peek.modificationdate = DateTime.Now;

                           db.Repository<peek>().Update(peek);

                          var i =db.SaveChanges();
                           // transaction.Commit();

                            return true;

                        });

                      await task.ConfigureAwait(false);

                       
                      

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task removepeeksbyprofileidandscreennames(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {

                            // peeks = this. db.Repository<peek>().Queryable().Where(p => p.profileid == profileid && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                            //update the profile status to 2
                            peek peek = new peek();
                            foreach (string value in model.targetscreennames)
                            {

                                int? peekprofile_id = db.Repository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                                var currentpeek = db.Repository<peek>().Queryable().Where(p => p.profile_id == model.profileid && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                                peek.deletedbymemberdate = null;
                                peek.modificationdate = DateTime.Now;
                               db.Repository<peek>().Update(currentpeek);

                            }

                          var i =db.SaveChanges();
                           // transaction.Commit();

                            return true;

                        });

                      await task.ConfigureAwait(false);

                             
                     
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task restorepeeksbyprofileidandscreennames(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {


                            // peeks = this. db.Repository<peek>().Queryable().Where(p => p.profileid == profileid && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                            //update the profile status to 2
                            peek peek = new peek();
                            foreach (string value in model.targetscreennames)
                            {

                                int? peekprofile_id = db.Repository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                                var currentpeek = db.Repository<peek>().Queryable().Where(p => p.profile_id == model.profileid && p.peekprofile_id == peekprofile_id).FirstOrDefault();
                                peek.deletedbymemberdate = null;
                                peek.modificationdate = DateTime.Now;
                               db.Repository<peek>().Update(currentpeek);

                            }

                          var i =db.SaveChanges();
                           // transaction.Commit();

                            return true;

                        });

                      await task.ConfigureAwait(false);

                       
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task updatepeekviewstatus(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {



                            var peek = db.Repository<peek>().Queryable().Where(p => p.peekprofile_id == model.targetprofileid && p.profile_id == model.profileid).FirstOrDefault();
                            //update the profile status to 2            
                            if (peek.viewdate == null)
                            {
                                peek.viewdate = DateTime.Now;
                                peek.modificationdate = DateTime.Now;
                               db.Repository<peek>().Update(peek);

                              var i =db.SaveChanges();
                               // transaction.Commit();
                            }
                            return true;

                        });

                      await task.ConfigureAwait(false);

                    
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        //count methods first
        /// <summary>
        /// count all total peeks
        /// </summary>       
        public async Task<int> getwhoiblockedcount(ProfileModel model)
        {

          ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var db = _unitOfWorkAsync;
            {
                try
                {

                    var task = Task.Factory.StartNew(() =>
                    {

                        return memberactionsextentions.getwhoiblockedcount(model, db);


                    });

                    return await task.ConfigureAwait(false);




                }
                catch (Exception ex)
                {

                    new Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<SearchResultsViewModel> getwhoiblocked(ProfileModel model)
        {


                ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
               
          var db = _unitOfWorkAsync;
         {
             try
             {


                 var task = Task.Factory.StartNew(() =>
                 {

                     if (model.page == null | model.page == 0) model.page = 1;
                     if (model.numberperpage == null | model.numberperpage == 0) model.numberperpage = 4;


                     var blocknew = (from f in db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.removedate == null)
                                     // join f in  db.Repository<profiledata>().Queryable() on p.blockprofile_id equals f.profile_id
                                     //  join z in  db.Repository<profile>().Queryable() on p.blockprofile_id equals z.id
                                     where (f.profilemetadata1.profile.status_id < 3)
                                     orderby (f.creationdate) descending
                                     select new MemberSearchViewModel
                                     {
                                         blockdate = f.creationdate,
                                         id = f.profilemetadata1.profile_id
                                         // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                     }).ToList();

                     int? pageint = model.page;
                     int? numberperpageint = model.numberperpage;

                     bool allowpaging = (blocknew.Count >= (pageint * numberperpageint) ? true : false);
                     var pageData = pageint > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(blocknew, pageint ?? 1, numberperpageint ?? 4) : blocknew.Take(numberperpageint.GetValueOrDefault());
                     //this.AddRange(pageData.ToList());
                     // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                     List<MemberSearchViewModel> results;
                    
                   
                        
                         results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList(), allphotos = false },db,_storedProcedures).OrderByDescending(f => f.blockdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                  
                      return new SearchResultsViewModel { results = results, totalresults = blocknew.Count() };
                 });

                return await task.ConfigureAwait(false);

            

             }
             catch (Exception ex)
             {

                new  Logging(applicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<SearchResultsViewModel> getwhoblockedme(ProfileModel model)
        {

          

                ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
               
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {
                     if (model.page == null | model.page == 0) model.page = 1;
                     if (model.numberperpage == null | model.numberperpage == 0) model.numberperpage = 4;

                     var whoblockedme = (from f in db.Repository<block>().Queryable().Where(p => p.blockprofile_id == model.profileid && p.removedate == null)
                                         //  join f in  db.Repository<profiledata>().Queryable() on p.profile_id equals f.profile_id
                                         // join z in  db.Repository<profile>().Queryable() on p.profile_id equals z.id
                                         where (f.profilemetadata1.profile.status_id < 3)
                                         orderby (f.creationdate) descending
                                         select new MemberSearchViewModel
                                         {
                                             blockdate = f.creationdate,
                                             id = f.profilemetadata1.profile_id
                                             // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                         }).ToList();

                     int? pageint = model.page;
                     int? numberperpageint = model.numberperpage;

                     bool allowpaging = (whoblockedme.Count >= (pageint * numberperpageint) ? true : false);
                     var pageData = pageint > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(whoblockedme, pageint ?? 1, numberperpageint ?? 4) : whoblockedme.Take(numberperpageint.GetValueOrDefault());
                     //this.AddRange(pageData.ToList());
                     // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                     List<MemberSearchViewModel> results;

                     results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList(), allphotos = false }, db, _storedProcedures).OrderByDescending(f => f.blockdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

                         return new SearchResultsViewModel { results = results, totalresults = whoblockedme.Count() };
                 });

                return await task.ConfigureAwait(false);


            
             }
             catch (Exception ex)
             {

                new  Logging(applicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<SearchResultsViewModel> getmutualblocks(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {


                            IEnumerable<MemberSearchViewModel> source = default(IEnumerable<MemberSearchViewModel>);

                            if (model.page == null || model.page == 0) model.page = 1;
                            if (model.numberperpage == null || model.numberperpage == 0) model.numberperpage = 4;

                            bool allowpaging = (source.Count() >= (model.page * model.numberperpage) ? true : false);
                            var pageData = model.page > 1 & allowpaging ?
                                new PaginatedList<MemberSearchViewModel>().GetCurrentPages(source.ToList(), model.page ?? 1, model.numberperpage ?? 20) : source.Take(model.numberperpage.GetValueOrDefault());

                            //TO do pagge this
                            return new SearchResultsViewModel { results = source.ToList(), totalresults = source.Count() };
                           // return mutualblocks.ToList();

                        });

                       return await task.ConfigureAwait(false);

                   

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<bool> checkblock(ProfileModel model)
        {


            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {



                            return db.Repository<block>().Queryable().Any(r => r.profile_id == model.profileid && r.blockprofile_id == model.targetprofileid);

                        });

                       return await task.ConfigureAwait(false);


                      


                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task addblock(ProfileModel model)
        {
                //create new inetrest object
            block block = new block();
            //make sure you are not trying to block at yourself
            if (model.profileid.ToString() == model.targetprofileid.ToString()) return ;

           

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            


                          //  var targetid = Convert.ToInt32(model.targetprofileid.ToString());

                            //check  block first  
                            //if this was a block being restored just do that part
                            var existingblock = db.Repository<block>().Queryable().Where(r => r.profile_id == model.profileid && r.blockprofile_id == model.targetprofileid).FirstOrDefault();

                            //just  update it if we have one already
                            if (existingblock != null)
                            {
                                existingblock.removedate = null; ;
                                existingblock.modificationdate = DateTime.Now;
                               db.Repository<block>().Update(existingblock);

                            }
                            else
                            {
                                //block = this. db.Repository<block>().Queryable().Where(p => p.profileid == profileid).FirstOrDefault();
                                //update the profile status to 2
                                block.profile_id = model.profileid.GetValueOrDefault();
                                block.blockprofile_id = model.targetprofileid.GetValueOrDefault();
                                // not dealing with this calulatin yet                           
                                block.creationdate = DateTime.Now;
                                //handele the update using EF
                                // this. db.Repository<profile>().Queryable().AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                               db.Repository<block>().Insert(block);

                            }

                          var i =db.SaveChanges();
                           // transaction.Commit();

                          //  return true;


                        });

                      await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task removeblock(ProfileModel model)
        {
            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            //var targetid = Convert.ToInt32(blockprofile_id);

                            var block = db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.blockprofile_id == model.targetprofileid).FirstOrDefault();
                            //update the profile status to 2

                            block.removedate = DateTime.Now;
                            block.modificationdate = DateTime.Now;
                           db.Repository<block>().Update(block);

                          var i =db.SaveChanges();
                           // transaction.Commit();

                         //   return true;


                        });

                      await task.ConfigureAwait(false);

                       
                       

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task restoreblock(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                           // var targetid = Convert.ToInt32(blockprofile_id);

                            var block = db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.blockprofile_id == model.targetprofileid).FirstOrDefault();
                            //update the profile status to 2

                            block.removedate = null;
                            block.modificationdate = DateTime.Now;

                           db.Repository<block>().Update(block);

                          var i =db.SaveChanges();
                           // transaction.Commit();

                         //   return true;

                        });

                      await task.ConfigureAwait(false);

                       
                     

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task removeblocksbyscreennames(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {


                            // blocks = this. db.Repository<block>().Queryable().Where(p => p.profileid == profileid && p.blockprofile_id == blockprofile_id).FirstOrDefault();
                            //update the profile status to 2
                            block block = new block();
                            foreach (string value in model.targetscreennames)
                            {


                                int? blockprofile_id = db.Repository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                                var currentblock = db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.blockprofile_id == blockprofile_id).FirstOrDefault();
                                block.removedate = null;
                                block.modificationdate = DateTime.Now;
                               db.Repository<block>().Update(currentblock);

                            }

                          var i =db.SaveChanges();
                           // transaction.Commit();

                          //  return true;

                        });

                      await task.ConfigureAwait(false);


                      

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task restoreblocksbyscreennames(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            // blocks = this. db.Repository<block>().Queryable().Where(p => p.profileid == profileid && p.blockprofile_id == blockprofile_id).FirstOrDefault();
                            //update the profile status to 2
                            block block = new block();
                            foreach (string value in model.targetscreennames)
                            {

                                int? blockprofile_id = db.Repository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                                var currentblock = db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.blockprofile_id == blockprofile_id).FirstOrDefault();
                                block.removedate = null;
                                block.modificationdate = DateTime.Now;
                               db.Repository<block>().Update(currentblock);

                            }

                          var i =db.SaveChanges();
                           // transaction.Commit();

                           // return true;


                        });

                      await task.ConfigureAwait(false);

                     
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task updateblockreviewstatus(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            var block = db.Repository<block>().Queryable().Where(p => p.blockprofile_id == model.targetprofileid && p.profile_id == model.profileid).FirstOrDefault();
                            //update the profile status to 2            

                            block.modificationdate = DateTime.Now;

                           db.Repository<block>().Update(block);

                          var i =db.SaveChanges();
                           // transaction.Commit();

                           // return true;

                        });

                      await task.ConfigureAwait(false);

                       
                      

                       

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<int> getwhoilikecount(ProfileModel model)
        {

                ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {

                 var task = Task.Factory.StartNew(() =>
                 {
                     return memberactionsextentions.getwhoilikecount(model, db);

                 });
                return await task.ConfigureAwait(false);


              

             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<int> getwholikesmecount(ProfileModel model)
        {

                ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {

                     return memberactionsextentions.getwholikesmecount(model, db);

                 });

                return await task.ConfigureAwait(false);

                

             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<int> getwhoislikesmenewcount(ProfileModel model)
        {

                ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {

                     return memberactionsextentions.getwhoislikesmenewcount(model, db);

                 });

                return await task.ConfigureAwait(false);


                


             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<SearchResultsViewModel> getwholikesmenew(ProfileModel model)
        {
         

                ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
               
          var db = _unitOfWorkAsync;
         {
             try
             {

                 var task = Task.Factory.StartNew(() =>
                 {

                     if (model.page == null | model.page == 0) model.page = 1;
                     if (model.numberperpage == null | model.numberperpage == 0) model.numberperpage = 4;

                     var MyActiveblocks = (from c in db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.removedate == null)
                                           select new
                                           {
                                               ProfilesBlockedId = c.blockprofile_id
                                           }).ToList();

                     //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                     //rematerialize on the back end.
                     //final query to send back only the profile datatas of the interests we want
                     var likenew = (from f in db.Repository<like>().Queryable().Where(p => p.likeprofile_id == model.profileid && p.viewdate == null).ToList()
                                    //join f in  db.Repository<profiledata>().Queryable() on p.profile_id equals f.profile_id
                                    //join z in  db.Repository<profile>().Queryable() on p.profile_id equals z.id
                                    where (f.profilemetadata1.profile.status_id < 3 && !(MyActiveblocks.Count != 0 && f.profilemetadata1 != null))//&& !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profilemetadata1.profile_id)))
                                    orderby (f.creationdate) descending
                                    select new MemberSearchViewModel
                                    {
                                        likedate = f.creationdate,
                                        id = f.profilemetadata1.profile_id
                                        // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                    }).ToList();


                     int? pageint = model.page;
                     int? numberperpageint = model.numberperpage;

                     bool allowpaging = (likenew.Count >= (pageint * numberperpageint) ? true : false);
                     var pageData = pageint > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(likenew, pageint ?? 1, numberperpageint ?? 4) : likenew.Take(numberperpageint.GetValueOrDefault());
                     //this.AddRange(pageData.ToList());
                     // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                     //return interests.ToList();
                     List<MemberSearchViewModel> results;

                     results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList(), allphotos = false }, db, _storedProcedures).OrderByDescending(f => f.likedate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

                         return new SearchResultsViewModel { results = results, totalresults = likenew.Count() };

                 });

                return await task.ConfigureAwait(false);

            

                

             }
             catch (Exception ex)
             {

                new  Logging(applicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<SearchResultsViewModel> getwholikesme(ProfileModel model)
        {

           

                ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
               
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {
                     if (model.page == null | model.page == 0) model.page = 1;
                     if (model.numberperpage == null | model.numberperpage == 0) model.numberperpage = 4;

                     var MyActiveblocks = (from c in db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.removedate == null)
                                           select new
                                           {
                                               ProfilesBlockedId = c.blockprofile_id
                                           }).ToList();

                     //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                     //rematerialize on the back end.
                     //final query to send back only the profile datatas of the interests we want
                     var wholikesme = (from f in db.Repository<like>().Queryable().Where(p => p.likeprofile_id == model.profileid && p.deletedbylikedate == null)
                                       // join f in  db.Repository<profiledata>().Queryable() on p.profile_id equals f.profile_id
                                       // join z in  db.Repository<profile>().Queryable() on p.profile_id equals z.id
                                       where (f.profilemetadata1.profile.status_id < 3 && !(MyActiveblocks.Count != 0 && f.profilemetadata1 != null))//&& !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profilemetadata1.profile_id)))
                                       orderby (f.creationdate) descending
                                       select new MemberSearchViewModel
                                       {
                                           likedate = f.creationdate,
                                           id = f.profilemetadata1.profile_id
                                           // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                       }).ToList();

                     int? pageint = model.page;
                     int? numberperpageint = model.numberperpage;

                     bool allowpaging = (wholikesme.Count >= (pageint * numberperpageint) ? true : false);
                     var pageData = pageint > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(wholikesme, pageint ?? 1, numberperpageint ?? 4) : wholikesme.Take(numberperpageint.GetValueOrDefault());
                     //this.AddRange(pageData.ToList());
                     // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                     //return interests.ToList();
                     List<MemberSearchViewModel> results;
                     results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList(), allphotos = false }, db, _storedProcedures).OrderByDescending(f => f.likedate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

                         return new SearchResultsViewModel { results = results, totalresults = wholikesme.Count() };
                 });

                return await task.ConfigureAwait(false);



            

             }
             catch (Exception ex)
             {

                new  Logging(applicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<SearchResultsViewModel> getwhoilike(ProfileModel model)
        {

            
                ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
               
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {

                     if (model.page == null | model.page == 0) model.page = 1;
                     if (model.numberperpage == null | model.numberperpage == 0) model.numberperpage = 4;

                     var MyActiveblocks = (from c in db.Repository<block>().Queryable().Where(p => p.profile_id == model.profileid && p.removedate == null)
                                           select new
                                           {
                                               ProfilesBlockedId = c.blockprofile_id
                                           }).ToList();

                     //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
                     //rematerialize on the back end.
                     //final query to send back only the profile datatas of the interests we want
                     var whoilike = (from f in db.Repository<like>().Queryable().Where(p => p.profile_id == model.profileid && p.deletedbymemberdate == null)
                                     //  join f in  db.Repository<profiledata>().Queryable() on p.likeprofile_id equals f.profile_id
                                     //  join z in  db.Repository<profile>().Queryable() on p.likeprofile_id equals z.id
                                     where (f.profilemetadata1.profile.status_id < 3 && !(MyActiveblocks.Count != 0 && f.profilemetadata1 != null))//&& !MyActiveblocks.Any(b => b.ProfilesBlockedId == f.profilemetadata1.profile_id)))
                                     orderby (f.creationdate) descending
                                     select new MemberSearchViewModel
                                     {
                                         likedate = f.creationdate,
                                         id = f.profilemetadata1.profile_id
                                         // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                     }).ToList();

                     int? pageint = model.page;
                     int? numberperpageint = model.numberperpage;

                     bool allowpaging = (whoilike.Count >= (pageint * numberperpageint) ? true : false);
                     var pageData = pageint > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(whoilike, pageint ?? 1, numberperpageint ?? 4) : whoilike.Take(numberperpageint.GetValueOrDefault());
                     //this.AddRange(pageData.ToList());
                     // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                     //return interests.ToList();
                     List<MemberSearchViewModel> results;
                     results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList(), allphotos = false },db,_storedProcedures).OrderByDescending(f => f.likedate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

                     return new SearchResultsViewModel { results = results, totalresults = whoilike.Count() };

                 });

                return await task.ConfigureAwait(false);


            

             }
             catch (Exception ex)
             {

                new  Logging(applicationEnum.MemberActionsService);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member actions service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
             }
       


         }

      
        }


        #region "update/check/change actions"


        /// <summary>
        ///  //returns a list of all mutal profiles i.e people who you both like 
        ///  //not inmplemented
        /// </summary 
        //work on this later
        public async Task<SearchResultsViewModel> getmutuallikes(ProfileModel model)
        {
                ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {

                     IEnumerable<MemberSearchViewModel> source = default(IEnumerable<MemberSearchViewModel>);
                     
                     if (model.page == null || model.page == 0) model.page = 1;
                     if (model.numberperpage == null || model.numberperpage == 0) model.numberperpage = 4;

                     bool allowpaging = (source.Count() >= (model.page * model.numberperpage) ? true : false);
                     var pageData = model.page > 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(source.ToList(), model.page ?? 1, model.numberperpage ?? 20) : source.Take(model.numberperpage.GetValueOrDefault());

                     //TO do pagge this
                     return new SearchResultsViewModel { results = source.ToList(), totalresults = source.Count() };
                     // return mutualblocks.ToList();

                 });

                return await task.ConfigureAwait(false);


            


             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task<bool>  checklike(ProfileModel model)
        {

                ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {

                 var task = Task.Factory.StartNew(() =>
                 {

                     return db.Repository<like>().Queryable().Any(r => r.profile_id == model.profileid && r.likeprofile_id == model.targetprofileid);

                 });
                return await task.ConfigureAwait(false);

               



             }
             catch (Exception ex)
             {

                   new  Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task addlike(ProfileModel model)
        {


          


            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            //create new inetrest object
                            like like = new like();
                            //make sure you are not trying to like at yourself
                            if (model.profileid.ToString() == model.targetprofileid.ToString()) return;

                            //check  like first  
                            //if this was a like being restored just do that part
                            var existinglike = db.Repository<like>().Queryable().Where(r => r.profile_id == model.profileid && r.likeprofile_id == model.targetprofileid).FirstOrDefault();

                            //just  update it if we have one already
                            if (existinglike != null)
                            {
                                existinglike.deletedbymemberdate = null; ;
                                existinglike.modificationdate = DateTime.Now;
                               db.Repository<like>().Update(existinglike);

                            }
                            else
                            {
                                //like = this. db.Repository<like>().Queryable().Where(p => p.profileid == profileid).FirstOrDefault();
                                //update the profile status to 2
                                like.profile_id = model.profileid.GetValueOrDefault();
                                like.likeprofile_id = model.targetprofileid.GetValueOrDefault();
                                like.mutual = false;  // not dealing with this calulatin yet
                                like.creationdate = DateTime.Now;
                                //handele the update using EF
                                // this. db.Repository<profile>().Queryable().AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                               db.Repository<like>().Insert(like);

                            }

                          var i =db.SaveChanges();
                           // transaction.Commit();

                          //  return true;

                        });

                      await task.ConfigureAwait(false);


                     

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task removelikebyprofileid(ProfileModel model)
        {


            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {


                           // var targetid = Convert.ToInt32(likeprofile_id);

                            var like = db.Repository<like>().Queryable().Where(p => p.profile_id == model.profileid && p.likeprofile_id == model.targetprofileid).FirstOrDefault();
                            //update the profile status to 2

                            like.deletedbymemberdate = DateTime.Now;
                            like.modificationdate = DateTime.Now;
                           db.Repository<like>().Update(like);

                          var i = db.SaveChanges();
                           // transaction.Commit();

                          //  return true;


                        });

                      await task.ConfigureAwait(false);

                       
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task removelikebylikeprofileid(ProfileModel model)
        {


            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {


                          //  var targetid = Convert.ToInt32(likeprofile_id);

                            var like = db.Repository<like>().Queryable().Where(p => p.profile_id == model.targetprofileid && p.likeprofile_id == model.profileid).FirstOrDefault();
                            //update the profile status to 2

                            like.deletedbylikedate = DateTime.Now;
                            like.modificationdate = DateTime.Now;
                           db.Repository<like>().Update(like);

                          var i = db.SaveChanges();
                           // transaction.Commit();

                           // return true;

                        });

                      await task.ConfigureAwait(false);

                     


                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task restorelikebyprofileid(ProfileModel model)
        {


            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {


                            //var targetid = Convert.ToInt32(likeprofile_id);

                            var like = db.Repository<like>().Queryable().Where(p => p.profile_id == model.profileid && p.likeprofile_id == model.targetprofileid).FirstOrDefault();
                            //update the profile status to 2

                            like.deletedbymemberdate = null;
                            like.modificationdate = DateTime.Now;

                           db.Repository<like>().Update(like);

                          var i =db.SaveChanges();
                           // transaction.Commit();

                           // return true;

                        });

                      await task.ConfigureAwait(false);

                     

                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task restorelikebylikeprofileid(ProfileModel model)
        {

            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {


                            //  var targetid = Convert.ToInt32(likeprofile_id);

                            var like = db.Repository<like>().Queryable().Where(p => p.profile_id == model.targetprofileid && p.likeprofile_id == model.profileid).FirstOrDefault();
                            //update the profile status to 2
                            //update the profile status to 2


                            like.deletedbylikedate = null;
                            like.modificationdate = DateTime.Now;

                           db.Repository<like>().Update(like);

                          var i =db.SaveChanges();
                           // transaction.Commit();

                           // return true;

                        });

                      await task.ConfigureAwait(false);



                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task removelikesbyprofileidandscreennames(ProfileModel model)
        {


            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {


                            // likes = this. db.Repository<like>().Queryable().Where(p => p.profileid == profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                            //update the profile status to 2
                            like like = new like();
                            foreach (string value in model.targetscreennames)
                            {

                                int? likeprofile_id = db.Repository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                                var currentlike = db.Repository<like>().Queryable().Where(p => p.profile_id == model.profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                                like.deletedbymemberdate = null;
                                like.modificationdate = DateTime.Now;
                               db.Repository<like>().Update(currentlike);

                            }

                          var i =db.SaveChanges();
                           // transaction.Commit();

                          //  return true;

                        });

                      await task.ConfigureAwait(false);


                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task restorelikesbyprofileidandscreennames(ProfileModel model)
        {


            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {

                            // likes = this. db.Repository<like>().Queryable().Where(p => p.profileid == profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                            //update the profile status to 2
                            like like = new like();
                            foreach (string value in model.targetscreennames)
                            {

                                int? likeprofile_id = db.Repository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                                var currentlike = db.Repository<like>().Queryable().Where(p => p.profile_id == model.profileid && p.likeprofile_id == likeprofile_id).FirstOrDefault();
                                like.deletedbymemberdate = null;
                                like.modificationdate = DateTime.Now;
                               db.Repository<like>().Update(currentlike);

                            }

                          var i =db.SaveChanges();
                           // transaction.Commit();

                           // return true;

                        });

                      await task.ConfigureAwait(false);

                       
                       
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                       new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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
        public async Task updatelikeviewstatus(ProfileModel model)
        {


            //update method code
             var db = _unitOfWorkAsync;
            {
              // //do not audit on adds
             //   using (var transaction = db.BeginTransaction())
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            //
                            //var targetid = Convert.ToInt32(modeltargetprofileid);

                            var like = db.Repository<like>().Queryable().Where(p => p.likeprofile_id == model.targetprofileid && p.profile_id == model.profileid).FirstOrDefault();
                            //update the profile status to 2            
                            if (like.viewdate == null)
                            {
                                like.viewdate = DateTime.Now;
                                like.modificationdate = DateTime.Now;
                               db.Repository<like>().Update(like);

                              var i =db.SaveChanges();
                               // transaction.Commit();
                            }

                        });
                      await task.ConfigureAwait(false);

                       
                     
              
                    }
                    catch (Exception ex)
                    {
                       // transaction.Rollback();
                        new  Logging(applicationEnum.MemberActionsService);
                        logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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

        #region "Agregate methods that pull i.e all settings"


        public async Task<MemberActionsModel> getmemberactionsbyprofileid(ProfileModel model)
        {
            var MemberActions = new MemberActionsModel();
          ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
            var db = _unitOfWorkAsync;
            {
                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {

                        //interest
                        MemberActions.whoiaminterestedintcount = memberactionsextentions.getwhoiaminterestedincount(model, db);      
                        MemberActions.interestcount =  memberactionsextentions.getwhoiaminterestedincount(model, db);                                        
                        MemberActions.interestnewcount =  memberactionsextentions.getwhoiaminterestedincount(model, db);

                        //peek
                        MemberActions.whoipeekedatcount = memberactionsextentions.getwhoipeekedatcount(model, db);
                        MemberActions.peekcount =  memberactionsextentions.getwhopeekedatmecount(model, db);
                        MemberActions.peeknewcount =  memberactionsextentions.getwhopeekedatmenewcount(model, db);

                        //like
                        MemberActions.whoilikecount =  memberactionsextentions.getwhoiaminterestedincount(model, db);
                        MemberActions.likecount =  memberactionsextentions.getwholikesmecount(model, db);
                        MemberActions.likenewcount =  memberactionsextentions.getwhoislikesmenewcount(model, db);

                        //block
                        MemberActions.blockcount =  memberactionsextentions.getwhoiblockedcount(model, db);
               

                        //TO do add this to the mail service as a separate call
                        //TO DO get mail count and new mail count
                        //First get the folder id of the user's inbox then you can get the messages as bellow

                        //place holder
                        int inboxfolderid = 1;

                        MemberActions.mailcount = mailextentions.getmailcountbyfolderid(model, inboxfolderid, db);

                        MemberActions.mailnewcount = mailextentions.getnewmailcountbyfolderid(model, inboxfolderid, db);

                        return  MemberActions;

                    });

                    return await task.ConfigureAwait(false);



                }
                catch (Exception ex)
                {

                    new Logging(applicationEnum.MemberActionsService);
                    logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, Convert.ToInt32(model.profileid));
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in member actions service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }

            }


        }

        #endregion


        #region "Search methods"



        #endregion


    }
}


//#region "Private methods used internally to this repo"
//private IQueryable<block> activeblocksbyprofileid(int profileid)
//{
//    //_unitOfWorkAsync.DisableProxyCreation = true;
//     var db = _unitOfWorkAsync;
//    {
//        try
//        {
//            IRepository<block> repo = db.Repository<block>();
//            //filter out blocked profiles 
//            return repo.Queryable().OfType<block>().Where(p =>p.profile_id == model.profileid && p.removedate != null);
//        }
//        catch (Exception ex)
//        {
//            //instantiate logger here so it does not break anything else.
//            new  Logging(applicationEnum.MemberActionsService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, profileid, null);
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


//       ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
//  var db = _unitOfWorkAsync;
// {
//     try
//     {
//         return (from p in  db.Repository<interest>().Queryable().Where(p =>p.interestprofile_id == model.profileid & p.deletedbymemberdate == null)
//                 join f in  db.Repository<profiledata>().Queryable() on p.profile_id equals f.profile_id
//                 join z in  db.Repository<profile>().Queryable() on p.profile_id equals z.id
//                 where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
//                 select new MemberSearchViewModel
//                 {
//                     interestdate = p.creationdate,
//                    id = f.profilemetadata1.profile_id 
//                     // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
//                 }).ToList();




//     }
//     catch (Exception ex)
//     {

//           new  Logging(applicationEnum.MemberActionsService);
//            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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

//       ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
//  var db = _unitOfWorkAsync;
// {
//     try
//     {
//           //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
//        //rematerialize on the back end.
//        //final query to send back only the profile datatas of the interests we want
//        return (from p in  db.Repository<interest>().Queryable().Where(p =>p.profile_id == model.profileid & p.deletedbymemberdate == null)
//                join f in  db.Repository<profiledata>().Queryable() on p.interestprofile_id equals f.profile_id
//                join z in  db.Repository<profile>().Queryable() on p.interestprofile_id equals z.id
//                where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
//                select new MemberSearchViewModel
//                {
//                    interestdate = p.creationdate,
//                   id = f.profilemetadata1.profile_id 
//                    // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
//                }).ToList();
//    }

//     catch (Exception ex)
//     {

//           new  Logging(applicationEnum.MemberActionsService);
//            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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

//       ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
//  var db = _unitOfWorkAsync;
// {
//     try
//     {
//         //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
//         //rematerialize on the back end.
//         //final query to send back only the profile datatas of the interests we want
//         return (from p in  db.Repository<peek>().Queryable().Where(p => p.peekprofile_id == model.profileid && p.deletedbymemberdate == null)
//                 join f in  db.Repository<profiledata>().Queryable() on p.profile_id equals f.profile_id
//                 join z in  db.Repository<profile>().Queryable() on p.profile_id equals z.id
//                 where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
//                 select new MemberSearchViewModel
//                 {
//                     peekdate = p.creationdate,
//                    id = f.profilemetadata1.profile_id 
//                     // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
//                 }).ToList();

//     }
//     catch (Exception ex)
//     {

//           new  Logging(applicationEnum.MemberActionsService);
//            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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

//       ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
//  var db = _unitOfWorkAsync;
// {
//     try
//     {

//         //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
//         //rematerialize on the back end.
//         //final query to send back only the profile datatas of the interests we want
//         return (from p in  db.Repository<peek>().Queryable().Where(p =>p.profile_id == model.profileid && p.deletedbymemberdate == null)
//                 join f in  db.Repository<profiledata>().Queryable() on p.peekprofile_id equals f.profile_id
//                 join z in  db.Repository<profile>().Queryable() on p.peekprofile_id equals z.id
//                 where (f.profile.status.id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
//                 select new MemberSearchViewModel
//                 {
//                     peekdate = p.creationdate,
//                    id = f.profilemetadata1.profile_id 
//                 }).ToList();

//     }
//     catch (Exception ex)
//     {

//           new  Logging(applicationEnum.MemberActionsService);
//            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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

//       ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
//  var db = _unitOfWorkAsync;
// {
//     try
//     {
//         return (from p in  db.Repository<block>().Queryable().Where(p =>p.profile_id == model.profileid && p.removedate == null)
//                 join f in  db.Repository<profiledata>().Queryable() on p.blockprofile_id equals f.profile_id
//                 join z in  db.Repository<profile>().Queryable() on p.blockprofile_id equals z.id
//                 where (f.profile.status.id < 3)
//                 orderby (p.creationdate) descending
//                 select new MemberSearchViewModel
//                 {
//                     blockdate = p.creationdate,
//                    id = f.profilemetadata1.profile_id 
//                 }).ToList();


//     }
//     catch (Exception ex)
//     {

//           new  Logging(applicationEnum.MemberActionsService);
//            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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

//       ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
//  var db = _unitOfWorkAsync;
// {
//     try
//     {
//         //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
//         //rematerialize on the back end.
//         //final query to send back only the profile datatas of the interests we want
//         return (from p in  db.Repository<like>().Queryable().Where(p => p.likeprofile_id == model.profileid && p.deletedbylikedate == null)
//                 join f in  db.Repository<profiledata>().Queryable() on p.profile_id equals f.profile_id
//                 join z in  db.Repository<profile>().Queryable() on p.profile_id equals z.id
//                 where (f.profile.status_id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
//                 orderby (p.creationdate) descending
//                 select new MemberSearchViewModel
//                 {
//                     likedate = p.creationdate,
//                    id = f.profilemetadata1.profile_id 
//                 }).ToList();

//     }
//     catch (Exception ex)
//     {

//           new  Logging(applicationEnum.MemberActionsService);
//            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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

//       ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
//  var db = _unitOfWorkAsync;
// {
//     try
//     {
//         //TO DO add the POCO types like members search model to these custom classes so we can do it in one query instead of having to
//         //rematerialize on the back end.
//         //final query to send back only the profile datatas of the interests we want
//         return (from p in  db.Repository<like>().Queryable().Where(p =>p.profile_id == model.profileid && p.deletedbymemberdate == null)
//                 join f in  db.Repository<profiledata>().Queryable() on p.likeprofile_id equals f.profile_id
//                 join z in  db.Repository<profile>().Queryable() on p.likeprofile_id equals z.id
//                 where (f.profile.status_id < 3 && !MyActiveblocks.Any(b => b.blockprofile_id == f.profile_id))
//                 orderby (p.creationdate) descending
//                 select new MemberSearchViewModel
//                 {
//                     likedate = p.creationdate,
//                    id = f.profilemetadata1.profile_id 
//                 }).ToList();


//     }
//     catch (Exception ex)
//     {

//           new  Logging(applicationEnum.MemberActionsService);
//            logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, Convert.ToInt32(model.profileid));
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