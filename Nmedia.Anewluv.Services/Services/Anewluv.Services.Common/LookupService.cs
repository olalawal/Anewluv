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
using Anewluv.Domain.Data;
using Nmedia.DataAccess.Interfaces;
using Anewluv.Caching;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data.log;
using Nmedia.Infrastructure.Domain.Data;

namespace Anewluv.Services.Common
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LookupService : ILookupService
    {


  //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        IUnitOfWork _unitOfWork;
       // private LoggingLibrary.Logging logger;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public LookupService(IUnitOfWork unitOfWork)
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


        #region "new lookups that come from the lu_tables that have to be displayed i.e such as photoformats etc"



        public List<lu_photoformat> getphotoformatlist()
        {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
               {
                  
                   try
                   {

                       
                    #if DISCONECTED
                                    List<lu_photoformat> photoformatlist = new List<lu_photoformat>();
                                    photoformatlist.Add(new lu_photoformat { description = "Male",  id  = 1, selected   = false });
                                    photoformatlist.Add(new lu_photoformat { description = "Female", id = 2, selected = false });
                                    return photoformatlist;
                
                    #else
                      return CachingFactory.SharedObjectHelper.getphotoformatlist(db);
                // return temp;
                   #endif
                   }               
                   catch (Exception ex)
                   {

                        using (var logger =  new  Logging(applicationEnum.LookupService))
                        {
                           logger.WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        }
                   
                        FaultReason faultreason = new FaultReason("Error in Lookup service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                   }
                  
    
            }

        }


        
        public List<lu_photoapprovalstatus> getphotoapprovalstatuslist()
        {


            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_photoapprovalstatus> photoapprovalstatuslist = new List<lu_photoapprovalstatus>();
                photoapprovalstatuslist.Add(new lu_photoapprovalstatus { description = "Male",  id  = 1, selected   = false });
                photoapprovalstatuslist.Add(new lu_photoapprovalstatus { description = "Female", id = 2, selected = false });
                return photoapprovalstatuslist;
                
#else
                    return CachingFactory.SharedObjectHelper.getphotoapprovalstatuslist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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
        public List<lu_photorejectionreason> getphotorejecttionreasonlist()
        {


            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_photorejectionreason> photorejectionreasonlist = new List<lu_photorejectionreason>();
                photorejectionreasonlist.Add(new lu_photorejectionreason { description = "Male",  id  = 1, selected   = false });
                photorejectionreasonlist.Add(new lu_photorejectionreason { description = "Female", id = 2, selected = false });
                return photorejectionreasonlist;
                
#else
                    return CachingFactory.SharedObjectHelper.getphotorejectionreasonlist(db);
                    // return temp;
#endif


                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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



        public List<lu_photostatus> getphotostatuslist()
        {


            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_photostatus> photostatuslist = new List<lu_photostatus>();
                photostatuslist.Add(new lu_photostatus { description = "Male",  id  = 1, selected   = false });
                photostatuslist.Add(new lu_photostatus { description = "Female", id = 2, selected = false });
                return photostatuslist;
                
#else
                    return CachingFactory.SharedObjectHelper.getphotostatuslist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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



        public List<lu_photoimagetype> getphotoimagetypeslist()
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_photoimagetype> photoimagetypelist = new List<lu_photoimagetype>();
                photoimagetypelist.Add(new lu_photoimagetype { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_photoimagetype { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return CachingFactory.SharedObjectHelper.getphotoimagetypelist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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

        //olawawl 3-13-2012 added more items 
        public List<lu_photostatusdescription> getphotostatusdescriptionlist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_photostatusdescription> photostatusdescriptionlist = new List<lu_photostatusdescription>();
                photostatusdescriptionlist.Add(new lu_photostatusdescription { description = "Male",  id  = 1, selected   = false });
                photostatusdescriptionlist.Add(new lu_photostatusdescription { description = "Female", id = 2, selected = false });
                return photostatusdescriptionlist;
                
#else
                    return CachingFactory.SharedObjectHelper.getphotostatusdescriptionlist(db);
                    // return temp;
#endif

                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_abusetype> getabusetypelist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_abusetype> abusetypelist = new List<lu_abusetype>();
                abusetypelist.Add(new lu_abusetype { description = "Male",  id  = 1, selected   = false });
                abusetypelist.Add(new lu_abusetype { description = "Female", id = 2, selected = false });
                return abusetypelist;
                
#else
                    return CachingFactory.SharedObjectHelper.getabusetypelist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_photoImagersizerformat> getphotoImagersizerformatlist()
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_photoImagersizerformat> getphotoImagersizerformatlist = new List<lu_photoImagersizerformat>();
                getphotoImagersizerformatlist.Add(new lu_photoImagersizerformat { description = "Male",  id  = 1, selected   = false });
                getphotoImagersizerformatlist.Add(new lu_photoImagersizerformat { description = "Female", id = 2, selected = false });
                return getphotoImagersizerformatlist;
                
#else
                    return CachingFactory.SharedObjectHelper.getphotoImagersizerformatlist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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
        public List<lu_profilestatus> getprofilestatuslist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_profilestatus> photoimagetypelist = new List<lu_profilestatus>();
                photoimagetypelist.Add(new lu_profilestatus { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_profilestatus { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return CachingFactory.SharedObjectHelper.getprofilestatuslist(db);
                    // return temp;
#endif

                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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



        public List<lu_role> getrolelist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_role> photoimagetypelist = new List<lu_role>();
                photoimagetypelist.Add(new lu_role { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_role { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return CachingFactory.SharedObjectHelper.getrolelist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_securityleveltype> getsecurityleveltypelist()
        {


            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_securityleveltype> photoimagetypelist = new List<lu_securityleveltype>();
                photoimagetypelist.Add(new lu_securityleveltype { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_securityleveltype { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return CachingFactory.SharedObjectHelper.getsecurityleveltypelist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_showme> getshowmelist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_showme> photoimagetypelist = new List<lu_showme>();
                photoimagetypelist.Add(new lu_showme { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_showme { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return CachingFactory.SharedObjectHelper.getshowmelist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_sortbytype> getsortbytypelist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_sortbytype> photoimagetypelist = new List<lu_sortbytype>();
                photoimagetypelist.Add(new lu_sortbytype { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_sortbytype { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return CachingFactory.SharedObjectHelper.getsortbytypelist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_securityquestion> getsecurityquestionlist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_securityquestion> photoimagetypelist = new List<lu_securityquestion>();
                photoimagetypelist.Add(new lu_securityquestion { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_securityquestion { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return CachingFactory.SharedObjectHelper.getsecurityquestionlist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_flagyesno> getflagyesnolist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_flagyesno> photoimagetypelist = new List<lu_flagyesno>();
                photoimagetypelist.Add(new lu_flagyesno { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_flagyesno { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return CachingFactory.SharedObjectHelper.getflagyesnolist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_profilefiltertype> getprofilefiltertypelist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_profilefiltertype> photoimagetypelist = new List<lu_profilefiltertype>();
                photoimagetypelist.Add(new lu_profilefiltertype { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_profilefiltertype { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return CachingFactory.SharedObjectHelper.getprofilefiltertypelist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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

        #endregion

        #region "Load Other misc stuff such as list of pages etc"

        public List<systempagesetting> getsystempagesettinglist()
        {



            List<systempagesetting> temp = new List<systempagesetting>();

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
            
                systempagesetting virtualpage = new systempagesetting();
                virtualpage.title  = "MembersHome";
                virtualpage.bodycssstylename  = "StandardWhiteBackground";

                temp.Add(virtualpage);
                          
                            return temp;

#else  //load list from database
                    temp = CachingFactory.CssStyleSelector.getsystempagesettingslist(db);
                    return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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

        public string getbodycssbypagename(string pagename)
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

                    return CachingFactory.CssStyleSelector.getbodycssbypagename(pagename, db).ToString();


                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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

        #endregion

        #region "generic lookup and collections"


        public List<lu_gender> getgenderlist()
        {
            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_gender> genderlist = new List<lu_gender>();
                genderlist.Add(new lu_gender { description = "Male",  id  = 1, selected   = false });
                genderlist.Add(new lu_gender { description = "Female", id = 2, selected = false });
                return genderlist;                
#else
                    return CachingFactory.SharedObjectHelper.getgenderlist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<age> getageslist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
                    return CachingFactory.SharedObjectHelper.getagelist();


                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<metricheight> getmetricheightlist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

                    return CachingFactory.SharedObjectHelper.getmetricheightlist();

                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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

        #endregion


        #region "Criteria Appearance dropdowns"



        public List<lu_bodytype> getbodytypelist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_bodytype> bodytypelist = new List<lu_bodytype>();
                bodytypelist.Add(new lu_bodytype { description = "Male",  id  = 1, selected   = false });
                bodytypelist.Add(new lu_bodytype { description = "Female", id = 2, selected = false });
                return bodytypelist;
                
#else
                    return CachingFactory.SharedObjectHelper.getbodytypelist(db);
                    // return temp;
#endif

                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_ethnicity> getethnicitylist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
            List<lu_ethnicity> ethnicitylist = new List<lu_ethnicity>();
            ethnicitylist.Add(new lu_ethnicity { description = "Male", id = 1, selected = false });
            ethnicitylist.Add(new lu_ethnicity { description = "Female", id = 2, selected = false });
            return ethnicitylist;

#else
                    return CachingFactory.SharedObjectHelper.getethnicitylist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_eyecolor> geteyecolorlist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_eyecolor> eyecolorlist = new List<lu_eyecolor>();
                eyecolorlist.Add(new lu_eyecolor { description = "Male",  id  = 1, selected   = false });
                eyecolorlist.Add(new lu_eyecolor { description = "Female", id = 2, selected = false });
                return eyecolorlist;
                
#else
                    return CachingFactory.SharedObjectHelper.geteyecolorlist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_haircolor> gethaircolorlist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_haircolor> haircolorlist = new List<lu_haircolor>();
                haircolorlist.Add(new lu_haircolor { description = "Male",  id  = 1, selected   = false });
                haircolorlist.Add(new lu_haircolor { description = "Female", id = 2, selected = false });
                return haircolorlist;
                
#else
                    return CachingFactory.SharedObjectHelper.gethaircolorlist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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

        #endregion

        #region "Criteria Character Dropdowns"

        public List<lu_diet> getdietlist()
        {


            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_diet> dietlist = new List<lu_diet>();
                dietlist.Add(new lu_diet { description = "Male",  id  = 1, selected   = false });
                dietlist.Add(new lu_diet { description = "Female", id = 2, selected = false });
                return dietlist;
                
#else
                    return CachingFactory.SharedObjectHelper.getdietlist(db);
                    //  return db.lu_diet.OrderBy(x => x.description).ToList();



                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_drinks> getdrinkslist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_drinks> drinkslist = new List<lu_drinks>();
                drinkslist.Add(new lu_drinks { description = "Male",  id  = 1, selected   = false });
                drinkslist.Add(new lu_drinks { description = "Female", id = 2, selected = false });
                return drinkslist;
                
#else
                    return CachingFactory.SharedObjectHelper.getdrinkslist(db);



                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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
        public List<lu_exercise> getexerciselist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_exercise> exerciselist = new List<lu_exercise>();
                exerciselist.Add(new lu_exercise { description = "Male",  id  = 1, selected   = false });
                exerciselist.Add(new lu_exercise { description = "Female", id = 2, selected = false });
                return exerciselist;
                
#else
                    return CachingFactory.SharedObjectHelper.getexerciselist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_hobby> gethobbylist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_hobby> hobbylist = new List<lu_hobby>();
                hobbylist.Add(new lu_hobby { description = "Male",  id  = 1, selected   = false });
                hobbylist.Add(new lu_hobby { description = "Female", id = 2, selected = false });
                return hobbylist;
                
#else

                    return CachingFactory.SharedObjectHelper.gethobbylist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_humor> gethumorlist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_humor> humorlist = new List<lu_humor>();
                humorlist.Add(new lu_humor { description = "Male",  id  = 1, selected   = false });
                humorlist.Add(new lu_humor { description = "Female", id = 2, selected = false });
                return humorlist;
                
#else

                    return CachingFactory.SharedObjectHelper.gethumorlist(db); ;

                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_politicalview> getpoliticalviewlist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_politicalview> politicalviewlist = new List<lu_politicalview>();
                politicalviewlist.Add(new lu_politicalview { description = "Male",  id  = 1, selected   = false });
                politicalviewlist.Add(new lu_politicalview { description = "Female", id = 2, selected = false });
                return politicalviewlist;
                
#else
                    return CachingFactory.SharedObjectHelper.getpoliticalviewlist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_religion> getreligionlist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_religion> religionlist = new List<lu_religion>();
                religionlist.Add(new lu_religion { description = "Male",  id  = 1, selected   = false });
                religionlist.Add(new lu_religion { description = "Female", id = 2, selected = false });
                return religionlist;
                
#else
                    return CachingFactory.SharedObjectHelper.getreligionlist(db);

                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_religiousattendance> getreligiousattendancelist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_religiousattendance> religiousattendancelist = new List<lu_religiousattendance>();
                religiousattendancelist.Add(new lu_religiousattendance { description = "Male",  id  = 1, selected   = false });
                religiousattendancelist.Add(new lu_religiousattendance { description = "Female", id = 2, selected = false });
                return religiousattendancelist;
                
#else
                    return CachingFactory.SharedObjectHelper.getreligiousattendancelist(db);
                    // return temp;
#endif

                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_sign> getsignlist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_sign> signlist = new List<lu_sign>();
                signlist.Add(new lu_sign { description = "Male",  id  = 1, selected   = false });
                signlist.Add(new lu_sign { description = "Female", id = 2, selected = false });
                return signlist;
                
#else
                    return CachingFactory.SharedObjectHelper.getsignlist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_smokes> getsmokeslist()
        {


            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_smokes> smokeslist = new List<lu_smokes>();
                smokeslist.Add(new lu_smokes { description = "Male",  id  = 1, selected   = false });
                smokeslist.Add(new lu_smokes { description = "Female", id = 2, selected = false });
                return smokeslist;
                
#else
                    return CachingFactory.SharedObjectHelper.getsmokeslist(db);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        #endregion

        #region "Criteria Lifestyle Dropdowns"

        public List<lu_educationlevel> geteducationlevellist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_educationlevel> educationlevellist = new List<lu_educationlevel>();
                educationlevellist.Add(new lu_educationlevel { description = "Male",  id  = 1, selected   = false });
                educationlevellist.Add(new lu_educationlevel { description = "Female", id = 2, selected = false });
                return educationlevellist;
                
#else
                    return CachingFactory.SharedObjectHelper.geteducationlevellist(db);
                    // return temp;
#endif

                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_employmentstatus> getemploymentstatuslist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_employmentstatus> employmentstatuslist = new List<lu_employmentstatus>();
                employmentstatuslist.Add(new lu_employmentstatus { description = "Male",  id  = 1, selected   = false });
                employmentstatuslist.Add(new lu_employmentstatus { description = "Female", id = 2, selected = false });
                return employmentstatuslist;
                
#else
                    return CachingFactory.SharedObjectHelper.getemploymentstatuslist(db);

#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_havekids> gethavekidslist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_havekids> havekidslist = new List<lu_havekids>();
                havekidslist.Add(new lu_havekids { description = "Male",  id  = 1, selected   = false });
                havekidslist.Add(new lu_havekids { description = "Female", id = 2, selected = false });
                return havekidslist;
                
#else
                    return CachingFactory.SharedObjectHelper.gethavekidslist(db);
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_incomelevel> getincomelevellist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_incomelevel> incomelevellist = new List<lu_incomelevel>();
                incomelevellist.Add(new lu_incomelevel { description = "Male",  id  = 1, selected   = false });
                incomelevellist.Add(new lu_incomelevel { description = "Female", id = 2, selected = false });
                return incomelevellist;
                
#else

                    return CachingFactory.SharedObjectHelper.getincomelevellist(db);
#endif

                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_livingsituation> getlivingsituationlist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_livingsituation> livingsituationlist = new List<lu_livingsituation>();
                livingsituationlist.Add(new lu_livingsituation { description = "Male",  id  = 1, selected   = false });
                livingsituationlist.Add(new lu_livingsituation { description = "Female", id = 2, selected = false });
                return livingsituationlist;
                
#else
                    return CachingFactory.SharedObjectHelper.getlivingsituationlist(db);
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_lookingfor> getlookingforlist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_lookingfor> lookingforlist = new List<lu_lookingfor>();
                lookingforlist.Add(new lu_lookingfor { description = "Male",  id  = 1, selected   = false });
                lookingforlist.Add(new lu_lookingfor { description = "Female", id = 2, selected = false });
                return lookingforlist;
                
#else
                    return CachingFactory.SharedObjectHelper.getlookingforlist(db);
#endif

                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_maritalstatus> getmaritalstatuslist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_maritalstatus> maritalstatuslist = new List<lu_maritalstatus>();
                maritalstatuslist.Add(new lu_maritalstatus { description = "Male",  id  = 1, selected   = false });
                maritalstatuslist.Add(new lu_maritalstatus { description = "Female", id = 2, selected = false });
                return maritalstatuslist;
                
#else
                    return CachingFactory.SharedObjectHelper.getmaritalstatuslist(db);

#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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


        public List<lu_profession> getprofessionlist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {

#if DISCONECTED
                List<lu_profession> professionlist = new List<lu_profession>();
                professionlist.Add(new lu_profession { description = "Male",  id  = 1, selected   = false });
                professionlist.Add(new lu_profession { description = "Female", id = 2, selected = false });
                return professionlist;
                
#else
                    return CachingFactory.SharedObjectHelper.getprofessionlist(db);
#endif

                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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
       
        
        public List<lu_wantskids> getwantskidslist()
        {

            _unitOfWork.DisableProxyCreation = true;
            using (var db = _unitOfWork)
            {

                try
                {
#if DISCONECTED
                List<lu_wantskids> wantskidslist = new List<lu_wantskids>();
                wantskidslist.Add(new lu_wantskids { description = "Male",  id  = 1, selected   = false });
                wantskidslist.Add(new lu_wantskids { description = "Female", id = 2, selected = false });
                return wantskidslist;
                
#else

                    return CachingFactory.SharedObjectHelper.getwantskidslist(db);

#endif

                }
                catch (Exception ex)
                {

                    using (var logger = new  Logging(applicationEnum.LookupService))
                    {
                        logger.WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, ex, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                    }

                    FaultReason faultreason = new FaultReason("Error in Lookup service");
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

        #endregion



    }
}
