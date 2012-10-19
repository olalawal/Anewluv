using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
//using System.ServiceModel;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using Shell.MVC2.Domain.Entities.Anewluv;
using System.Text;
using Dating.Server.Data.Models;


namespace Shell.MVC2.Interfaces
{

  public interface IGeoRepository
    {

       string getcountrynamebycountryid(int countryId);
       RegisterModel verifyorupdateregistrationgeodata(ValidateRegistrationGeoDataModel model);
       //gets the country list and orders it
       //added sorting
        List<Country_PostalCode_List> getcountry_postalcode_listandorderbycountry();
        List<country> getcountrylist();//10-15-2012 ned method to for drop down lists
       /// <summary>
       /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
       /// </summary>
       ///       
        int getcountry_postalcodestatusbycountryname(string strCountryName);
        string getcountrynamebycountryid(int strCountryID)   ;
        int getcountryidbycountryname(string strCountryName);
        List<CityList> getcitylistdynamic(string strCountryName, string strPrefixText, string strPostalcode);
        List<GpsData> getgpsdatabycountrypostalcodeandcity(string strCountryName, string strPostalcode, string strCity);
        List<GpsData> getgpsdatabycountryandcity(string strCountryName, string strCity);
        GpsData getgpsdatasinglebycitycountryandpostalcode(string strCountryName, string strPostalcode, string strCity);
        List<PostalCodeList> getpostalcodesbycountryandcityprefixdynamic(string strCountryName, string strCity, string StrprefixText);
       //gets the single geo code as string
        string getgeopostalcodebycountrynameandcity(string strCountryName, string strCity);
        bool validatepostalcodebycountryandcity(string strCountryName, string strCity, string StrPostalCode);
        List<PostalCodeList> getpostalcodesbycountryandlatlongdynamic(string strCountryName, string strlattitude, string strlongitude);
        List<PostalCodeList> getpostalcodesbycountrynamecityandstateprovincedynamic(string strCountryName, string strCity, string strStateProvince);

             
    
 //Moved from lookup service
        List<citystateprovince> getfilteredcitiesold(string filter, string country, int offset);
    
        List<citystateprovince> getfilteredcities(string filter, string country, int offset);
    
        List<postalcodes> getfilteredpostalcodes(string filter, string country, string City, int offset);
    }
}
