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

       string getcountrynamebycountryid(int countryid);
       registermodel verifyorupdateregistrationgeodata(ValidateRegistrationGeoDataModel model);
       //gets the country list and orders it
       //added sorting
        List<countrypostalcode> getcountryandpostalcodestatuslist();
        List<country> getcountrylist();//10-15-2012 ned method to for drop down lists
       /// <summary>
       /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
       /// </summary>
       ///       
        bool getpostalcodestatusbycountryname(string countryname);

        int getcountryidbycountryname(string countryname);
        List<citystateprovince> getcitylistdynamic(string countryname, string postalcode, string prfefixtext);
        List<gpsdata> getgpsdatabycountrypostalcodeandcity(string countryname, string postalcode, string city);
        List<gpsdata> getgpsdatabycountryandcity(string countryname, string city);
        gpsdata getgpsdatasinglebycitycountryandpostalcode(string countryname, string postalcode, string city);
        List<postalcode > getpostalcodesbycountryandcityprefixdynamic(string countryname, string city, string prefixtext);
       //gets the single geo code as string
        List<postalcode> getgeopostalcodebycountrynameandcity(string countryname, string city);
        bool validatepostalcodebycountryandcity(string countryname, string city, string postalcode);
        List<postalcode> getpostalcodesbycountryandlatlongdynamic(string countryname, string lattitude, string longitude);
        List<postalcode> getpostalcodesbycountrynamecityandstateprovincedynamic(string countryname, string city, string stateprovince);

             
    
 //Moved from lookup service
        List<citystateprovince> getfilteredcitiesold(string filter, string country, int offset);    
        List<citystateprovince> getfilteredcities(string filter, string country, int offset);
        List<postalcode> getfilteredpostalcodes(string filter, string country, string City, int offset);

        double? getdistancebetweenmembers(double lat1, double lon1, double lat2, double lon2, string unit);
    }
}
