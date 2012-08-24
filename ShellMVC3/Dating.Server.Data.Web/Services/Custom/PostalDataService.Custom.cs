

namespace Dating.Server.Data.Services
{

    using System;
    using System.Collections.Generic; 
    using System.Data;
    using System.Linq;
    using System.ServiceModel.DomainServices.EntityFramework;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    using Dating.Server.Data.Models;
    using System.Security.Principal;

    using Dating.Server.Data;

    using System.Data.EntityClient;
    using System.Collections.ObjectModel;   
    using System.Diagnostics;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;


    using System.Data.Objects.DataClasses;
 




    public partial class PostalDataService : LinqToEntitiesDomainService<PostalData2Entities>
    {

        public override void Initialize(DomainServiceContext context)
        {
            base.Initialize(context);
           
        }


        //[EdmFunction("PostalData2Model.Store", "fnGetDistance")]
        //public static double fnGetDistance(double lat1, double long1, double lat2, double long2, string ReturnType)
        //{
        //    throw new NotSupportedException("This function can only be used in a LINQ to Entities query");
        //}




        #region "Dynamic Stored Procs"        


        public IQueryable<CityList> GetCityListDynamic(string strCountryName, string strPrefixText, string strPostalcode)
        {
            IQueryable<CityList> functionReturnValue = default(IQueryable<CityList>);



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

                                    City = objDataReader["City"].ToString(),
                                    State_Province = objDataReader["State_Province"].ToString()
                                });

                                // _CityList.Add(New CityList() With { _
                                //.City = objDataReader("City").ToString, .State_Province = objDataReader("State_Province").ToString, _
                                //.PostalCode = objDataReader("PostalCode").ToString, .Latitude = objDataReader("Latitude").ToString, _
                                //.Longitude = objDataReader("Longitude").ToString
                                //})

                                // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                            }
                        }
                    }
                    conn.Close();
                }
                functionReturnValue = _CityList.AsQueryable();
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

        public IQueryable<GpsData> GetGpsDataByCountryPostalCodeandCity(string strCountryName,  string strPostalcode, string strCity)
        {
            IQueryable<GpsData> functionReturnValue = default(IQueryable<GpsData>);

            List<GpsData> _GpsData = new List<GpsData>();
            strCountryName = string.Format(strCountryName.Replace( " ", ""));
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
                               
                                Latitude = float.Parse(objDataReader["Latitude"].ToString()),
                                Longitude = float.Parse(objDataReader["Longitude"].ToString()),
                                State_Province = objDataReader["State_Province"].ToString()
                            });

                            // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                        }
                    }
                }
                conn.Close();
            }
            functionReturnValue = _GpsData.AsQueryable();
            //Return Me.ObjectContext.citylis
            return functionReturnValue;
         
        }

        public IQueryable<GpsData> GetGpsDataByCountryAndCity(string strCountryName, string strCity)
        {
            IQueryable<GpsData> functionReturnValue = default(IQueryable<GpsData>);

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

                                Latitude = float.Parse(objDataReader["Latitude"].ToString()),
                                Longitude = float.Parse(objDataReader["Longitude"].ToString()),
                                State_Province = objDataReader["State_Province"].ToString()
                            });

                            // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                        }
                    }
                }
                conn.Close();
            }
            functionReturnValue = _GpsData.AsQueryable();
            //Return Me.ObjectContext.citylis
            return functionReturnValue;

        }

        public GpsData GetGpsDataSingleByCityCountryAndPostalCode(string strCountryName,  string strPostalcode ,string strCity)
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
                           
                               _GpsData.Latitude = float.Parse(objDataReader["Latitude"].ToString());
                                 _GpsData.Longitude = float.Parse(objDataReader["Longitude"].ToString());
                                 _GpsData.State_Province = objDataReader["State_Province"].ToString();
                        }

                        
                    }
                    conn.Close();
                }
            
               return _GpsData;
            }
           
         

        }

        public IQueryable<PostalCodeList> GetPostalCodesByCountryAndCityPrefixDynamic(string strCountryName, string strCity, string StrprefixText)
        {
            IQueryable<PostalCodeList> functionReturnValue = default(IQueryable<PostalCodeList>);

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
                            _PostalCodeList.Add(new PostalCodeList { PostalCode = objDataReader["PostalCode"].ToString() });

                            // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                        }
                    }
                }
                conn.Close();
            }
            functionReturnValue = _PostalCodeList.AsQueryable();
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
                            geoCode = objDataReader["PostalCode"].ToString();
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
            string PostalCode = "";

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
                            PostalCode = objDataReader["Postalcode"].ToString();
                        }
                    }
                }
                conn.Close();
            }
            functionReturnValue = PostalCode;
            //Return Me.ObjectContext.citylis
            if (PostalCode != "")
            { return true;  }
            else
            { return false; }
          
        }

        public IQueryable<PostalCodeList> GetPostalCodesByCountryAndLatLongDynamic(string strCountryName,string strlattitude , string strlongitude)
        {
            IQueryable<PostalCodeList> functionReturnValue = default(IQueryable<PostalCodeList>);

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
                            _PostalCodeList.Add(new PostalCodeList { PostalCode = objDataReader["PostalCode"].ToString() });

                            // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                        }
                    }
                }
                conn.Close();
            }
            functionReturnValue = _PostalCodeList.AsQueryable();
            //Return Me.ObjectContext.citylis
            return functionReturnValue;
        }



        public IQueryable<PostalCodeList> GetPostalCodesByCountryNameCityandStateProvinceDynamic(string strCountryName, string strCity, string strStateProvince)
        {
            IQueryable<PostalCodeList> functionReturnValue = default(IQueryable<PostalCodeList>);

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
                            _PostalCodeList.Add(new PostalCodeList { PostalCode = objDataReader["PostalCode"].ToString() });

                            // Console.WriteLine("ID: {0} Grade: {1}", rdr("StudentID"), rdr("Grade"))
                        }
                    }
                }
                conn.Close();
            }
            functionReturnValue = _PostalCodeList.AsQueryable();
            //Return Me.ObjectContext.citylis
            return functionReturnValue;
        }


        #endregion

        #region "Spatial Functions"



   

       // probbaly won't use this, lets add lat long and other stuff to model
        //public Nullable<double> DistBTWMembersByLatLon(string profileid1, string profileid2)
        //{
        //    DatingService datingService = new DatingService();
        //    //confusion here
        //    PostalDataService postalService = new PostalDataService();

            
            

        //    profiledata[] tmpprofile = new profiledata[2];
           
        //    //create and array of GPS data
        //   IQueryable<GpsData>[] _GpsData =  new IQueryable<GpsData>[2];


        //   //first get the postal codes and countries
        //  tmpprofile[0]= datingService.GetProfileDataByProfileID(profileid1);
        //  tmpprofile[1] = datingService.GetProfileDataByProfileID(profileid2);

           

        //   // string StrSQL = "Select PostalCode from profilesData Where profileID=";
        //    //Data_Access.OpenDatingDB();
        //   // System.Data.SqlClient.SqlDataReader ConnectionReader = null;
       

        //    //get profile Data to get country and postal code then we can use 
        //    //    List<GpsData> _GpsData = new List<GpsData>()

        //    try
        //    {
        //        //get dps data now
        //       // _GpsData[0] = postalService.GetGpsDataByCountryPostalCodeandCity (GetCountryNameByCountryID(tmpprofile[0].CountryID),tmpprofile[0].PostalCode,"");
        //      //  _GpsData[1] = postalService.GetGpsDataByCountryPostalCodeandCity(GetCountryNameByCountryID(tmpprofile[1].CountryID), tmpprofile[1].PostalCode, ""); 
 
         
        //        //now use gps data to get the values
                   
        //            return GetdistanceBetweenMembers(_GpsData[0].FirstOrDefault().Latitude, 
        //                                             _GpsData[0].FirstOrDefault().Longitude,
        //                                             _GpsData[1].FirstOrDefault().Latitude,
        //                                             _GpsData[1].FirstOrDefault().Longitude,"M");
             
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}


       // use this function to get distance at the same time, add it to the model
        public Nullable<double> GetdistanceBetweenMembers(double lat1, double lon1, double lat2, double lon2, string  unit)
        {

            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == "K")
            {
                dist = dist * 1.609344;
            }
            else if (unit == "N")
            {
                dist = dist * 0.8684;
            }
            return dist;
        }

      

        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return rad / Math.PI * 180.0;
        }

        #endregion

        #region "Custom EF queries"

       

        #endregion

    }

}
