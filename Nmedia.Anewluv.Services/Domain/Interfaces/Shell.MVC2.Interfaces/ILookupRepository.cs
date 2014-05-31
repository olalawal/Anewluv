using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anewluv.Domain.Data;


namespace Shell.MVC2.Interfaces
{
    public interface ILookupRepository
    {


       



          
            
             
             List<systempagesetting> getsystempagesettinglist();
             string getbodycssbypagename(string pagename);            
             List<lu_gender> getgenderlist();           
             List<age> getageslist();
             List<metricheight> getmetricheightlist();
             //List<age> createagelist(); //not created by database  


             #region "lookups needed for making other calls i.e enum exposure"
             List<lu_photoformat> getphotoformatlist();
             List<lu_photoapprovalstatus> getphotoapprovalstatuslist();
             List<lu_photorejectionreason> getphotorejecttionreasonlist();
             List<lu_photostatus> getphotostatuslist();
             List<lu_photoimagetype> getphotoimagetypeslist();  
                                            
              List<lu_photostatusdescription> getphotostatusdescriptionlist();
              List<lu_abusetype> getabusetypelist();
              List<lu_profilestatus> getprofilestatuslist ();  
              List<lu_photoImagersizerformat> getphotoImagersizerformatlist ();        
              List<lu_role> getrolelist();
              List<lu_securityleveltype> getsecurityleveltypelist();
              List<lu_showme> getshowmelist();
              List<lu_sortbytype> getsortbytypelist();
              List<lu_securityquestion> getsecurityquestionlist();
              List<lu_flagyesno> getflagyesnolist();
                                      
              List<lu_profilefiltertype> getprofilefiltertypelist();


             #endregion

             #region "Criteria Appearance dropdowns"

             // List<metricheights> createheightmetriclist();  //not
             List<lu_ethnicity> getethnicitylist();           
             List<lu_bodytype> getbodytypelist();
             List<lu_eyecolor> geteyecolorlist();
             List<lu_haircolor> gethaircolorlist();                   
           
            #endregion

            #region "Criteria Character Dropdowns"

             List<lu_diet> getdietlist();
             List<lu_drinks> getdrinkslist();
             List<lu_exercise> getexerciselist();
             List<lu_hobby> gethobbylist();
             List<lu_humor> gethumorlist();
             List<lu_politicalview> getpoliticalviewlist();
             List<lu_religion> getreligionlist();
             List<lu_religiousattendance> getreligiousattendancelist();
             List<lu_sign> getsignlist();
             List<lu_smokes> getsmokeslist();            


            #endregion

            #region "Criteria Lifestyle Dropdowns"
             List<lu_educationlevel> geteducationlevellist();
             List<lu_employmentstatus> getemploymentstatuslist();
             List<lu_havekids> gethavekidslist();
             List<lu_incomelevel> getincomelevellist();
             List<lu_livingsituation> getlivingsituationlist();
             List<lu_lookingfor> getlookingforlist();
             List<lu_maritalstatus> getmaritalstatuslist();
             List<lu_profession> getprofessionlist();
             List<lu_wantskids> getwantskidslist();          

            #endregion

          


        }





    
}