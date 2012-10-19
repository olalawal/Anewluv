using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel.Web;
using System.ServiceModel;
using System.Text;
using System.Web.Security;
using Dating.Server.Data.Models;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


namespace Shell.MVC2.Services.Contracts
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ILookupService
    {

        [WebGet]
        [OperationContract]
        List<systempagesetting> getsystempagesettinglist();
        [WebGet]
        [OperationContract]
        List<lu_gender> getgenderlist();
        [WebGet]
        [OperationContract]
        List<age> getageslist();
        [WebGet]
        [OperationContract]
        List<metricheight> getmetricheightlist();
        //List<age> createagelist(); //not created by database 


        #region "Criteria Appearance dropdowns"
        [WebGet]
        [OperationContract]
        List<lu_ethnicity> getethnicitylist();
        [WebGet]
        [OperationContract]
        List<lu_bodytype> getbodytypelist();
        [WebGet]
        [OperationContract]
        List<lu_eyecolor> geteyecolorlist();
        [WebGet]
        [OperationContract]
        List<lu_haircolor> gethaircolorlist();


        #endregion

        #region "Criteria Character Dropdowns"
        [WebGet]
        [OperationContract]
        List<lu_diet> getdietlist();
        [WebGet]
        [OperationContract]
        List<lu_drinks> getdrinkslist();
        [WebGet]
        [OperationContract]
        List<lu_exercise> getexerciselist();
        [WebGet]
        [OperationContract]
        List<lu_hobby> gethobbylist();
        [WebGet]
        [OperationContract]
        List<lu_humor> gethumorlist();
        [WebGet]
        [OperationContract]
        List<lu_politicalview> getpoliticalviewlist();
        [WebGet]
        [OperationContract]
        List<lu_religion> getreligionlist();
        [WebGet]
        [OperationContract]
        List<lu_religiousattendance> getreligiousattendancelist();
        [WebGet]
        [OperationContract]
        List<lu_sign> getsignlist();
        [WebGet]
        [OperationContract]
        List<lu_smokes> getsmokeslist();



        #endregion

        #region "Criteria Lifestyle Dropdowns"
        [WebGet]
        [OperationContract]
        List<lu_educationlevel> geteducationlevellist();
        [WebGet]
        [OperationContract]
        List<lu_employmentstatus> getemploymentstatuslist();
        [WebGet]
        [OperationContract]
        List<lu_havekids> gethavekidslist();
        [WebGet]
        [OperationContract]
        List<lu_incomelevel> getincomelevellist();
        [WebGet]
        [OperationContract]
        List<lu_livingsituation> getlivingsituationlist();
        [WebGet]
        [OperationContract]
        List<lu_lookingfor> getlookingforlist();
        [WebGet]
        [OperationContract]
        List<lu_maritalstatus> getmaritalstatuslist();
        [WebGet]
        [OperationContract]
        List<lu_profession> getprofessionlist();
        [WebGet]
        [OperationContract]
        List<lu_wantskids> getwantskidslist();


        #endregion


    }
}
