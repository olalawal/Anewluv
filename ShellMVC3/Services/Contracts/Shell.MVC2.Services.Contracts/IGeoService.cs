using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

using System.Text;
using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv;

namespace Shell.MVC2.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IGeoService
    {
        [WebGet]
        [OperationContract]
        string getcountrynamebycountryid(int countryId);
        [WebInvoke ]
        [OperationContract]
        RegisterModel verifyorupdateregistrationgeodata(ValidateRegistrationGeoDataModel model);
        //gets the country list and orders it
        //added sorting
        [WebGet]
        [OperationContract]
        List<Country_PostalCode_List> getcountry_postalcode_listandorderbycountry();
    
        /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// </summary>
        ///       
        [WebGet]
        [OperationContract]
        int getcountry_postalcodestatusbycountryname(string strCountryName);
        
        [WebGet]
        [OperationContract]
        int getcountryidbycountryname(string strCountryName);
        [WebGet]
        [OperationContract]
        List<CityList> getcitylistdynamic(string strCountryName, string strPrefixText, string strPostalcode);
        [WebGet]
        [OperationContract]
        List<GpsData> getgpsdatabycountrypostalcodeandcity(string strCountryName, string strPostalcode, string strCity);
        [WebGet]
        [OperationContract]
        List<GpsData> getgpsdatabycountryandcity(string strCountryName, string strCity);
        [WebGet]
        [OperationContract]
        GpsData getgpsdatasinglebycitycountryandpostalcode(string strCountryName, string strPostalcode, string strCity);
        [WebGet]
        [OperationContract]
        List<PostalCodeList> getpostalcodesbycountryandcityprefixdynamic(string strCountryName, string strCity, string StrprefixText);
        //gets the single geo code as string
        [WebGet]
        [OperationContract]
        string getgeopostalcodebycountrynameandcity(string strCountryName, string strCity);
        [WebGet]
        [OperationContract]
        bool validatepostalcodebycountryandcity(string strCountryName, string strCity, string StrPostalCode);
        [WebGet]
        [OperationContract]
        List<PostalCodeList> getpostalcodesbycountryandlatlongdynamic(string strCountryName, string strlattitude, string strlongitude);
        [WebGet]
        [OperationContract]
        List<PostalCodeList> getpostalcodesbycountrynamecityandstateprovincedynamic(string strCountryName, string strCity, string strStateProvince);

        //TO DO move these lookups to geo
        // List<string> getcountrylist(string countryname);
        [WebGet]
        [OperationContract]
        List<country> getcountrylist();
        [WebGet]
        [OperationContract]
        List<citystateprovince> getfilteredcitiesold(string filter, string country, int offset);
        [WebGet]
        [OperationContract]
        List<citystateprovince> getfilteredcities(string filter, string country, int offset);
        [WebGet]
        [OperationContract]
        List<postalcodes> getfilteredpostalcodes(string filter, string country, string City, int offset);

    }

}
