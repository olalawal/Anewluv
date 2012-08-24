using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Dating.Server.Data.Services;
using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

using Shell.MVC2.Interfaces;
using System.Data.EntityClient;
using System.Data;

using Shell.MVC2.Data.Infrastructure;

namespace Shell.MVC2.Data
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GeoService" in code, svc and config file together.
    public class GeoRepository : GeoRepositoryBase , IGeoRepository
    {
       // private PostalData2Entities _postalcontext;

        public GeoRepository(PostalData2Entities postalcontext)
            : base(postalcontext)
        {
           
        }





        public string GetCountryNameByCountryId(int countryId)
        {            
            return (from p in _postalcontext.Country_PostalCode_List
                    where p.CountryID == countryId
                    select p.CountryName).FirstOrDefault();
            //return postaldataservicecontext.GetCountryNameByCountryID(profiledata.countryid);
        }
        public RegisterModel VerifyOrUpdateRegistrationGeoData(ValidateRegistrationGeoDataModel model)
        {
            //4-24-2012 fixed code to hanlde if we did not have a postcal code
            GpsData gpsData = new GpsData();
            string[] tempCityAndStateProvince = model.GeoRegisterModel.City .Split(',');
            //int countryID;

            //attmept to get postal postalcode if it is empty
            model.GeoRegisterModel.ZipOrPostalCode = (model.GeoRegisterModel.ZipOrPostalCode == null) ? this.GetGeoPostalCodebyCountryNameAndCity(model.GeoRegisterModel.Country, tempCityAndStateProvince[0]) : model.GeoRegisterModel.ZipOrPostalCode;
            model.GeoRegisterModel.Stateprovince = ((tempCityAndStateProvince.Count() > 1)) ? tempCityAndStateProvince[1] : "NA";
            //countryID = postaldataservicecontext.GetCountryIdByCountryName(model.GeoRegisterModel.Country);

            //check if the  city and country match
            if (model.GeoRegisterModel.Country == model.GeoMembersModel.MyQuickSearch.MySelectedCountryName && 
                tempCityAndStateProvince[0] == model.GeoMembersModel.MyQuickSearch.MySelectedCity)
            {
                if (model.GeoRegisterModel.lattitude  != null | model.GeoRegisterModel.lattitude  == 0)
                    return model.GeoRegisterModel;

            }

            //get GPS data here
            //conver the unquiqe coountry Name to an ID
            //store country ID for use later
            //get the longidtue and latttude 
            //1-11-2011 postal code and city are flipped by the way not this function should be renamed
            //TO DO rename this function.                  
            gpsData = this.GetGpsDataSingleByCityCountryAndPostalCode(model.GeoRegisterModel.Country, model.GeoRegisterModel.ZipOrPostalCode, tempCityAndStateProvince[0]);


            model.GeoRegisterModel.lattitude  = (gpsData != null) ? gpsData.Latitude  : 0;
            model.GeoRegisterModel.longitude = (gpsData != null) ? gpsData.Longitude  : 0;

            return model.GeoRegisterModel ;

        }
        //gets the country list and orders it
        //added sorting
        public List<Country_PostalCode_List> GetCountry_PostalCode_ListAndOrderByCountry()
        {
            List<Country_PostalCode_List> myQuery = default(List<Country_PostalCode_List>);

           
            myQuery = _postalcontext.Country_PostalCode_List.Where(p => p.CountryName != "").OrderBy(p => p.CountryName).ToList();



            return myQuery;
        }
        /// <summary>
        /// Gets the Status of weather this country has valid postal codes or just GeoCodes which are just id values identifying a city
        /// </summary>
        /// 
        public int GetCountry_PostalCodeStatusByCountryName(string strCountryName)
        {
            List<Country_PostalCode_List> myQuery = default(List<Country_PostalCode_List>);
            //Dim ctx As New Entities()
            myQuery = _postalcontext.Country_PostalCode_List.Where(p => p.CountryName == strCountryName).ToList();

            return myQuery.FirstOrDefault().PostalCodes.Value;
        }      
        public string GetCountryNameByCountryID(int strCountryID)
        {
            List<Country_PostalCode_List> CountryCodeQuery = default(List<Country_PostalCode_List>);

            CountryCodeQuery = _postalcontext.Country_PostalCode_List.Where(p => p.CountryID  == strCountryID).ToList();

            if (CountryCodeQuery.Count() > 0)
            {
                return CountryCodeQuery.FirstOrDefault().CountryName.ToString();

            }
            else
            {
                return "";
            }
        }               
        public int GetCountryIdByCountryName(string strCountryName)
        {
            List<Country_PostalCode_List> CountryCodeQuery = default(List<Country_PostalCode_List>);

            CountryCodeQuery = _postalcontext.Country_PostalCode_List.Where(p => p.CountryName == strCountryName).ToList();

            if (CountryCodeQuery.Count() > 0)
            {
                return CountryCodeQuery.FirstOrDefault().CountryID;

            }
            else
            {
                return 0;
            }
        }

        //Dynamic LINQ to Entites quries 
        //*****************************************************************************************************************************************
        public List<CityList> GetCityListDynamic(string strCountryName, string strPrefixText, string strPostalcode)
        {
            List<CityList> functionReturnValue = default(List<CityList>);



            List<CityList> _CityList = new List<CityList>();
            strPostalcode = string.Format("{0}%", strPostalcode.Replace("'", "''"));
            // fix country names if theres a space
            strCountryName = string.Format(strCountryName.Replace(" ", ""));

            //test this as well for added 2/28/2011 - trimming spaces in search text since i am removing spaces in sql script
            //for cites as well so its a 1 to 1 search no spaces on input and on db side
            strPrefixText = string.Format("{0}%", strPrefixText.Replace(" ", ""));
            //11/13/2009 addded wild ca


            try
            {
                using (EntityConnection conn = new EntityConnection("name=PostalData2Entities"))
                {
                    conn.Open();



                    // Create an EntityCommand. 
                    using (EntityCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "PostalData2Entities.GetCityListByCountryPostalCode";
                        cmd.CommandType = CommandType.StoredProcedure;
                        //add parameters
                        EntityParameter param = new EntityParameter();
                        EntityParameter param2 = new EntityParameter();
                        EntityParameter param3 = new EntityParameter();
                        param.Value = strCountryName;
                        param.ParameterName = "StrcountryDatabaseName";
                        cmd.Parameters.Add(param);


                        param2.Value = strPrefixText;
                        param2.ParameterName = "StrPrefixText";
                        cmd.Parameters.Add(param2);



                        param3.Value = strPostalcode;
                        param3.ParameterName = "StrPostalCode";
                        cmd.Parameters.Add(param3);



                        //ad a fake record ID for uniquenet contraint as well and append it to each row                    
                        // Execute the command. 
                        using (EntityDataReader objDataReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                        {
                            // Read the results returned by the stored procedure. 
                            while (objDataReader.Read())
                            {
                                _CityList.Add(new CityList
                                {

                                   City  = objDataReader["city"].ToString(),
                                     State_Province   = objDataReader["stateprovince"].ToString()
                                });

                                // _CityList.Add(New CityList() With { _
                                //.city = objDataReader("city").ToString, .stateprovince = objDataReader("stateprovince").ToString, _
                                //.postalcode = objDataReader("postalcode").ToString, .latitude = objDataReader("latitude").ToString, _
                                //.longitude = objDataReader("longitude").ToString
                                //})

                                // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                            }
                        }
                    }
                    conn.Close();
                }
                functionReturnValue = _CityList.ToList();
                //Return Me.ObjectContext.citylis
                return functionReturnValue;

            }
            catch (Exception ex)
            {
                // status = MembershipCreateStatus.ProviderError;
                //  newUser = null;
                //throw ex;

                return null;
            }




        }
        public List<GpsData> GetGpsDataByCountryPostalCodeandCity(string strCountryName, string strPostalcode, string strCity)
        {
            List<GpsData> functionReturnValue = default(List<GpsData>);

            List<GpsData> _GpsData = new List<GpsData>();
            strCountryName = string.Format(strCountryName.Replace(" ", ""));
            // fix country names if theres a space
            // strCity = String.Format("{0}%", strCity) '11/13/2009 addded wild ca


            using (EntityConnection conn = new EntityConnection("name=PostalData2Entities"))
            {
                conn.Open();

                // Create an EntityCommand. 
                using (EntityCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PostalData2Entities.GetGpsDataByCityCountryAndPostalCode";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //add parameters
                    EntityParameter param = new EntityParameter();
                    EntityParameter param2 = new EntityParameter();
                    EntityParameter param3 = new EntityParameter();
                    param.Value = strCountryName;
                    param.ParameterName = "StrcountryName";
                    cmd.Parameters.Add(param);

                    param2.Value = strPostalcode;
                    param2.ParameterName = "StrPostalCode";
                    cmd.Parameters.Add(param2);

                    param3.Value = strCity;
                    param3.ParameterName = "StrCity";
                    cmd.Parameters.Add(param3);




                    //ad a fake record ID for uniquenet contraint as well and append it to each row
                    // Execute the command. 
                    using (EntityDataReader objDataReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        // Read the results returned by the stored procedure. 
                        while (objDataReader.Read())
                        {
                            _GpsData.Add(new GpsData
                            {

                              Latitude  = float.Parse(objDataReader["latitude"].ToString()),
                                Longitude   = float.Parse(objDataReader["longitude"].ToString()),
                                State_Province  = objDataReader["stateprovince"].ToString()
                            });

                            // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                        }
                    }
                }
                conn.Close();
            }
            functionReturnValue = _GpsData.ToList();
            //Return Me.ObjectContext.citylis
            return functionReturnValue;

        }
        public List<GpsData> GetGpsDataByCountryAndCity(string strCountryName, string strCity)
        {
            List<GpsData> functionReturnValue = default(List<GpsData>);

            List<GpsData> _GpsData = new List<GpsData>();
            strCountryName = string.Format(strCountryName.Replace(" ", ""));
            // fix country names if theres a space
            // strCity = String.Format("{0}%", strCity) '11/13/2009 addded wild ca


            using (EntityConnection conn = new EntityConnection("name=PostalData2Entities"))
            {
                conn.Open();

                // Create an EntityCommand. 
                using (EntityCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PostalData2Entities.GetGpsDataByCountryNameAndCity";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //add parameters
                    EntityParameter param = new EntityParameter();
                    EntityParameter param2 = new EntityParameter();
                    EntityParameter param3 = new EntityParameter();
                    param.Value = strCountryName;
                    param.ParameterName = "StrcountryName";
                    cmd.Parameters.Add(param);

                    param2.Value = strCity;
                    param2.ParameterName = "StrCity";
                    cmd.Parameters.Add(param2);




                    //ad a fake record ID for uniquenet contraint as well and append it to each row
                    // Execute the command. 
                    using (EntityDataReader objDataReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        // Read the results returned by the stored procedure. 
                        while (objDataReader.Read())
                        {
                            _GpsData.Add(new GpsData
                            {

                              Latitude   = float.Parse(objDataReader["latitude"].ToString()),
                                Longitude   = float.Parse(objDataReader["longitude"].ToString()),
                                State_Province   = objDataReader["stateprovince"].ToString()
                            });

                            // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                        }
                    }
                }
                conn.Close();
            }
            functionReturnValue = _GpsData.ToList();
            //Return Me.ObjectContext.citylis
            return functionReturnValue;

        }
        public GpsData GetGpsDataSingleByCityCountryAndPostalCode(string strCountryName, string strPostalcode, string strCity)
        {
            //GpsData functionReturnValue = default(GpsData);

            GpsData _GpsData = new GpsData();
            strCountryName = string.Format(strCountryName.Replace(" ", ""));
            // fix country names if theres a space
            // strCity = String.Format("{0}%", strCity) '11/13/2009 addded wild ca


            using (EntityConnection conn = new EntityConnection("name=PostalData2Entities"))
            {
                conn.Open();

                // Create an EntityCommand. 
                using (EntityCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PostalData2Entities.GetGpsDataByCityCountryAndPostalCode";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //add parameters
                    EntityParameter param = new EntityParameter();
                    EntityParameter param2 = new EntityParameter();
                    EntityParameter param3 = new EntityParameter();
                    param.Value = strCountryName;
                    param.ParameterName = "StrcountryName";
                    cmd.Parameters.Add(param);


                    param2.Value = strPostalcode;
                    param2.ParameterName = "StrPostalCode";
                    cmd.Parameters.Add(param2);

                    param3.Value = strCity;
                    param3.ParameterName = "StrCity";
                    cmd.Parameters.Add(param3);






                    //ad a fake record ID for uniquenet contraint as well and append it to each row
                    // Execute the command. 
                    using (EntityDataReader objDataReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        // Read the results returned by the stored procedure. 
                        while (objDataReader.Read())
                        {

                            _GpsData.Latitude  = float.Parse(objDataReader["latitude"].ToString());
                            _GpsData.Longitude  = float.Parse(objDataReader["longitude"].ToString());
                            _GpsData.State_Province = objDataReader["stateprovince"].ToString();
                        }


                    }
                    conn.Close();
                }

                return _GpsData;
            }



        }
        public List<PostalCodeList> GetPostalCodesByCountryAndCityPrefixDynamic(string strCountryName, string strCity, string StrprefixText)
        {
            List<PostalCodeList> functionReturnValue = default(List<PostalCodeList>);

            //added 5/12/2011 to handle empty countries
            if (strCountryName == null) return null;

            List<PostalCodeList> _PostalCodeList = new List<PostalCodeList>();
            strCity = string.Format("{0}%", strCity);
            strCountryName = string.Format(strCountryName.Replace(" ", ""));
            // fix country names if theres a space
            StrprefixText = string.Format("{0}%", StrprefixText);
            //11/13/2009 addded wild ca


            using (EntityConnection conn = new EntityConnection("name=PostalData2Entities"))
            {
                conn.Open();

                // Create an EntityCommand. 
                using (EntityCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PostalData2Entities.GetPostalCodesByCountryAndCityPrefixDynamic";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //add parameters
                    EntityParameter param = new EntityParameter();
                    EntityParameter param2 = new EntityParameter();
                    EntityParameter param3 = new EntityParameter();
                    param.Value = strCountryName;
                    param.ParameterName = "StrcountryDatabaseName";
                    cmd.Parameters.Add(param);


                    param2.Value = strCity;
                    param2.ParameterName = "StrCity";
                    cmd.Parameters.Add(param2);



                    param3.Value = StrprefixText;
                    param3.ParameterName = "StrprefixText";
                    cmd.Parameters.Add(param3);



                    //ad a fake record ID for uniquenet contraint as well and append it to each row
                    // int intRecordID = 1;
                    // Execute the command. 
                    using (EntityDataReader objDataReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        // Read the results returned by the stored procedure. 
                        while (objDataReader.Read())
                        {
                            _PostalCodeList.Add(new PostalCodeList { PostalCode   = objDataReader["postalcode"].ToString() });

                            // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                        }
                    }
                }
                conn.Close();
            }
            functionReturnValue = _PostalCodeList.ToList();
            //Return Me.ObjectContext.citylis
            return functionReturnValue;
        }
        //gets the single geo code as string
        public string GetGeoPostalCodebyCountryNameAndCity(string strCountryName, string strCity)
        {



            //Dim _PostalCodeList As New List(Of PostalCodeList)()
            string geoCode = "";
            //  strCity = String.Format(strCity)
            strCountryName = string.Format(strCountryName.Replace(" ", ""));
            // fix country names if theres a space

            using (EntityConnection conn = new EntityConnection("name=PostalData2Entities"))
            {
                conn.Open();

                // Create an EntityCommand. 
                using (EntityCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PostalData2Entities.GetGeoPostalCodebyCountryNameAndCity";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //add parameters
                    EntityParameter param = new EntityParameter();
                    EntityParameter param2 = new EntityParameter();
                    EntityParameter param3 = new EntityParameter();
                    param.Value = strCountryName;
                    param.ParameterName = "StrcountryName";
                    cmd.Parameters.Add(param);


                    param2.Value = strCity;
                    param2.ParameterName = "StrCity";
                    cmd.Parameters.Add(param2);

                    //ad a fake record ID for uniquenet contraint as well and append it to each row
                    //int intRecordID = 1;
                    // Execute the command. 
                    using (EntityDataReader objDataReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        // Read the results returned by the stored procedure. 
                        while (objDataReader.Read())
                        {
                            geoCode = objDataReader["postalcode"].ToString();
                            // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                            if (geoCode != "") return geoCode;
                        }
                    }
                }
                conn.Close();
            }
            return "";

        }
        public bool ValidatePostalCodeByCOuntryandCity(string strCountryName, string strCity, string StrPostalCode)
        {
            string functionReturnValue = null;



            //Dim _PostalCodeList As New List(Of PostalCodeList)()
            string postalcode = "";

            strCity = string.Format("{0}%", strCity);
            strCountryName = string.Format(strCountryName.Replace(" ", ""));
            // fix country names if theres a space


            using (EntityConnection conn = new EntityConnection("name=PostalData2Entities"))
            {
                conn.Open();

                // Create an EntityCommand. 
                using (EntityCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PostalData2Entities.ValidatePostalCodeByCOuntryandCity";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //add parameters
                    EntityParameter param = new EntityParameter();
                    EntityParameter param2 = new EntityParameter();
                    EntityParameter param3 = new EntityParameter();
                    param.Value = strCountryName;
                    param.ParameterName = "StrcountryName";
                    cmd.Parameters.Add(param);


                    param2.Value = strCity;
                    param2.ParameterName = "StrCity";
                    cmd.Parameters.Add(param2);


                    param3.Value = StrPostalCode;
                    param3.ParameterName = "StrPostalCode";
                    cmd.Parameters.Add(param3);



                    //ad a fake record ID for uniquenet contraint as well and append it to each row
                    //int intRecordID = 1;
                    // Execute the command. 
                    using (EntityDataReader objDataReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        // Read the results returned by the stored procedure. 
                        while (objDataReader.Read())
                        {
                            postalcode = objDataReader["Postalcode"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            functionReturnValue = postalcode;
            //Return Me.ObjectContext.citylis
            if (postalcode != "")
            { return true; }
            else
            { return false; }

        }
        public List<PostalCodeList> GetPostalCodesByCountryAndLatLongDynamic(string strCountryName, string strlattitude, string strlongitude)
        {
            List<PostalCodeList> functionReturnValue = default(List<PostalCodeList>);

            //added 5/12/2011 to handle empty countries
            if (strCountryName == null) return null;
            List<PostalCodeList> _PostalCodeList = new List<PostalCodeList>();
            strCountryName = string.Format(strCountryName.Replace(" ", ""));
            // fix country names if theres a space
            //StrprefixText = string.Format("{0}%", StrprefixText);



            using (EntityConnection conn = new EntityConnection("name=PostalData2Entities"))
            {
                conn.Open();

                // Create an EntityCommand. 
                using (EntityCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PostalData2Entities.GetPostalCodesByCountryAndLatLong";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //add parameters
                    EntityParameter param = new EntityParameter();
                    EntityParameter param2 = new EntityParameter();
                    EntityParameter param3 = new EntityParameter();
                    param.Value = strCountryName;
                    param.ParameterName = "StrcountryDatabaseName";
                    cmd.Parameters.Add(param);


                    param2.Value = strlattitude;
                    param2.ParameterName = "StrLattitude";
                    cmd.Parameters.Add(param2);



                    param3.Value = strlongitude;
                    param3.ParameterName = "StrLongitude";
                    cmd.Parameters.Add(param3);



                    //ad a fake record ID for uniquenet contraint as well and append it to each row
                    // int intRecordID = 1;
                    // Execute the command. 
                    using (EntityDataReader objDataReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        // Read the results returned by the stored procedure. 
                        while (objDataReader.Read())
                        {
                            _PostalCodeList.Add(new PostalCodeList { PostalCode   = objDataReader["postalcode"].ToString() });

                            // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                        }
                    }
                }
                conn.Close();
            }
            functionReturnValue = _PostalCodeList.ToList();
            //Return Me.ObjectContext.citylis
            return functionReturnValue;
        }
        public List<PostalCodeList> GetPostalCodesByCountryNameCityandStateProvinceDynamic(string strCountryName, string strCity, string strStateProvince)
        {
            List<PostalCodeList> functionReturnValue = default(List<PostalCodeList>);

            //added 5/12/2011 to handle empty countries
            if (strCountryName == null) return null;
            List<PostalCodeList> _PostalCodeList = new List<PostalCodeList>();
            strCountryName = string.Format(strCountryName.Replace(" ", ""));
            // fix country names if theres a space
            //StrprefixText = string.Format("{0}%", StrprefixText);



            using (EntityConnection conn = new EntityConnection("name=PostalData2Entities"))
            {
                conn.Open();

                // Create an EntityCommand. 
                using (EntityCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "PostalData2Entities.GetPostalCodesByCountryNameCityandStateProvince";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //add parameters
                    EntityParameter param = new EntityParameter();
                    EntityParameter param2 = new EntityParameter();
                    EntityParameter param3 = new EntityParameter();
                    param.Value = strCountryName;
                    param.ParameterName = "StrcountryDatabaseName";
                    cmd.Parameters.Add(param);


                    param2.Value = strCity;
                    param2.ParameterName = "StrCity";
                    cmd.Parameters.Add(param2);



                    param3.Value = strStateProvince;
                    param3.ParameterName = "StrStateProvince";
                    cmd.Parameters.Add(param3);



                    //ad a fake record ID for uniquenet contraint as well and append it to each row
                    // int intRecordID = 1;
                    // Execute the command. 
                    using (EntityDataReader objDataReader = cmd.ExecuteReader(CommandBehavior.SequentialAccess))
                    {
                        // Read the results returned by the stored procedure. 
                        while (objDataReader.Read())
                        {
                            _PostalCodeList.Add(new PostalCodeList { PostalCode   = objDataReader["postalcode"].ToString() });

                            // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                        }
                    }
                }
                conn.Close();
            }
            functionReturnValue = _PostalCodeList.ToList();
            //Return Me.ObjectContext.citylis
            return functionReturnValue;
        }
    }
}
