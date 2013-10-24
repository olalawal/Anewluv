using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Dating.Server.Data.Models;

using System.Web;
using System.Net;



using Shell.MVC2.Services.Contracts;
using System.ServiceModel.Activation;
using Anewluv.DataAccess.Interfaces;
using Anewluv.Domain.Data;
using LoggingLibrary;
using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
using System.Data.SqlClient;



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



            public string getcountrynamebycountryid(string countryid)
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
                            SqlParameter parameter = new SqlParameter("CountryID", countryid);
                            parameter.ParameterName = "@CountryID";
                            parameter.SqlDbType = System.Data.SqlDbType.VarChar;
                            parameter.Size = 40;
                            //Procedure or function 'usp_GetHudFeeReviewLoanDetails' expects parameter '@LoanNbr', which was not supplied.
                            //parameter.TypeName 
                            var parameters = new object[] { parameter };
 
                            //object params                      
                             countryname = db.ObjectContext.ExecuteStoreQuery(query + " " + parameter.ParameterName, parameters).FirstOrDefault();
                            if (countryname != null) return countryname;
                    
                        }
                        catch (Exception ex)
                        {

                            Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                            new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption);
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
        //4-24-2012 fixed code to hanlde if we did not have a postcal code
                gpsdata gpsData = new gpsdata();
            string[] tempcityAndStateProvince = model.GeoRegisterModel.city .Split(',');
            //int countryID;

            //attmept to get postal postalcode if it is empty

            model.GeoRegisterModel.ziporpostalcode = (model.GeoRegisterModel.ziporpostalcode == null) ? 
           this.getpostalcodesbycountrynamecity(model.GeoRegisterModel.country, tempcityAndStateProvince[0]).Where(p=>p.postalcodevalue  == model.GeoRegisterModel.ziporpostalcode ).FirstOrDefault().postalcodevalue  :
           model.GeoRegisterModel.ziporpostalcode;
            model.GeoRegisterModel.stateprovince = ((tempcityAndStateProvince.Count() > 1)) ? tempcityAndStateProvince[1] : "NA";
            //countryID = postaldataservicecontext.GetcountryIdBycountryName(model.GeoRegisterModel.country);

            //check if the  city and country match
            if (model.GeoRegisterModel.country == model.GeoMembersModel.myquicksearch.myselectedcountryname && 
                tempcityAndStateProvince[0] == model.GeoMembersModel.myquicksearch.myselectedcity)
            {
                if (model.GeoRegisterModel.lattitude  != null | model.GeoRegisterModel.lattitude  == 0)
                    return model.GeoRegisterModel;

            }

            //get GPS data here
            //conver the unquiqe coountry Name to an ID
            //store country ID for use later
            //get the longidtue and latttude 
            //1-11-2011 postal code and city are flipped by the way not this function should be renamed
            //TO DO rename this function.                  
            gpsData = this.getgpsdatabycitycountrypostalcode(model.GeoRegisterModel.country, model.GeoRegisterModel.ziporpostalcode, tempcityAndStateProvince[0]);


            model.GeoRegisterModel.lattitude  = (gpsData != null) ? gpsData.lattitude   : 0;
            model.GeoRegisterModel.longitude = (gpsData != null) ? gpsData.longitude   : 0;

            return model.GeoRegisterModel ;

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


              
                    return _georepository.verifyorupdateregistrationgeodata(model);


              
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
                                return CachingFactory.SharedObjectHelper.getcountryandpostalcodestatuslist(_postalcontext);


                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


                try
                {

                    return _georepository.getcountryandpostalcodestatuslist();

                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            public List<country> getcountrylist()
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                            #if DISCONECTED
                       
                                                    List<country> countrylist = new List<country>();
                                                    countrylist.Add(new country { countryvalue = "United", countryindex = "44", selected = false });
                                                    countrylist.Add(new country { countryvalue = "Canada", countryindex = "43", selected = false });
                                                    return countrylist;

                            #else
                                            //List<country> tmplist = new List<country>();
                                            //// Loop over the int List and modify it.
                                            ////insert the first one as ANY
                                            //tmplist.Add(new country { id = "0", name  = "Any" });
                                            //foreach (countrypostalcode item in this.getcountry_postalcode_listandorderbycountry())
                                            //{
                                            //    var currentcountry = new country { id = item.id .ToString(),  name = item.name   };
                                            //    tmplist.Add(currentcountry);
                                            //}
                                            //return tmplist;
                                            return CachingFactory.SharedObjectHelper.getcountrylist(_postalcontext);

                            #endif

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

                try
                {
                    return _georepository.getcountrylist();


                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
           
            /// <summary>
            /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
            /// </summary>
            /// 
            public bool getpostalcodestatusbycountryname(string countryname)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                               List<Country_PostalCode_List> myQuery = default(List<Country_PostalCode_List>);
            //Dim ctx As New Entities()
            myQuery = _postalcontext.GetCountry_PostalCode_List().ToList().Where(p => p.CountryName  == countryname).ToList();

            return (myQuery.Count > 0 ? true : false);
          //  return myQuery.FirstOrDefault().PostalCodes.Value

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

                try
                {
                    return _georepository.getpostalcodestatusbycountryname(countryname);


                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            public int getcountryidbycountryname(string country)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                         List<Country_PostalCode_List> countryCodeQuery = default(List<Country_PostalCode_List>);
                 //3-18-2013 olawal added code to remove the the spaces when we test
            countryCodeQuery = _postalcontext.GetCountry_PostalCode_List().ToList().Where(p => p.CountryName .Replace(" ","") == countryname).ToList();

            if (countryCodeQuery.Count() > 0)
            {
                return countryCodeQuery.FirstOrDefault().CountryID ;

            }
            else
            {
                return 0;
            }
            }

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

                try
                {

                    return _georepository.getcountryidbycountryname(country);

                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            //Dynamic LINQ to Entites quries 
            //*****************************************************************************************************************************************
            public List<citystateprovince> getcitystateprovincelistbycountrynamepostalcodefilter(string country, string postalcode, string filter)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        var citylist = _postalcontext.GetCityListDynamic(countryname, postalcode, filter);
                        int index = 0;
                        return ((from s in citylist.ToList() select new citystateprovince { citystateprovincevalue = s.City + "," + s.State_Province }).ToList());
         

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
              
                try
                {
                    return _georepository.getcitystateprovincelistbycountrynamepostalcodefilter(country, postalcode ,filter );

                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
                     
            }
            public List<gpsdata> getgpsdatalistbycountrycitypostalcode(string country, string city, string postalcode)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        var gpsdatalist = _postalcontext.GetGpsDataByCountryPostalCodeandCity(countryname, postalcode, city);
                        return ((from s in gpsdatalist.ToList() select new gpsdata { lattitude = s.Latitude, longitude = s.Longitude, stateprovince = s.State_Province }).ToList());
            
        

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

                try
                {
                    return _georepository.getgpsdatalistbycountrycitypostalcode(country, city,postalcode );


                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            public List<gpsdata> getgpsdatalistbycountrycity(string country, string city)
            {

                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        var gpsdatalist = _postalcontext.GetGpsDataByCountryAndCity(countryname, city);
                        return ((from s in gpsdatalist.ToList() select new gpsdata { lattitude = s.Latitude, longitude = s.Longitude, stateprovince = s.State_Province }).ToList());
         

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


                 try
                 {
                     return _georepository.getgpsdatalistbycountrycity(country, city);


                 }
                 catch (Exception ex)
                 {
                     //can parse the error to build a more custom error mssage and populate fualt faultreason
                     FaultReason faultreason = new FaultReason("Error in GeoService service");
                     string ErrorMessage = "";
                     string ErrorDetail = "ErrorMessage: " + ex.Message;
                     throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                 }
            }
            public gpsdata getgpsdatabycitycountrypostalcode(string country, string city, string postalcode)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        var s = _postalcontext.GetGpsDataSingleByCityCountryAndPostalCode(countryname, postalcode, city);
                        if (s != null)
                        {
                            return new gpsdata { lattitude = s.Latitude, longitude = s.Longitude, stateprovince = s.State_Province };
                        }
                        return gpsdata;

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
               
                try
                {

                    return _georepository.getgpsdatabycitycountrypostalcode(country,city,postalcode);

                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            public List<postalcode > getpostalcodesbycountrycityfilter(string country,string city, string filter)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        var gpsdatalist = _postalcontext.GetPostalCodesByCountryAndCityPrefixDynamic(countryname, city, filter);
                        return ((from s in gpsdatalist.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
        

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

                try
                {
                    return _georepository.getpostalcodesbycountrycityfilter(country, city, filter);

                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            //gets the single geo code as string
            public List<postalcode> getpostalcodesbycountrynamecity(string country, string city)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        var postalcodelist = _postalcontext.getpostalcodesbycountrynamecity(countryname, city);
                        return ((from s in postalcodelist.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
         

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


                try
                {
                    return _georepository.getpostalcodesbycountrynamecity(country, city);


                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

            public bool validatepostalcodebycountrycitypostalcode(string country, string city, string postalcode)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        var foundpostalcodes = _postalcontext.ValidatePostalCodeByCOuntryandCity(countryname, city, postalcode);
                        return foundpostalcodes;
                        //if (foundpostalcodes Count() > 0) return true;

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
               
                try
                {
                    return _georepository.validatepostalcodebycountrycitypostalcode(country, city, postalcode);


                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }

            public List<postalcode> getpostalcodesbycountrylatlong(string country, string lattitude, string longitude)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        var geopostalcodes = _postalcontext.GetPostalCodesByCountryAndLatLongDynamic(countryname, lattitude, longitude);
                        return ((from s in geopostalcodes.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
        

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }

                try
                {

                    return _georepository.getpostalcodesbycountrylatlong(country, lattitude, longitude);


                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            public List<postalcode> getpostalcodesbycountrynamecitystateprovince(string country, string city, string stateprovince)
            {

                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {
                        var geopostalcodes = _postalcontext.GetPostalCodesByCountryNameCityandStateProvinceDynamic(countryname, city, stateprovince);
                        return ((from s in geopostalcodes.ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());
         

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


                try
                {
                    return _georepository.getpostalcodesbycountrynamecitystateprovince(country, city, stateprovince);

                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            public List<citystateprovince> getfilteredcitiesbycountryfilter(string country, string filter)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {

                        var customers = _postalcontext.GetCityListDynamic(country, "", filter).Take(50);

                        temp = (from s in customers.ToList() select new citystateprovince { citystateprovincevalue = s.City + "," + s.State_Province }).ToList();
                        return temp;

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }


                try
                {

                    return _georepository.getfilteredcitiesbycountryfilter(country, filter);

                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
            public List<postalcode> getfilteredpostalcodesbycountrycityfilter(string country, string city, string filter)
            {


                _unitOfWork.DisableProxyCreation = true;
                using (var db = _unitOfWork)
                {
                    try
                    {


                        var customers = _postalcontext.GetPostalCodesByCountryAndCityPrefixDynamic(country, city, filter);

                        return ((from s in customers.Take(25).ToList() select new postalcode { postalcodevalue = s.PostalCode }).ToList());

                    }
                    catch (Exception ex)
                    {

                        Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                        new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                        //can parse the error to build a more custom error mssage and populate fualt faultreason
                        FaultReason faultreason = new FaultReason("Error in GeoService service");
                        string ErrorMessage = "";
                        string ErrorDetail = "ErrorMessage: " + ex.Message;
                        throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                        //throw convertedexcption;
                    }

                }
               
                try
                {

                    return _georepository.getfilteredpostalcodesbycountrycityfilter(country, city, filter);

                }
                catch (Exception ex)
                {
                    //can parse the error to build a more custom error mssage and populate fualt faultreason
                    FaultReason faultreason = new FaultReason("Error in GeoService service");
                    string ErrorMessage = "";
                    string ErrorDetail = "ErrorMessage: " + ex.Message;
                    throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
                }
            }
     
        }
    }

