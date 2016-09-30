using GeoData.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoData.Domain.Models
{
public interface IGeoDataStoredProcedures
{

    string GetCountryNameByCountryID(string countryid);

    Task<int> GetCountryCountryIDByCountryName(string countryname);

    IEnumerable<Country_PostalCode_List> GetCountryPostalCodeList();


     IEnumerable<CityList> CityListbycountryNamePostalcodeandCity(string countryname, string filter, string PostalCodeList);



     IEnumerable<gpsdata> GetGPSDatasByCountryIdPostalCode(string countryid, string PostalCode);

    IEnumerable<gpsdata> GetGPSDatasByCountryPostalCode(string countryname, string PostalCode);

    IEnumerable<gpsdata> GetGPSDataByCountryAndCityStateProvince(string countryname, string cityname, string stateprovince);

    IEnumerable<gpsdata> GetGPSDataByCountryIdAndCityStateProvince(string countryid, string cityname, string stateprovince);

    IEnumerable<gpsdata> GetGPSDataByCountryAndCity(string countryname, string cityname);

    IEnumerable<gpsdata> GetGPSDataByCountryIdAndCity(string countryid, string cityname);

    IEnumerable<PostalCodeList> GetPostalCodesByCountryNameCityandPrefix(string countryname,string cityname, string filter);
   
     IEnumerable<PostalCodeList> GetPostalCodesByCountryNameCity(string countryname, string cityname);
   
     IEnumerable<PostalCodeList> ValidatePostalCodeByCountryNameandCity(string countryname, string cityname, string strpostalcode);
   
     IEnumerable<PostalCodeList> GetPostalCodesByCountryAndLatLong(string countryname, string lattitude, string longitude);
   
     IEnumerable<PostalCodeList> GetPostalCodesByCountryNameCityandStateProvince(string  countryname,string cityname, string stateprovince);
   
     IEnumerable<CityList> CityListbycountryNamePostalcodeandCity(string countryname,string filter);
 
     IEnumerable<CityList> CityListbycountryNameCityFilter(string countryname, string filter) ; 

     IEnumerable<CityList> CityListbycountryIDCityFilter(string countryid,string filter);

     IEnumerable<PostalCodeList> GetPostalCodesByCountryIDCityandPrefix(string countryid,string cityname,string filter);

     Task<PostalCodeList> GetPostalCodeByCountryNameandCity(string countryname, string city);

     bool GetPostalCodeStatusByCountryID(string countryid);

     bool  GetPostalCodeStatusBycountryName(string countryname);


     Task<CityList> GetCityStateprovinceBycountryandlatlong(string countryname, string lattitude, string longitude);
   
}
}