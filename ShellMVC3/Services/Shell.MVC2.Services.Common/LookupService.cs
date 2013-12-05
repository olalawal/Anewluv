using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Shell.MVC2.Services.Contracts;
using Shell.MVC2.Interfaces;



using System.Web;
using System.Net;


using System.ServiceModel.Activation;

using Anewluv.Domain.Data;

namespace Shell.MVC2.Services.Common
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LookupService : ILookupService 
    {


        private ILookupRepository  _lookuprepository;
        // private string _apikey;

        public LookupService(ILookupRepository lookuprepository)
        {
            _lookuprepository = lookuprepository;
            //  _apikey  = HttpContext.Current.Request.QueryString["apikey"];
            //   throw new System.ServiceModel.Web.WebFaultException<string>("Invalid API Key", HttpStatusCode.Forbidden);

        }

      
    
      public  List<lu_photostatusdescription> getphotostatusdescriptionlist()
    {
        try
        {
            return _lookuprepository.getphotostatusdescriptionlist();

        }
        catch (Exception ex)
        {
            //can parse the error to build a more custom error mssage and populate fualt faultreason
            FaultReason faultreason = new FaultReason("Error in Lookup service");
            string ErrorMessage = "";
            string ErrorDetail = "ErrorMessage: " + ex.Message;
            throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
        }
        }


      public List<lu_abusetype> getabusetypelist()
        {
            try
            {
                return _lookuprepository.getabusetypelist();

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in Lookup service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }


      public List<lu_profilestatus> getprofilestatuslist()
        {
            try
            {
                return _lookuprepository.getprofilestatuslist();

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in Lookup service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }


      public List<lu_photoImagersizerformat> getphotoImagersizerformatlist()
        {

            try
            {
                return _lookuprepository.getphotoImagersizerformatlist();

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in Lookup service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }


      public List<lu_role> getrolelist()
        {

            try
            {
                return _lookuprepository.getrolelist();

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in Lookup service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }


      public List<lu_securityleveltype> getsecurityleveltypelist()
        {
            try
            {
                return _lookuprepository.getsecurityleveltypelist();

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in Lookup service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }


      public List<lu_showme> getshowmelist()
        {

            try
            {
                return _lookuprepository.getshowmelist();

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in Lookup service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }


      public List<lu_sortbytype> getsortbytypelist()
        {
            try
            {
                return _lookuprepository.getsortbytypelist();

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in Lookup service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }


      public List<lu_securityquestion> getsecurityquestionlist()
        {
            try
            {
                return _lookuprepository.getsecurityquestionlist();

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in Lookup service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }


      public List<lu_flagyesno> getflagyesnolist()
        {
            try
            {
                return _lookuprepository.getflagyesnolist();

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in Lookup service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }


      public List<lu_profilefiltertype> getprofilefiltertypelist()
        {
            try
            {
                return _lookuprepository.getprofilefiltertypelist();

            }
            catch (Exception ex)
            {
                //can parse the error to build a more custom error mssage and populate fualt faultreason
                FaultReason faultreason = new FaultReason("Error in Lookup service");
                string ErrorMessage = "";
                string ErrorDetail = "ErrorMessage: " + ex.Message;
                throw new FaultException<ServiceFault>(new ServiceFault(ErrorMessage, ErrorDetail), faultreason);
            }
        }


        public List<lu_photoformat> getphotoformatlist()       {

             return _lookuprepository.getphotoformatlist();

        }
        public List<lu_photoapprovalstatus> getphotoapprovalstatuslist()
        {

            return _lookuprepository.getphotoapprovalstatuslist();

        }
        public List<lu_photorejectionreason> getphotorejecttionreasonlist()
        {

            return _lookuprepository.getphotorejecttionreasonlist();

        }
        public List<lu_photostatus> getphotostatuslist()
        {

            return _lookuprepository.getphotostatuslist();

        }

        public List<lu_photoimagetype> getphotoimagetypeslist()
        {
            return _lookuprepository.getphotoimagetypeslist();
        }
        
 
        public List<systempagesetting> getsystempagesettinglist()
        {
            return _lookuprepository.getsystempagesettinglist();

        }

        public string getbodycssbypagename(string pagename)
        {
            return _lookuprepository.getbodycssbypagename(pagename);
        }

 
        public List<lu_gender> getgenderlist()
        {

            return _lookuprepository.getgenderlist();

        }
        public List<age> getageslist()
        {
            return _lookuprepository.getageslist(); 
     
        }
        public List<metricheight> getmetricheightlist()
        {

            return _lookuprepository.getmetricheightlist();
        }

       
             public List<lu_bodytype> getbodytypelist()
        {

            return _lookuprepository.getbodytypelist();

        }


        public List<lu_ethnicity> getethnicitylist()
        {

            return _lookuprepository.getethnicitylist();
        }

        public List<lu_eyecolor> geteyecolorlist()
        {

            return _lookuprepository.geteyecolorlist();
        }

        public List<lu_haircolor> gethaircolorlist()
        {

            return _lookuprepository.gethaircolorlist();

        }



        public List<lu_diet> getdietlist()
        {
            return _lookuprepository.getdietlist();

        }

        public List<lu_drinks> getdrinkslist()
        {
            return _lookuprepository.getdrinkslist();
        }

        public List<lu_exercise> getexerciselist()
        {

            return _lookuprepository.getexerciselist();

        }
       
        public List<lu_hobby> gethobbylist()
        {
            return _lookuprepository.gethobbylist();

        }

        public List<lu_humor> gethumorlist()
        {
            return _lookuprepository.gethumorlist();

        }

        public List<lu_politicalview> getpoliticalviewlist()
        {

            return _lookuprepository.getpoliticalviewlist();

        }

        public List<lu_religion> getreligionlist()
        {

            return _lookuprepository.getreligionlist();

        }


        public List<lu_religiousattendance> getreligiousattendancelist()
        {

            return _lookuprepository.getreligiousattendancelist();

        }


        public List<lu_sign> getsignlist()
        {

            return _lookuprepository.getsignlist();

        }

        public List<lu_smokes> getsmokeslist()
        {
            return _lookuprepository.getsmokeslist();

        }



        public List<lu_educationlevel> geteducationlevellist()
        {

            return _lookuprepository.geteducationlevellist();

        }

        public List<lu_employmentstatus> getemploymentstatuslist()
        {
            return _lookuprepository.getemploymentstatuslist();

        }

        public List<lu_havekids> gethavekidslist()
        {
            return _lookuprepository.gethavekidslist();
        }


        public List<lu_incomelevel> getincomelevellist()
        {

            return _lookuprepository.getincomelevellist();
        }


        public List<lu_livingsituation> getlivingsituationlist()
        {
            return _lookuprepository.getlivingsituationlist();
            
        }
        public List<lu_lookingfor> getlookingforlist()
        {

            return _lookuprepository.getlookingforlist();

        }


        public List<lu_maritalstatus> getmaritalstatuslist()
        {
            return _lookuprepository.getmaritalstatuslist();

        }


        public List<lu_profession> getprofessionlist()
        {

            return _lookuprepository.getprofessionlist();

        }


        public List<lu_wantskids> getwantskidslist()
        {

            return _lookuprepository.getwantskidslist();

        }

       

     
     
     
    }

}
