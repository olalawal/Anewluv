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
using Repository.Pattern.Infrastructure;

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

        public MemberActionsService([IAnewluvEntitesScope]IUnitOfWorkAsync unitOfWork, [ISpatialEntitesScope]IGeoDataStoredProcedures storedProcedures)
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
        public async Task<int> getmyactioncount(ProfileModel model)
        {
          
               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {

                     //return memberactionsextentions.getmyactionbyprofileidandactiontype(model, db, model.actiontypeid.Value).Count();
                     return db.Repository<action>().getmyactionsbyprofileidandactiontype(model.profileid.Value, model.actiontypeid.Value).Select(f => f.target_profile_id).Distinct().Count();

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
        /// count all total interests , this is distinct count , for premuim we can do non distinct
        /// </summary>       
        public async Task<int> getothersactioncount(ProfileModel model)
        {

               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 //distinct counts 
                 var task = Task.Factory.StartNew(() =>
                 {

                     return db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, model.actiontypeid.Value).Select(f=>f.creator_profile_id).Distinct().Count();
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
        public async Task<int> getothersactioncountnew(ProfileModel model)
        {

               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {
                     return db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, model.actiontypeid.Value).Where(p => p.viewdate != null).Select(f=>f.creator_profile_id).Distinct().Count();    

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
        public async Task<SearchResultsViewModel> getmyaction(ProfileModel model)
        {

            
           
             var db = _unitOfWorkAsync;
            {

                try
                {
                    var task = Task.Factory.StartNew(() =>
                    {

                        if (model.page == null | model.page == 0) model.page = 1;
                        if (model.numberperpage == null | model.numberperpage == 0) model.numberperpage = 4;

                        var interests = memberactionsextentions.getmyactiveactionprofilesbyprofileidandactiontype(model, db, model.actiontypeid.Value, model.unviewedactions.GetValueOrDefault()).Select
                         (f => new MemberSearchViewModel
                                             {
                                                 interestdate = f.creationdate,
                                                 id = f.profilemetadata.profile_id
                                                 // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                                             }).Distinct().ToList();


                        int? pageint = model.page;
                        int? numberperpageint = model.numberperpage;

                        bool allowpaging = (interests.Count > numberperpageint) ? true : false;
                        var pageData = pageint >= 1 & allowpaging ?
                            new PaginatedList<MemberSearchViewModel>().GetCurrentPages(interests, pageint ?? 1, numberperpageint ?? 4) : interests.Take(numberperpageint.GetValueOrDefault());
                        //this.AddRange(pageData.ToList());
                        // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                        //return interests.ToList();
                        List<MemberSearchViewModel> results;



                        results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList() }, db, _storedProcedures).OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();
                        
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
        public async Task<SearchResultsViewModel> getothersaction(ProfileModel model)
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


                     var whoisinterestedinme = memberactionsextentions.getactiveotheractionprofilesbyprofileidandactiontype(model, db, model.actiontypeid.Value,model.unviewedactions.GetValueOrDefault()).Select
                         (f => new MemberSearchViewModel
                         {
                             interestdate = f.creationdate,
                             id = f.profilemetadata.profile_id
                             // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                         }).ToList();


                     int? pageint = Convert.ToInt32(model.page);
                     int? numberperpageint = Convert.ToInt32(model.numberperpage);

                     bool allowpaging = (whoisinterestedinme.Count >  numberperpageint) ? true : false;
                     var pageData = pageint >= 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(whoisinterestedinme, pageint ?? 1, numberperpageint ?? 4) : whoisinterestedinme.Take(numberperpageint.GetValueOrDefault());
                     //this.AddRange(pageData.ToList());
                     // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                     //return interests.ToList();
                     List<MemberSearchViewModel> results;
                     results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList() }, db, _storedProcedures).OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

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
        public async Task<SearchResultsViewModel> getothersactionnew(ProfileModel model)
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


                     int? pageint = model.page;
                     int? numberperpageint = model.numberperpage;

                     var whoisinterestedinmenew = memberactionsextentions.getactiveotheractionprofilesbyprofileidandactiontype(model, db, model.actiontypeid.Value, true).Select
                      (f => new MemberSearchViewModel
                      {
                          interestdate = f.creationdate,
                          id = f.profilemetadata.profile_id
                          // perfectmatchsettings = f.profilemetadata.searchsettings.Where(g => g.myperfectmatch == true).FirstOrDefault()   //GetPerFectMatchprofilemetadata.searchsettingsByprofileid(p.profileid )
                      }).ToList();



                     bool allowpaging = (whoisinterestedinmenew.Count > numberperpageint) ? true : false;
                     var pageData = pageint >= 1 & allowpaging ?
                         new PaginatedList<MemberSearchViewModel>().GetCurrentPages(whoisinterestedinmenew, pageint ?? 1, numberperpageint ?? 4) : whoisinterestedinmenew.Take(numberperpageint.GetValueOrDefault());
                     //this.AddRange(pageData.ToList());
                     // var pagedinterests = interests.OrderByDescending(f => f.interestdate.Value).Skip((Page ?? 1 - 1) * NumberPerPage ?? 4).Take(NumberPerPage ?? 4).ToList();

                     //return interests.ToList();
                     List<MemberSearchViewModel> results;
                     results = membermappingextentions.mapmembersearchviewmodels(new ProfileModel { profileid = model.profileid, modelstomap = pageData.ToList() }, db, _storedProcedures).OrderByDescending(f => f.interestdate.Value).ThenByDescending(f => f.lastlogindate.Value).ToList();

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
        public async Task<SearchResultsViewModel> getmutualactions(ProfileModel model)
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

                     bool allowpaging = (source.Count() >  model.numberperpage) ? true : false;
                     var pageData = model.page >= 1 & allowpaging ?
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
        public async Task<bool> checkaction(ProfileModel model)
        {

               ////  _unitOfWorkAsync.DisableProxyCreation = false; _unitOfWorkAsync.DisableLazyLoading = false;
          var db = _unitOfWorkAsync;
         {
             try
             {
                 var task = Task.Factory.StartNew(() =>
                 {

                  return db.Repository<action>().getmyactionsbyprofileidandactiontype(model.profileid.GetValueOrDefault(),model.actiontypeid.Value) 
                         .Any(r => r.id == model.targetprofileid);

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
        /// Adds a new action for any user this is the only ADD function
        /// </summary>
        public async Task addmyaction(ProfileModel model)
        {

            bool updated = false;
            //update method code
             var db = _unitOfWorkAsync;
            {
             
                    try
                    {
                       var activitylist = new List<ActivityModel>(); OperationContext ctx = OperationContext.Current;
                        var task = Task.Factory.StartNew(() =>
                        {


                            //create new inetrest object
                          //  interest interest = new interest();
                            //make sure you are not trying to interest at yourself
                            if ((model.profileid !=null & model.targetprofileid !=null) &&  model.profileid.ToString() == model.targetprofileid.ToString()) return;


                            //check  interest first  
                            //if this was a interest being restored just do that part
                            //var existinginterest =  memberactionsextentions.getotheractionbyprofileidandactiontype(model, db, model.actiontypeid.Value)
                            //.Where (r => r.id == model.targetprofileid).FirstOrDefault();

                           
                            //only update undleted peeks to keep the view date fresh
                            //just  update it if we have one already
                            //only change the view date since we changed only that and modify date
                            //if (existinginterest != null && model.actiontypeid ==  (int)actiontypeEnum.Peek)
                            //{
                            //    //get the actual action
                            //   var action =  db.Repository<action>().Queryable()
                            //       .Where(z=>z.creator_profile_id == model.profileid && z.target_profile_id == existinginterest.id).FirstOrDefault();

                            //    //action.deletedbycreatordate = null; ;
                            //    action.modificationdate = DateTime.Now;
                            //    action.viewdate = DateTime.Now;  //REST view ?
                            //    updated = true;
                            //    db.Repository<action>().Update(action);

                            //}
                            //else
                            //{
                                updated = true;
                                var newaction = new action();
                                var newnote = new note();
                                newaction.ObjectState = ObjectState.Added;
                                newaction.creator_profile_id = model.profileid.Value;
                                newaction.target_profile_id = model.targetprofileid.GetValueOrDefault();
                                newaction.actiontype_id = (int)model.actiontypeid;
                                newaction.active = true;
                                //TO DO add notes if posible
                                if (model.note != null &&  model.note !="" )
                                {
                                    newaction.notes.Add(new note { action_id = newaction.id, notedetail = model.note, creationdate = DateTime.Now, notetype_id = (int)notetypeEnum.UserActionAttachment, ObjectState = ObjectState.Added });
                                }
                                newaction.creationdate = DateTime.Now;
                                //handele the update using EF
                                // this. db.Repository<profile>().Queryable().AttachAsModified(Profile, this.ChangeSet.GetOriginal(Profile));
                                db.Repository<action>().Insert(newaction);

                                //int? currentactivitytype = null;
                                // switch ( model.actiontypeid.GetValueOrDefault())
                                //{
                                //    case (int)actiontypeEnum.Like:
                                //        currentactivitytype = (int)activitytypeEnum.sentlike;
                                //        break;
                                //    case (int)actiontypeEnum.Peek:
                                //        currentactivitytype = (int)activitytypeEnum.pe;
                                //        break;
                                //    case (int)actiontypeEnum.Interest:
                                //        currentactivitytype = (int)activitytypeEnum.sentlike;
                                //        break;
                                //    case (int)actiontypeEnum.Peek:
                                //        currentactivitytype = (int)activitytypeEnum.sentlike;
                                //        break;
                                //    case (int)actiontypeEnum.Peek:
                                //        currentactivitytype = (int)activitytypeEnum.sentlike;
                                //        break;
                                //    case (int)actiontypeEnum.Peek:
                                //        currentactivitytype = (int)activitytypeEnum.sentlike;
                                //        break;
                                //    case (int)actiontypeEnum.Peek:
                                //        currentactivitytype = (int)activitytypeEnum.sentlike;
                                //        break;
                                //    default:
                                //        currentactivitytype = null;
                                //        break;
                                //}

                                // activitylist.Add(Api.AnewLuvLogging.CreateActivity(model.profileid.GetValueOrDefault(), currentactivitytype,ctx));}

                          //  }

                            if (updated)
                              db.SaveChanges();
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

        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
        public async Task removemyactionbyprofileid(ProfileModel model)
        {

            bool updated = false;
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

                            var results = db.Repository<action>().getmyactionsbyprofileidandactiontype(model.profileid.Value, model.actiontypeid.Value).Where(p => p.creator_profile_id == model.targetprofileid);
                            // memberactionsextentions.getmyactionbyprofileidandactiontype(model, db, model.actiontypeid.Value).Where(p => p.id == profileid); //.FirstOrDefault();
                            //update the profile status to 2

                            if (results != null)
                            {
                                //find all new ones to disable
                                foreach (action action in results)
                                {
                                    updated = true;
                                    //get the actual action                                      
                                    action.deletedbycreatordate = DateTime.Now; ;
                                    action.modificationdate = DateTime.Now;
                                    action.active = false;
                                    db.Repository<action>().Update(action);
                                }
                            }



                            if (updated)
                                db.SaveChangesAsync();
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
        ///  Update action with view , basically profile views ,i.e if somone is your interest and they viewed your profile update this view type if they come from a specific page
        /// </summary 
        public async Task updateotheractionviewstatus(ProfileModel model)
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

                            var action =  db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value,model.actiontypeid.Value).Where(p => p.target_profile_id == model.targetprofileid.Value).FirstOrDefault();
                            //update the profile status to 2

                            //if (interest != null)
                          //  {
                                //get the actual action
                             //   var action = db.Repository<action>().Queryable()
                             //       .Where(z => z.creator_profile_id == interest.id && z.target_profile_id == model.profileid).FirstOrDefault();

                                if (action.viewdate == null)
                                {
                                    action.modificationdate = DateTime.Now;
                                    action.viewdate = DateTime.Now;
                                    db.Repository<action>().Update(action);
                                    var i = db.SaveChanges();
                                }
                            //}

                       

                        });

                        await task.ConfigureAwait(false);



                    }
                    catch (Exception ex)
                    {
                        // transaction.Rollback();
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
        }


        /// <summary>
        ///  //Removes an interest i.e changes the interest to deleted so they do not shwo up to you anymore unless filtered in that person anymore
        ///  Right now it is a straight delete no history i.e you could keep spamming but they can interest u
        ///  //not inmplemented
        /// </summary 
         public async Task removeothersactionbyprofileid(ProfileModel model)
        {

             bool updated = false;
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
                            //DO not allow to remove blocks
                            if ((model.actiontypeid  == (int)actiontypeEnum.Block)) return;


                            var results = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value,(int)model.actiontypeid.Value).Where(p=>p.creator_profile_id == model.targetprofileid ).ToList();
                                 // memberactionsextentions.getmyactionbyprofileidandactiontype(model, db, model.actiontypeid.Value).Where(p => p.id == profileid); //.FirstOrDefault();
                                 //update the profile status to 2

                                 if (results != null)
                                 {
                                     //find all new ones to disable
                                     foreach (action action in results)
                                     {
                                         updated = true;
                                         action.deletedbytargetdate = DateTime.Now; ;
                                         action.modificationdate = DateTime.Now;
                                         action.active = false;
                                         db.Repository<action>().Update(action);

                                     }
                                 }
                             
                             if (updated)
                            db.SaveChangesAsync();

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
     

        /// <summary>
        ///  //Removes an iterest i.e makes you not interested in that person anymore
        ///  //removed multiples 
        /// </summary 
         public async Task restoreothersactionsbyprofileidandscreennames(ProfileModel model)
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


                            //// interests = this. db.Repository<interest>().Queryable().Where(p => p.profileid == profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                            ////update the profile status to 2
                            //interest interest = new interest();
                            //foreach (string value in model.targetscreennames)
                            //{
                            //    int? interestprofile_id = db.Repository<profile>().getprofilebyscreenname(new ProfileModel { screenname = value }).id;
                            //    var currentinterest = db.Repository<interest>().Queryable().Where(p => p.profile_id == model.profileid && p.interestprofile_id == interestprofile_id).FirstOrDefault();
                            //    interest.deletedbymemberdate = null;
                            //    interest.modificationdate = DateTime.Now;
                            //   db.Repository<interest>().Update(currentinterest);

                            //}

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

      
        #endregion

         #region "bulk operations"


         /// <summary>
         ///  bulk removeal of the users actions towards other users. i.e mypeeks , myinterests i.e
         /// </summary 
         public async Task removemyactionbyprofileidbulk(ProfileModel model)
         {

             bool updated = false;
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

                             //dont allow regular members to rmove peeks they made maybe later gold or plat members can
                             if ((model.actiontypeid == (int)actiontypeEnum.Peek)) return;


                             foreach (string id in model.profileids)
                             {

                              int profileid = Convert.ToInt32(id);
                                 //changed to allow adding constantly new items so we need to reset all the others
                              //get your actions to this specfic profile ID of this action type
                              var results = db.Repository<action>().getmyactionsbyprofileidandactiontype(model.profileid.Value, model.actiontypeid.Value).Where(p => p.target_profile_id == profileid).ToList();

                             // memberactionsextentions.getmyactionbyprofileidandactiontype(model, db, model.actiontypeid.Value).Where(p => p.id == profileid); //.FirstOrDefault();
                             //update the profile status to 2

                              if (results != null)
                             {
                                  //find all new ones to disable
                                  foreach (action action in results )
                                  {
                                      updated = true;
                                      //get the actual action                                      
                                      action.deletedbycreatordate = DateTime.Now; ;
                                      action.modificationdate = DateTime.Now;
                                      action.active = false;                                
                                      db.Repository<action>().Update(action);
                                  }
                             }


                             if (updated)
                             db.SaveChangesAsync();
                             // transaction.Commit();
                             }
                         });

                         await task.ConfigureAwait(false);




                     }
                     catch (Exception ex)
                     {
                         // transaction.Rollback();
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


         }

        /// <summary>
        ///  bulk removal of others actions to a user , i.r you dont want to see the likes of another user in you list they will be removed
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         public async Task removeothersactionnbyprofileidbulk(ProfileModel model)
         {

              bool updated = false;
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
                             //DO not allow to remove blocks
                             if ((model.actiontypeid == (int)actiontypeEnum.Block)) return;


                             foreach (string id in model.profileids)
                             {

                                 var otherprofileid =  Convert.ToInt32(id);
                                 //get all this users matching actions to the 
                                 var results = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value,model.actiontypeid.Value).Where(p => p.creator_profile_id == otherprofileid ).ToList() ;

                                 // memberactionsextentions.getmyactionbyprofileidandactiontype(model, db, model.actiontypeid.Value).Where(p => p.id == profileid); //.FirstOrDefault();
                                 //update the profile status to 2

                                 if (results != null)
                                 {
                                     //find all new ones to disable
                                     foreach (action action in results)
                                     {
                                         updated = true;
                                         action.deletedbytargetdate = DateTime.Now; ;
                                         action.modificationdate = DateTime.Now;
                                         action.active = false;
                                         db.Repository<action>().Update(action);

                                     }
                                 }
                             }
                             if (updated)
                            db.SaveChangesAsync();
                             // transaction.Commit();

                             //  return true;

                         });
                         await task.ConfigureAwait(false);




                     }
                     catch (Exception ex)
                     {
                         // transaction.Rollback();
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


         }

        #endregion


         //end of basica action methods
        //********************************************************

       
      
        

        

        #region "Agregate methods that pull i.e all settings"

        
        /// <summary>
        /// TO DO distinct on these 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                        MemberActions.whoiaminterestedintcount = db.Repository<action>().getmyactionsbyprofileidandactiontype(model.profileid.Value,(int)actiontypeEnum.Interest).Select(f=>f.target_profile_id).Distinct().Count();
                        MemberActions.interestcount = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Interest).Count();
                        MemberActions.interestnewcount = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Interest).Where(z => z.viewdate == null).Count();

                        //peek
                        MemberActions.whoipeekedatcount = db.Repository<action>().getmyactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Peek).Select(f => f.target_profile_id).Distinct().Count();
                        MemberActions.peekcount = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Peek).Count();
                        MemberActions.peeknewcount = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Peek).Where(z => z.viewdate == null).Count();

                        //like
                        MemberActions.whoilikecount = db.Repository<action>().getmyactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Like).Select(f => f.target_profile_id).Distinct().Count();
                        MemberActions.likecount = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Like).Count();
                        MemberActions.likenewcount = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Like).Where(z=>z.viewdate ==null).Count();

                        //block
                        MemberActions.blockcount = db.Repository<action>().getothersactionsbyprofileidandactiontype(model.profileid.Value, (int)actiontypeEnum.Block).Select(f => f.target_profile_id).Distinct().Count();
               

                        //TO do add this to the mail service as a separate call
                        //TO DO get mail count and new mail count
                        //First get the folder id of the user's inbox then you can get the messages as bellow

                        //place holder
                       
                        var allmails = _unitOfWorkAsync.Repository<mailboxfolder>().getmailfolderdetails(
                            new MailModel { profileid = model.profileid.Value, mailboxfolderid = (int)mailfoldertypeEnum.Inbox },db );
                        //TO DO maybe do this on client

                        if (allmails != null)
                        {
                          //  var sentfolderdetail = allmails.folders.Where(z => z.folderid == (int)mailfoldertypeEnum.Sent).FirstOrDefault();
                            var recivedfolderdetail = allmails.folders.Where(z => z.folderid == (int)mailfoldertypeEnum.Inbox).FirstOrDefault();
                           
                           //MemberActions.mailsentcount = sentfolderdetail != null ? sentfolderdetail.totalmessagecount : null;
                            MemberActions.mailreceivedcount = recivedfolderdetail != null ? recivedfolderdetail.totalmessagecount : null;
                            MemberActions.mailreceivednewcount = recivedfolderdetail != null ? recivedfolderdetail.undreadmessagecount : null;
                        }
                        
                      

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