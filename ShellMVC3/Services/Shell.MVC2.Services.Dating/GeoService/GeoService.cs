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
           
            /// <summary>
            /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
            /// </summary>
            /// 
            public bool getpostalcodestatusbycountryname(string countryname)
            {
              
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

