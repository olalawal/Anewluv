using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Net;
using System.ServiceModel.Activation;
//using Nmedia.DataAccess.Interfaces;
using System.Data.SqlClient;
using GeoData.Domain.Models;
using GeoData.Domain.Models.ViewModels;
using Anewluv.Services.Contracts;
using LoggingLibrary;

using Anewluv.Domain.Data.ViewModels;
using Anewluv.DataExtentionMethods;
using Nmedia.Infrastructure.Domain.Data.log;

using GeoData.Domain.ViewModels;
using Nmedia.Infrastructure.ExceptionHandling;
using Nmedia.Infrastructure.Domain.Data;
using System.Threading.Tasks;
using Anewluv.Services.Contracts.ServiceResponse;
using Repository.Pattern.UnitOfWork;



namespace Anewluv.Services.Spatial
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
 
         [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
        [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]  
        public class GeoService :  IGeoService
        {
               //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IGeoDataStoredProcedures _storedProcedures;
        private LoggingLibrary.Logging logger;
       // enviromentEnum currentenviroment = enviromentEnum.dev;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public GeoService(IUnitOfWorkAsync unitOfWork, IGeoDataStoredProcedures storedProcedures)
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
            ////_unitOfWorkAsync.DisableProxyCreation = true;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        }




            public async Task<string> getcountrynamebycountryid(GeoModel model)
            {
               
                    string countryname = "";
                    //_unitOfWorkAsync.DisableProxyCreation = true;
                    //using (var db = _unitOfWorkAsync)
                    {
                       try                       
                       {
                           var task = Task.Factory.StartNew(() =>
                           {

                               return spatialextentions.getcountrynamebycountryid(model, _storedProcedures);

                           });
                           return await task.ConfigureAwait(false);

                    

                        }
                        catch (Exception ex)
                        {

                            Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.countryid.ToString(), "", "", ex.Message, ex.InnerException);
                            new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment,  convertedexcption);
                            //can parse the error to build a more custom error mssage and populate fualt faultreason
                            FaultReason faultreason = new FaultReason("Error in GeoService service");
                            string ErrorMessage = "";
                            string ErrorDetail = "ErrorMessage: " + ex.Message;
                            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                            //throw convertedexcption;
                        }

                    }

                            return countryname;      
             
            }

            public async Task<registermodel> verifyorupdateregistrationgeodata(registermodel model)
            {
                
                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {

                            //4-24-2012 fixed code to hanlde if we did not have a postcal code
                            gpsdata gpsData = new gpsdata();
                            // string[] tempcityAndStateProvince = model.city.Split(',');
                            //int countryID;
                            //model.stateprovince = ((tempcityAndStateProvince.Count() > 1)) ? tempcityAndStateProvince[1] : "NA";
                            GeoModel geomodel = new GeoModel { country = model.country, city = model.city };
                            //attmept to get postal postalcode if it is empty
                            if (model.ziporpostalcode == null)
                            {
                               // gpsData = this.getgpsdatabycountrycity(geomodel, _unitOfWorkAsync);

                                //var myresult = Task.Factory.StartNew(() =>
                                //{
                                //    Task<gpsdata> returnedTaskTResult = this.getgpsdatabycountrycity(geomodel, _unitOfWorkAsync);
                                //    return returnedTaskTResult.Result;
                                //});
                                //gpsData = myresult.Result;

                                gpsData = this.getgpsdatabycountrycity(geomodel, _unitOfWorkAsync);
                            }
                            else
                            {
                             //   geomodel.postalcode = model.ziporpostalcode;
                               // var myresult = Task.Factory.StartNew(() =>
                               // {
                                //    Task<gpsdata> returnedTaskTResult = this.getgpsdatabycitycountrypostalcode(geomodel, _unitOfWorkAsync);
                              //      return returnedTaskTResult.Result;
                              //  });
                            //    gpsData = myresult.Result;
                              //  geomodel.postalcode = model.ziporpostalcode;
                              //  gpsData = this.getgpsdatabycitycountrypostalcode(geomodel, db);

                                gpsData =  this.getgpsdatabycitycountrypostalcode(geomodel, _unitOfWorkAsync);
                            }

                            //this.getpostalcodesbycountrynamecity(new GeoModel { country = model.country, city = tempcityAndStateProvince[0] })
                            //.FirstOrDefault().postalcodevalue :   model.ziporpostalcode;                       
                            //countryID = postaldataservicecontext.GetcountryIdBycountryName(model.GeoRegisterModel.country);                       
                            //get GPS data here this works as long as zip or postal code was populated from above
                            //conver the unquiqe coountry Name to an ID
                            //store country ID for use later
                            //get the longidtue and latttude 
                            //1-11-2011 postal code and city are flipped by the way not this function should be renamed
                            //TO DO rename this function.                  
                            // gpsData =  this.getgpsdatabycitycountrypostalcode(new GeoModel { country = model.country, postalcode = model.ziporpostalcode, city = tempcityAndStateProvince[0] },db);
                            model.lattitude = (gpsData != null) ? Convert.ToDouble(gpsData.Latitude) : 0;
                            model.longitude = (gpsData != null) ? Convert.ToDouble(gpsData.Longitude) : 0;
                            model.stateprovince = (gpsData != null) ? gpsData.State_Province : "";

                            return model;

                        });
                        return await task.ConfigureAwait(false);

                     

                      

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException("" , "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
              
            }
            //gets the country list and orders it
            //added sorting
            public async Task<List<countrypostalcode>> getcountrypostalcodestatuslist()
            {
                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {
                        //TO DO put back in cache
                        //return CachingFactory.SharedObjectHelper.getcountryandpostalcodestatuslist(_postalcontext);

                        var task = Task.Factory.StartNew(() =>
                        {

                            var Query = _unitOfWorkAsync.Repository<Country_PostalCode_List>().Query(p => p.CountryName != "").Select().ToList().OrderBy(p => p.CountryName);

                            return (from s in Query
                                    select new countrypostalcode
                                    {
                                        name = s.CountryName,
                                        id = s.CountryID.ToString(),
                                        code = s.Country_Code,
                                        customregionid = s.CountryCustomRegionID,
                                        region = s.Country_Region,
                                        haspostalcode = Convert.ToBoolean(s.PostalCodes)
                                    }).ToList();
                        });
                        return await task.ConfigureAwait(false);

                    


                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException("", "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


             
            }

            public async Task<List<country>> getcountrylist()
            {
                
                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {

                         var task = Task.Factory.StartNew(() =>
                        {
                       
                            List<country> countries = new List<country>();
                            #if DISCONECTED
                       
                                                        List<country> countrylist = new List<country>();
                                                        countrylist.Add(new country { countryvalue = "United", countryindex = "44", selected = false });
                                                        countrylist.Add(new country { countryvalue = "Canada", countryindex = "43", selected = false });
                                                        return countrylist;

                             #else
                             
                             //  return CachingFactory.SharedObjectHelper.getcountrylist(_postalcontext);
                            //TO do move to caches server 

                            countries.Add(new country { id = "0", name = "Any" });
                            foreach (Country_PostalCode_List item in  _unitOfWorkAsync.Repository<Country_PostalCode_List>().Query().Select().OrderBy(p => p.CountryName))
                            {
                                var currentcountry = new country { id = item.CountryID.ToString(), name = item.CountryName };
                                countries.Add(currentcountry);
                            }

                            return countries;
                            #endif
                        });
                         return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException("", "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

                
            }
           
            /// <summary>
            /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
            /// </summary>        
            public async Task<bool> getpostalcodestatusbycountryname(GeoModel model)

            {

                if (model.country == null ) return false;

                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {

                            return spatialextentions.getpostalcodestatusbycountryname(model, _unitOfWorkAsync);

                        });
                        return await task.ConfigureAwait(false);


                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

               
            }

            public async Task<int> getcountryidbycountryname(GeoModel model)
            {


                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {

                    try
                    {
                            var task = Task.Factory.StartNew(() =>
                        {

                            if (model.country == null ) return 0;

                            List<Country_PostalCode_List> countryCodeQuery = default(List<Country_PostalCode_List>);
                            //3-18-2013 olawal added code to remove the the spaces when we test
                            countryCodeQuery = _unitOfWorkAsync.Repository<Country_PostalCode_List>().Query (p => p.CountryName.Replace(" ", "") == model.country).Select().ToList();

                            if (countryCodeQuery.Count() > 0)
                            {
                                return countryCodeQuery.FirstOrDefault().CountryID;

                            }
                            else
                            {
                                return 0;
                            }
                        });
                            return await task.ConfigureAwait(false);


                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                        new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
            }
            

             //TO DO fix sproc
            //This actually onlu uues counry and filter
            //Dynamic LINQ to Entites quries 
            //*****************************************************************************************************************************************
            public async Task<List<citystateprovince>> getcitystateprovincelistbycountrynamepostalcodefilter(GeoModel model)
            {


                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {



                        });
                        return await task.ConfigureAwait(false);

                        if (model.country == null | model.filter == null | model.postalcode == null) return null;


                        List<CityList> _CityList = new List<CityList>();
                        model.postalcode = string.Format("{0}%", model.postalcode.Replace("'", "''"));
                        // fix country names if theres a space
                        model.country = string.Format(model.country.Replace(" ", ""));
                        //test this as well for added 2/28/2011 - trimming spaces in search text since i am removing spaces in sql script
                        //for cites as well so its a 1 to 1 search no spaces on input and on db side
                        model.filter = string.Format("{0}%", model.filter.Replace(" ", ""));
                        //11/13/2009 addded wild ca

             
                        var citylist = _storedProcedures.CityListbycountryNamePostalcodeandCity(model.country,model.filter);                      
                        
                        int index = 0;
                        return ((from s in citylist.ToList() select new citystateprovince { citystateprovincevalue = s.City + "," + s.State_Province }).ToList());
         

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
              
                
                     
            }

            public async Task<List<gpsdata>> getgpsdatalistbycountrycitypostalcode(GeoModel model)
            {


                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {



                        var task = Task.Factory.StartNew(() =>
                        {

                            //
                            if (model.country == null | model.city == null | model.postalcode == null) return null;



                            //   IQueryable<GpsData> functionReturnValue = default(IQueryable<GpsData>);

                            List<gpsdata> _GpsData = new List<gpsdata>();
                            model.country = string.Format(model.country.Replace(" ", ""));
                            // fix country names if theres a space
                            // strCity = String.Format("{0}%", strCity) '11/13/2009 addded wild ca


                            var gpsdatalist = _storedProcedures.GetGPSDatasByPostalCodeandCity(model.country, model.city, model.postalcode);
                            return ((from s in gpsdatalist.ToList() select new gpsdata { Latitude = s.Latitude, Longitude = s.Longitude, State_Province = s.State_Province }).ToList());
            

                        });
                        return await task.ConfigureAwait(false);
            
        

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

              
            }

            public async Task<gpsdata> getgpsdatabycountrycity(GeoModel model)
            {

                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            return getgpsdatabycountrycity(model, _unitOfWorkAsync);

                        });
                        return await task.ConfigureAwait(false);

                       

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


               
            }

            public async Task<gpsdata> getgpsdatabycitycountrypostalcode(GeoModel model)
            {
             //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {

                            return this.getgpsdatabycitycountrypostalcode(model, _unitOfWorkAsync);
                

                        });
                        return await task.ConfigureAwait(false);
                   }
                     catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }
                }

            }


            public async Task<bool> validatepostalcodebycountrycitypostalcode(GeoModel model)
            {
                
                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            if (model.country == null | model.filter == null | model.postalcode == null) return false;

                            //Dim _PostalCodeList As New List(Of PostalCodeList)()

                            model.city = string.Format("{0}%", model.city);
                            model.country = string.Format(model.country.Replace(" ", ""));

                            // fix country names if theres a space


                            var foundpostalcodes = _storedProcedures.ValidatePostalCodeByCountryNameandCity(model.country, model.city, model.postalcode);
                            // var foundpostalcodes = _postalcontext.ValidatePostalCodeByCOuntryandCity(countryname, city, postalcode);
                            // return foundpostalcodes;
                            if (foundpostalcodes.Count() > 0) return true;
                            return false;

                        });
                        return await task.ConfigureAwait(false);

              

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
               
               
            }

            public async Task<List<postalcode>> getpostalcodesbycountrylatlong(GeoModel model)
            {

                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {


                            if (model.country == null | model.lattitude == null | model.longitude == null) return null;


                            //added 5/12/2011 to handle empty countries
                            if (model.country == null) return null;
                            //TO DO copy postal code stuff from shared models into here
                            //List<PostalCodeItem> _PostalCodeList = new List<PostalCodeList>();
                            model.country = string.Format(model.country.Replace(" ", ""));
                            // fix country names if theres a space
                            //StrprefixText = string.Format("%{0}%", StrprefixText);




                            var postalcodelist = _storedProcedures.GetPostalCodesByCountryAndLatLong(model.country, model.lattitude, model.longitude);
                            //  var geopostalcodes = _postalcontext.GetPostalCodesByCountryAndLatLongDynamic(countryname, lattitude, longitude);
                            if (postalcodelist != null)
                                return ((from s in postalcodelist.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());

                            return null;

                        });
                        return await task.ConfigureAwait(false);

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

               
            }

            public async Task<List<postalcode>> getpostalcodesbycountrynamecitystateprovince(GeoModel model)
            {

                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {

                            if (model.country == null | model.city == null | model.stateprovince == null) return null;


                            //sp_GetPostalCodeByCountryNameCityandStateProvince

                            var postalcodelist = _storedProcedures.GetPostalCodesByCountryNameCityandStateProvince(model.country, model.city, model.stateprovince);
                            //  var geopostalcodes = _postalcontext.GetPostalCodesByCountryNameCityandStateProvinceDynamic(countryname, city, stateprovince);
                            if (postalcodelist != null)
                                return ((from s in postalcodelist.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());

                            return null;

                        });
                        return await task.ConfigureAwait(false);
                      
                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


            }
      
            /// <summary>
            /// postal code is an optional argument
            /// </summary>
            /// <param name="country"></param>
            /// <param name="filter"></param>
            /// <param name="postalcode"></param>
            /// <returns></returns>
           public async Task<List<citystateprovince>> getfilteredcitybycountrypostalcodefilter(GeoModel model)
            {


                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {

                           var task = Task.Factory.StartNew(() =>
                            {

                        if (model.country == null || model.filter == null) return null;

                       model.country = string.Format(model.country.Replace(" ", ""));

                        //test this as well for added 2/28/2011 - trimming spaces in search text since i am removing spaces in sql script
                        //for cites as well so its a 1 to 1 search no spaces on input and on db side
                        model.filter = string.Format("{0}%", model.filter.Replace(" ", ""));
                        //11/13/2009 addded wild ca

                        

                       // var cities = _postalcontext.GetCityListDynamic(country, "", filter).Take(50);

                        var clitylist = _storedProcedures.CityListbycountryNameCityFilter(model.country, model.filter);
                        var temp = (from s in clitylist.ToList()
                                    select new citystateprovince
                                    {
                                        citystateprovincevalue = s.State_Province != "" ?
                                            s.City + "," + s.State_Province : s.City
                                    }).ToList();
                        return temp;

                            });
                       return await task.ConfigureAwait(false);


                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


            }

            public async Task<List<citystateprovince>> getfilteredcitybycountrycityfilter(GeoModel model)
            {

                //var params = new object[] {new SqlParameter("@FirstName", "Bob")};
                //this._repositoryContext.ObjectContext.ExecuteStoreQuery<ResultType>("GetByName @FirstName", params) 



                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {

                            if (model.country == null || model.filter == null) return null;

                            model.country = string.Format(model.country.Replace(" ", ""));

                            //test this as well for added 2/28/2011 - trimming spaces in search text since i am removing spaces in sql script
                            //for cites as well so its a 1 to 1 search no spaces on input and on db side
                            model.filter = string.Format("{0}%", model.filter.Replace(" ", ""));
                            //11/13/2009 addded wild ca

                           

                            // var cities = _postalcontext.GetCityListDynamic(country, "", filter).Take(50);
                                var citylist = _storedProcedures.CityListbycountryNameCityFilter(model.country, model.filter);
                            var temp = (from s in citylist.ToList()
                                        select new citystateprovince
                                        {
                                            citystateprovincevalue = s.State_Province != "" ?
                                                s.City + "," + s.State_Province : s.City
                                        }).ToList();
                            return temp;
                        });
                        return await task.ConfigureAwait(false);




                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                        new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


            }

             /// <summary>
             /// NEW added
             /// </summary>
             /// <param name="model"></param>
             /// <returns></returns>
            public async Task<List<citystateprovince>> getfilteredcitybycountryidcityfilter(GeoModel model)
            {

                //var params = new object[] {new SqlParameter("@FirstName", "Bob")};
                //this._repositoryContext.ObjectContext.ExecuteStoreQuery<ResultType>("GetByName @FirstName", params) 


                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {
                        var task = Task.Factory.StartNew(() =>
                        {

                            if (model.countryid == null || model.filter == null) return null;

                            //test this as well for added 2/28/2011 - trimming spaces in search text since i am removing spaces in sql script
                            //for cites as well so its a 1 to 1 search no spaces on input and on db side
                            model.filter = string.Format("{0}%", model.filter.Replace(" ", ""));
                            //11/13/2009 addded wild ca                           

                           
                                  var citylist = _storedProcedures.CityListbycountryIDCityFilter(model.countryid, model.filter);
                            var temp = (from s in citylist.ToList()
                                        select new citystateprovince
                                        {
                                            citystateprovincevalue = s.State_Province != "" ?
                                                s.City + "," + s.State_Province : s.City
                                        }).ToList();
                            return temp;
                        });
                        return await task.ConfigureAwait(false);




                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                        new Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


            }

            public async Task<List<postalcode>> getfilteredpostalcodesbycountrycityfilter(GeoModel model)
            {


                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {
                          var task = Task.Factory.StartNew(() =>
                        {


                            if (model.country == null | model.filter == null | model.city == null) return null;


                            //added 5/12/2011 to handle empty countries
                            if (model.country == null) return null;
                            List<PostalCodeList> _PostalCodeList = new List<PostalCodeList>();
                            model.city = string.Format("{0}%", model.city);
                            model.country = string.Format(model.country.Replace(" ", ""));
                            // fix country names if theres a space
                            model.filter = string.Format("{0}%", model.filter);
                            //11/13/2009 addded wild ca

                            //sp_GetPostalCodesByCountryNameCityandPrefix
                              var postalcodelist = _storedProcedures.GetPostalCodesByCountryIDCityandPrefix(model.country, model.filter, model.postalcode);
                          
                            if (postalcodelist !=null)
                                return ((from s in postalcodelist.Take(25).ToList() select new postalcode { postalcodevalue = s.PostalCode, lattitude = s.LATITUDE, longitude = s.LONGITUDE }).ToList());

                            return null;

                        });
                          return await task.ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
               
             
            }

             /// <summary>
             /// NEW added
             /// </summary>
             /// <param name="model"></param>
             /// <returns></returns>
            public async Task< List<postalcode>> getfilteredpostalcodesbycountryidcityfilter(GeoModel model)
            {


                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {

                          var task = Task.Factory.StartNew(() =>
                        {
                           // if (model.country == null | model.filter == null | model.city == null) return null;

                            //added 5/12/2011 to handle empty countries
                            if (model.countryid == null) return null;
                            List<PostalCodeList> _PostalCodeList = new List<PostalCodeList>();
                            model.city = string.Format("{0}%", model.city);
                            //model.country = string.Format(model.country.Replace(" ", ""));
                            // fix country names if theres a space
                            model.filter = string.Format("{0}%", model.filter);
                            //11/13/2009 addded wild ca

                            //sp_GetPostalCodesByCountryNameCityandPrefix

                            //  var customers = _postalcontext.GetPostalCodesByCountryAndCityPrefixDynamic(country, city, filter);
                              var postalcodelist = _storedProcedures.GetPostalCodesByCountryNameCityandPrefix(model.country,model.city, model.filter);
                            //TO DO scafold in lattitude and longitude
                            if (postalcodelist != null)
                                return ((from s in postalcodelist.Take(25).ToList() select new postalcode { postalcodevalue = s.PostalCode, lattitude =s.LATITUDE,longitude =s.LONGITUDE }).ToList());

                            return null;
                        });
                          return await task.ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                        new Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


            }        

            public async Task<List<postalcode>> getpostalcodesbycountrycityfilter(GeoModel model)
            {

                //_unitOfWorkAsync.DisableProxyCreation = true;
                //using (var db = _unitOfWorkAsync)
                {
                    try
                    {

                        var task = Task.Factory.StartNew(() =>
                        {
                            if (model.country == null | model.filter == null | model.filter == null) return null;


                            //added 5/12/2011 to handle empty countries
                            if (model.country == null) return null;

                            //  List<PostalCode> _PostalCodeList = new List<PostalCode>();
                            model.city = string.Format("{0}%", model.city);
                            model.country = string.Format(model.country.Replace(" ", ""));
                            // fix country names if theres a space
                            model.filter = string.Format("{0}%", model.filter);
                            //11/13/2009 addded wild ca

                         
                              var postalcodelist = _storedProcedures.GetPostalCodesByCountryNameCityandPrefix(model.country, model.city,model.filter);
                            // var gpsdatalist = _postalcontext.GetPostalCodesByCountryAndCityPrefixDynamic(countryname, city, filter);
                            //TO DO remove this and reutner object as is
                            return ((from s in postalcodelist.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
                        });
                        return await task.ConfigureAwait(false);

                       
        

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

           
            }

            //gets the single geo code as string
            //No using internal method
            public async Task<List<postalcode>> getpostalcodesbycountrynamecity(GeoModel model)
            {

                //_unitOfWorkAsync.DisableProxyCreation = true;
              
                    try
                    {


                        var task = Task.Factory.StartNew(() =>
                        {

                            if (model.country == null | model.city == null) return null;


                            //added 5/12/2011 to handle empty countries
                            if (model.country == null) return null;

                            //List<PostalCodeList> _PostalCodeList = new List<PostalCodeList>();
                            model.city = string.Format("{0}%", model.city);
                            model.country = string.Format(model.country.Replace(" ", ""));
                            // fix country names if theres a space
                            // StrprefixText = string.Format("%{0}%", StrprefixText);
                            //11/13/2009 addded wild ca


                            var postalcodelist = _storedProcedures.GetPostalCodesByCountryNameCity(model.country, model.city);

                            //  var postalcodelist = _postalcontext.getpostalcodesbycountrynamecity(countryname, city);
                            return ((from s in postalcodelist.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());

                        });
                        return await task.ConfigureAwait(false);

                        
         

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

       

            #region "private shared functions"

            private  gpsdata getgpsdatabycitycountrypostalcode(GeoModel model, IUnitOfWorkAsync db)
                        {

                            //_unitOfWorkAsync.DisableProxyCreation = true;
                          //  //using (var db = _unitOfWorkAsync)
                          //  {
                                try
                                {


                                    if (model.country == null | model.city == null ) return null;

                                    //   IQueryable<GpsData> functionReturnValue = default(IQueryable<GpsData>);

                                    List<gpsdata> _GpsData = new List<gpsdata>();
                                    model.country = string.Format(model.country.Replace(" ", ""));
                                    // fix country names if theres a space
                                    // strCity = String.Format("{0}%", strCity) '11/13/2009 addded wild ca

                      
                                      var gpsdata = _storedProcedures.GetGPSDatasByPostalCodeandCity(model.country, model.city, model.postalcode);
                                    return gpsdata.ToList().FirstOrDefault();
                                    //var s = _postalcontext.GetGpsDataSingleByCityCountryAndPostalCode(countryname, postalcode, city);
                                    //if (gpsdata != null)
                                    //{
                                    //    return new gpsdata { lattitude = s.Latitude, longitude = s.Longitude, stateprovince = s.State_Province };
                                    //}
                                    //return gpsdata;

                                }
                                catch (Exception ex)
                                {

                                    Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                                     new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                                    string ErrorMessage = "";
                                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                                    //throw convertedexcption;
                                }

                           // }
               
             
                        }

            private gpsdata getgpsdatabycountrycity(GeoModel model, IUnitOfWorkAsync db)
                        {

                            //_unitOfWorkAsync.DisableProxyCreation = true;
               
                                try
                                {
                                    if (model.country == null | model.city == null) return null;


                                    //IQueryable<GpsData> functionReturnValue = default(IQueryable<GpsData>);

                                    List<gpsdata> _GpsData = new List<gpsdata>();
                                    model.country = string.Format(model.country.Replace(" ", ""));
                                    // fix country names if theres a space
                                    // strCity = String.Format("{0}%", strCity) '11/13/2009 addded wild ca

                     
                                      var gpsdatalist = _storedProcedures.GetGPSDataByCountryAndCity(model.country, model.city);
                                    //  var gpsdatalist = _postalcontext.GetGpsDataByCountryAndCity(countryname, city);
                                    return ((from s in gpsdatalist.ToList() select new gpsdata { Latitude = s.Latitude, Longitude = s.Longitude, State_Province = s.State_Province }).ToList().FirstOrDefault());


                                }
                                catch (Exception ex)
                                {

                                    Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                                    new  Logging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, convertedexcption);
                                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                                    string ErrorMessage = "";
                                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                                    //throw convertedexcption;
                                }

                



                        }
            #endregion

            #region "Spatial Functions"






            // use this function to get distance at the same time, add it to the model
            public double? getdistancebetweenmembers(GeoModel model)
            {
                return spatialextentions.getdistancebetweenmembers(Convert.ToDouble(model.lattitude), Convert.ToDouble(model.longitude), Convert.ToDouble(model.lattitude2), Convert.ToDouble(model.longitude2), model.unit);
               
            }

          

            #endregion


        }
    }

