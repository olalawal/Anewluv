using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Interfaces;
using Dating.Server.Data.Models;
using Dating.Server.Data.ViewModels;
using Shell.MVC2.Services.Contracts;



namespace Shell.MVC2.Services.Dating
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
 

        public class GeoService :  IGeoService
        {
            private IGeoRepository _geoRepo;

            public GeoService(IGeoRepository geoRepo)
            {
                _geoRepo = geoRepo;
            }

         
            //gets the country list and orders it
            //added sorting
            public List<Country_PostalCode_List> GetCountry_PostalCode_ListAndOrderByCountry()
            {
                return _geoRepo.GetCountry_PostalCode_ListAndOrderByCountry();
            }

            /// <summary>
            /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
            /// </summary>
            /// 
          
            public int GetCountry_PostalCodeStatusByCountryName(string strCountryName)
            {
                return _geoRepo.GetCountry_PostalCodeStatusByCountryName(strCountryName);
            }

          
            public string GetCountryNameByCountryId(int intCountryID)
            {
                return _geoRepo.GetCountryNameByCountryId(intCountryID);
            }
            
          
            public int GetCountryIdByCountryName(string strCountryName)
            {
                return _geoRepo.GetCountryIdByCountryName(strCountryName);
            }

            public List<CityList> GetCityListDynamic(string strCountryName, string strPrefixText, string strPostalcode)
            {
                return _geoRepo.GetCityListDynamic(strCountryName, strPrefixText, strPostalcode);
            }
            public List<GpsData> GetGpsDataByCountryPostalCodeandCity(string strCountryName, string strPostalcode, string strCity)
            {
                return _geoRepo.GetGpsDataByCountryPostalCodeandCity(strCountryName, strPostalcode, strCity);
            }
            public List<GpsData> GetGpsDataByCountryAndCity(string strCountryName, string strCity)
            {
                return _geoRepo.GetGpsDataByCountryAndCity(strCountryName, strCity);
            }
            public GpsData GetGpsDataSingleByCityCountryAndPostalCode(string strCountryName, string strPostalcode, string strCity)
            {
                return _geoRepo.GetGpsDataSingleByCityCountryAndPostalCode(strCountryName, strPostalcode, strCity);
            }
            public List<PostalCodeList> GetPostalCodesByCountryAndCityPrefixDynamic(string strCountryName, string strCity, string StrprefixText)
            {
                return _geoRepo.GetPostalCodesByCountryAndCityPrefixDynamic(strCountryName, strCity, StrprefixText);
            }
            //gets the single geo code as string
            public string GetGeoPostalCodebyCountryNameAndCity(string strCountryName, string strCity)
            {
                return _geoRepo.GetGeoPostalCodebyCountryNameAndCity(strCountryName, strCity);
            }
            public bool ValidatePostalCodeByCOuntryandCity(string strCountryName, string strCity, string StrPostalCode)
            {
                return _geoRepo.ValidatePostalCodeByCOuntryandCity(strCountryName, strCity, StrPostalCode);
            }

            public RegisterModel VerifyOrUpdateRegistrationGeoData(ValidateRegistrationGeoDataModel model)
        {
            return _geoRepo.VerifyOrUpdateRegistrationGeoData(model);
        }
            
            
            public List<PostalCodeList> GetPostalCodesByCountryAndLatLongDynamic(string strCountryName, string strlattitude, string strlongitude)
            {
                return _geoRepo.GetPostalCodesByCountryAndLatLongDynamic(strCountryName, strlattitude, strlongitude);

            }

            public List<PostalCodeList> GetPostalCodesByCountryNameCityandStateProvinceDynamic(string strCountryName, string strCity, string strStateProvince)
            {
                return _geoRepo.GetPostalCodesByCountryNameCityandStateProvinceDynamic(strCountryName, strCity, strStateProvince);
            }
     
        }
    }

