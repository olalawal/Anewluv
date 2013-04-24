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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code  config file together.
    [ServiceContract]
    public interface IGeoService
    {

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getcountrynamebycountryid/{countryid}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string getcountrynamebycountryid(string countryid);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebInvoke(UriTemplate = "/verifyorupdateregistrationgeodata/", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        registermodel verifyorupdateregistrationgeodata(ValidateRegistrationGeoDataModel model);
        //gets the country list  orders it
        //added sorting

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getcountryandpostalcodestatuslist/", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<countrypostalcode> getcountryandpostalcodestatuslist();

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getcountrylist/", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<country> getcountrylist();

        /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// </summary>
        ///       

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getpostalcodestatusbycountryname/{country}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool getpostalcodestatusbycountryname(string country);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getcountryidbycountryname/{country}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        int getcountryidbycountryname(string country);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getcitystateprovincelistbycountrynamepostalcodefilter/{country}/{postalcode}/{filter}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<citystateprovince> getcitystateprovincelistbycountrynamepostalcodefilter(string country, string postalcode, string filter);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgpsdatalistbycountrycitypostalcode/{country}/{city}/{postalcode}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<gpsdata> getgpsdatalistbycountrycitypostalcode(string country, string city, string postalcode);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgpsdatalistbycountrycity/{country}/{city}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<gpsdata> getgpsdatalistbycountrycity(string country, string city);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getgpsdatabycitycountrypostalcode/{country}/{city}/{postalcode}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        gpsdata getgpsdatabycitycountrypostalcode(string country, string city, string postalcode);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getpostalcodesbycountrycityfilter/{country}/{city}/{filter}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<postalcode> getpostalcodesbycountrycityfilter(string country, string city, string filter);
        //gets the single geo code as string


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getpostalcodesbycountrynamecity/{country}/{city}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<postalcode> getpostalcodesbycountrynamecity(string country, string city);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/validatepostalcodebycountrycitypostalcode/{country}/{city}/{postalcode}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        bool validatepostalcodebycountrycitypostalcode(string country, string city, string postalcode);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getpostalcodesbycountryandlatlong/{country}/{lattitude}/{longitude}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<postalcode> getpostalcodesbycountrylatlong(string country, string lattitude, string longitude);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getpostalcodesbycountrynamecityandstateprovince/{country}/{city}/{stateprovince}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<postalcode> getpostalcodesbycountrynamecitystateprovince(string country, string city, string stateprovince);

        //TO DO move these lookups to geo
        // List<string> getcountrylist(string country);

        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getfilteredcitiesbycountryandfilter/{country}/{filter}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<citystateprovince> getfilteredcitiesbycountryfilter(string country, string filter);


        [OperationContract(), FaultContractAttribute(typeof(ServiceFault), Action = "http://Schemas.Testws.Medtox.com")]
        [WebGet(UriTemplate = "/getfilteredpostalcodesbycountrycityandfilter/{country}/{city}/{filter}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        List<postalcode> getfilteredpostalcodesbycountrycityfilter(string country, string city, string filter);

    }

}
