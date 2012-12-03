using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;

//TO DO move these services to a new WCF service for now use it from the domain service
using Dating.Server.Data.Services;

using System.Data;
using System.Data.Entity;
using System.Web;

using Shell.MVC2.Infrastructure;

using Shell.MVC2.Interfaces;
using Shell.MVC2.AppFabric ;







namespace Shell.MVC2.Data
{
    public class LookupRepository : MemberRepositoryBase, ILookupRepository
    {
        // private AnewluvContext _db;
        //TO DO move from ria servives
       // private IGeoRepository _georepository;
        private IMemberRepository _memberrepository;


        public LookupRepository(AnewluvContext datingcontext, IMemberRepository memberrepository)
            : base(datingcontext)
        {
           // _georepository = georepository;
            _memberrepository = memberrepository;
        }

        
        #region "Load Other misc stuff such as list of pages etc"

        public List<systempagesetting> getsystempagesettinglist()
        {

           
                List<systempagesetting> temp = new List<systempagesetting>();

#if DISCONECTED
            
                systempagesetting virtualpage = new systempagesetting();
                virtualpage.title  = "MembersHome";
                virtualpage.bodycssstylename  = "StandardWhiteBackground";

                temp.Add(virtualpage);
                          
                            return temp;

#else  //load list from database
                temp = CachingFactory.CssStyleSelector.getsystempagesettingslist(_datingcontext);
                return temp;
#endif
            
        }

        public string getbodycssbypagename(string pagename)
        {
        
                return CachingFactory.CssStyleSelector.getbodycssbypagename(pagename,_datingcontext).ToString();
        }

        #endregion

        #region "generic lookup and collections"

        public List<lu_gender> getgenderlist()
        {
           

#if DISCONECTED
                List<lu_gender> genderlist = new List<lu_gender>();
                genderlist.Add(new lu_gender { description = "Male",  id  = 1, selected   = false });
                genderlist.Add(new lu_gender { description = "Female", id = 2, selected = false });
                return genderlist;
                
#else
            return CachingFactory.SharedObjectHelper.getgenderlist(_datingcontext);
                // return temp;
#endif

            
        }       
        public List<age> getageslist()
        {
            
               return  CachingFactory.SharedObjectHelper.getagelist();
            
        }
        public List<metricheight> getmetricheightlist()
        {

            
                return CachingFactory.SharedObjectHelper.getmetricheightlist();
            

        }

        #endregion


        //public SelectList GendersSelectList()
        //{
        //    SelectList genders = new SelectList(_datingcontext.GetGenders().ToList(), "GenderID", "GenderName");
        //    return genders;
        //}
                
        #region "Criteria Appearance dropdowns"



        public List<lu_bodytype> getbodytypelist()
        {
          


#if DISCONECTED
                List<lu_bodytype> bodytypelist = new List<lu_bodytype>();
                bodytypelist.Add(new lu_bodytype { description = "Male",  id  = 1, selected   = false });
                bodytypelist.Add(new lu_bodytype { description = "Female", id = 2, selected = false });
                return bodytypelist;
                
#else



                return CachingFactory.SharedObjectHelper.getbodytypelist(_datingcontext);



                // return temp;
#endif

            
        }


        public List<lu_ethnicity> getethnicitylist()
        {



#if DISCONECTED
            List<lu_ethnicity> ethnicitylist = new List<lu_ethnicity>();
            ethnicitylist.Add(new lu_ethnicity { description = "Male", id = 1, selected = false });
            ethnicitylist.Add(new lu_ethnicity { description = "Female", id = 2, selected = false });
            return ethnicitylist;

#else



                return CachingFactory.SharedObjectHelper.getethnicitylist(_datingcontext);


        
                // return temp;
#endif

        }
        
        public List<lu_eyecolor> geteyecolorlist()
        {
           


#if DISCONECTED
                List<lu_eyecolor> eyecolorlist = new List<lu_eyecolor>();
                eyecolorlist.Add(new lu_eyecolor { description = "Male",  id  = 1, selected   = false });
                eyecolorlist.Add(new lu_eyecolor { description = "Female", id = 2, selected = false });
                return eyecolorlist;
                
#else



                return CachingFactory.SharedObjectHelper.geteyecolorlist(_datingcontext);



                // return temp;
#endif

            }
        
        public List<lu_haircolor> gethaircolorlist()
        {
           

#if DISCONECTED
                List<lu_haircolor> haircolorlist = new List<lu_haircolor>();
                haircolorlist.Add(new lu_haircolor { description = "Male",  id  = 1, selected   = false });
                haircolorlist.Add(new lu_haircolor { description = "Female", id = 2, selected = false });
                return haircolorlist;
                
#else



                return CachingFactory.SharedObjectHelper.gethaircolorlist(_datingcontext);



                // return temp;
#endif

            
        }       
        
        #endregion

        #region "Criteria Character Dropdowns"

        public List<lu_diet> getdietlist()
        {
           

#if DISCONECTED
                List<lu_diet> dietlist = new List<lu_diet>();
                dietlist.Add(new lu_diet { description = "Male",  id  = 1, selected   = false });
                dietlist.Add(new lu_diet { description = "Female", id = 2, selected = false });
                return dietlist;
                
#else



                return _datingcontext.lu_diet.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_drinks> getdrinkslist()
        {
           

#if DISCONECTED
                List<lu_drinks> drinkslist = new List<lu_drinks>();
                drinkslist.Add(new lu_drinks { description = "Male",  id  = 1, selected   = false });
                drinkslist.Add(new lu_drinks { description = "Female", id = 2, selected = false });
                return drinkslist;
                
#else



                return _datingcontext.lu_drinks.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_exercise> getexerciselist()
        {
            


#if DISCONECTED
                List<lu_exercise> exerciselist = new List<lu_exercise>();
                exerciselist.Add(new lu_exercise { description = "Male",  id  = 1, selected   = false });
                exerciselist.Add(new lu_exercise { description = "Female", id = 2, selected = false });
                return exerciselist;
                
#else



                return _datingcontext.lu_exercise.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_hobby> gethobbylist()
        {
            


#if DISCONECTED
                List<lu_hobby> hobbylist = new List<lu_hobby>();
                hobbylist.Add(new lu_hobby { description = "Male",  id  = 1, selected   = false });
                hobbylist.Add(new lu_hobby { description = "Female", id = 2, selected = false });
                return hobbylist;
                
#else



                return _datingcontext.lu_hobby.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_humor> gethumorlist()
        {
           

#if DISCONECTED
                List<lu_humor> humorlist = new List<lu_humor>();
                humorlist.Add(new lu_humor { description = "Male",  id  = 1, selected   = false });
                humorlist.Add(new lu_humor { description = "Female", id = 2, selected = false });
                return humorlist;
                
#else



                return _datingcontext.lu_humor.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_politicalview> getpoliticalviewlist()
        {
           

#if DISCONECTED
                List<lu_politicalview> politicalviewlist = new List<lu_politicalview>();
                politicalviewlist.Add(new lu_politicalview { description = "Male",  id  = 1, selected   = false });
                politicalviewlist.Add(new lu_politicalview { description = "Female", id = 2, selected = false });
                return politicalviewlist;
                
#else



                return _datingcontext.lu_politicalview.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_religion> getreligionlist()
        {
          

#if DISCONECTED
                List<lu_religion> religionlist = new List<lu_religion>();
                religionlist.Add(new lu_religion { description = "Male",  id  = 1, selected   = false });
                religionlist.Add(new lu_religion { description = "Female", id = 2, selected = false });
                return religionlist;
                
#else



                return _datingcontext.lu_religion.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_religiousattendance> getreligiousattendancelist()
        {
          

#if DISCONECTED
                List<lu_religiousattendance> religiousattendancelist = new List<lu_religiousattendance>();
                religiousattendancelist.Add(new lu_religiousattendance { description = "Male",  id  = 1, selected   = false });
                religiousattendancelist.Add(new lu_religiousattendance { description = "Female", id = 2, selected = false });
                return religiousattendancelist;
                
#else



                return _datingcontext.lu_religiousattendance.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_sign> getsignlist()
        {
            


#if DISCONECTED
                List<lu_sign> signlist = new List<lu_sign>();
                signlist.Add(new lu_sign { description = "Male",  id  = 1, selected   = false });
                signlist.Add(new lu_sign { description = "Female", id = 2, selected = false });
                return signlist;
                
#else



                return _datingcontext.lu_sign.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_smokes> getsmokeslist()
        {
           

#if DISCONECTED
                List<lu_smokes> smokeslist = new List<lu_smokes>();
                smokeslist.Add(new lu_smokes { description = "Male",  id  = 1, selected   = false });
                smokeslist.Add(new lu_smokes { description = "Female", id = 2, selected = false });
                return smokeslist;
                
#else



                return _datingcontext.lu_smokes.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }


        #endregion

        #region "Criteria Lifestyle Dropdowns"

        public List<lu_educationlevel> geteducationlevellist()
        {
            


#if DISCONECTED
                List<lu_educationlevel> educationlevellist = new List<lu_educationlevel>();
                educationlevellist.Add(new lu_educationlevel { description = "Male",  id  = 1, selected   = false });
                educationlevellist.Add(new lu_educationlevel { description = "Female", id = 2, selected = false });
                return educationlevellist;
                
#else



                return _datingcontext.lu_educationlevel.OrderBy(x => x.description).ToList();



                // return temp;
#endif

           
        }
        public List<lu_employmentstatus> getemploymentstatuslist()
        {
           
#if DISCONECTED
                List<lu_employmentstatus> employmentstatuslist = new List<lu_employmentstatus>();
                employmentstatuslist.Add(new lu_employmentstatus { description = "Male",  id  = 1, selected   = false });
                employmentstatuslist.Add(new lu_employmentstatus { description = "Female", id = 2, selected = false });
                return employmentstatuslist;
                
#else



                return _datingcontext.lu_employmentstatus.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_havekids> gethavekidslist()
        {
          

#if DISCONECTED
                List<lu_havekids> havekidslist = new List<lu_havekids>();
                havekidslist.Add(new lu_havekids { description = "Male",  id  = 1, selected   = false });
                havekidslist.Add(new lu_havekids { description = "Female", id = 2, selected = false });
                return havekidslist;
                
#else



                return _datingcontext.lu_havekids.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_incomelevel> getincomelevellist()
        {
           


#if DISCONECTED
                List<lu_incomelevel> incomelevellist = new List<lu_incomelevel>();
                incomelevellist.Add(new lu_incomelevel { description = "Male",  id  = 1, selected   = false });
                incomelevellist.Add(new lu_incomelevel { description = "Female", id = 2, selected = false });
                return incomelevellist;
                
#else



                return _datingcontext.lu_incomelevel.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_livingsituation> getlivingsituationlist()
        {
           

#if DISCONECTED
                List<lu_livingsituation> livingsituationlist = new List<lu_livingsituation>();
                livingsituationlist.Add(new lu_livingsituation { description = "Male",  id  = 1, selected   = false });
                livingsituationlist.Add(new lu_livingsituation { description = "Female", id = 2, selected = false });
                return livingsituationlist;
                
#else



                return _datingcontext.lu_livingsituation.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_lookingfor> getlookingforlist()
        {
            


#if DISCONECTED
                List<lu_lookingfor> lookingforlist = new List<lu_lookingfor>();
                lookingforlist.Add(new lu_lookingfor { description = "Male",  id  = 1, selected   = false });
                lookingforlist.Add(new lu_lookingfor { description = "Female", id = 2, selected = false });
                return lookingforlist;
                
#else



                return _datingcontext.lu_lookingfor.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_maritalstatus> getmaritalstatuslist()
        {
            


#if DISCONECTED
                List<lu_maritalstatus> maritalstatuslist = new List<lu_maritalstatus>();
                maritalstatuslist.Add(new lu_maritalstatus { description = "Male",  id  = 1, selected   = false });
                maritalstatuslist.Add(new lu_maritalstatus { description = "Female", id = 2, selected = false });
                return maritalstatuslist;
                
#else



                return _datingcontext.lu_maritalstatus.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }
        public List<lu_profession> getprofessionlist()
        {
           

#if DISCONECTED
                List<lu_profession> professionlist = new List<lu_profession>();
                professionlist.Add(new lu_profession { description = "Male",  id  = 1, selected   = false });
                professionlist.Add(new lu_profession { description = "Female", id = 2, selected = false });
                return professionlist;
                
#else



                return _datingcontext.lu_profession.OrderBy(x => x.description).ToList();



                // return temp;
#endif

           
        }
        public List<lu_wantskids> getwantskidslist()
        {
           


#if DISCONECTED
                List<lu_wantskids> wantskidslist = new List<lu_wantskids>();
                wantskidslist.Add(new lu_wantskids { description = "Male",  id  = 1, selected   = false });
                wantskidslist.Add(new lu_wantskids { description = "Female", id = 2, selected = false });
                return wantskidslist;
                
#else



                return _datingcontext.lu_wantskids.OrderBy(x => x.description).ToList();



                // return temp;
#endif

            
        }

        #endregion


     


      
      

   


  


    }
}
