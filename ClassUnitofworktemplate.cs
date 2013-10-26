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
                  db.GetRepository<Country_PostalCode_List>().Find()

             }
             catch (Exception ex)
             {

                 //instantiate logger here so it does not break anything else.
                 logger = new ErroLogging(applicationEnum.MemberService);
                 //int profileid = Convert.ToInt32(viewerprofileid);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(viewerprofileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
         
                 //throw convertedexcption;
             }

         }

         _unitOfWork.DisableProxyCreation = true;
         using (var db = _unitOfWork)
         {
             try
             {
                  db.GetRepository<Country_PostalCode_List>().Find()

             }
             catch (Exception ex)
             {

                 //instantiate logger here so it does not break anything else.
                 logger = new ErroLogging(applicationEnum.MemberService);
                 //int profileid = Convert.ToInt32(viewerprofileid);
                 logger.WriteSingleEntry(logseverityEnum.CriticalError, ex, Convert.ToInt32(model.profileid));
                 //can parse the error to build a more custom error mssage and populate fualt faultreason
                 FaultReason faultreason = new FaultReason("Error in member service");
                 string ErrorMessage = "";
                 string ErrorDetail = "ErrorMessage: " + ex.Message;
                 throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);

                 //throw convertedexcption;
             }


         }

        addd
             
             db.Add(NewPhoto);           
            int i = db.Commit();
            transaction.Commit();

        update

              db.Update(myProfile);
                           int i = db.Commit();
                           transaction.Commit();


        //TO DO track the transaction types only rollback on DB connections
                            //rollback transaction
                            transaction.Rollback();


              return  db.GetRepository<profiledata>().Find().Single(p=>p.profilemetadata.photos.ToList().Any(z=>z.id == model.photoid)).gender.description;
           
	}
}
