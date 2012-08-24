using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

using System.Text;
using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

namespace Shell.MVC2.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IGeoService
    {
        [WebGet]
        [OperationContract]
        string GetCountryNameByCountryId(int countryId);


        // TODO: Add your service operations here

        [WebInvoke]
        [OperationContract]
        RegisterModel VerifyOrUpdateRegistrationGeoData(ValidateRegistrationGeoDataModel model);
        //gets the country list and orders it
        //added sorting
        [WebGet]
        [OperationContract]
         List<Country_PostalCode_List> GetCountry_PostalCode_ListAndOrderByCountry();
        /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// </summary>
        ///   
        [WebGet]
        [OperationContract]
         int GetCountry_PostalCodeStatusByCountryName(string strCountryName);
       
               
        [WebGet]
        [OperationContract]
        int GetCountryIdByCountryName(string strCountryName);

        [WebGet]
        [OperationContract]
         List<CityList> GetCityListDynamic(string strCountryName, string strPrefixText, string strPostalcode);
        [WebGet]
        [OperationContract]
         List<GpsData> GetGpsDataByCountryPostalCodeandCity(string strCountryName, string strPostalcode, string strCity);
        [WebGet]
        [OperationContract]
         List<GpsData> GetGpsDataByCountryAndCity(string strCountryName, string strCity);
        [WebGet]
        [OperationContract]
         GpsData GetGpsDataSingleByCityCountryAndPostalCode(string strCountryName, string strPostalcode, string strCity);
        [WebGet]
        [OperationContract]
         List<PostalCodeList> GetPostalCodesByCountryAndCityPrefixDynamic(string strCountryName, string strCity, string StrprefixText);
        //gets the single geo code as string
        [WebGet]
        [OperationContract]
         string GetGeoPostalCodebyCountryNameAndCity(string strCountryName, string strCity);
        [WebGet]
        [OperationContract]
         bool ValidatePostalCodeByCOuntryandCity(string strCountryName, string strCity, string StrPostalCode);
        [WebGet]
        [OperationContract]
         List<PostalCodeList> GetPostalCodesByCountryAndLatLongDynamic(string strCountryName, string strlattitude, string strlongitude);
        [WebGet]
        [OperationContract]
         List<PostalCodeList> GetPostalCodesByCountryNameCityandStateProvinceDynamic(string strCountryName, string strCity, string strStateProvince);
     
        

    }

}
