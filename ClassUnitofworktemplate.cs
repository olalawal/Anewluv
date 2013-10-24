using System;

public class Class1
{
	public Class1()
	{

         _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {


             }
             catch (Exception ex)
             {

                 Exception convertedexcption = new CustomExceptionTypes.GeoLocationException(countryid.ToString(), "", "", ex.Message, ex.InnerException);
                 new ErroLogging(applicationEnum.GeoLocationService).WriteSingleEntry(logseverityEnum.CriticalError, convertedexcption, null, null);
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in GeoService service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
         
                 //throw convertedexcption;
             }

         }
	}
}
