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

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getcountrynamebycountryid/{countryId}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string getcountrynamebycountryid(string countryId);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/verifyorupdateregistrationgeodata/", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        registermodel verifyorupdateregistrationgeodata(ValidateRegistrationGeoDataModel model);
        //gets the country list and orders it
        //added sorting

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getcountry_postalcode_listandorderbycountry/", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<Country_PostalCode_List> getcountry_postalcode_listandorderbycountry();
    
        /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// </summary>
        ///       

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getcountry_postalcodestatusbycountryname/{strCountryName}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getcountry_postalcodestatusbycountryname(string strCountryName);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getcountryidbycountryname/{strCountryName}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getcountryidbycountryname(string strCountryName);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getcitylistdynamic/{strCountryName}/{strPrefixText}/{strPostalcode}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<CityList> getcitylistdynamic(string strCountryName, string strPrefixText, string strPostalcode);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgpsdatabycountrypostalcodeandcity/{strCountryName}/{strPostalcode}/{strCity}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<GpsData> getgpsdatabycountrypostalcodeandcity(string strCountryName, string strPostalcode, string strCity);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgpsdatabycountryandcity/{strCountryName}/{strCity}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<GpsData> getgpsdatabycountryandcity(string strCountryName, string strCity);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgpsdatasinglebycitycountryandpostalcode/{strCountryName}/{strPostalcode}/{strCity}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        GpsData getgpsdatasinglebycitycountryandpostalcode(string strCountryName, string strPostalcode, string strCity);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getpostalcodesbycountryandcityprefixdynamic/{strCountryName}/{strCity}/{StrprefixText}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<PostalCodeList> getpostalcodesbycountryandcityprefixdynamic(string strCountryName, string strCity, string StrprefixText);
        //gets the single geo code as string


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgeopostalcodebycountrynameandcity/{strCountryName}/{strCity}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string getgeopostalcodebycountrynameandcity(string strCountryName, string strCity);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/validatepostalcodebycountryandcity/{strCountryName}/{strCity}/{StrPostalCode}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool validatepostalcodebycountryandcity(string strCountryName, string strCity, string StrPostalCode);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getpostalcodesbycountryandlatlongdynamic/{strCountryName}/{strlattitude}/{strlongitude}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<PostalCodeList> getpostalcodesbycountryandlatlongdynamic(string strCountryName, string strlattitude, string strlongitude);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getpostalcodesbycountrynamecityandstateprovincedynamic/{strCountryName}/{strCity}/{strStateProvince}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<PostalCodeList> getpostalcodesbycountrynamecityandstateprovincedynamic(string strCountryName, string strCity, string strStateProvince);

        //TO DO move these lookups to geo
        // List<string> getcountrylist(string countryname);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getcountrylist/", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<country> getcountrylist();


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getfilteredcitiesold/{filter}/{country}/{offset}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<citystateprovince> getfilteredcitiesold(string filter, string country, string offset);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getfilteredcities/{filter}/{country}/{offset}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<citystateprovince> getfilteredcities(string filter, string country, string offset);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getfilteredpostalcodes/{filter}/{country}/{City}/{offset}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<postalcodes> getfilteredpostalcodes(string filter, string country, string City, string offset);

    }

}
