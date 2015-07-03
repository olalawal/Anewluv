#region

using GeoData.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

#endregion

namespace GeoData.Domain.Models
{
    public partial class PostalData2Context : IGeoDataStoredProcedures

            


{


      public  IEnumerable<Country_PostalCode_List> GetCountryPostalCodeList()
        {

            var Query = Database.SqlQuery<Country_PostalCode_List>("Select * from Country_PostalCode_List").ToListAsync().Result;
                
                //_unitOfWorkAsync.Repository<Country_PostalCode_List>().Query(p => p.CountryName != "").Select().ToList().OrderBy(p => p.CountryName);

            return Query;
        
        }

     public string GetCountryNameByCountryID(string countryid)
    {
        string query = "sp_GetCountryNameByCountryID";
        SqlParameter parameter = new SqlParameter("@CountryID", countryid);
        parameter.ParameterName = "@CountryID";
        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter.Size = 40;
        //Procedure or function 'usp_GetHudFeeReviewLoanDetails' expects parameter '@LoanNbr', which was not supplied.
        //parameter.TypeName 
        var parameters = new object[] { parameter };

        //object params                      
      //  countryname = geodb.ExecuteStoredProcedure<string>().Select().FirstOrDefault();


       // var dd = Database.SqlQuery(query + " @countryCode" + " ", parameters);

        return Database.SqlQuery<string>(query + " @CountryID ", parameters).FirstAsync().Result;
    
      //  var data = myquery.ToListAsync();
      //  return data.First();
        //return data.Result.ToString();


    }

     public IEnumerable<CityList> CityListbycountryNamePostalcodeandCity(string countryname, string filter, string PostalCodeList)
    {
   

        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", countryname);
        parameter.ParameterName = "@StrcountryDatabaseName";
        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter.Size = 50;

        SqlParameter parameter2 = new SqlParameter("@StrPrefixText", filter);
        parameter2.ParameterName = "@StrPrefixText";
        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter2.Size = 40;

        SqlParameter parameter3 = new SqlParameter("@StrPostalCode", PostalCodeList);
        parameter3.ParameterName = "@StrPostalCode";
        parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter3.Size = 40;

       
       var parameters = new object[] { parameter, parameter2, parameter3 };
       string query = "sp_CityListbycountryNamePostalcodeandCity";

       return Database.SqlQuery<CityList>(query + " @StrcountryDatabaseName,@StrPrefixText,@StrPostalCode", parameters);
    }

     public IEnumerable<gpsdata> GetGPSDatasByPostalCodeandCity(string countryname,string cityname, string PostalCode )
    {

        string query = "GetGPSDatasByPostalCodeandCity";

        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", countryname);
        parameter.ParameterName = "@StrcountryDatabaseName";
        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter.Size = 50;

        SqlParameter parameter2 = new SqlParameter("@StrPostalCode", PostalCode);
        parameter2.ParameterName = "@StrPostalCode";
        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter2.Size = 40;

        SqlParameter parameter3 = new SqlParameter("@StrCity", cityname);
        parameter3.ParameterName = "@StrCity";
        parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter3.Size = 100;

        var parameters = new object[] { parameter, parameter2, parameter3 };

        //var gpsdatalist = db.ExecuteStoredProcedure<gpsdata>(query + " @StrcountryDatabaseName,@StrPostalCode,@StrCity ", parameters).ToList();

        return Database.SqlQuery<gpsdata>(query + " @StrcountryDatabaseName,@StrPostalCode,@StrCity ", parameters);
    }

    public IEnumerable<gpsdata> GetGPSDataByCountryAndCity(string countryname, string cityname )
    {
        string query = "sp_GetGPSDataByCountryAndCity";

        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", countryname);
        parameter.ParameterName = "@StrcountryDatabaseName";
        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter.Size = 50;


        SqlParameter parameter2 = new SqlParameter("@StrCity", cityname);
        parameter2.ParameterName = "@StrCity";
        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter2.Size = 100;

        var parameters = new object[] { parameter, parameter2 };

       // var gpsdatalist = db.ExecuteStoredProcedure<gpsdata>(query + " @StrcountryDatabaseName,@StrCity ", parameters).ToList();

        return Database.SqlQuery<gpsdata>(query + " @StrcountryDatabaseName,@StrCity ", parameters);
    }

    public IEnumerable<PostalCodeList> GetPostalCodesByCountryNameCityandPrefix(string countryname,string cityname, string filter)
    {
        try
        {
            string query = "sp_GetPostalCodesByCountryNameCityandPrefix";

            SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", countryname);
            parameter.ParameterName = "@StrcountryDatabaseName";
            parameter.SqlDbType = System.Data.SqlDbType.VarChar;
            parameter.Size = 50;

            SqlParameter parameter2 = new SqlParameter("@StrCity", cityname);
            parameter2.ParameterName = "@StrCity";
            parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
            parameter2.Size = 100;

            SqlParameter parameter3 = new SqlParameter("@StrprefixText", filter);
            parameter3.ParameterName = "@StrprefixText";
            parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
            parameter3.Size = 40;

            var parameters = new object[] { parameter, parameter2, parameter3 };

            //   var postalcodes = db.ExecuteStoredProcedure<PostalCodeList>(query + " @StrcountryDatabaseName,@StrCity,@StrprefixText", parameters).ToList();
            ////
            return Database.SqlQuery<PostalCodeList>(query + " @StrcountryDatabaseName,@StrCity,@StrprefixText", parameters);
        }
        catch (Exception ex) { throw ex; }
    }

    public IEnumerable<PostalCodeList> GetPostalCodesByCountryNameCity(string countryname, string cityname)
    {
        string query = "sp_GetPostalCodesByCountryNameCity";

        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", countryname);
        parameter.ParameterName = "@StrcountryDatabaseName";
        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter.Size = 50;


        SqlParameter parameter2 = new SqlParameter("@StrCity", cityname);
        parameter2.ParameterName = "@StrCity";
        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter2.Size = 100;

        var parameters = new object[] { parameter, parameter2 };

        //var postalcodelist = _unitOfWorkAsync.ExecuteStoredProcedure<PostalCodeList>(query + " @StrcountryDatabaseName,@StrCity", parameters).ToList();

        return Database.SqlQuery<PostalCodeList>(query + " @StrcountryDatabaseName,@StrCity", parameters);
    }

    public IEnumerable<PostalCodeList> ValidatePostalCodeByCountryNameandCity(string countryname, string cityname, string strpostalcode)
    {
        try
        {
            string query = "sp_ValidatePostalCodeByCountryNameandCity";

            SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", countryname);
            parameter.ParameterName = "@StrcountryDatabaseName";
            parameter.SqlDbType = System.Data.SqlDbType.VarChar;
            parameter.Size = 50;

            SqlParameter parameter2 = new SqlParameter("@StrCity", cityname);
            parameter2.ParameterName = "@StrCity";
            parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
            parameter2.Size = 100;

            SqlParameter parameter3 = new SqlParameter("@StrPostalCode", strpostalcode);
            parameter3.ParameterName = "@StrPostalCode";
            parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
            parameter3.Size = 40;


            var parameters = new object[] { parameter, parameter2, parameter3 };

            //  var foundpostalcodes = db.ExecuteStoredProcedure<PostalCodeList>().ToList();

           return Database.SqlQuery<PostalCodeList>(query + " @StrcountryDatabaseName,@StrCity,@StrPostalCode", parameters);

          
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public IEnumerable<PostalCodeList> GetPostalCodesByCountryAndLatLong(string countryname, string lattitude, string longitude)
    {
        string query = "sp_GetPostalCodesByCountryAndLatLong";

        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", countryname);
        parameter.ParameterName = "@StrcountryDatabaseName";
        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter.Size = 50;

        SqlParameter parameter2 = new SqlParameter("@StrLattitude", lattitude);
        parameter2.ParameterName = "@StrLattitude";
        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter2.Size = 25;

        SqlParameter parameter3 = new SqlParameter("@StrLongitude", longitude);
        parameter3.ParameterName = "@StrLongitude";
        parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter3.Size = 25;


        var parameters = new object[] { parameter, parameter2, parameter3 };


       // var geopostalcodes = db.ExecuteStoredProcedure<PostalCodeList>();
        return Database.SqlQuery<PostalCodeList>(query + " @StrcountryDatabaseName.@StrLattitude,@StrLongitude", parameters);
    }

    public IEnumerable<PostalCodeList> GetPostalCodesByCountryNameCityandStateProvince(string  countryname,string cityname,string stateprovince)
    {
        string query = "sp_GetPostalCodeByCountryNameCityandStateProvince";

        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", countryname);
        parameter.ParameterName = "@StrcountryDatabaseName";
        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter.Size = 50;

        SqlParameter parameter2 = new SqlParameter("@StrCity", cityname);
        parameter2.ParameterName = "@StrCity";
        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter2.Size = 50;

        SqlParameter parameter3 = new SqlParameter("StrStateProvince", stateprovince);
        parameter3.ParameterName = "@StrStateProvince";
        parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter3.Size = 100;


        var parameters = new object[] { parameter, parameter2, parameter3 };

       // var geopostalcodes = db.ExecuteStoredProcedure<PostalCodeList>().ToList();

        return Database.SqlQuery<PostalCodeList>(query + " @StrcountryDatabaseName,@StrCity,@StrStateProvince", parameters);
    }

    public IEnumerable<CityList> CityListbycountryNamePostalcodeandCity(string countryname,string filter)
    {
        string query = "sp_CityListbycountryNamePostalcodeandCity";

        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", countryname);
        parameter.ParameterName = "@StrcountryDatabaseName";
        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter.Size = 150;

        SqlParameter parameter2 = new SqlParameter("@StrPrefixText", filter);
        parameter2.ParameterName = "@StrPrefixText";
        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter2.Size = 25;
        
        var parameters = new object[] { parameter, parameter2 };

        //var cities = db.ExecuteStoredProcedure<CityList>().Take(50);
        return Database.SqlQuery<CityList>(query + " @StrcountryDatabaseName,@StrPrefixText", parameters);
    }

    public IEnumerable<CityList> CityListbycountryNameCityFilter(string countryname, string filter)
    {
        string query = "sp_CityListbycountryNameCityFilter";

        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", countryname);
        parameter.ParameterName = "@StrcountryDatabaseName";
        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter.Size = 150;

        SqlParameter parameter2 = new SqlParameter("@StrPrefixText", filter);
        parameter2.ParameterName = "@StrPrefixText";
        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter2.Size = 25;


        var parameters = new object[] { parameter, parameter2 };

      
        return Database.SqlQuery<CityList>(query + " @StrcountryDatabaseName,@StrPrefixText", parameters);
    }

    public IEnumerable<CityList> CityListbycountryIDCityFilter(string countryid,string filter)
    {
        string query = "sp_CityListbycountryIDCityFilter";

        SqlParameter parameter = new SqlParameter("@StrcountryID", countryid);
        parameter.ParameterName = "@StrcountryID";
        parameter.SqlDbType = System.Data.SqlDbType.Int;


        SqlParameter parameter2 = new SqlParameter("@StrPrefixText", filter);
        parameter2.ParameterName = "@StrPrefixText";
        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter2.Size = 25;


        var parameters = new object[] { parameter, parameter2 };

       // var cities = db.ExecuteStoredProcedure<CityList>(query + " @StrcountryID,@StrPrefixText", parameters).Take(50);

        // var cities = _postalcontext.GetCityListDynamic(country, "", filter).Take(50);

        return Database.SqlQuery<CityList>(query + " @StrcountryID,@StrPrefixText", parameters);
    }   
        
    public IEnumerable<PostalCodeList> GetPostalCodesByCountryIDCityandPrefix(string countryid,string cityname,string filter)
    {

        string query = "sp_GetPostalCodesByCountryIDCityandPrefix";

        SqlParameter parameter = new SqlParameter("@StrcountryID", Convert.ToInt32(countryid));
        parameter.ParameterName = "@StrcountryID";
        parameter.SqlDbType = System.Data.SqlDbType.Int;
        //parameter.Size = 50;

        SqlParameter parameter2 = new SqlParameter("@StrCity", cityname);
        parameter2.ParameterName = "@StrCity";
        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter2.Size = 100;

        SqlParameter parameter3 = new SqlParameter("StrprefixText", filter);
        parameter3.ParameterName = "@StrprefixText";
        parameter3.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter3.Size = 40;

        var parameters = new object[] { parameter, parameter2, parameter3 };

       // var postalcodes = db.ExecuteStoredProcedure<PostalCodeList>().ToList();


        return Database.SqlQuery<PostalCodeList>(query + " @StrcountryID,@StrCity,@StrPrefixText" + " ", parameters);
    }



    public async Task<PostalCodeList> GetPostalCodeByCountryNameandCity(string countryname, string city)
    {
        string query = "sp_GetGeoPostalCodeByCountryNameandCity";

        SqlParameter parameter = new SqlParameter("@StrcountryDatabaseName", countryname);
        parameter.ParameterName = "@StrcountryDatabaseName";
        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter.Size = 50;

        SqlParameter parameter2 = new SqlParameter("@StrCity", city);
        parameter2.ParameterName = "@StrCity";
        parameter2.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter2.Size = 50;        

        var parameters = new object[] { parameter, parameter2 };
     
        return await Database.SqlQuery<PostalCodeList>(query + " @StrcountryDatabaseName,@StrCity", parameters).FirstOrDefaultAsync();
    }

    public bool GetPostalCodeStatusByCountryID(string countryid)
    {

        string query = "sp_GetPostalCodeStatusByCountryID";

        SqlParameter parameter = new SqlParameter("@countryCode", Convert.ToInt32(countryid));
        parameter.ParameterName = "@countryCode";
        parameter.SqlDbType = System.Data.SqlDbType.Int;
        //parameter.Size = 50;

       

        var parameters = new object[] { parameter };

       // var postalcodes = db.ExecuteStoredProcedure<PostalCodeList>().ToList();


        var dd =  Database.ExecuteSqlCommand(query + " @countryCode" + " ", parameters);
        if (dd == 1)
            return true;

        return false;
    }

    public bool GetPostalCodeStatusBycountryName(string countryname)
    {

        string query = "sp_GetPostalCodeStatusByCountryName";

        SqlParameter parameter = new SqlParameter("@countryName", Convert.ToInt32(countryname));
        parameter.ParameterName = "@countryName";
        parameter.SqlDbType = System.Data.SqlDbType.Int;
        //parameter.Size = 50;



        var parameters = new object[] { parameter };

        // var postalcodes = db.ExecuteStoredProcedure<PostalCodeList>().ToList();


        var dd = Database.ExecuteSqlCommand(query + " @countryName" + " ", parameters);
        if (dd == 1)
            return true;

        return false;
    }


	
	
	
	


}

}