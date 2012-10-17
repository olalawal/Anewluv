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

       string GetCountryNameByCountryId(int countryId);
       RegisterModel VerifyOrUpdateRegistrationGeoData(ValidateRegistrationGeoDataModel model);
       //gets the country list and orders it
       //added sorting
        List<Country_PostalCode_List> GetCountry_PostalCode_ListAndOrderByCountry();
        List<country> getcountrylist();//10-15-2012 ned method to for drop down lists
       /// <summary>
       /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
       /// </summary>
       ///       
        int GetCountry_PostalCodeStatusByCountryName(string strCountryName);
        string GetCountryNameByCountryID(int strCountryID)   ;
        int GetCountryIdByCountryName(string strCountryName);
        List<CityList> GetCityListDynamic(string strCountryName, string strPrefixText, string strPostalcode);
        List<GpsData> GetGpsDataByCountryPostalCodeandCity(string strCountryName, string strPostalcode, string strCity);
        List<GpsData> GetGpsDataByCountryAndCity(string strCountryName, string strCity);
        GpsData GetGpsDataSingleByCityCountryAndPostalCode(string strCountryName, string strPostalcode, string strCity);
        List<PostalCodeList> GetPostalCodesByCountryAndCityPrefixDynamic(string strCountryName, string strCity, string StrprefixText);
       //gets the single geo code as string
        string GetGeoPostalCodebyCountryNameAndCity(string strCountryName, string strCity);
        bool ValidatePostalCodeByCOuntryandCity(string strCountryName, string strCity, string StrPostalCode);
        List<PostalCodeList> GetPostalCodesByCountryAndLatLongDynamic(string strCountryName, string strlattitude, string strlongitude);
        List<PostalCodeList> GetPostalCodesByCountryNameCityandStateProvinceDynamic(string strCountryName, string strCity, string strStateProvince);
     
    }
}
