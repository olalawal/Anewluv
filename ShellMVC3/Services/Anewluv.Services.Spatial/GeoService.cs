using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Net;
using System.ServiceModel.Activation;
using Nmedia.DataAccess.Interfaces;
using System.Data.SqlClient;
using GeoData.Domain.Models;
using GeoData.Domain.Models.ViewModels;
using Anewluv.Services.Contracts;
using LoggingLibrary;

using Anewluv.Domain.Data.ViewModels;
using Anewluv.DataExtentionMethods;
using Nmedia.Infrastructure.Domain.Data.errorlog;
using Anewluv.Lib;
using GeoData.Domain.ViewModels;



namespace Anewluv.Services.Spatial
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
 
         [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
        [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]  
        public class GeoService :  IGeoService
        {
               //if our repo was generic it would be IPromotionRepository<T>  etc IPromotionRepository<reviews> 
        //private IPromotionRepository  promotionrepository;

        IUnitOfWork _unitOfWork;
        private LoggingLibrary.ErroLogging logger;
       // logenviromentEnum currentenviroment = logenviromentEnum.dev;

        //  private IMemberActionsRepository  _memberactionsrepository;
        // private string _apikey;

        public GeoService(IUnitOfWork unitOfWork)
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



            public string getcountrynamebycountryid(GeoModel model)
            {
               
                    string countryname = "";
                    _unitOfWork.DisableProxyCreation = true;
                    using (var db = _unitOfWork)
                    {
                       try                       
                       {
                           //return (from p in _postalcontext.GetCountry_PostalCode_List()
                           //where p.CountryID  == countryid
                           //select p.CountryName ).FirstOrDefault();
                           //return postaldataservicecontext.GetcountryNameBycountryID(profiledata.countryid);      
                            db.SetIsolationToDefault = true;
                            //TDocRecon loandetail2 = new TDocRecon();
                            string query = "sp_GetCountryNameByCountryID";
                            SqlParameter parameter = new SqlParameter("@CountryID", model.countryid);
                            parameter.ParameterName = "@CountryID";
                            parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                            parameter.Size = 40;
                            //Procedure or function 'usp_GetHudFeeReviewLoanDetails' expects parameter '@LoanNbr', which was not supplied.
                            //parameter.TypeName 
                            var parameters = new object[] { parameter };
 
                            //object params                      
                            countryname = db.ExecuteStoredProcedure<string>(query + " @CountryID ", parameters).FirstOrDefault();
                            if (countryname != null) return countryname;
                    

                        }
                        catch (Exception ex)
                        {

                            Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.countryid.ToString(), "", "", ex.Message, ex.InnerException);
                            new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment,  convertedexcption);
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

            public registermodel verifyorupdateregistrationgeodata(ValidateRegistrationGeoDataModel model)
            {
                
                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        ////4-24-2012 fixed code to hanlde if we did not have a postcal code
                        //gpsdata gpsData = new gpsdata();
                        //string[] tempcityAndStateProvince = model.GeoRegisterModel.city .Split(',');
                        ////int countryID;

                        ////attmept to get postal postalcode if it is empty

                        //model.GeoRegisterModel.ziporpostalcode = (model.GeoRegisterModel.ziporpostalcode == null) ?



                        //this.getpostalcodesbycountrynamecity(new GeoModel { country = model.GeoRegisterModel.country, city = tempcityAndStateProvince[0] }).Where(p => p.postalcodevalue == model.GeoRegisterModel.ziporpostalcode).FirstOrDefault().postalcodevalue :
                        //model.GeoRegisterModel.ziporpostalcode;
                        //model.GeoRegisterModel.stateprovince = ((tempcityAndStateProvince.Count() > 1)) ? tempcityAndStateProvince[1] : "NA";
                        ////countryID = postaldataservicecontext.GetcountryIdBycountryName(model.GeoRegisterModel.country);

                        ////check if the  city and country match
                        //if (model.GeoRegisterModel.country == model.GeoMembersModel.myquicksearch.myselectedcountryname && 
                        //    tempcityAndStateProvince[0] == model.GeoMembersModel.myquicksearch.myselectedcity)
                        //{
                        //    if (model.GeoRegisterModel.lattitude  != null | model.GeoRegisterModel.lattitude  == 0)
                        //        return model.GeoRegisterModel;
                        //}

                        ////get GPS data here
                        ////conver the unquiqe coountry Name to an ID
                        ////store country ID for use later
                        ////get the longidtue and latttude 
                        ////1-11-2011 postal code and city are flipped by the way not this function should be renamed
                        ////TO DO rename this function.                  
                        //gpsData = this.getgpsdatabycitycountrypostalcode(new GeoModel { country = model.GeoRegisterModel.country, postalcode = model.GeoRegisterModel.ziporpostalcode, city = tempcityAndStateProvince[0] });
                        //model.GeoRegisterModel.lattitude  = (gpsData != null) ?  Convert.ToDouble( gpsData.Latitude )   : 0;
                        //model.GeoRegisterModel.longitude = (gpsData != null) ? Convert.ToDouble(gpsData.Longitude) : 0;

                       // return model.GeoRegisterModel ;

                        return null;

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException("" , "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
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
            public List<countrypostalcode> getcountryandpostalcodestatuslist()
            {
                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        //TO DO put back in cache
                        //return CachingFactory.SharedObjectHelper.getcountryandpostalcodestatuslist(_postalcontext);

                        var Query = db.GetRepository<Country_PostalCode_List>().Find().ToList().Where(p => p.CountryName != "").OrderBy(p => p.CountryName).ToList();

                       return  (from s in Query
                                                 select new countrypostalcode
                                                 {
                                                     name = s.CountryName,
                                                     code = s.Country_Code,
                                                     customregionid = s.CountryCustomRegionID,
                                                     region = s.Country_Region,
                                                     haspostalcode = Convert.ToBoolean(s.PostalCodes)
                                                 }).ToList();


                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException("", "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


             
            }

            public List<country> getcountrylist()
            {
                
                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
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
                        foreach (Country_PostalCode_List item in db.GetRepository<Country_PostalCode_List>().Find().ToList().OrderBy(p => p.CountryName))
                        {
                            var currentcountry = new country { id = item.CountryID.ToString(), name = item.CountryName };
                            countries.Add(currentcountry);
                        }

                        return countries;
                            #endif

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException("", "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
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
            public bool getpostalcodestatusbycountryname(GeoModel model)

            {

                if (model.country == null ) return false;

                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                             //  List<Country_PostalCode_List> myQuery = default(List<Country_PostalCode_List>);
                            //Dim ctx As New Entities()
                              var myQuery = db.GetRepository<Country_PostalCode_List>().Find().ToList().ToList().Where(p => p.CountryName == model.country).ToList();

                            return (myQuery.Count > 0 ? true : false);
                          //  return myQuery.FirstOrDefault().PostalCodes.Value

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

               
            }

            public int getcountryidbycountryname(GeoModel model)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        if (model.country == null ) return 0;

                        List<Country_PostalCode_List> countryCodeQuery = default(List<Country_PostalCode_List>);
                        //3-18-2013 olawal added code to remove the the spaces when we test
                        countryCodeQuery = db.GetRepository<Country_PostalCode_List>().Find().ToList().Where(p => p.CountryName.Replace(" ", "") == model.country).ToList();

                        if (countryCodeQuery.Count() > 0)
                        {
                            return countryCodeQuery.FirstOrDefault().CountryID;

                        }
                        else
                        {
                            return 0;
                        }


                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                        new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
            }
            
            //Dynamic LINQ to Entites quries 
            //*****************************************************************************************************************************************
            public List<citystateprovince> getcitystateprovincelistbycountrynamepostalcodefilter(GeoModel model)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        if (model.country == null | model.filter == null | model.postalcode == null) return null;


                        List<CityList> _CityList = new List<CityList>();
                        model.postalcode = string.Format("%{0}%", model.postalcode.Replace("'", "''"));
                        // fix country names if theres a space
                        model.country = string.Format(model.country.Replace(" ", ""));
                        //test this as well for added 2/28/2011 - trimming spaces in search text since i am removing spaces in sql script
                        //for cites as well so its a 1 to 1 search no spaces on input and on db side
                        model.filter = string.Format("%{0}%", model.filter.Replace(" ", ""));
                        //11/13/2009 addded wild ca

                        string query = "sp_CityListbycountryNamePostalcodeandCity";

                        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", model.country);
                        parameter.ParameterName = "@StrcountryDatabaseName";
                        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter.Size = 50;

                        SqlParameter parameter2 = new SqlParameter("@StrPrefixText", model.filter);
                        parameter2.ParameterName = "@StrPrefixText";
                        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter2.Size = 40;

                        SqlParameter parameter3 = new SqlParameter("@StrPostalCode", model.postalcode);
                        parameter3.ParameterName = "@StrPostalCode";
                        parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter3.Size = 40;

                        var parameters = new object[] { parameter, parameter2, parameter3 };

                        var citylist = db.ExecuteStoredProcedure<CityList>(query + " @StrcountryDatabaseName,@StrPrefixText,@StrPostalCode", parameters).ToList();

                        
                        int index = 0;
                        return ((from s in citylist.ToList() select new citystateprovince { citystateprovincevalue = s.City + "," + s.State_Province }).ToList());
         

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
              
                
                     
            }

            public List<gpsdata> getgpsdatalistbycountrycitypostalcode(GeoModel model)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        //
                        if (model.country == null | model.city == null | model.postalcode == null) return null;



                         //   IQueryable<GpsData> functionReturnValue = default(IQueryable<GpsData>);

                          List<gpsdata> _GpsData = new List<gpsdata>();
                         model.country = string.Format(model.country.Replace(" ", ""));
                          // fix country names if theres a space
                          // strCity = String.Format("{0}%", strCity) '11/13/2009 addded wild ca

                          string query = "sp_GetGPSDataByPostalCodeandCity";

                          SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", model.country);
                          parameter.ParameterName = "@StrcountryDatabaseName";
                          parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                          parameter.Size = 50;                        

                          SqlParameter parameter2 = new SqlParameter("@StrPostalCode", model.postalcode);
                          parameter2.ParameterName = "@StrPostalCode";
                          parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
                          parameter2.Size = 40;

                          SqlParameter parameter3 = new SqlParameter("@StrCity", model.city);
                          parameter3.ParameterName = "@StrCity";
                          parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
                          parameter3.Size = 100;

                          var parameters = new object[] { parameter,parameter2,parameter3 };

                          var gpsdatalist = db.ExecuteStoredProcedure<gpsdata>(query + " @StrcountryDatabaseName,@StrPostalCode,@StrCity ", parameters).ToList();
                      
                             return ((from s in gpsdatalist.ToList() select new gpsdata {  Latitude = s.Latitude,  Longitude  = s.Longitude, State_Province = s.State_Province }).ToList());
            
        

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

              
            }
         
            public List<gpsdata> getgpsdatalistbycountrycity(GeoModel model)
            {

                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        if (model.country == null | model.city == null) return null;


                        //IQueryable<GpsData> functionReturnValue = default(IQueryable<GpsData>);

                        List<gpsdata> _GpsData = new List<gpsdata>();
                       model.country = string.Format(model.country.Replace(" ", ""));
                        // fix country names if theres a space
                        // strCity = String.Format("{0}%", strCity) '11/13/2009 addded wild ca

                        string query = "sp_GetGPSDataByCountryAndCity";

                        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", model.country);
                        parameter.ParameterName = "@StrcountryDatabaseName";
                        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter.Size = 50;

                    
                        SqlParameter parameter2 = new SqlParameter("@StrCity", model.city);
                        parameter2.ParameterName = "@StrCity";
                        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter2.Size = 100;

                        var parameters = new object[] { parameter, parameter2  };

                        var gpsdatalist = db.ExecuteStoredProcedure<gpsdata>(query + " @StrcountryDatabaseName,@StrCity ", parameters).ToList();

                      //  var gpsdatalist = _postalcontext.GetGpsDataByCountryAndCity(countryname, city);
                        return ((from s in gpsdatalist.ToList() select new gpsdata {   Latitude  = s.Latitude,  Longitude  = s.Longitude,  State_Province  = s.State_Province }).ToList());
         

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


               
            }
        
            public gpsdata getgpsdatabycitycountrypostalcode(GeoModel model)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {


                        if (model.country == null | model.filter == null | model.postalcode == null) return null;

                        //   IQueryable<GpsData> functionReturnValue = default(IQueryable<GpsData>);

                        List<gpsdata> _GpsData = new List<gpsdata>();
                        model.country = string.Format(model.country.Replace(" ", ""));
                        // fix country names if theres a space
                        // strCity = String.Format("{0}%", strCity) '11/13/2009 addded wild ca

                        string query = "sp_GetGPSDataByPostalCodeandCity";

                        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", model.country);
                        parameter.ParameterName = "@StrcountryDatabaseName";
                        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter.Size = 50;

                        SqlParameter parameter2 = new SqlParameter("@StrPostalCode", model.postalcode);
                        parameter2.ParameterName = "@StrPostalCode";
                        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter2.Size = 40;

                        SqlParameter parameter3 = new SqlParameter("@StrCity", model.city);
                        parameter3.ParameterName = "@StrCity";
                        parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter3.Size = 100;

                        var parameters = new object[] { parameter, parameter2, parameter3 };

                        var gpsdata = db.ExecuteStoredProcedure<gpsdata>(query + " @StrcountryDatabaseName,@StrPostalCode,@StrCity", parameters);

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
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
               
             
            }

            public List<postalcode > getpostalcodesbycountrycityfilter(GeoModel model)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        if (model.country == null | model.filter == null | model.filter == null) return null;


                        //added 5/12/2011 to handle empty countries
                        if (model.country == null) return null;

                      //  List<PostalCode> _PostalCodeList = new List<PostalCode>();
                        model.city = string.Format("%{0}%", model.city);
                       model.country = string.Format(model.country.Replace(" ", ""));
                        // fix country names if theres a space
                        model.filter = string.Format("%{0}%", model.filter);
                        //11/13/2009 addded wild ca

                        string query = "sp_GetPostalCodesByCountryNameCityandPrefix";

                        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", model.country);
                        parameter.ParameterName = "@StrcountryDatabaseName";
                        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter.Size = 50;

                        SqlParameter parameter2 = new SqlParameter("@StrCity", model.city);
                        parameter2.ParameterName = "@StrCity";
                        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter2.Size = 100;

                        SqlParameter parameter3 = new SqlParameter("StrprefixText", model.filter);
                        parameter3.ParameterName = "@StrprefixText";
                        parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter3.Size = 40;

                        var parameters = new object[] { parameter, parameter2, parameter3 };

                        var postalcodes = db.ExecuteStoredProcedure<PostalCodeList>(query + " @StrcountryDatabaseName,@StrCity,@StrprefixText", parameters).ToList();

                       // var gpsdatalist = _postalcontext.GetPostalCodesByCountryAndCityPrefixDynamic(countryname, city, filter);
                        //TO DO remove this and reutner object as is
                        return ((from s in postalcodes.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
        

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
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
            private List<postalcode> getpostalcodesbycountrynamecity(GeoModel model)
            {

                _unitOfWork.DisableProxyCreation = true;
              
                    try
                    {

                        if (model.country == null | model.city == null ) return null;


                        //added 5/12/2011 to handle empty countries
                        if (model.country == null) return null;

                        //List<PostalCodeList> _PostalCodeList = new List<PostalCodeList>();
                        model.city = string.Format("%{0}%", model.city);
                       model.country = string.Format(model.country.Replace(" ", ""));
                        // fix country names if theres a space
                        // StrprefixText = string.Format("%{0}%", StrprefixText);
                        //11/13/2009 addded wild ca

                        string query = "sp_GetPostalCodesByCountryNameCity";

                        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", model.country);
                        parameter.ParameterName = "@StrcountryDatabaseName";
                        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter.Size = 50;


                        SqlParameter parameter2 = new SqlParameter("@StrCity", model.city);
                        parameter2.ParameterName = "@StrCity";
                        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter2.Size = 100;

                        var parameters = new object[] { parameter, parameter2 };

                        var postalcodelist = _unitOfWork.ExecuteStoredProcedure<PostalCodeList>(query + " @StrcountryDatabaseName,@StrCity", parameters).ToList();


                      //  var postalcodelist = _postalcontext.getpostalcodesbycountrynamecity(countryname, city);
                        return ((from s in postalcodelist.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
         

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

            public bool validatepostalcodebycountrycitypostalcode(GeoModel model)
            {
                
                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        if (model.country == null | model.filter == null | model.postalcode == null) return false;

                        //Dim _PostalCodeList As New List(Of PostalCodeList)()
                       
                        model.city = string.Format("%{0}%", model.city);
                       model.country = string.Format(model.country.Replace(" ", ""));
                        // fix country names if theres a space

                        string query = "sp_ValidatePostalCodeByCountryNameandCity";

                        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", model.country);
                        parameter.ParameterName = "@StrcountryDatabaseName";
                        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter.Size = 50;

                        SqlParameter parameter2 = new SqlParameter("@StrCity", model.city);
                        parameter2.ParameterName = "@StrCity";
                        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter2.Size = 100;

                        SqlParameter parameter3 = new SqlParameter("@StrPostalCode", model.postalcode);
                        parameter3.ParameterName = "@StrPostalCode";
                        parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter3.Size = 40;
                                           

                        var parameters = new object[] { parameter, parameter2, parameter3 };

                        var foundpostalcodes = db.ExecuteStoredProcedure<PostalCodeList>(query + " @StrcountryDatabaseName,@StrCity,@StrPostalCode", parameters).ToList();

                       // var foundpostalcodes = _postalcontext.ValidatePostalCodeByCOuntryandCity(countryname, city, postalcode);
                       // return foundpostalcodes;
                        if (foundpostalcodes.Count() > 0) return true;
                        return false;

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country.ToString(), "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
               
               
            }

            public List<postalcode> getpostalcodesbycountrylatlong(GeoModel model)
            {

                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        if (model.country == null | model.lattitude == null | model.longitude == null) return null;


                        //added 5/12/2011 to handle empty countries
                        if (model.country == null) return null;
                        //TO DO copy postal code stuff from shared models into here
                        //List<PostalCodeItem> _PostalCodeList = new List<PostalCodeList>();
                       model.country = string.Format(model.country.Replace(" ", ""));
                        // fix country names if theres a space
                        //StrprefixText = string.Format("%{0}%", StrprefixText);


                        string query = "sp_GetPostalCodesByCountryAndLatLong";

                        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", model.country);
                        parameter.ParameterName = "@StrcountryDatabaseName";
                        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter.Size = 50;

                        SqlParameter parameter2 = new SqlParameter("@StrLattitude", model.lattitude);
                        parameter2.ParameterName = "@StrLattitude";
                        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter2.Size = 25;

                        SqlParameter parameter3 = new SqlParameter("@StrLongitude", model.longitude);
                        parameter3.ParameterName = "@StrLongitude";
                        parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter3.Size = 25;


                        var parameters = new object[] { parameter, parameter2, parameter3 };


                        var geopostalcodes = db.ExecuteStoredProcedure<PostalCodeList>(query + " @StrcountryDatabaseName.@StrLattitude,@StrLongitude", parameters);
   

                      //  var geopostalcodes = _postalcontext.GetPostalCodesByCountryAndLatLongDynamic(countryname, lattitude, longitude);
                       if (geopostalcodes != null)
                        return ((from s in geopostalcodes.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());

                       return null;
                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

               
            }

            public List<postalcode> getpostalcodesbycountrynamecitystateprovince(GeoModel model)
            {

                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        if (model.country == null | model.city == null | model.stateprovince == null) return null;


                        //sp_GetPostalCodeByCountryNameCityandStateProvince

                        string query = "sp_GetPostalCodeByCountryNameCityandStateProvince";

                        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", model.country);
                        parameter.ParameterName = "@StrcountryDatabaseName";
                        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter.Size = 50;

                        SqlParameter parameter2 = new SqlParameter("@StrCity", model.city);
                        parameter2.ParameterName = "@StrCity";
                        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter2.Size = 50;

                        SqlParameter parameter3 = new SqlParameter("StrStateProvince", model.stateprovince);
                        parameter3.ParameterName = "@StrStateProvince";
                        parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter3.Size = 100;
                        

                        var parameters = new object[] { parameter, parameter2, parameter3 };

                        var geopostalcodes = db.ExecuteStoredProcedure<PostalCodeList>(query + " @StrcountryDatabaseName,@StrCity,@StrStateProvince", parameters).ToList();

                      //  var geopostalcodes = _postalcontext.GetPostalCodesByCountryNameCityandStateProvinceDynamic(countryname, city, stateprovince);
                        if (geopostalcodes != null)
                        return ((from s in geopostalcodes.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());

                        return null;
                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
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
            public List<citystateprovince> getfilteredcitybycountryandpostalcodefilter(GeoModel model)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        if (model.country == null || model.filter == null) return null;

                       model.country = string.Format(model.country.Replace(" ", ""));

                        //test this as well for added 2/28/2011 - trimming spaces in search text since i am removing spaces in sql script
                        //for cites as well so its a 1 to 1 search no spaces on input and on db side
                        model.filter = string.Format("%{0}%", model.filter.Replace(" ", ""));
                        //11/13/2009 addded wild ca

                        string query = "sp_CityListbycountryNamePostalcodeandCity";

                        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", model.country);
                        parameter.ParameterName = "@StrcountryDatabaseName";
                        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter.Size = 150;
                                                
                        SqlParameter parameter2 = new SqlParameter("@StrPrefixText", model.filter);
                        parameter2.ParameterName = "@StrPrefixText";
                        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter2.Size = 25;

                     

                        var parameters = new object[] { parameter, parameter2 };

                        var cities = db.ExecuteStoredProcedure<CityList>(query + " @StrcountryDatabaseName,@StrPrefixText", parameters).Take(50);

                       // var cities = _postalcontext.GetCityListDynamic(country, "", filter).Take(50);

                        var temp = (from s in cities.ToList() select new citystateprovince { citystateprovincevalue = s.City + "," + s.State_Province }).ToList();
                        return temp;

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


            }

            public List<citystateprovince> getfilteredcitybycountryandcityfilter(GeoModel model)
            {

                //var params = new object[] {new SqlParameter("@FirstName", "Bob")};
                //this._repositoryContext.ObjectContext.ExecuteStoreQuery<ResultType>("GetByName @FirstName", params) 



                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        if (model.country == null || model.filter == null) return null;

                       model.country = string.Format(model.country.Replace(" ", ""));

                        //test this as well for added 2/28/2011 - trimming spaces in search text since i am removing spaces in sql script
                        //for cites as well so its a 1 to 1 search no spaces on input and on db side
                        model.filter = string.Format("%{0}%", model.filter.Replace(" ", ""));
                        //11/13/2009 addded wild ca

                        string query = "sp_CityListbycountryNameCityFilter";

                        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", model.country);
                        parameter.ParameterName = "@StrcountryDatabaseName";
                        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter.Size = 150;

                        SqlParameter parameter2 = new SqlParameter("@StrPrefixText", model.filter);
                        parameter2.ParameterName = "@StrPrefixText";
                        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter2.Size = 25;

                        
                        var parameters = new object[] { parameter, parameter2 };

                        var cities = db.ExecuteStoredProcedure<CityList>(query + " @StrcountryDatabaseName,@StrPrefixText", parameters).Take(50);

                        // var cities = _postalcontext.GetCityListDynamic(country, "", filter).Take(50);

                        var temp = (from s in cities.ToList() select new citystateprovince { citystateprovincevalue = s.City + "," + s.State_Province }).ToList();
                        return temp;

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                        new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


            }

            public List<postalcode> getfilteredpostalcodesbycountrycityfilter(GeoModel model)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        if (model.country == null | model.filter == null | model.city == null) return null;


                        //added 5/12/2011 to handle empty countries
                        if (model.country == null) return null;
                        List<PostalCodeList> _PostalCodeList = new List<PostalCodeList>();
                        model.city = string.Format("%{0}%", model.city);
                       model.country = string.Format(model.country.Replace(" ", ""));
                        // fix country names if theres a space
                        model.filter = string.Format("%{0}%", model.filter);
                        //11/13/2009 addded wild ca

                        //sp_GetPostalCodesByCountryNameCityandPrefix

                        string query = "sp_GetPostalCodesByCountryNameCityandPrefix";

                        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", model.country);
                        parameter.ParameterName = "@StrcountryDatabaseName";
                        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter.Size = 50;

                        SqlParameter parameter2 = new SqlParameter("@StrCity", model.city);
                        parameter2.ParameterName = "@StrCity";
                        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter2.Size = 100;

                        SqlParameter parameter3 = new SqlParameter("StrprefixText", model.filter);
                        parameter3.ParameterName = "@StrprefixText";
                        parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
                        parameter3.Size = 40;

                        var parameters = new object[] { parameter, parameter2, parameter3 };

                        var postalcodes = db.ExecuteStoredProcedure<PostalCodeList>(query + " @StrcountryDatabaseName,@StrCity,@StrPrefixText" + " ", parameters).ToList();

                        //  var customers = _postalcontext.GetPostalCodesByCountryAndCityPrefixDynamic(country, city, filter);

                        if (postalcodes !=null) 
                        return ((from s in postalcodes.Take(25).ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());

                        return null;
                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(model.country, "", "", ex.Message, ex.InnerException);
                         new ErroLogging(logapplicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError,globals.getenviroment, convertedexcption);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
               
             
            }


            #region "Spatial Functions"






            // use this function to get distance at the same time, add it to the model
            public double? getdistancebetweenmembers(GeoModel model)
            {
                return spatialextentions.getdistancebetweenmembers(Convert.ToDouble(model.lattitude), Convert.ToDouble(model.longitude), Convert.ToDouble(model.lattitude2), Convert.ToDouble(model.longitude2), model.unit);
               
            }

          

            #endregion


        }
    }

