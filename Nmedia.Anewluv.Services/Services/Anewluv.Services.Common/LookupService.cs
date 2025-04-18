﻿using System;
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
//using Nmedia.DataAccess.Interfaces;
using Anewluv.Caching;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Data.log;
using Nmedia.Infrastructure.Domain.Data;
using Repository.Pattern.UnitOfWork;
using GeoData.Domain.Models;
using Nmedia.Infrastructure.DependencyInjection;
using Nmedia.Infrastructure.DTOs;
using Anewluv.Caching.RedisCaching;

namespace Anewluv.Services.Common
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LookupService : ILookupService
    {


  //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IGeoDataStoredProcedures _storedProcedures;
        private LoggingLibrary.Logging logger;



        public LookupService([IAnewluvEntitesScope]IUnitOfWorkAsync unitOfWork,[ISpatialEntitesScope]IGeoDataStoredProcedures storedProcedures)
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


        #region "new lookups that come from the lu_tables that have to be displayed i.e such as photoformats etc"



        public List<lu_photoformat> getphotoformatlist()
        {


               
             
               {
                  
                   try
                   {

                       
                    #if DISCONECTED
                                    List<lu_photoformat> photoformatlist = new List<lu_photoformat>();
                                    photoformatlist.Add(new lu_photoformat { description = "Male",  id  = 1, selected   = false });
                                    photoformatlist.Add(new lu_photoformat { description = "Female", id = 2, selected = false });
                                    return photoformatlist;
                
                    #else
                      return RedisCacheFactory.SharedObjectHelper.getphotoformatlist(_unitOfWorkAsync);
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
        public List<listitem> getphotoapprovalstatuslist()
        {


           
         
            {

                try
                {
#if DISCONECTED
                List<lu_photoapprovalstatus> photoapprovalstatuslist = new List<lu_photoapprovalstatus>();
                photoapprovalstatuslist.Add(new lu_photoapprovalstatus { description = "Male",  id  = 1, selected   = false });
                photoapprovalstatuslist.Add(new lu_photoapprovalstatus { description = "Female", id = 2, selected = false });
                return photoapprovalstatuslist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getphotoapprovalstatuslist(_unitOfWorkAsync);
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


           
         
            {

                try
                {
#if DISCONECTED
                List<lu_photorejectionreason> photorejectionreasonlist = new List<lu_photorejectionreason>();
                photorejectionreasonlist.Add(new lu_photorejectionreason { description = "Male",  id  = 1, selected   = false });
                photorejectionreasonlist.Add(new lu_photorejectionreason { description = "Female", id = 2, selected = false });
                return photorejectionreasonlist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getphotorejectionreasonlist(_unitOfWorkAsync);
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
        public List<listitem> getphotostatuslist()
        {


           
         
            {

                try
                {
#if DISCONECTED
                List<lu_photostatus> photostatuslist = new List<lu_photostatus>();
                photostatuslist.Add(new lu_photostatus { description = "Male",  id  = 1, selected   = false });
                photostatuslist.Add(new lu_photostatus { description = "Female", id = 2, selected = false });
                return photostatuslist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getphotostatuslist(_unitOfWorkAsync);
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
        public List<listitem> getphotoimagetypeslist()
        {
           
         
            {

                try
                {
#if DISCONECTED
                List<lu_photoimagetype> photoimagetypelist = new List<lu_photoimagetype>();
                photoimagetypelist.Add(new lu_photoimagetype { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_photoimagetype { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getphotoimagetypelist(_unitOfWorkAsync);
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
        public List<listitem> getphotosecurityleveltypelist()
        {


            {

                try
                {
#if DISCONECTED
                List<lu_photoimagetype> photoimagetypelist = new List<lu_photoimagetype>();
                photoimagetypelist.Add(new lu_photoimagetype { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_photoimagetype { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getphotosecurityleveltypelist(_unitOfWorkAsync);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new Logging(applicationEnum.LookupService))
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

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_photostatusdescription> photostatusdescriptionlist = new List<lu_photostatusdescription>();
                photostatusdescriptionlist.Add(new lu_photostatusdescription { description = "Male",  id  = 1, selected   = false });
                photostatusdescriptionlist.Add(new lu_photostatusdescription { description = "Female", id = 2, selected = false });
                return photostatusdescriptionlist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getphotostatusdescriptionlist(_unitOfWorkAsync);
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


        public List<listitem> getabusetypelist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_abusetype> abusetypelist = new List<lu_abusetype>();
                abusetypelist.Add(new lu_abusetype { description = "Male",  id  = 1, selected   = false });
                abusetypelist.Add(new lu_abusetype { description = "Female", id = 2, selected = false });
                return abusetypelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getabusetypelist(_unitOfWorkAsync);
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


        public List<listitem> getphotoImagersizerformatlist()
        {
           
         
            {

                try
                {
#if DISCONECTED
                List<lu_photoImagersizerformat> getphotoImagersizerformatlist = new List<lu_photoImagersizerformat>();
                getphotoImagersizerformatlist.Add(new lu_photoImagersizerformat { description = "Male",  id  = 1, selected   = false });
                getphotoImagersizerformatlist.Add(new lu_photoImagersizerformat { description = "Female", id = 2, selected = false });
                return getphotoImagersizerformatlist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getphotoImagersizerformatlist(_unitOfWorkAsync);
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
        public List<listitem> getprofilestatuslist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_profilestatus> photoimagetypelist = new List<lu_profilestatus>();
                photoimagetypelist.Add(new lu_profilestatus { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_profilestatus { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getprofilestatuslist(_unitOfWorkAsync);
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

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_role> photoimagetypelist = new List<lu_role>();
                photoimagetypelist.Add(new lu_role { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_role { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getrolelist(_unitOfWorkAsync);
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


        public List<listitem> getsecurityleveltypelist()
        {


           
         
            {

                try
                {

#if DISCONECTED
                List<lu_securityleveltype> photoimagetypelist = new List<lu_securityleveltype>();
                photoimagetypelist.Add(new lu_securityleveltype { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_securityleveltype { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getsecurityleveltypelist(_unitOfWorkAsync);
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


        public List<listitem> getshowmelist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_showme> photoimagetypelist = new List<lu_showme>();
                photoimagetypelist.Add(new lu_showme { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_showme { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getshowmelist(_unitOfWorkAsync);
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


        public List<listitem> getsortbytypelist()
        {

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_sortbytype> photoimagetypelist = new List<lu_sortbytype>();
                photoimagetypelist.Add(new lu_sortbytype { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_sortbytype { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getsortbytypelist(_unitOfWorkAsync);
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


        public List<listitem> getsecurityquestionlist()
        {

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_securityquestion> photoimagetypelist = new List<lu_securityquestion>();
                photoimagetypelist.Add(new lu_securityquestion { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_securityquestion { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getsecurityquestionlist(_unitOfWorkAsync);
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

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_flagyesno> photoimagetypelist = new List<lu_flagyesno>();
                photoimagetypelist.Add(new lu_flagyesno { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_flagyesno { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getflagyesnolist(_unitOfWorkAsync);
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


        public List<listitem> getprofilefiltertypelist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_profilefiltertype> photoimagetypelist = new List<lu_profilefiltertype>();
                photoimagetypelist.Add(new lu_profilefiltertype { description = "Male",  id  = 1, selected   = false });
                photoimagetypelist.Add(new lu_profilefiltertype { description = "Female", id = 2, selected = false });
                return photoimagetypelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getprofilefiltertypelist(_unitOfWorkAsync);
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
                    temp = RedisCacheFactory.SharedObjectHelper.CssStyleSelector.getsystempagesettingslist(_unitOfWorkAsync);
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
           
         
            {

                try
                {

                    var temp = RedisCacheFactory.SharedObjectHelper.CssStyleSelector.getbodycssbypagename(pagename, _unitOfWorkAsync).ToString();
                    return temp;


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


        public List<listitem> getgenderlist()
        {
           
         
            {

                try
                {
#if DISCONECTED
                List<lu_gender> genderlist = new List<lu_gender>();
                genderlist.Add(new lu_gender { description = "Male",  id  = 1, selected   = false });
                genderlist.Add(new lu_gender { description = "Female", id = 2, selected = false });
                return genderlist;                
#else
                    return RedisCacheFactory.SharedObjectHelper.getgenderlist(_unitOfWorkAsync);
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

           
         
            {

                try
                {
                    return RedisCacheFactory.SharedObjectHelper.getagelist();


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

           
         
            {

                try
                {

                    return RedisCacheFactory.SharedObjectHelper.getmetricheightlist();

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



        public List<listitem> getbodytypelist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_bodytype> bodytypelist = new List<lu_bodytype>();
                bodytypelist.Add(new lu_bodytype { description = "Male",  id  = 1, selected   = false });
                bodytypelist.Add(new lu_bodytype { description = "Female", id = 2, selected = false });
                return bodytypelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getbodytypelist(_unitOfWorkAsync);
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


        public List<listitem> getethnicitylist()
        {

           
         
            {

                try
                {

#if DISCONECTED
            List<lu_ethnicity> ethnicitylist = new List<lu_ethnicity>();
            ethnicitylist.Add(new lu_ethnicity { description = "Male", id = 1, selected = false });
            ethnicitylist.Add(new lu_ethnicity { description = "Female", id = 2, selected = false });
            return ethnicitylist;

#else
                    return RedisCacheFactory.SharedObjectHelper.getethnicitylist(_unitOfWorkAsync);
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


        public List<listitem> geteyecolorlist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_eyecolor> eyecolorlist = new List<lu_eyecolor>();
                eyecolorlist.Add(new lu_eyecolor { description = "Male",  id  = 1, selected   = false });
                eyecolorlist.Add(new lu_eyecolor { description = "Female", id = 2, selected = false });
                return eyecolorlist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.geteyecolorlist(_unitOfWorkAsync);
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


        public List<listitem> gethaircolorlist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_haircolor> haircolorlist = new List<lu_haircolor>();
                haircolorlist.Add(new lu_haircolor { description = "Male",  id  = 1, selected   = false });
                haircolorlist.Add(new lu_haircolor { description = "Female", id = 2, selected = false });
                return haircolorlist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.gethaircolorlist(_unitOfWorkAsync);
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

        public List<listitem> gethotfeaturelist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_hotfeature> hotfeaturelist = new List<lu_hotfeature>();
                hotfeaturelist.Add(new lu_hotfeature { description = "Male",  id  = 1, selected   = false });
                hotfeaturelist.Add(new lu_hotfeature { description = "Female", id = 2, selected = false });
                return hotfeaturelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.gethotfeaturelist(_unitOfWorkAsync);
                    // return temp;
#endif
                }
                catch (Exception ex)
                {

                    using (var logger = new Logging(applicationEnum.LookupService))
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

        public List<listitem> getdietlist()
        {


           
         
            {

                try
                {
#if DISCONECTED
                List<lu_diet> dietlist = new List<lu_diet>();
                dietlist.Add(new lu_diet { description = "Male",  id  = 1, selected   = false });
                dietlist.Add(new lu_diet { description = "Female", id = 2, selected = false });
                return dietlist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getdietlist(_unitOfWorkAsync);
                    //  return _unitOfWorkAsync.lu_diet.OrderBy(x => x.description).ToList();



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


        public List<listitem> getdrinkslist()
        {

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_drinks> drinkslist = new List<lu_drinks>();
                drinkslist.Add(new lu_drinks { description = "Male",  id  = 1, selected   = false });
                drinkslist.Add(new lu_drinks { description = "Female", id = 2, selected = false });
                return drinkslist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getdrinkslist(_unitOfWorkAsync);



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
        public List<listitem> getexerciselist()
        {

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_exercise> exerciselist = new List<lu_exercise>();
                exerciselist.Add(new lu_exercise { description = "Male",  id  = 1, selected   = false });
                exerciselist.Add(new lu_exercise { description = "Female", id = 2, selected = false });
                return exerciselist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getexerciselist(_unitOfWorkAsync);
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


        public List<listitem> gethobbylist()
        {

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_hobby> hobbylist = new List<lu_hobby>();
                hobbylist.Add(new lu_hobby { description = "Male",  id  = 1, selected   = false });
                hobbylist.Add(new lu_hobby { description = "Female", id = 2, selected = false });
                return hobbylist;
                
#else

                    return RedisCacheFactory.SharedObjectHelper.gethobbylist(_unitOfWorkAsync);
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


        public List<listitem> gethumorlist()
        {

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_humor> humorlist = new List<lu_humor>();
                humorlist.Add(new lu_humor { description = "Male",  id  = 1, selected   = false });
                humorlist.Add(new lu_humor { description = "Female", id = 2, selected = false });
                return humorlist;
                
#else

                    return RedisCacheFactory.SharedObjectHelper.gethumorlist(_unitOfWorkAsync); ;

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


        public List<listitem> getpoliticalviewlist()
        {

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_politicalview> politicalviewlist = new List<lu_politicalview>();
                politicalviewlist.Add(new lu_politicalview { description = "Male",  id  = 1, selected   = false });
                politicalviewlist.Add(new lu_politicalview { description = "Female", id = 2, selected = false });
                return politicalviewlist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getpoliticalviewlist(_unitOfWorkAsync);
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


        public List<listitem> getreligionlist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_religion> religionlist = new List<lu_religion>();
                religionlist.Add(new lu_religion { description = "Male",  id  = 1, selected   = false });
                religionlist.Add(new lu_religion { description = "Female", id = 2, selected = false });
                return religionlist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getreligionlist(_unitOfWorkAsync);

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


        public List<listitem> getreligiousattendancelist()
        {

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_religiousattendance> religiousattendancelist = new List<lu_religiousattendance>();
                religiousattendancelist.Add(new lu_religiousattendance { description = "Male",  id  = 1, selected   = false });
                religiousattendancelist.Add(new lu_religiousattendance { description = "Female", id = 2, selected = false });
                return religiousattendancelist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getreligiousattendancelist(_unitOfWorkAsync);
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


        public List<listitem> getsignlist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_sign> signlist = new List<lu_sign>();
                signlist.Add(new lu_sign { description = "Male",  id  = 1, selected   = false });
                signlist.Add(new lu_sign { description = "Female", id = 2, selected = false });
                return signlist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getsignlist(_unitOfWorkAsync);
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


        public List<listitem> getsmokeslist()
        {


           
         
            {

                try
                {
#if DISCONECTED
                List<lu_smokes> smokeslist = new List<lu_smokes>();
                smokeslist.Add(new lu_smokes { description = "Male",  id  = 1, selected   = false });
                smokeslist.Add(new lu_smokes { description = "Female", id = 2, selected = false });
                return smokeslist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getsmokeslist(_unitOfWorkAsync);
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

        public List<listitem> geteducationlevellist()
        {

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_educationlevel> educationlevellist = new List<lu_educationlevel>();
                educationlevellist.Add(new lu_educationlevel { description = "Male",  id  = 1, selected   = false });
                educationlevellist.Add(new lu_educationlevel { description = "Female", id = 2, selected = false });
                return educationlevellist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.geteducationlevellist(_unitOfWorkAsync);
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


        public List<listitem> getemploymentstatuslist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_employmentstatus> employmentstatuslist = new List<lu_employmentstatus>();
                employmentstatuslist.Add(new lu_employmentstatus { description = "Male",  id  = 1, selected   = false });
                employmentstatuslist.Add(new lu_employmentstatus { description = "Female", id = 2, selected = false });
                return employmentstatuslist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getemploymentstatuslist(_unitOfWorkAsync);

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


        public List<listitem> gethavekidslist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_havekids> havekidslist = new List<lu_havekids>();
                havekidslist.Add(new lu_havekids { description = "Male",  id  = 1, selected   = false });
                havekidslist.Add(new lu_havekids { description = "Female", id = 2, selected = false });
                return havekidslist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.gethavekidslist(_unitOfWorkAsync);
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


        public List<listitem> getincomelevellist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_incomelevel> incomelevellist = new List<lu_incomelevel>();
                incomelevellist.Add(new lu_incomelevel { description = "Male",  id  = 1, selected   = false });
                incomelevellist.Add(new lu_incomelevel { description = "Female", id = 2, selected = false });
                return incomelevellist;
                
#else

                    return RedisCacheFactory.SharedObjectHelper.getincomelevellist(_unitOfWorkAsync);
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


        public List<listitem> getlivingsituationlist()
        {

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_livingsituation> livingsituationlist = new List<lu_livingsituation>();
                livingsituationlist.Add(new lu_livingsituation { description = "Male",  id  = 1, selected   = false });
                livingsituationlist.Add(new lu_livingsituation { description = "Female", id = 2, selected = false });
                return livingsituationlist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getlivingsituationlist(_unitOfWorkAsync);
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


        public List<listitem> getlookingforlist()
        {

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_lookingfor> lookingforlist = new List<lu_lookingfor>();
                lookingforlist.Add(new lu_lookingfor { description = "Male",  id  = 1, selected   = false });
                lookingforlist.Add(new lu_lookingfor { description = "Female", id = 2, selected = false });
                return lookingforlist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getlookingforlist(_unitOfWorkAsync);
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


        public List<listitem> getmaritalstatuslist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_maritalstatus> maritalstatuslist = new List<lu_maritalstatus>();
                maritalstatuslist.Add(new lu_maritalstatus { description = "Male",  id  = 1, selected   = false });
                maritalstatuslist.Add(new lu_maritalstatus { description = "Female", id = 2, selected = false });
                return maritalstatuslist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getmaritalstatuslist(_unitOfWorkAsync);

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


        public List<listitem> getprofessionlist()
        {

           
         
            {

                try
                {

#if DISCONECTED
                List<lu_profession> professionlist = new List<lu_profession>();
                professionlist.Add(new lu_profession { description = "Male",  id  = 1, selected   = false });
                professionlist.Add(new lu_profession { description = "Female", id = 2, selected = false });
                return professionlist;
                
#else
                    return RedisCacheFactory.SharedObjectHelper.getprofessionlist(_unitOfWorkAsync);
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


        public List<listitem> getwantskidslist()
        {

           
         
            {

                try
                {
#if DISCONECTED
                List<lu_wantskids> wantskidslist = new List<lu_wantskids>();
                wantskidslist.Add(new lu_wantskids { description = "Male",  id  = 1, selected   = false });
                wantskidslist.Add(new lu_wantskids { description = "Female", id = 2, selected = false });
                return wantskidslist;
                
#else

                    return RedisCacheFactory.SharedObjectHelper.getwantskidslist(_unitOfWorkAsync);

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
