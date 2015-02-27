using GeoData.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace GeoData.Domain.Models
{
public interface IGeoDataStoredProcedures
{

      string GetCountryNameByCountryID(string countryid);

     IEnumerable<CityList> CityListbycountryNamePostalcodeandCity(string countryname, string filter, string PostalCodeList);
   
      IEnumerable<gpsdata> GetGPSDatasByPostalCodeandCity(string countryname,string cityname, string PostalCode );
 
     IEnumerable<gpsdata> GetGPSDataByCountryAndCity(string countryname, string cityname );
    
     IEnumerable<PostalCodeList> GetPostalCodesByCountryNameCityandPrefix(string countryname,string cityname, string filter);
   
     IEnumerable<PostalCodeList> GetPostalCodesByCountryNameCity(string countryname, string cityname);
   
     IEnumerable<PostalCodeList> ValidatePostalCodeByCountryNameandCity(string countryname, string cityname, string strpostalcode);
   
     IEnumerable<PostalCodeList> GetPostalCodesByCountryAndLatLong(string countryname, string lattitude, string longitude);
   
     IEnumerable<PostalCodeList> GetPostalCodesByCountryNameCityandStateProvince(string  countryname,string cityname, string stateprovince);
   
     IEnumerable<CityList> CityListbycountryNamePostalcodeandCity(string countryname,string filter);
 
     IEnumerable<CityList> CityListbycountryNameCityFilter(string countryname, string filter) ; 

     IEnumerable<CityList> CityListbycountryIDCityFilter(string countryid,string filter);

     IEnumerable<PostalCodeList> GetPostalCodesByCountryIDCityandPrefix(string countryid,string cityname,string filter);
   
}
}