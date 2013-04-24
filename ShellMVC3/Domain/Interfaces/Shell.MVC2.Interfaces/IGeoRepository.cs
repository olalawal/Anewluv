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
        bool getpostalcodestatusbycountryname(string country);
        int getcountryidbycountryname(string country);
        List<citystateprovince> getcitystateprovincelistbycountrynamepostalcodefilter(string country, string postalcode, string filter);
        List<gpsdata> getgpsdatalistbycountrycitypostalcode(string country,  string city,string postalcode);
        List<gpsdata> getgpsdatalistbycountrycity(string country, string city);
        gpsdata getgpsdatabycitycountrypostalcode(string country,string city, string postalcode);
        List<postalcode > getpostalcodesbycountrycityfilter(string country,string city, string filter);
       //gets the single geo code as string
        List<postalcode> getpostalcodesbycountrynamecity(string country, string city);
        bool validatepostalcodebycountrycitypostalcode(string country, string city, string postalcode);
        List<postalcode> getpostalcodesbycountrylatlong(string country, string lattitude, string longitude);
        List<postalcode> getpostalcodesbycountrynamecitystateprovince(string country, string city, string stateprovince);
      
 //Moved from lookup service
        //List<citystateprovince> getfilteredcitiesold(string country, int MaxItems);    
       List<citystateprovince> getfilteredcitiesbycountryfilter(string country,string filter);
       List<postalcode> getfilteredpostalcodesbycountrycityfilter(string country, string city, string filter);
      
       double? getdistancebetweenmembers(double lat1, double lon1, double lat2, double lon2, string unit);
    }
}
