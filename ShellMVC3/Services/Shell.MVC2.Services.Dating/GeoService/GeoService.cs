using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Interfaces;
using Dating.Server.Data.Models;

using System.Web;
using System.Net;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

using Shell.MVC2.Services.Contracts;
using System.ServiceModel.Activation;



namespace Shell.MVC2.Services.Dating
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
 
        [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
        [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]  
        public class GeoService :  IGeoService
        {
            private IGeoRepository _georepository;
           

            public GeoService(IGeoRepository georepository)
            {
                _georepository = georepository;
     
            }


            public string getcountrynamebycountryid(string countryid)
            {
               
                try
                {

                    return _georepository.getcountrynamebycountryid(Convert.ToInt32(countryid));

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
            public registermodel verifyorupdateregistrationgeodata(ValidateRegistrationGeoDataModel model)
            {
              
                try
                {
                    return _georepository.verifyorupdateregistrationgeodata(model);


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
            //gets the country list and orders it
            //added sorting
            public List<countrypostalcode> getcountryandpostalcodestatuslist()
            {
              
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

            /// <summary>
            /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
            /// </summary>
            /// 
            public bool getpostalcodestatusbycountryname(string strcountryname)
            {
              
                try
                {
                    return _georepository.getpostalcodestatusbycountryname(strcountryname);


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

            public int getcountryidbycountryname(string strCountryName)
            {
               
                try
                {

                    return _georepository.getcountryidbycountryname(strCountryName);

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
            public List<CityList> getcitylistdynamic(string strCountryName, string strPrefixText, string strPostalcode)
            {
              
                try
                {
                    return _georepository.getcitylistdynamic(strCountryName, strPrefixText, strPostalcode);

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
            public List<GpsData> getgpsdatabycountrypostalcodeandcity(string strCountryName, string strPostalcode, string strCity)
            {
              
                try
                {
                    return _georepository.getgpsdatabycountrypostalcodeandcity(strCountryName, strPostalcode, strCity);


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
            public List<GpsData> getgpsdatabycountryandcity(string strCountryName, string strCity)
            {
               
                 try
                 {
                     return _georepository.getgpsdatabycountryandcity(strCountryName, strCity);


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
            public GpsData getgpsdatasinglebycitycountryandpostalcode(string strCountryName, string strPostalcode, string strCity)
            {
               
                try
                {

                    return _georepository.getgpsdatasinglebycitycountryandpostalcode(strCountryName, strPostalcode, strCity);

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
            public List<PostalCodeList> getpostalcodesbycountryandcityprefixdynamic(string strCountryName, string strCity, string StrprefixText)
            {
                
                try
                {
                    return _georepository.getpostalcodesbycountryandcityprefixdynamic(strCountryName, strCity, StrprefixText);

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
            public string getgeopostalcodebycountrynameandcity(string strCountryName, string strCity)
            {
               
                try
                {
                    return _georepository.getgeopostalcodebycountrynameandcity(strCountryName, strCity);


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

            public bool validatepostalcodebycountryandcity(string strCountryName, string strCity, string StrPostalCode)
            {
               
                try
                {
                    return _georepository.validatepostalcodebycountryandcity(strCountryName, strCity, StrPostalCode);


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

            public List<PostalCodeList> getpostalcodesbycountryandlatlongdynamic(string strCountryName, string strlattitude, string strlongitude)
            {
               
                try
                {

                    return _georepository.getpostalcodesbycountryandlatlongdynamic(strCountryName, strlattitude, strlongitude);


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
            public List<PostalCodeList> getpostalcodesbycountrynamecityandstateprovincedynamic(string strCountryName, string strCity, string strStateProvince)
            {
                
                try
                {
                    return _georepository.getpostalcodesbycountrynamecityandstateprovincedynamic(strCountryName, strCity, strStateProvince);

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

            public List<citystateprovince> getfilteredcitiesold(string filter, string Country, string offset)
            {
              
                try
                {

                    return _georepository.getfilteredcitiesold(filter, Country, Convert.ToInt32(offset));

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
            public List<citystateprovince> getfilteredcities(string filter, string Country, string offset)
            {
             
                try
                {

                    return _georepository.getfilteredcities(filter, Country, Convert.ToInt32(offset));

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
            public List<postalcodes> getfilteredpostalcodes(string filter, string Country, string City, string offset)
            {
               
                try
                {

                    return _georepository.getfilteredpostalcodes(filter, Country, City, Convert.ToInt32(offset));

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

