using System;

public class Class1
{
	public Class1()
	{

        //return (from p in _postalcontext.GetCountry_PostalCode_List()
        //where p.CountryID  == countryid
        //select p.CountryName ).FirstOrDefault();
        //return postaldataservicecontext.GetcountryNameBycountryID(profiledata.countryid);      
        db.SetIsolationToDefault = true;
        //TDocRecon loandetail2 = new TDocRecon();
        string query = "sp_GetCountryNameByCountryID";
        SqlParameter parameter = new SqlParameter("CountryID", countryid);
        parameter.ParameterName = "@CountryID";
        parameter.SqlDbType = System.Data.SqlDbType.VarChar;
        parameter.Size = 40;
        //Procedure or function 'usp_GetHudFeeReviewLoanDetails' expects parameter '@LoanNbr', which was not supplied.
        //parameter.TypeName 
        var parameters = new object[] { parameter };

        //object params                      
        countryname = db.ObjectContext.ExecuteStoreQuery(query + " " + parameter.ParameterName, parameters).FirstOrDefault();
        if (countryname != null) return countryname;
	}
}
