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

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;
using System.ServiceModel.Activation;

namespace Shell.MVC2.Services.Common
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MembersService" in both code and config file together.
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
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




        public List<lu_photoformat> getphotoformatlist()
        {

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
