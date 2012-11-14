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



namespace Shell.MVC2.Services.Dating
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
 

        public class GeoService :  IGeoService
        {
            private IGeoRepository _georepository;
            private string _apikey;

            public GeoService(IGeoRepository georepository)
            {
                _georepository = georepository;
                _apikey = HttpContext.Current.Request.QueryString["apikey"];
                throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);
            }


            public string getcountrynamebycountryid(int countryid)
            {
                return _georepository.getcountrynamebycountryid(countryid);
            }
            public RegisterModel verifyorupdateregistrationgeodata(ValidateRegistrationGeoDataModel model)
            {
                return _georepository.verifyorupdateregistrationgeodata(model);

            }
            //gets the country list and orders it
            //added sorting
            public List<Country_PostalCode_List> getcountry_postalcode_listandorderbycountry()
            {
                return _georepository.getcountry_postalcode_listandorderbycountry();
            }

            /// <summary>
            /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
            /// </summary>
            /// 
            public int getcountry_postalcodestatusbycountryname(string strCountryName)
            {
                return _georepository.getcountry_postalcodestatusbycountryname(strCountryName);
            }

            public int getcountryidbycountryname(string strCountryName)
            {
                return _georepository.getcountryidbycountryname(strCountryName);
            }
            //Dynamic LINQ to Entites quries 
            //*****************************************************************************************************************************************
            public List<CityList> getcitylistdynamic(string strCountryName, string strPrefixText, string strPostalcode)
            {
                return _georepository.getcitylistdynamic(strCountryName,  strPrefixText,  strPostalcode);
                     
            }
            public List<GpsData> getgpsdatabycountrypostalcodeandcity(string strCountryName, string strPostalcode, string strCity)
            {
                return _georepository.getgpsdatabycountrypostalcodeandcity(strCountryName,  strPostalcode,  strCity);
            }
            public List<GpsData> getgpsdatabycountryandcity(string strCountryName, string strCity)
            {
                 return _georepository.getgpsdatabycountryandcity( strCountryName,  strCity);
            }
            public GpsData getgpsdatasinglebycitycountryandpostalcode(string strCountryName, string strPostalcode, string strCity)
            {
                return _georepository.getgpsdatasinglebycitycountryandpostalcode(strCountryName,  strPostalcode,  strCity);
            }
            public List<PostalCodeList> getpostalcodesbycountryandcityprefixdynamic(string strCountryName, string strCity, string StrprefixText)
            {
                return _georepository.getpostalcodesbycountryandcityprefixdynamic(strCountryName,  strCity,  StrprefixText);
            }
            //gets the single geo code as string
            public string getgeopostalcodebycountrynameandcity(string strCountryName, string strCity)
            {
                return _georepository.getgeopostalcodebycountrynameandcity(strCountryName, strCity);
            }

            public bool validatepostalcodebycountryandcity(string strCountryName, string strCity, string StrPostalCode)
            {
                return _georepository.validatepostalcodebycountryandcity(strCountryName,  strCity,  StrPostalCode);
            }

            public List<PostalCodeList> getpostalcodesbycountryandlatlongdynamic(string strCountryName, string strlattitude, string strlongitude)
            {
                return _georepository.getpostalcodesbycountryandlatlongdynamic(strCountryName,  strlattitude,  strlongitude);
            }
            public List<PostalCodeList> getpostalcodesbycountrynamecityandstateprovincedynamic(string strCountryName, string strCity, string strStateProvince)
            {
                return _georepository.getpostalcodesbycountrynamecityandstateprovincedynamic(strCountryName, strCity,  strStateProvince);
            }
            public List<country> getcountrylist()
            {
                return _georepository.getcountrylist();
            }
            public List<citystateprovince> getfilteredcitiesold(string filter, string Country, int offset)
            {
                return _georepository.getfilteredcitiesold(filter,  Country,  offset);
            }
            public List<citystateprovince> getfilteredcities(string filter, string Country, int offset)
            {
                return _georepository.getfilteredcities(filter, Country, offset);
            }
            public List<postalcodes> getfilteredpostalcodes(string filter, string Country, string City, int offset)
            {
                return _georepository.getfilteredpostalcodes(filter, Country, City, offset);
            }
     
        }
    }

