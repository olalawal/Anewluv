using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


using Shell.MVC2.Interfaces;
using Shell.MVC2.Infrastructure;
using System.Data;

namespace Shell.MVC2.Data
{


    /////################# TO DO make all the invidual save for the per page basis private , i.e save everything as one big blob since we do
    //// not want to limit how the UI creators set up thier UI.  Reuturn all errors with the AnewLuv Messages thing for the UPDATEs and have the UI creator
    /// navigate the user to the pages with issues them selvs.

/// <summary>
/// TO DO split off search settings methods , if needed they should be references as an interface
/// </summary>
   public  class EditMemberRepository : MemberRepositoryBase  
    {

       
       
        private  AnewluvContext db; // = new AnewluvContext();    
        private IMemberRepository _membersrepository;
        
       //private  PostalData2Entities postaldb; //= new PostalData2Entities();

        public class CheckBox
        {
            public int id { get; set; }
            public string description { get; set; }
            public bool selected { get; set; }
        }

        public EditMemberRepository(AnewluvContext datingcontext, IMemberRepository membersrepository)
            : base(datingcontext)
        {
            _membersrepository = membersrepository;
        }
      
       
       // constructor
       public BasicSettingsViewModel getbasicsettingsviewmodel(int intprofileid)
            {
                try
                {

                    searchsetting p = db.searchsetting.Where(z => z.profile_id == intprofileid && z.myperfectmatch == true).FirstOrDefault();
                    BasicSettingsViewModel model = new BasicSettingsViewModel();

                    //populate values here ok ?
                    if (p != null)


                        model.searchname = p.searchname == null ? "Unamed Search" : p.searchname;
                    model.distancefromme = p.distancefromme == null ? 500 : p.distancefromme.GetValueOrDefault();
                    model.searchrank = p.searchrank == null ? 0 : p.searchrank.GetValueOrDefault();

                    //populate ages select list here I guess
                    //TODO get from app fabric
                    // SharedRepository sharedrepository = new SharedRepository();
                    //Ages = sharedrepository.AgesSelectList;

                    model.agemax = p.agemax == null ? 99 : p.agemax.GetValueOrDefault();
                    model.agemin = p.agemin == null ? 18 : p.agemin.GetValueOrDefault();




                    model.myperfectmatch = p.myperfectmatch == null ? false : p.myperfectmatch.Value;
                    model.systemmatch = p.systemmatch == null ? false : p.systemmatch.Value;
                    model.savedsearch = p.savedsearch == null ? false : p.savedsearch.Value;

                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allShowMe = db.lu_showme;
                    var ShowMeValues = new HashSet<int>(p.showme.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.ShowMe )
                    //{
                    //    (item.ShowMe.ShowMeName);
                    //}
                    foreach (var _ShowMe in allShowMe)
                    {
                        model.showmelist.Add(new lu_showme
                        {
                            id = _ShowMe.id,
                            description = _ShowMe.description,
                            selected = ShowMeValues.Contains(_ShowMe.id)
                        });
                    }

                    var allSortByTypes = db.lu_sortbytype;
                    var SortByTypeValues = new HashSet<int>(p.sortbytypes.Select(c => c.id.GetValueOrDefault()));

                    foreach (var _SortByType in allSortByTypes)
                    {
                        model.sortbytypelist.Add(new lu_sortbytype
                       {
                           description = _SortByType.description,
                           id = _SortByType.id,
                           selected = SortByTypeValues.Contains(_SortByType.id)
                       });
                    }


                    //populate BodyTypes uings other test value type
                    //TEST how this works vs other method
                    //foreach (var item in p.SortByType)
                    //{
                    //    SortBy.Add(item.SortByType);
                    //}


                    var allgenders = db.lu_gender;
                    var gendervalues = new HashSet<int>(p.genders.Select(c => c.id.GetValueOrDefault()));

                    //set default if non selected
                    // logic is if its a female then show them the male checked and vice versa
                    if (gendervalues.Count == 0)
                    {
                        if (p.profilemetadata.profile.profiledata.gender.id == 1)
                        {
                            gendervalues.Add(2);
                        }
                        else
                        {
                            gendervalues.Add(1);
                        }
                    }


                    foreach (var _gender in allgenders)
                    {
                        model.genderslist.Add(new lu_gender
                        {
                            description = _gender.description,
                            id = _gender.id,
                            selected = SortByTypeValues.Contains(_gender.id)
                        });
                    }





                    //for the gender only select one , the oposite of the members 
                    //set defualts
                    //if (p.Genders  == 1)
                    //{
                    //    LookingForGender = "Male";
                    //}
                    //else
                    //{
                    //    LookingForGender = "Female";
                    //}

                    return model;
                }
                catch
                {
                    //TO DO log errors here

                }

                return null;
            }       
       //Using a contstructor populate the current values I suppose
       //The actual values will bind to viewmodel I think
       public AppearanceSettingsViewModel getappearancesettingsviewmodel(int intprofileid)
            {


                try
                {
                    searchsetting p = db.searchsetting.Where(z => z.profile_id == intprofileid && z.myperfectmatch == true).FirstOrDefault();
                    AppearanceSettingsViewModel model = new AppearanceSettingsViewModel();

                    //hight in inches max defualt is 7"6 min default is 4"0
                    model.heightmax = p.heightmax == null ? -1 : p.heightmax.GetValueOrDefault();
                    model.heightmin = p.heightmin == null ? -1 : p.heightmin.GetValueOrDefault();

                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allbodytype = db.lu_bodytype;
                    var bodytypevalues = new HashSet<int>(p.bodytypes.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.bodytype )
                    //{
                    //    (item.bodytype.bodytypeName);
                    //}
                    foreach (var _bodytype in allbodytype)
                    {
                        model.bodytypeslist.Add(new lu_bodytype
                        {
                            id = _bodytype.id,
                            description = _bodytype.description,
                            selected = bodytypevalues.Contains(_bodytype.id)
                        });
                    }

                    //ethnicities
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allethnicity = db.lu_ethnicity;
                    var ethnicityvalues = new HashSet<int>(p.ethnicitys.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.ethnicity )
                    //{
                    //    (item.ethnicity.ethnicityName);
                    //}
                    foreach (var _ethnicity in allethnicity)
                    {
                        model.ethnicitylist.Add(new lu_ethnicity
                        {
                            id = _ethnicity.id,
                            description = _ethnicity.description,
                            selected = ethnicityvalues.Contains(_ethnicity.id)
                        });
                    }

                    //eye color 

                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var alleyecolor = db.lu_eyecolor;
                    var eyecolorvalues = new HashSet<int>(p.eyecolors.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.eyecolor )
                    //{
                    //    (item.eyecolor.eyecolorName);
                    //}
                    foreach (var _eyecolor in alleyecolor)
                    {
                        model.eyecolorlist.Add(new lu_eyecolor
                        {
                            id = _eyecolor.id,
                            description = _eyecolor.description,
                            selected = eyecolorvalues.Contains(_eyecolor.id)
                        });
                    }

                    // hair color
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allhaircolor = db.lu_haircolor;
                    var haircolorvalues = new HashSet<int>(p.haircolors.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.haircolor )
                    //{
                    //    (item.haircolor.haircolorName);
                    //}
                    foreach (var _haircolor in allhaircolor)
                    {
                        model.haircolorlist.Add(new lu_haircolor
                        {
                            id = _haircolor.id,
                            description = _haircolor.description,
                            selected = haircolorvalues.Contains(_haircolor.id)
                        });
                    }

                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allhotfeature = db.lu_hotfeature;
                    var hotfeaturevalues = new HashSet<int>(p.hotfeatures.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.hotfeature )
                    //{
                    //    (item.hotfeature.hotfeatureName);
                    //}
                    foreach (var _hotfeature in allhotfeature)
                    {
                        model.hotfeaturelist.Add(new lu_hotfeature
                        {
                            id = _hotfeature.id,
                            description = _hotfeature.description,
                            selected = hotfeaturevalues.Contains(_hotfeature.id)
                        });
                    }

                    return model;
                
                }
                catch
                {
                //TO DO log stuff here
                }

                return null;
            }                   
       //populate the enities
       public LifeStyleSettingsViewModel getlifestylesettingsviewmodel(int intprofileid)
            {


                try
                {
                    searchsetting p = db.searchsetting.Where(z => z.profile_id == intprofileid && z.myperfectmatch == true).FirstOrDefault();
                    LifeStyleSettingsViewModel model = new LifeStyleSettingsViewModel();

                    #region "EducationLevel"
                    //EducationLevel checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var alleducationlevel = db.lu_educationlevel;
                    var educationlevelvalues = new HashSet<int>(p.educationlevels.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.educationlevel )
                    //{
                    //    (item.educationlevel.educationlevelName);
                    //}
                    foreach (var _educationlevel in alleducationlevel)
                    {
                        model.educationlevellist.Add (new lu_educationlevel
                        {
                            id = _educationlevel.id,
                            description = _educationlevel.description,
                            selected = educationlevelvalues.Contains(_educationlevel.id)
                        });
                    }
                    #endregion
                    #region "EmploymentStatus"
                    //EmploymentStatus checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allemploymentstatus = db.lu_employmentstatus;
                    var employmentstatusvalues = new HashSet<int>(p.employmentstatus.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.employmentstatus )
                    //{
                    //    (item.employmentstatus.employmentstatusName);
                    //}
                    foreach (var _employmentstatus in allemploymentstatus)
                    {
                        model.employmentstatuslist.Add(new lu_employmentstatus
                        {
                            id = _employmentstatus.id,
                            description = _employmentstatus.description,
                            selected = employmentstatusvalues.Contains(_employmentstatus.id)
                        });
                    }
                    #endregion
                    #region "IncomeLevel"
                    //IncomeLevel checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allincomelevel = db.lu_incomelevel;
                    var incomelevelvalues = new HashSet<int>(p.incomelevels.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.incomelevel )
                    //{
                    //    (item.incomelevel.incomelevelName);
                    //}
                    foreach (var _incomelevel in allincomelevel)
                    {
                        model.incomelevellist.Add(new lu_incomelevel
                        {
                            id = _incomelevel.id,
                            description = _incomelevel.description,
                            selected = incomelevelvalues.Contains(_incomelevel.id)
                        });
                    }
                    #endregion
                    #region "LookingFor"
                    //LookingFor checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var alllookingfor = db.lu_lookingfor;
                    var lookingforvalues = new HashSet<int>(p.lookingfor .Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.lookingfor )
                    //{
                    //    (item.lookingfor.lookingforName);
                    //}
                    foreach (var _lookingfor in alllookingfor)
                    {
                        model.lookingforlist.Add(new lu_lookingfor
                        {
                            id = _lookingfor.id,
                            description = _lookingfor.description,
                            selected = lookingforvalues.Contains(_lookingfor.id)
                        });
                    }

                    #endregion
                    #region "WantsKids"

                    //WantsKids checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allwantskids = db.lu_wantskids;
                    var wantskidsvalues = new HashSet<int>(p.wantkids.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.wantskids )
                    //{
                    //    (item.wantskids.wantskidsName);
                    //}
                    foreach (var _wantskids in allwantskids)
                    {
                        model.wantskidslist.Add(new lu_wantskids
                        {
                            id = _wantskids.id,
                            description = _wantskids.description,
                            selected = wantskidsvalues.Contains(_wantskids.id)
                        });
                    }
                    #endregion
                    #region "Profession"

                    //Profession checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allprofession = db.lu_profession;
                    var professionvalues = new HashSet<int>(p.professions.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.profession )
                    //{
                    //    (item.profession.professionName);
                    //}
                    foreach (var _profession in allprofession)
                    {
                        model.professionlist.Add(new lu_profession
                        {
                            id = _profession.id,
                            description = _profession.description,
                            selected = professionvalues.Contains(_profession.id)
                        });
                    }
                    #endregion
                    #region "Marital STatus"

                    //MaritalStatus checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allmaritalstatus = db.lu_maritalstatus;
                    var maritalstatusvalues = new HashSet<int>(p.maritalstatuses.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.maritalstatus )
                    //{
                    //    (item.maritalstatus.maritalstatusName);
                    //}
                    foreach (var _maritalstatus in allmaritalstatus)
                    {
                        model.maritalstatuslist.Add(new lu_maritalstatus
                        {
                            id = _maritalstatus.id,
                            description = _maritalstatus.description,
                            selected = maritalstatusvalues.Contains(_maritalstatus.id)
                        });
                    }
                    #endregion
                    #region "Living Situation"

                    //LivingSituation checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var alllivingsituation = db.lu_livingsituation;
                    var livingsituationvalues = new HashSet<int>(p.livingstituations .Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.livingsituation )
                    //{
                    //    (item.livingsituation.livingsituationName);
                    //}
                    foreach (var _livingsituation in alllivingsituation)
                    {
                        model.livingsituationlist.Add(new lu_livingsituation
                        {
                            id = _livingsituation.id,
                            description = _livingsituation.description,
                            selected = livingsituationvalues.Contains(_livingsituation.id)
                        });
                    }
                    #endregion
                    #region "HaveKids"


                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allhavekids = db.lu_havekids;
                    var havekidsvalues = new HashSet<int>(p.havekids.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.havekids )
                    //{
                    //    (item.havekids.havekidsName);
                    //}
                    foreach (var _havekids in allhavekids)
                    {
                        model.havekidslist.Add(new lu_havekids
                        {
                            id = _havekids.id,
                            description = _havekids.description,
                            selected = havekidsvalues.Contains(_havekids.id)
                        });
                    }

                    #endregion

                    return model;
                }
                catch
                {
                //TO DO log errors here

                }

                return null;
             }     
         //Using a contstructor populate the current values I suppose
            //The actual values will bind to viewmodel I think
       public CharacterSettingsViewModel getcharactersettingsviewmodel(int intprofileid)
            {


                try
                {
                    searchsetting p = db.searchsetting.Where(z => z.profile_id == intprofileid && z.myperfectmatch == true).FirstOrDefault();
                    CharacterSettingsViewModel model = new CharacterSettingsViewModel();
                    #region "Diet"
                    //Diet checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var alldiet = db.lu_diet;
                    var dietvalues = new HashSet<int>(p.diets.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.diet )
                    //{
                    //    (item.diet.dietName);
                    //}
                    foreach (var _diet in alldiet)
                    {
                        model.dietlist.Add(new lu_diet
                        {
                            id = _diet.id,
                            description = _diet.description,
                            selected = dietvalues.Contains(_diet.id)
                        });
                    }
                    #endregion
                    #region "Humor"
                    //Humor checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allhumor = db.lu_humor;
                    var humorvalues = new HashSet<int>(p.humors.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.humor )
                    //{
                    //    (item.humor.humorName);
                    //}
                    foreach (var _humor in allhumor)
                    {
                        model.humorlist.Add(new lu_humor
                        {
                            id = _humor.id,
                            description = _humor.description,
                            selected = humorvalues.Contains(_humor.id)
                        });
                    }
                    #endregion
                    #region "Hobby"
                    //Hobby checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allhobby = db.lu_hobby;
                    var hobbyvalues = new HashSet<int>(p.hobbies .Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.hobby )
                    //{
                    //    (item.hobby.hobbyName);
                    //}
                    foreach (var _hobby in allhobby)
                    {
                        model.hobbylist.Add(new lu_hobby
                        {
                            id = _hobby.id,
                            description = _hobby.description,
                            selected = hobbyvalues.Contains(_hobby.id)
                        });
                    }
                    #endregion
                    #region "Drinks"
                    //Drinks checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var alldrinks = db.lu_drinks;
                    var drinksvalues = new HashSet<int>(p.drinks.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.drinks )
                    //{
                    //    (item.drinks.drinksName);
                    //}
                    foreach (var _drinks in alldrinks)
                    {
                        model.drinkslist.Add(new lu_drinks
                        {
                            id = _drinks.id,
                            description = _drinks.description,
                            selected = drinksvalues.Contains(_drinks.id)
                        });
                    }
                    #endregion
                    #region "Exercise"
                    //Exercise checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allexercise = db.lu_exercise;
                    var exercisevalues = new HashSet<int>(p.exercises.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.exercise )
                    //{
                    //    (item.exercise.exerciseName);
                    //}
                    foreach (var _exercise in allexercise)
                    {
                        model.exerciselist.Add(new lu_exercise
                        {
                            id = _exercise.id,
                            description = _exercise.description,
                            selected = exercisevalues.Contains(_exercise.id)
                        });
                    }
                    #endregion
                    #region "Smokes"
                    //Smokes checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allsmokes = db.lu_smokes;
                    var smokesvalues = new HashSet<int>(p.smokes.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.smokes )
                    //{
                    //    (item.smokes.smokesName);
                    //}
                    foreach (var _smokes in allsmokes)
                    {
                        model.smokeslist.Add(new lu_smokes
                        {
                            id = _smokes.id,
                            description = _smokes.description,
                            selected = smokesvalues.Contains(_smokes.id)
                        });
                    }
                    #endregion
                    #region "Sign"
                    //Sign checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allsign = db.lu_sign;
                    var signvalues = new HashSet<int>(p.signs.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.sign )
                    //{
                    //    (item.sign.signName);
                    //}
                    foreach (var _sign in allsign)
                    {
                        model.signlist.Add(new lu_sign
                        {
                            id = _sign.id,
                            description = _sign.description,
                            selected = signvalues.Contains(_sign.id)
                        });
                    }
                    #endregion
                    #region "PoliticalView"
                    //PoliticalView checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allpoliticalview = db.lu_politicalview;
                    var politicalviewvalues = new HashSet<int>(p.politicalviews.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.politicalview )
                    //{
                    //    (item.politicalview.politicalviewName);
                    //}
                    foreach (var _politicalview in allpoliticalview)
                    {
                        model.politicalviewlist.Add(new lu_politicalview
                        {
                            id = _politicalview.id,
                            description = _politicalview.description,
                            selected = politicalviewvalues.Contains(_politicalview.id)
                        });
                    }
                    #endregion
                    #region "Religion"
                    //Religion checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allreligion = db.lu_religion;
                    var religionvalues = new HashSet<int>(p.religions.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.religion )
                    //{
                    //    (item.religion.religionName);
                    //}
                    foreach (var _religion in allreligion)
                    {
                        model.religionlist.Add(new lu_religion
                        {
                            id = _religion.id,
                            description = _religion.description,
                            selected = religionvalues.Contains(_religion.id)
                        });
                    }
                    #endregion
                    #region "ReligiousAttendance"
                    //ReligiousAttendance checkbox values populated here
                    //pilot how to show the rest of the values 
                    //sample of doing string values
                    var allreligiousattendance = db.lu_religiousattendance;
                    var religiousattendancevalues = new HashSet<int>(p.religiousattendances.Select(c => c.id.GetValueOrDefault()));
                    //foreach (var item in p.religiousattendance )
                    //{
                    //    (item.religiousattendance.religiousattendanceName);
                    //}
                    foreach (var _religiousattendance in allreligiousattendance)
                    {
                        model.religiousattendancelist.Add(new lu_religiousattendance
                        {
                            id = _religiousattendance.id,
                            description = _religiousattendance.description,
                            selected = religiousattendancevalues.Contains(_religiousattendance.id)
                        });
                    }
                    #endregion

                    return model;
                }
                catch
                {
                    //TO DO log errors here

                }
                return null;
            }

       //here are the methdods that actually modify settings i.e old UI vs new
       #region "profile visisiblity settings update here"

       public bool UpdateProfileVisibilitySettings(visiblitysetting model)
       {
           if (model.id != null)
           {

               //Impement on member service ?
               // datingservice.UpdateProfileVisiblitySetting(model);


               return true;
           }
           return false;
       }
       #endregion
       
       #region "Edit profile Basic Settings Updates here
       public BasicSettingsViewModel EditProfileBasicSettings(BasicSettingsViewModel newmodel, int _ProfileID)
       {
           //get the profile details :
           profile profile = db.profiles.Where(p => p.id == _ProfileID).First();
           //create the search settings i.e matches if it does not exist 
           if (profile.profilemetadata.searchsettings.Count() == 0) _membersrepository.createmyperfectmatchsearchsettingsbyprofileid(_ProfileID);
           searchsetting SearchSettingsToUpdate = db.searchsetting.Where(p => p.profile_id  == _ProfileID && p.myperfectmatch == true && p.searchname  == "MyPerfectMatch").First();

           //TO DO this might be suplerflous ?
           var  newmodel2 = this.getbasicsettingsviewmodel(profile.id);  

           newmodel = EditProfileBasicSettingsPage1Update(newmodel, profile, SearchSettingsToUpdate);
           newmodel = EditProfileBasicSettingsPage2Update(newmodel, profile, SearchSettingsToUpdate);
          return newmodel;
       }
       private BasicSettingsViewModel EditProfileBasicSettingsPage1Update(BasicSettingsViewModel newmodel, profile profile, searchsetting oldsearchsettings)
       {

         
           //get all the values that should post from page 1
           //var AboutMe = formCollection["Editor"];  
           //var MyCatchyIntroLine = formCollection["MyCatchyIntroLine"];
           var AboutMe = newmodel.aboutme ;
           var MyCatchyIntroLine = newmodel.mycatchyintroline;
           var agemin = newmodel.agemin;
           var agemax = newmodel.agemax;
           //get current values from DB in case some values were not updated

           //TO DO this might be suplerflous ?
          // newmodel = this.getbasicsettingsviewmodel(profile.id);  
           //test if thoe are empty
           // var testLookingForAgeFrom = formCollection["BasicSearchSettings.agemin"];

           //check if user checked at least one gender
           // bool isGenderSelected = formCollection.GetValues("SelectedGenderIds").Contains("true");  
           var newGendersValues = newmodel.genderslist;  // formCollection["SelectedGenderIds"];;

           //re populate the models
           //build Basic Profile Settings from Submited view 
           newmodel.aboutme  = AboutMe;
           newmodel.mycatchyintroline = MyCatchyIntroLine;

           // map the basic search settings to the search settings pulled from databse
           // model.BasicSearchSettings = new SearchModelBasicSettings(SearchSettingsToUpdate);
           //update the searchmodl settings with current settings
           newmodel.agemin = agemin;
           newmodel.agemax = agemax;
          
         
           try
           {
               //link the profiledata entities
               profile.modificationdate  = DateTime.Now;
               //manually update model i think
               //set properties in the about me
               profile.profiledata.aboutme = AboutMe;
               profile.profiledata.mycatchyintroLine  = MyCatchyIntroLine;
               //detrmine if we are in edit or add mode for search settings for perfect match
               //if its null add a new entity  
               //noew update searchsettings text values
               oldsearchsettings.agemax = agemax;
               oldsearchsettings.agemin = agemin;
               oldsearchsettings.lastupdatedate  = DateTime.Now; //addded time stamp for updates this should be somone where else tho ?
               //TO DO move this code to searchssettings Repositoury
               this.updatesearchsettingsgenders(newmodel.genderslist.ToList(), oldsearchsettings);
               //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
               db.SaveChanges();
               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               return newmodel ;
           }
           catch (DataException)
           {
               //Log the error (add a variable name after DataException) 
              // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
              // return model;
           }
           return null;
       }
       private BasicSettingsViewModel EditProfileBasicSettingsPage2Update(BasicSettingsViewModel newmodel, profile profile, searchsetting oldsearchsettings)
       {
           

           var DistanceFromMe = newmodel.distancefromme;
           //re populate the models
           //build Basic Profile Settings from Submited view 
           // model.BasicProfileSettings. = AboutMe;
         

           //reload search settings since it seems the checkbox values are lost on postback
           //we really should just rebuild them from form collection imo        
           //update the searchmodl settings with current settings on the UI
         

           //update show me and sortby with correct values from UI as well
           //this code is just for serv side validation does nothing atm
           //showme next
         

           try
           {
               //link the profiledata entities
               profile.modificationdate = DateTime.Now;

               oldsearchsettings.distancefromme = newmodel.distancefromme;


               //TO DO move this code to searchssettings Repositoury             
                 this.updatesearchsettingsshowme(newmodel.showmelist.ToList(), oldsearchsettings);
                 this.updatesearchsettingssortbytype(newmodel.sortbytypelist.ToList(), oldsearchsettings);
               oldsearchsettings.lastupdatedate  = DateTime.Now;

               //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
               db.SaveChanges();

               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case
               //  membersmodel.profiledata = ProfileDataToUpdate;
               //  CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID,ProfileDataToUpdate  );

               //model.CurrentErrors.Clear();
               return newmodel;
           }
           catch (DataException)
           {
               //Log the error (add a variable name after DataException) 
               //model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               //return model;
           }
           return null;
       }
       #endregion


       #region "other editpages to implement"
       //#region "Edit profile Appeareance Settings Updates here"

       //public EditProfileSettingsViewModel EditProfileAppearanceSettingsPage1Update(EditProfileSettingsViewModel model,
       //FormCollection formCollection, int?[] SelectedYourBodyTypesID, string _ProfileID)
       //{

       //    profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
       //    if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
       //    SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


       //    // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

       //    //TO DO finde a better way to do this I guess get the current

       //    //re populate the models TO DO not sure this is needed index valiues are stored
       //    //if there are checkbox values on basic settings we would need to reload as well
       //    //build Basic Profile Settings from Submited view 
       //    // model.BasicProfileSettings. = AboutMe;
       //    //relaod appreadnce settings as needed
       //    var UiHeight = model.AppearanceSettings.Height;
       //    var UiBodyType = model.AppearanceSettings.BodyTypesID;

       //    model.AppearanceSettings = new EditProfileAppearanceSettingsModel(ProfileDataToUpdate);

       //    //noew updated the reloaded model with the saved higit on UI
       //    model.AppearanceSettings.Height = UiHeight;
       //    model.AppearanceSettings.BodyTypesID = UiBodyType;

       //    var heightmin = model.AppearanceSearchSettings.heightmin == -1 ? 48 : model.AppearanceSearchSettings.heightmin;
       //    var heightmax = model.AppearanceSearchSettings.heightmax == -1 ? 89 : model.AppearanceSearchSettings.heightmax;


       //    //reload search settings since it seems the checkbox values are lost on postback
       //    //we really should just rebuild them from form collection imo
       //    model.AppearanceSearchSettings = new SearchModelAppearanceSettings(SearchSettingsToUpdate);
       //    //update the reloaded  searchmodl settings with current settings on the UI
       //    model.AppearanceSearchSettings.heightmin = heightmin;
       //    model.AppearanceSearchSettings.heightmax = heightmax;


       //    //update the searchmodl settings with current settings            
       //    //update body types mine For UI
       //    IEnumerable<int?> EnumerableYourBodyTypes = SelectedYourBodyTypesID;

       //    var YourBodyTypesValues = EnumerableYourBodyTypes != null ? new HashSet<int?>(EnumerableYourBodyTypes) : null;

       //    foreach (var _BodyTypes in model.AppearanceSearchSettings.bodytypeslist)
       //    {
       //        _BodyTypes.Selected = YourBodyTypesValues != null ? YourBodyTypesValues.Contains(_BodyTypes.BodyTypesID) : false;
       //    }


       //    try
       //    {
       //        //link the profiledata entities
       //        ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;

       //        //search settings will never be null anymore , should have been created before we got here and added the members model
       //        //Add searchh settings as well if its not null
       //        //if (ModelToUpdate.SearchSettings == null)
       //        //{
       //        //    SearchSetting NewSearchSettings = new SearchSetting();
       //        //    ProfileDataToUpdate.SearchSettings.Add(NewSearchSettings);
       //        //}
       //        //else
       //        //{
       //        //    ProfileDataToUpdate.SearchSettings.Add(ModelToUpdate.SearchSettings);
       //        //}

       //        //detrmine if we are in edit or add mode for search settings for perfect match
       //        //if its null add a new entity  
       //        //noew update searchsettings text values
       //        //update my settings 
       //        ProfileDataToUpdate.Height = Convert.ToInt32(model.AppearanceSettings.Height);
       //        ProfileDataToUpdate.BodyTypeID = model.AppearanceSettings.BodyTypesID;

       //        //now update the search settings 
       //        SearchSettingsToUpdate.HeightMin = model.AppearanceSearchSettings.heightmin;
       //        SearchSettingsToUpdate.HeightMax = model.AppearanceSearchSettings.heightmax;
       //        SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;
       //        UpdateSearchSettingsBodyTypes(SelectedYourBodyTypesID, ProfileDataToUpdate);


       //        //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
       //        int changes = db.SaveChanges();

       //        //TOD DO
       //        //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
       //        //update session too just in case
       //        //membersmodel.profiledata = ProfileDataToUpdate;               

       //        //   CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID,ProfileDataToUpdate  );
       //        model.CurrentErrors.Clear();
       //        return model;
       //    }
       //    catch (DataException)
       //    {
       //        //Log the error (add a variable name after DataException) 
       //        model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
       //        return model;
       //    }

       //}

       //public EditProfileSettingsViewModel EditProfileAppearanceSettingsPage2Update(EditProfileSettingsViewModel model,
       //     FormCollection formCollection, int?[] SelectedYourEthnicityIds, int?[] SelectedMyEthnicityIds, string _ProfileID
       //    )
       //{


       //    profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
       //    if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
       //    SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();

       //    //re populate the models TO DO not sure this is needed index valiues are stored
       //    //if there are checkbox values on basic settings we would need to reload as well
       //    //build Basic Profile Settings from Submited view 
       //    // model.BasicProfileSettings. = AboutMe;
       //    model.AppearanceSettings = new EditProfileAppearanceSettingsModel(ProfileDataToUpdate);


       //    //reload search settings since it seems the checkbox values are lost on postback
       //    //we really should just rebuild them from form collection imo
       //    model.AppearanceSearchSettings = new SearchModelAppearanceSettings(SearchSettingsToUpdate);



       //    //update the searchmodl settings with current settings            
       //    //update UI display values with current displayed values as well for check boxes

       //    IEnumerable<int?> EnumerableMyEthnicity = SelectedMyEthnicityIds;

       //    var MyEthnicityValues = EnumerableMyEthnicity != null ? new HashSet<int?>(EnumerableMyEthnicity) : null;

       //    foreach (var _Ethnicity in model.AppearanceSettings.Myethnicitylist)
       //    {
       //        _Ethnicity.Selected = MyEthnicityValues != null ? MyEthnicityValues.Contains(_Ethnicity.EthnicityID) : false;
       //    }

       //    IEnumerable<int?> EnumerableYourEthnicity = SelectedYourEthnicityIds;

       //    var YourEthnicityValues = EnumerableYourEthnicity != null ? new HashSet<int?>(EnumerableYourEthnicity) : null;

       //    foreach (var _Ethnicity in model.AppearanceSearchSettings.ethnicitylist)
       //    {
       //        _Ethnicity.Selected = YourEthnicityValues != null ? YourEthnicityValues.Contains(_Ethnicity.EthnicityID) : false;
       //    }


       //    try
       //    {
       //        //link the profiledata entities
       //        ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;

       //        ////Add searchh settings as well if its not null
       //        //if (ModelToUpdate.SearchSettings == null)
       //        //{
       //        //    SearchSetting NewSearchSettings = new SearchSetting();
       //        //    ProfileDataToUpdate.SearchSettings.Add(NewSearchSettings);
       //        //}
       //        //else
       //        //{
       //        //    ProfileDataToUpdate.SearchSettings.Add(ModelToUpdate.SearchSettings);
       //        //}

       //        UpdateSearchSettingsEthnicity(SelectedYourEthnicityIds, ProfileDataToUpdate);
       //        UpdateProfileDataEthnicity(SelectedMyEthnicityIds, ProfileDataToUpdate);
       //        SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;

       //        //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
       //        db.SaveChanges();

       //        //TOD DO
       //        //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
       //        //update session too just in case
       //        //membersmodel.profiledata = ProfileDataToUpdate;
       //        //   CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID ,ProfileDataToUpdate );

       //        model.CurrentErrors.Clear();
       //        return model;
       //    }
       //    catch (DataException)
       //    {
       //        //Log the error (add a variable name after DataException) 
       //        model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
       //        return model;
       //    }

       //}

       //public EditProfileSettingsViewModel EditProfileAppearanceSettingsPage3Update(EditProfileSettingsViewModel model,
       //     FormCollection formCollection, int?[] SelectedYourEyeColorIds,
       //    int?[] SelectedYourHairColorIds,
       //    string _ProfileID)
       //{

       //    profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
       //    if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
       //    SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();



       //    //reload search settings since it seems the checkbox values are lost on postback
       //    //we really should just rebuild them from form collection imo


       //    //re populate the models TO DO not sure this is needed index valiues are stored
       //    //if there are checkbox values on basic settings we would need to reload as well
       //    //build Basic Profile Settings from Submited view 
       //    // model.BasicProfileSettings. = AboutMe;
       //    //temp store values on UI also handle ANY case here !!
       //    //just for conistiancy.
       //    var EyColorID = model.AppearanceSettings.EyeColorID;
       //    var HairCOlorID = model.AppearanceSettings.HairColorID;
       //    model.AppearanceSettings = new EditProfileAppearanceSettingsModel(ProfileDataToUpdate);
       //    model.AppearanceSettings.HairColorID = HairCOlorID;
       //    model.AppearanceSettings.EyeColorID = EyColorID;


       //    //reload search settings since it seems the checkbox values are lost on postback
       //    //we really should just rebuild them from form collection imo
       //    model.AppearanceSearchSettings = new SearchModelAppearanceSettings(SearchSettingsToUpdate);
       //    //update the reloaded  searchmodl settings with current settings on the UI


       //    //update the searchmodl settings with current settings            
       //    //update UI display values with current displayed values as well for check boxes
       //    IEnumerable<int?> EnumerableYourHairColor = SelectedYourHairColorIds;

       //    var YourHairColorValues = EnumerableYourHairColor != null ? new HashSet<int?>(EnumerableYourHairColor) : null;

       //    foreach (var _HairColor in model.AppearanceSearchSettings.haircolorlist)
       //    {
       //        _HairColor.Selected = YourHairColorValues != null ? YourHairColorValues.Contains(_HairColor.HairColorID) : false;
       //    }

       //    IEnumerable<int?> EnumerableYourEyeColor = SelectedYourEyeColorIds;

       //    var YourEyeColorValues = EnumerableYourEyeColor != null ? new HashSet<int?>(EnumerableYourEyeColor) : null;

       //    foreach (var _EyeColor in model.AppearanceSearchSettings.eyecolorlist)
       //    {
       //        _EyeColor.Selected = YourEyeColorValues != null ? YourEyeColorValues.Contains(_EyeColor.EyeColorID) : false;
       //    }

       //    //UI updates done

       //    //get active profile data 



       //    try
       //    {
       //        ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;

       //        //update my settings 
       //        ProfileDataToUpdate.EyeColorID = model.AppearanceSettings.EyeColorID;
       //        ProfileDataToUpdate.HairColorID = model.AppearanceSettings.HairColorID;

       //        //now update the search settings 
       //        SearchSettingsToUpdate.HeightMin = model.AppearanceSearchSettings.heightmin;
       //        SearchSettingsToUpdate.HeightMax = model.AppearanceSearchSettings.heightmax;

       //        UpdateSearchSettingsEyeColor(SelectedYourEyeColorIds, ProfileDataToUpdate);
       //        UpdateSearchSettingsHairColor(SelectedYourHairColorIds, ProfileDataToUpdate);

       //        SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;
       //        //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
       //        db.SaveChanges();

       //        //TOD DO
       //        //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
       //        //update session too just in case
       //        // membersmodel.profiledata = ProfileDataToUpdate;
       //        // CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, membersmodel.Profile.ProfileID);

       //        model.CurrentErrors.Clear();
       //        return model;
       //    }
       //    catch (DataException)
       //    {
       //        //Log the error (add a variable name after DataException) 
       //        model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
       //        return model;
       //    }
       //}

       //public EditProfileSettingsViewModel EditProfileAppearanceSettingsPage4Update(EditProfileSettingsViewModel model,
       //     FormCollection formCollection, int?[] SelectedYourHotFeatureIds, int?[] SelectedMyHotFeatureIds, string _ProfileID)
       //{

       //    profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
       //    if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
       //    SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


       //    //no validation needed just save 
       //    //TOD DO move this to a function i think or repository for search and have it populate those there
       //    //repopulate checkboxes for postback
       //    //reload search settings since it seems the checkbox values are lost on postback
       //    //we really should just rebuild them from form collection imo

       //    //reload the Apppearance values
       //    model.AppearanceSettings = new EditProfileAppearanceSettingsModel(ProfileDataToUpdate);

       //    model.AppearanceSearchSettings = new SearchModelAppearanceSettings(SearchSettingsToUpdate);

       //    //update the searchmodl settings with current settings            
       //    //update UI display values with current displayed values as well for check boxes
       //    IEnumerable<int?> EnumerableYourHotFeature = SelectedYourHotFeatureIds;

       //    var YourHotFeatureValues = EnumerableYourHotFeature != null ? new HashSet<int?>(EnumerableYourHotFeature) : null;

       //    foreach (var _HotFeature in model.AppearanceSearchSettings.hotfeaturelist)
       //    {
       //        _HotFeature.Selected = YourHotFeatureValues != null ? YourHotFeatureValues.Contains(_HotFeature.HotFeatureID) : false;
       //    }

       //    IEnumerable<int?> EnumerableMyHotFeature = SelectedMyHotFeatureIds;

       //    var MyHotFeatureValues = EnumerableMyHotFeature != null ? new HashSet<int?>(EnumerableMyHotFeature) : null;

       //    foreach (var _HotFeature in model.AppearanceSettings.Myhotfeaturelist)
       //    {
       //        _HotFeature.Selected = MyHotFeatureValues != null ? MyHotFeatureValues.Contains(_HotFeature.HotFeatureID) : false;
       //    }

       //    //UI updates done

       //    //get active profile data 
       //    //get active profile data 


       //    try
       //    {
       //        //link the profiledata entities

       //        ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;
       //        //now update the search settings 
       //        UpdateSearchSettingsHotFeature(SelectedYourHotFeatureIds, ProfileDataToUpdate);
       //        UpdateProfileDataHotFeature(SelectedMyHotFeatureIds, ProfileDataToUpdate);

       //        SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;
       //        //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
       //        db.SaveChanges();

       //        //TOD DO
       //        //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
       //        //update session too just in case
       //        //membersmodel.profiledata = ProfileDataToUpdate;
       //        // CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID ,ProfileDataToUpdate );

       //        model.CurrentErrors.Clear();
       //        return model;
       //    }
       //    catch (DataException)
       //    {
       //        //Log the error (add a variable name after DataException) 
       //        model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
       //        return model;
       //    }
       //}

       //#endregion


//       #region "Edit profile LifeStyle Settings Updates here"

//       public EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage1Update(EditProfileSettingsViewModel model,
//       FormCollection formCollection, int?[] SelectedYourMaritalStatusIds, int?[] SelectedYourLivingSituationIds,
//           int?[] SelectedYourLookingForIds, int?[] SelectedMyLookingForIds, string _ProfileID)
//       {
//           profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
//           if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
//           SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           //re populate the models TO DO not sure this is needed index valiues are stored
//           //if there are checkbox values on basic settings we would need to reload as well
//           //build Basic Profile Settings from Submited view 
//           // model.BasicProfileSettings. = AboutMe;
//           //temp store values on UI also handle ANY case here !!
//           //just for conistiancy.
//           var MyMaritalStatusID = model.LifeStyleSettings.MaritalStatusID;
//           var MyLivingSituationID = model.LifeStyleSettings.LivingSituationID;
//           model.LifeStyleSettings = new EditProfileLifeStyleSettingsModel(ProfileDataToUpdate);
//           model.LifeStyleSettings.MaritalStatusID = MyMaritalStatusID;
//           model.LifeStyleSettings.LivingSituationID = MyLivingSituationID;


//           //reload search settings since it seems the checkbox values are lost on postback
//           //we really should just rebuild them from form collection imo
//           model.LifeStyleSearchSettings = new SearchModelLifeStyleSettings(SearchSettingsToUpdate);
//           //update the reloaded  searchmodl settings with current settings on the UI


//           //update the searchmodl settings with current settings            
//           //update UI display values with current displayed values as well for check boxes

//           IEnumerable<int?> EnumerableMyLookingFor = SelectedMyLookingForIds;

//           var MyLookingForValues = EnumerableMyLookingFor != null ? new HashSet<int?>(EnumerableMyLookingFor) : null;

//           foreach (var _LookingFor in model.LifeStyleSettings.MyLookingForList)
//           {
//               _LookingFor.Selected = MyLookingForValues != null ? MyLookingForValues.Contains(_LookingFor.MyLookingForID) : false;
//           }


//           IEnumerable<int?> EnumerableYourLookingFor = SelectedYourLookingForIds;

//           var YourLookingForValues = EnumerableYourLookingFor != null ? new HashSet<int?>(EnumerableYourLookingFor) : null;

//           foreach (var _LookingFor in model.LifeStyleSearchSettings.lookingforlist)
//           {
//               _LookingFor.Selected = YourLookingForValues != null ? YourLookingForValues.Contains(_LookingFor.LookingForID) : false;
//           }

//           IEnumerable<int?> EnumerableYourLivingSituation = SelectedYourLivingSituationIds;

//           var YourLivingSituationValues = EnumerableYourLivingSituation != null ? new HashSet<int?>(EnumerableYourLivingSituation) : null;

//           foreach (var _LivingSituation in model.LifeStyleSearchSettings.livingsituationlist)
//           {
//               _LivingSituation.Selected = YourLivingSituationValues != null ? YourLivingSituationValues.Contains(_LivingSituation.LivingSituationID) : false;
//           }

//           IEnumerable<int?> EnumerableYourMaritalStatus = SelectedYourMaritalStatusIds;

//           var YourMaritalStatusValues = EnumerableYourMaritalStatus != null ? new HashSet<int?>(EnumerableYourMaritalStatus) : null;

//           foreach (var _MaritalStatus in model.LifeStyleSearchSettings.maritalstatuslist)
//           {
//               _MaritalStatus.Selected = YourMaritalStatusValues != null ? YourMaritalStatusValues.Contains(_MaritalStatus.MaritalStatusID) : false;
//           }



//           // profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
//           //  SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           try
//           {
//               ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;
//               ProfileDataToUpdate.MaritalStatusID = Convert.ToInt32(model.LifeStyleSettings.MaritalStatusID);
//               ProfileDataToUpdate.LivingSituationID = model.LifeStyleSettings.LivingSituationID;


//               UpdateProfileDataLookingFor(SelectedMyLookingForIds, ProfileDataToUpdate);
//               UpdateSearchSettingsLookingFor(SelectedYourLookingForIds, ProfileDataToUpdate);
//               UpdateSearchSettingsMaritalStatus(SelectedYourMaritalStatusIds, ProfileDataToUpdate);
//               UpdateSearchSettingsLivingSituation(SelectedYourLivingSituationIds, ProfileDataToUpdate);
//               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;


//               //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
//               int changes = db.SaveChanges();

//               //TOD DO
//               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
//               //update session too just in case
//               //membersmodel.profiledata = ProfileDataToUpdate;

//               CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID, ProfileDataToUpdate);


//               model.CurrentErrors.Clear();
//               return model;
//           }
//           catch (DataException)
//           {
//               //Log the error (add a variable name after DataException) 
//               model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
//               return model;
//           }

//       }

//       public EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage2Update(EditProfileSettingsViewModel model,
//   FormCollection formCollection, int?[] SelectedYourHaveKidsIds, int?[] SelectedYourWantsKidsIds,
//        string _ProfileID)
//       {
//           // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

//           profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
//           if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
//           SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           //re populate the models TO DO not sure this is needed index valiues are stored
//           //if there are checkbox values on basic settings we would need to reload as well
//           //build Basic Profile Settings from Submited view 
//           // model.BasicProfileSettings. = AboutMe;
//           //temp store values on UI also handle ANY case here !!
//           //just for conistiancy.
//           var MyWantKidsID = model.LifeStyleSettings.WantsKidsID;
//           var MyHaveKidsID = model.LifeStyleSettings.HaveKidsId;
//           model.LifeStyleSettings = new EditProfileLifeStyleSettingsModel(ProfileDataToUpdate);
//           model.LifeStyleSettings.WantsKidsID = MyWantKidsID;
//           model.LifeStyleSettings.HaveKidsId = MyHaveKidsID;


//           //reload search settings since it seems the checkbox values are lost on postback
//           //we really should just rebuild them from form collection imo
//           model.LifeStyleSearchSettings = new SearchModelLifeStyleSettings(SearchSettingsToUpdate);
//           //update the reloaded  searchmodl settings with current settings on the UI


//           //update the searchmodl settings with current settings            
//           //update UI display values with current displayed values as well for check boxes



//           IEnumerable<int?> EnumerableYourWantsKids = SelectedYourWantsKidsIds;

//           var YourWantsKidsValues = EnumerableYourWantsKids != null ? new HashSet<int?>(EnumerableYourWantsKids) : null;

//           foreach (var _WantsKids in model.LifeStyleSearchSettings.wantskidslist)
//           {
//               _WantsKids.Selected = YourWantsKidsValues != null ? YourWantsKidsValues.Contains(_WantsKids.WantsKidsID) : false;
//           }


//           IEnumerable<int?> EnumerableYourHaveKids = SelectedYourHaveKidsIds;

//           var YourHaveKidsValues = EnumerableYourHaveKids != null ? new HashSet<int?>(EnumerableYourHaveKids) : null;

//           foreach (var _HaveKids in model.LifeStyleSearchSettings.havekidslist)
//           {
//               _HaveKids.Selected = YourHaveKidsValues != null ? YourHaveKidsValues.Contains(_HaveKids.HaveKidsID) : false;
//           }



//           //profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
//           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           try
//           {



//               ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;

//               ProfileDataToUpdate.WantsKidsID = Convert.ToInt32(model.LifeStyleSettings.WantsKidsID);
//               ProfileDataToUpdate.HaveKidsId = model.LifeStyleSettings.HaveKidsId;



//               UpdateSearchSettingsWantsKids(SelectedYourWantsKidsIds, ProfileDataToUpdate);
//               UpdateSearchSettingsHaveKids(SelectedYourHaveKidsIds, ProfileDataToUpdate);

//               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;

//               //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
//               int changes = db.SaveChanges();

//               //TOD DO
//               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
//               //update session too just in case
//               //membersmodel.profiledata = ProfileDataToUpdate;

//               // CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, membersmodel.Profile.ProfileID);


//               model.CurrentErrors.Clear();
//               return model;
//           }
//           catch (DataException)
//           {
//               //Log the error (add a variable name after DataException) 
//               model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
//               return model;
//           }

//       }


//       public EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage3Update(EditProfileSettingsViewModel model,
//FormCollection formCollection, int?[] SelectedYourEmploymentStatusIds, int?[] SelectedYourIncomeLevelIds,
//     string _ProfileID)
//       {
//           // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

//           profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
//           if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
//           SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           //re populate the models TO DO not sure this is needed index valiues are stored
//           //if there are checkbox values on basic settings we would need to reload as well
//           //build Basic Profile Settings from Submited view 
//           // model.BasicProfileSettings. = AboutMe;
//           //temp store values on UI also handle ANY case here !!
//           //just for conistiancy.
//           var MyIncomeLevelID = model.LifeStyleSettings.IncomeLevelID;
//           var MyEmploymentStatusID = model.LifeStyleSettings.EmploymentStatusID;
//           model.LifeStyleSettings = new EditProfileLifeStyleSettingsModel(ProfileDataToUpdate);
//           model.LifeStyleSettings.IncomeLevelID = MyIncomeLevelID;
//           model.LifeStyleSettings.EmploymentStatusID = MyEmploymentStatusID;


//           //reload search settings since it seems the checkbox values are lost on postback
//           //we really should just rebuild them from form collection imo
//           model.LifeStyleSearchSettings = new SearchModelLifeStyleSettings(SearchSettingsToUpdate);
//           //update the reloaded  searchmodl settings with current settings on the UI


//           //update the searchmodl settings with current settings            
//           //update UI display values with current displayed values as well for check boxes



//           IEnumerable<int?> EnumerableYourIncomeLevel = SelectedYourIncomeLevelIds;

//           var YourIncomeLevelValues = EnumerableYourIncomeLevel != null ? new HashSet<int?>(EnumerableYourIncomeLevel) : null;

//           foreach (var _IncomeLevel in model.LifeStyleSearchSettings.incomelevellist)
//           {
//               _IncomeLevel.Selected = YourIncomeLevelValues != null ? YourIncomeLevelValues.Contains(_IncomeLevel.IncomeLevelID) : false;
//           }


//           IEnumerable<int?> EnumerableYourEmploymentStatus = SelectedYourEmploymentStatusIds;

//           var YourEmploymentStatusValues = EnumerableYourEmploymentStatus != null ? new HashSet<int?>(EnumerableYourEmploymentStatus) : null;

//           foreach (var _EmploymentStatus in model.LifeStyleSearchSettings.employmentstatuslist)
//           {
//               _EmploymentStatus.Selected = YourEmploymentStatusValues != null ? YourEmploymentStatusValues.Contains(_EmploymentStatus.EmploymentStatusID) : false;
//           }



//           //profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
//           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           try
//           {
//               ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;

//               ProfileDataToUpdate.IncomeLevelID = Convert.ToInt32(model.LifeStyleSettings.IncomeLevelID);
//               ProfileDataToUpdate.EmploymentSatusID = model.LifeStyleSettings.EmploymentStatusID;



//               UpdateSearchSettingsIncomeLevel(SelectedYourIncomeLevelIds, ProfileDataToUpdate);
//               UpdateSearchSettingsEmploymentStatus(SelectedYourEmploymentStatusIds, ProfileDataToUpdate);
//               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;


//               //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
//               int changes = db.SaveChanges();

//               //TOD DO
//               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
//               //update session too just in case
//               // membersmodel.profiledata = ProfileDataToUpdate;
//               //CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, membersmodel.Profile.ProfileID);


//               model.CurrentErrors.Clear();
//               return model;
//           }
//           catch (DataException)
//           {
//               //Log the error (add a variable name after DataException) 
//               model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
//               return model;
//           }

//       }

//       public EditProfileSettingsViewModel EditProfileLifeStyleSettingsPage4Update(EditProfileSettingsViewModel model,
//FormCollection formCollection, int?[] SelectedYourEducationLevelIds, int?[] SelectedYourProfessionIds,
//     string _ProfileID)
//       {

//           profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
//           if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
//           SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();

//           //MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

//           //re populate the models TO DO not sure this is needed index valiues are stored
//           //if there are checkbox values on basic settings we would need to reload as well
//           //build Basic Profile Settings from Submited view 
//           // model.BasicProfileSettings. = AboutMe;
//           //temp store values on UI also handle ANY case here !!
//           //just for conistiancy.
//           var MyProfessionID = model.LifeStyleSettings.ProfessionID;
//           var MyEducationLevelID = model.LifeStyleSettings.EducationLevelID;
//           model.LifeStyleSettings = new EditProfileLifeStyleSettingsModel(ProfileDataToUpdate);
//           model.LifeStyleSettings.ProfessionID = MyProfessionID;
//           model.LifeStyleSettings.EducationLevelID = MyEducationLevelID;


//           //reload search settings since it seems the checkbox values are lost on postback
//           //we really should just rebuild them from form collection imo
//           model.LifeStyleSearchSettings = new SearchModelLifeStyleSettings(SearchSettingsToUpdate);
//           //update the reloaded  searchmodl settings with current settings on the UI


//           //update the searchmodl settings with current settings            
//           //update UI display values with current displayed values as well for check boxes



//           IEnumerable<int?> EnumerableYourProfession = SelectedYourProfessionIds;

//           var YourProfessionValues = EnumerableYourProfession != null ? new HashSet<int?>(EnumerableYourProfession) : null;

//           foreach (var _Profession in model.LifeStyleSearchSettings.professionlist)
//           {
//               _Profession.Selected = YourProfessionValues != null ? YourProfessionValues.Contains(_Profession.ProfessionID) : false;
//           }


//           IEnumerable<int?> EnumerableYourEducationLevel = SelectedYourEducationLevelIds;

//           var YourEducationLevelValues = EnumerableYourEducationLevel != null ? new HashSet<int?>(EnumerableYourEducationLevel) : null;

//           foreach (var _EducationLevel in model.LifeStyleSearchSettings.educationlevellist)
//           {
//               _EducationLevel.Selected = YourEducationLevelValues != null ? YourEducationLevelValues.Contains(_EducationLevel.EducationLevelID) : false;
//           }



//           //profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
//           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           try
//           {

//               ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;
//               ProfileDataToUpdate.ProfessionID = Convert.ToInt32(model.LifeStyleSettings.ProfessionID);
//               ProfileDataToUpdate.EducationLevelID = model.LifeStyleSettings.EducationLevelID;
//               UpdateSearchSettingsProfession(SelectedYourProfessionIds, ProfileDataToUpdate);
//               UpdateSearchSettingsEducationLevel(SelectedYourEducationLevelIds, ProfileDataToUpdate);


//               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;
//               //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
//               int changes = db.SaveChanges();

//               //TOD DO
//               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
//               //update session too just in case
//               //membersmodel.profiledata = ProfileDataToUpdate;

//               //CachingFactory.MembersViewModelHelper.UpdateMemberData(membersmodel, membersmodel.Profile.ProfileID);


//               model.CurrentErrors.Clear();
//               return model;
//           }
//           catch (DataException)
//           {
//               //Log the error (add a variable name after DataException) 
//               model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
//               return model;
//           }

//       }

//       #endregion


//       #region "Edit profile Character Settings Updates here"

//       public EditProfileSettingsViewModel EditProfileCharacterSettingsPage1Update(EditProfileSettingsViewModel model,
//       FormCollection formCollection, int?[] SelectedYourDietIds, int?[] SelectedYourDrinksIds,
//           int?[] SelectedYourExerciseIds, int?[] SelectedYourSmokesIds, string _ProfileID)
//       {
//           //5-10-2012 moved this to get these items first.
//           profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
//           if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
//           SearchSetting SearchSettingsToUpdate = membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID);// db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           //MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

//           //re populate the models TO DO not sure this is needed index valiues are stored
//           //if there are checkbox values on basic settings we would need to reload as well
//           //build Basic Profile Settings from Submited view 
//           // model.BasicProfileSettings. = AboutMe;
//           //temp store values on UI also handle ANY case here !!
//           //just for conistiancy.
//           var MyDietID = model.CharacterSettings.DietID;
//           var MyDrinksID = model.CharacterSettings.DrinksID;
//           var MyExerciseID = model.CharacterSettings.ExerciseID;
//           var MySmokesID = model.CharacterSettings.SmokesID;
//           //TO DO read from name value collection to incrfeate efficency
//           model.CharacterSettings = new EditProfileCharacterSettingsModel(ProfileDataToUpdate);
//           model.CharacterSettings.DietID = MyDietID;
//           model.CharacterSettings.DrinksID = MyDrinksID;
//           model.CharacterSettings.ExerciseID = MyExerciseID;
//           model.CharacterSettings.SmokesID = MySmokesID;


//           //reload search settings since it seems the checkbox values are lost on postback
//           //we really should just rebuild them from form collection imo
//           model.CharacterSearchSettings = new SearchModelCharacterSettings(SearchSettingsToUpdate);
//           //update the reloaded  searchmodl settings with current settings on the UI



//           IEnumerable<int?> EnumerableYourExercise = SelectedYourExerciseIds;

//           var YourExerciseValues = EnumerableYourExercise != null ? new HashSet<int?>(EnumerableYourExercise) : null;

//           foreach (var _Exercise in model.CharacterSearchSettings.exerciselist)
//           {
//               _Exercise.Selected = YourExerciseValues != null ? YourExerciseValues.Contains(_Exercise.ExerciseID) : false;
//           }

//           IEnumerable<int?> EnumerableYourDrinks = SelectedYourDrinksIds;

//           var YourDrinksValues = EnumerableYourDrinks != null ? new HashSet<int?>(EnumerableYourDrinks) : null;

//           foreach (var _Drinks in model.CharacterSearchSettings.drinkslist)
//           {
//               _Drinks.Selected = YourDrinksValues != null ? YourDrinksValues.Contains(_Drinks.DrinksID) : false;
//           }

//           IEnumerable<int?> EnumerableYourDiet = SelectedYourDietIds;

//           var YourDietValues = EnumerableYourDiet != null ? new HashSet<int?>(EnumerableYourDiet) : null;

//           foreach (var _Diet in model.CharacterSearchSettings.dietlist)
//           {
//               _Diet.Selected = YourDietValues != null ? YourDietValues.Contains(_Diet.DietID) : false;
//           }

//           IEnumerable<int?> EnumerableYourSmokes = SelectedYourSmokesIds;

//           var YourSmokesValues = EnumerableYourSmokes != null ? new HashSet<int?>(EnumerableYourSmokes) : null;

//           foreach (var _Smokes in model.CharacterSearchSettings.smokeslist)
//           {
//               _Smokes.Selected = YourSmokesValues != null ? YourSmokesValues.Contains(_Smokes.SmokesID) : false;
//           }




//           //profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
//           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           try
//           {
//               ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;

//               ProfileDataToUpdate.DietID = Convert.ToInt32(model.CharacterSettings.DietID);
//               ProfileDataToUpdate.DrinksID = model.CharacterSettings.DrinksID;
//               ProfileDataToUpdate.ExerciseID = model.CharacterSettings.ExerciseID;
//               ProfileDataToUpdate.SmokesID = model.CharacterSettings.SmokesID;


//               UpdateSearchSettingsExercise(SelectedYourExerciseIds, ProfileDataToUpdate);
//               UpdateSearchSettingsDiet(SelectedYourDietIds, ProfileDataToUpdate);
//               UpdateSearchSettingsDrinks(SelectedYourDrinksIds, ProfileDataToUpdate);
//               UpdateSearchSettingsSmokes(SelectedYourSmokesIds, ProfileDataToUpdate);

//               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;

//               //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
//               int changes = db.SaveChanges();

//               //TOD DO
//               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
//               //update session too just in case
//               // membersmodel.profiledata = ProfileDataToUpdate;

//               //CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID, ProfileDataToUpdate);


//               model.CurrentErrors.Clear();
//               return model;
//           }
//           catch (DataException)
//           {
//               //Log the error (add a variable name after DataException) 
//               model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
//               return model;
//           }

//       }

//       public EditProfileSettingsViewModel EditProfileCharacterSettingsPage2Update(EditProfileSettingsViewModel model,
//   FormCollection formCollection, int?[] SelectedYourHobbyIds, int?[] SelectedMyHobbyIds, int?[] SelectedYourSignIds,
//        string _ProfileID)
//       {
//           // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);
//           //5-10-2012 moved this to get these items first.
//           profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
//           if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
//           SearchSetting SearchSettingsToUpdate = membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID); // db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();



//           //re populate the models TO DO not sure this is needed index valiues are stored
//           //if there are checkbox values on basic settings we would need to reload as well
//           //build Basic Profile Settings from Submited view 
//           // model.BasicProfileSettings. = AboutMe;
//           //temp store values on UI also handle ANY case here !!
//           //just for conistiancy.
//           var MySignID = model.CharacterSettings.SignID;
//           model.CharacterSettings = new EditProfileCharacterSettingsModel(ProfileDataToUpdate);
//           model.CharacterSettings.SignID = MySignID;



//           //reload search settings since it seems the checkbox values are lost on postback
//           //we really should just rebuild them from form collection imo
//           model.CharacterSearchSettings = new SearchModelCharacterSettings(SearchSettingsToUpdate);
//           //update the reloaded  searchmodl settings with current settings on the UI


//           //update the searchmodl settings with current settings            
//           //update UI display values with current displayed values as well for check boxes



//           IEnumerable<int?> EnumerableMyHobby = SelectedMyHobbyIds;

//           var MyHobbyValues = EnumerableMyHobby != null ? new HashSet<int?>(EnumerableMyHobby) : null;

//           foreach (var _Hobby in model.CharacterSettings.MyHobbyList)
//           {
//               _Hobby.Selected = MyHobbyValues != null ? MyHobbyValues.Contains(_Hobby.MyHobbyID) : false;
//           }

//           IEnumerable<int?> EnumerableYourHobby = SelectedYourHobbyIds;

//           var YourHobbyValues = EnumerableYourHobby != null ? new HashSet<int?>(EnumerableYourHobby) : null;

//           foreach (var _Hobby in model.CharacterSearchSettings.hobbylist)
//           {
//               _Hobby.Selected = YourHobbyValues != null ? YourHobbyValues.Contains(_Hobby.HobbyID) : false;
//           }


//           IEnumerable<int?> EnumerableYourSign = SelectedYourSignIds;

//           var YourSignValues = EnumerableYourSign != null ? new HashSet<int?>(EnumerableYourSign) : null;

//           foreach (var _Sign in model.CharacterSearchSettings.signlist)
//           {
//               _Sign.Selected = YourSignValues != null ? YourSignValues.Contains(_Sign.SignID) : false;
//           }



//           // profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
//           // SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           try
//           {

//               ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;

//               ProfileDataToUpdate.SignID = Convert.ToInt32(model.CharacterSettings.SignID);




//               UpdateProfileDataHobby(SelectedMyHobbyIds, ProfileDataToUpdate);
//               UpdateSearchSettingsHobby(SelectedYourHobbyIds, ProfileDataToUpdate);
//               UpdateSearchSettingsSign(SelectedYourSignIds, ProfileDataToUpdate);

//               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;

//               //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
//               int changes = db.SaveChanges();

//               //TOD DO
//               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
//               //update session too just in case
//               // membersmodel.profiledata = ProfileDataToUpdate;

//               // CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID, ProfileDataToUpdate);

//               model.CurrentErrors.Clear();
//               return model;
//           }
//           catch (DataException)
//           {
//               //Log the error (add a variable name after DataException) 
//               model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
//               return model;
//           }

//       }



//       public EditProfileSettingsViewModel EditProfileCharacterSettingsPage3Update(EditProfileSettingsViewModel model,
//FormCollection formCollection, int?[] SelectedYourReligionIds, int?[] SelectedYourReligiousAttendanceIds,
//     string _ProfileID)
//       {
//           // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);
//           //5-10-2012 moved this to get these items first.
//           profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
//           if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
//           SearchSetting SearchSettingsToUpdate = membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID); //db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           //re populate the models TO DO not sure this is needed index valiues are stored
//           //if there are checkbox values on basic settings we would need to reload as well
//           //build Basic Profile Settings from Submited view 
//           // model.BasicProfileSettings. = AboutMe;
//           //temp store values on UI also handle ANY case here !!
//           //just for conistiancy.
//           var MyReligiousAttendanceID = model.CharacterSettings.ReligiousAttendanceID;
//           var MyReligionID = model.CharacterSettings.ReligionID;
//           model.CharacterSettings = new EditProfileCharacterSettingsModel(ProfileDataToUpdate);
//           model.CharacterSettings.ReligiousAttendanceID = MyReligiousAttendanceID;
//           model.CharacterSettings.ReligionID = MyReligionID;


//           //reload search settings since it seems the checkbox values are lost on postback
//           //we really should just rebuild them from form collection imo
//           model.CharacterSearchSettings = new SearchModelCharacterSettings(SearchSettingsToUpdate);
//           //update the reloaded  searchmodl settings with current settings on the UI


//           //update the searchmodl settings with current settings            
//           //update UI display values with current displayed values as well for check boxes



//           IEnumerable<int?> EnumerableYourReligiousAttendance = SelectedYourReligiousAttendanceIds;

//           var YourReligiousAttendanceValues = EnumerableYourReligiousAttendance != null ? new HashSet<int?>(EnumerableYourReligiousAttendance) : null;

//           foreach (var _ReligiousAttendance in model.CharacterSearchSettings.religiousattendancelist)
//           {
//               _ReligiousAttendance.Selected = YourReligiousAttendanceValues != null ? YourReligiousAttendanceValues.Contains(_ReligiousAttendance.ReligiousAttendanceID) : false;
//           }


//           IEnumerable<int?> EnumerableYourReligion = SelectedYourReligionIds;

//           var YourReligionValues = EnumerableYourReligion != null ? new HashSet<int?>(EnumerableYourReligion) : null;

//           foreach (var _Religion in model.CharacterSearchSettings.religionlist)
//           {
//               _Religion.Selected = YourReligionValues != null ? YourReligionValues.Contains(_Religion.ReligionID) : false;
//           }



//           //   profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
//           //  SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           try
//           {
//               ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;

//               ProfileDataToUpdate.ReligiousAttendanceID = Convert.ToInt32(model.CharacterSettings.ReligiousAttendanceID);
//               ProfileDataToUpdate.ReligionID = Convert.ToInt32(model.CharacterSettings.ReligionID);
//               ProfileDataToUpdate.EmploymentSatusID = model.CharacterSettings.ReligionID;


//               UpdateSearchSettingsReligiousAttendance(SelectedYourReligiousAttendanceIds, ProfileDataToUpdate);
//               UpdateSearchSettingsReligion(SelectedYourReligionIds, ProfileDataToUpdate);

//               //added modifciation date 1-9-2012 , confirm that it works as an inclided
//               ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;
//               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;

//               //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
//               int changes = db.SaveChanges();

//               //TOD DO
//               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
//               //update session too just in case
//               // membersmodel.profiledata = ProfileDataToUpdate;

//               //CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID, ProfileDataToUpdate);


//               model.CurrentErrors.Clear();
//               return model;
//           }
//           catch (DataException)
//           {
//               //Log the error (add a variable name after DataException) 
//               model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
//               return model;
//           }

//       }

//       public EditProfileSettingsViewModel EditProfileCharacterSettingsPage4Update(EditProfileSettingsViewModel model,
//FormCollection formCollection, int?[] SelectedYourPoliticalViewIds, int?[] SelectedYourHumorIds,
//     string _ProfileID)
//       {
//           // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);
//           //5-10-2012 moved this to get these items first.
//           profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == _ProfileID).First();
//           if (ProfileDataToUpdate.SearchSettings.Count() == 0) membersrepository.CreateMyPerFectMatchSearchSettingsByProfileID(_ProfileID);
//           SearchSetting SearchSettingsToUpdate = membersrepository.GetPerFectMatchSearchSettingsByProfileID(_ProfileID); //db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();

//           //re populate the models TO DO not sure this is needed index valiues are stored
//           //if there are checkbox values on basic settings we would need to reload as well
//           //build Basic Profile Settings from Submited view 
//           // model.BasicProfileSettings. = AboutMe;
//           //temp store values on UI also handle ANY case here !!
//           //just for conistiancy.
//           var MyHumorID = model.CharacterSettings.HumorID;
//           var MyPoliticalViewID = model.CharacterSettings.PoliticalViewID;
//           model.CharacterSettings = new EditProfileCharacterSettingsModel(ProfileDataToUpdate);
//           model.CharacterSettings.HumorID = MyHumorID;
//           model.CharacterSettings.PoliticalViewID = MyPoliticalViewID;


//           //reload search settings since it seems the checkbox values are lost on postback
//           //we really should just rebuild them from form collection imo
//           model.CharacterSearchSettings = new SearchModelCharacterSettings(SearchSettingsToUpdate);
//           //update the reloaded  searchmodl settings with current settings on the UI


//           //update the searchmodl settings with current settings            
//           //update UI display values with current displayed values as well for check boxes



//           IEnumerable<int?> EnumerableYourHumor = SelectedYourHumorIds;

//           var YourHumorValues = EnumerableYourHumor != null ? new HashSet<int?>(EnumerableYourHumor) : null;

//           foreach (var _Humor in model.CharacterSearchSettings.humorlist)
//           {
//               _Humor.Selected = YourHumorValues != null ? YourHumorValues.Contains(_Humor.HumorID) : false;
//           }


//           IEnumerable<int?> EnumerableYourPoliticalView = SelectedYourPoliticalViewIds;

//           var YourPoliticalViewValues = EnumerableYourPoliticalView != null ? new HashSet<int?>(EnumerableYourPoliticalView) : null;

//           foreach (var _PoliticalView in model.CharacterSearchSettings.politicalviewlist)
//           {
//               _PoliticalView.Selected = YourPoliticalViewValues != null ? YourPoliticalViewValues.Contains(_PoliticalView.PoliticalViewID) : false;
//           }



//           // profiledata ProfileDataToUpdate = db.ProfileDatas.Include("profile").Where(p => p.ProfileID == membersmodel.profiledata.ProfileID).First();
//           //   SearchSetting SearchSettingsToUpdate = db.SearchSettings.Where(p => p.ProfileID == _ProfileID && p.MyPerfectMatch == true && p.SearchName == "MyPerfectMatch").First();


//           try
//           {
//               ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;

//               ProfileDataToUpdate.HumorID = Convert.ToInt32(model.CharacterSettings.HumorID);
//               ProfileDataToUpdate.PoliticalViewID = Convert.ToInt32(model.CharacterSettings.PoliticalViewID);
//               ProfileDataToUpdate.EmploymentSatusID = model.CharacterSettings.PoliticalViewID;



//               UpdateSearchSettingsHumor(SelectedYourHumorIds, ProfileDataToUpdate);
//               UpdateSearchSettingsPoliticalView(SelectedYourPoliticalViewIds, ProfileDataToUpdate);



//               //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
//               int changes = db.SaveChanges();

//               //TOD DO
//               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
//               //update session too just in case
//               // membersmodel.profiledata = ProfileDataToUpdate;

//               CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID, ProfileDataToUpdate);


//               model.CurrentErrors.Clear();
//               return model;
//           }
//           catch (DataException)
//           {
//               //Log the error (add a variable name after DataException) 
//               model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
//               return model;
//           }

//       }

//       #endregion
       #endregion

       //TO DO move to search setting repo i think

       #region "checkbox updated functions searchsettings values for all lists"


       //10-3-2012 oawlal made the functuon far more generic so it will work alot better for perefect match or any search setting 
       private void updatesearchsettingsgenders(List<lu_gender> selectedgenders, searchsetting currentsearchsetting)
       {
           if (selectedgenders == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Genders  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_Genders CurrentSearchSettings_Genders = db.SearchSettings_Genders.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedGendersHS = new HashSet<int?>(selectedGenders);
           //get the values for this members searchsettings Genders
           //var SearchSettingsGenders = new HashSet<int?>(currentsearchsetting.genders.Select(c => c.id));
           foreach (var gender in db.lu_gender)
           {
               if (selectedgenders.Contains(gender))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.genders.Any(p => p.id == gender.id))
                   {

                       //SearchSettings_Genders.GendersID = Genders.GendersID;
                       var temp = new searchsetting_gender();
                       temp.id = gender.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_gender.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.genders.Any(p => p.id == gender.id))
                   {
                       var Temp = db.searchsetting_gender.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == gender.id).First();
                       db.searchsetting_gender.Remove(Temp);

                   }
               }
           }
       }

       //show me
       private void updatesearchsettingsshowme(List<lu_showme> selectedshowme, searchsetting currentsearchsetting)
       {
           if (selectedshowme == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_showme  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_showme CurrentSearchSettings_showme = db.SearchSettings_showme.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedshowmeHS = new HashSet<int?>(selectedshowme);
           //get the values for this members searchsettings showme
           //var SearchSettingsshowme = new HashSet<int?>(currentsearchsetting.showme.Select(c => c.id));
           foreach (var showme in db.lu_showme)
           {
               if (selectedshowme.Contains(showme) )
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.showme.Any(p => p.id == showme.id))
                   {

                       //SearchSettings_showme.showmeID = showme.showmeID;
                       var temp = new searchsetting_showme();
                       temp.id = showme.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_showme.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.showme.Any(p => p.id == showme.id))
                   {
                       var temp = db.searchsetting_showme.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == showme.id).First();
                       db.searchsetting_showme.Remove(temp);

                   }
               }
           }
       }
       //sort by
       private void updatesearchsettingssortbytype(List<lu_sortbytype> selectedsortbytype, searchsetting currentsearchsetting)
       {
           if (selectedsortbytype == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_sortbytype  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_sortbytype CurrentSearchSettings_sortbytype = db.SearchSettings_sortbytype.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedsortbytypeHS = new HashSet<int?>(selectedsortbytype);
           //get the values for this members searchsettings sortbytype
           //var SearchSettingssortbytype = new HashSet<int?>(currentsearchsetting.sortbytype.Select(c => c.id));
           foreach (var sortbytype in db.lu_sortbytype)
           {
               if (selectedsortbytype.Contains(sortbytype))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.sortbytypes.Any(p => p.id == sortbytype.id))
                   {

                       //SearchSettings_sortbytype.sortbytypeID = sortbytype.sortbytypeID;
                       var temp = new searchsetting_sortbytype();
                       temp.id = sortbytype.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_sortbytype.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.sortbytypes.Any(p => p.id == sortbytype.id))
                   {
                       var temp = db.searchsetting_sortbytype.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == sortbytype.id).First();
                       db.searchsetting_sortbytype.Remove(temp);

                   }
               }
           }
       }
       //body types
       private void updatesearchsettingsbodytype(List<lu_bodytype> selectedbodytype, searchsetting currentsearchsetting)
       {
           if (selectedbodytype == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_bodytype  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_bodytype CurrentSearchSettings_bodytype = db.SearchSettings_bodytype.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedbodytypeHS = new HashSet<int?>(selectedbodytype);
           //get the values for this members searchsettings bodytype
           //var SearchSettingsbodytype = new HashSet<int?>(currentsearchsetting.bodytype.Select(c => c.id));
           foreach (var bodytype in db.lu_bodytype)
           {
               if (selectedbodytype.Contains(bodytype))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.bodytypes.Any(p => p.id == bodytype.id))
                   {

                       //SearchSettings_bodytype.bodytypeID = bodytype.bodytypeID;
                       var temp = new searchsetting_bodytype();
                       temp.id = bodytype.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_bodytype.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.bodytypes.Any(p => p.id == bodytype.id))
                   {
                       var temp = db.searchsetting_bodytype.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == bodytype.id).First();
                       db.searchsetting_bodytype.Remove(temp);

                   }
               }
           }
       }
       //etnicity
       private void updatesearchsettingsethnicity(List<lu_ethnicity> selectedethnicity, searchsetting currentsearchsetting)
       {
           if (selectedethnicity == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_ethnicity  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_ethnicity CurrentSearchSettings_ethnicity = db.SearchSettings_ethnicity.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedethnicityHS = new HashSet<int?>(selectedethnicity);
           //get the values for this members searchsettings ethnicity
           //var SearchSettingsethnicity = new HashSet<int?>(currentsearchsetting.ethnicity.Select(c => c.id));
           foreach (var ethnicity in db.lu_ethnicity)
           {
               if (selectedethnicity.Contains(ethnicity))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.ethnicitys.Any(p => p.id == ethnicity.id))
                   {

                       //SearchSettings_ethnicity.ethnicityID = ethnicity.ethnicityID;
                       var temp = new searchsetting_ethnicity();
                       temp.id = ethnicity.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_ethnicity.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.ethnicitys.Any(p => p.id == ethnicity.id))
                   {
                       var temp = db.searchsetting_ethnicity.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == ethnicity.id).First();
                       db.searchsetting_ethnicity.Remove(temp);

                   }
               }
           }
       }
       //hair color
       private void updatesearchsettingshaircolor(List<lu_haircolor> selectedhaircolor, searchsetting currentsearchsetting)
       {
           if (selectedhaircolor == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_haircolor  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_haircolor CurrentSearchSettings_haircolor = db.SearchSettings_haircolor.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedhaircolorHS = new HashSet<int?>(selectedhaircolor);
           //get the values for this members searchsettings haircolor
           //var SearchSettingshaircolor = new HashSet<int?>(currentsearchsetting.haircolor.Select(c => c.id));
           foreach (var haircolor in db.lu_haircolor)
           {
               if (selectedhaircolor.Contains(haircolor))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.haircolors.Any(p => p.id == haircolor.id))
                   {

                       //SearchSettings_haircolor.haircolorID = haircolor.haircolorID;
                       var temp = new searchsetting_haircolor();
                       temp.id = haircolor.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_haircolor.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.haircolors.Any(p => p.id == haircolor.id))
                   {
                       var temp = db.searchsetting_haircolor.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == haircolor.id).First();
                       db.searchsetting_haircolor.Remove(temp);

                   }
               }
           }
       }
       //eye color
       private void updatesearchsettingseyecolor(List<lu_eyecolor> selectedeyecolor, searchsetting currentsearchsetting)
       {
           if (selectedeyecolor == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_eyecolor  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_eyecolor CurrentSearchSettings_eyecolor = db.SearchSettings_eyecolor.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedeyecolorHS = new HashSet<int?>(selectedeyecolor);
           //get the values for this members searchsettings eyecolor
           //var SearchSettingseyecolor = new HashSet<int?>(currentsearchsetting.eyecolor.Select(c => c.id));
           foreach (var eyecolor in db.lu_eyecolor)
           {
               if (selectedeyecolor.Contains(eyecolor))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.eyecolors.Any(p => p.id == eyecolor.id))
                   {

                       //SearchSettings_eyecolor.eyecolorID = eyecolor.eyecolorID;
                       var temp = new searchsetting_eyecolor();
                       temp.id = eyecolor.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_eyecolor.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.eyecolors.Any(p => p.id == eyecolor.id))
                   {
                       var temp = db.searchsetting_eyecolor.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == eyecolor.id).First();
                       db.searchsetting_eyecolor.Remove(temp);

                   }
               }
           }
       }
     //hot feature
       private void updatesearchsettingshotfeature(List<lu_hotfeature> selectedhotfeature, searchsetting currentsearchsetting)
       {
           if (selectedhotfeature == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_hotfeature  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_hotfeature CurrentSearchSettings_hotfeature = db.SearchSettings_hotfeature.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedhotfeatureHS = new HashSet<int?>(selectedhotfeature);
           //get the values for this members searchsettings hotfeature
           //var SearchSettingshotfeature = new HashSet<int?>(currentsearchsetting.hotfeature.Select(c => c.id));
           foreach (var hotfeature in db.lu_hotfeature)
           {
               if (selectedhotfeature.Contains(hotfeature))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.hotfeatures.Any(p => p.id == hotfeature.id))
                   {

                       //SearchSettings_hotfeature.hotfeatureID = hotfeature.hotfeatureID;
                       var temp = new searchsetting_hotfeature();
                       temp.id = hotfeature.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_hotfeature.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.hotfeatures.Any(p => p.id == hotfeature.id))
                   {
                       var temp = db.searchsetting_hotfeature.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == hotfeature.id).First();
                       db.searchsetting_hotfeature.Remove(temp);

                   }
               }
           }
       }
       //diet
       private void updatesearchsettingsdiet(List<lu_diet> selecteddiet, searchsetting currentsearchsetting)
       {
           if (selecteddiet == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_diet  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_diet CurrentSearchSettings_diet = db.SearchSettings_diet.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selecteddietHS = new HashSet<int?>(selecteddiet);
           //get the values for this members searchsettings diet
           //var SearchSettingsdiet = new HashSet<int?>(currentsearchsetting.diet.Select(c => c.id));
           foreach (var diet in db.lu_diet)
           {
               if (selecteddiet.Contains(diet))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.diets.Any(p => p.id == diet.id))
                   {

                       //SearchSettings_diet.dietID = diet.dietID;
                       var temp = new searchsetting_diet();
                       temp.id = diet.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_diet.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.diets.Any(p => p.id == diet.id))
                   {
                       var temp = db.searchsetting_diet.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == diet.id).First();
                       db.searchsetting_diet.Remove(temp);

                   }
               }
           }
       }
       //drinks
       private void updatesearchsettingsdrinks(List<lu_drinks> selecteddrinks, searchsetting currentsearchsetting)
       {
           if (selecteddrinks == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_drinks  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_drinks CurrentSearchSettings_drinks = db.SearchSettings_drinks.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selecteddrinksHS = new HashSet<int?>(selecteddrinks);
           //get the values for this members searchsettings drinks
           //var SearchSettingsdrinks = new HashSet<int?>(currentsearchsetting.drinks.Select(c => c.id));
           foreach (var drinks in db.lu_drinks)
           {
               if (selecteddrinks.Contains(drinks))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.drinks.Any(p => p.id == drinks.id))
                   {

                       //SearchSettings_drinks.drinksID = drinks.drinksID;
                       var temp = new searchsetting_drink();
                       temp.id = drinks.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_drink.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.drinks.Any(p => p.id == drinks.id))
                   {
                       var temp = db.searchsetting_drink.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == drinks.id).First();
                       db.searchsetting_drink.Remove(temp);

                   }
               }
           }
       }
       //excerize
       private void updatesearchsettingsexercise(List<lu_exercise> selectedexercise, searchsetting currentsearchsetting)
       {
           if (selectedexercise == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_exercise  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_exercise CurrentSearchSettings_exercise = db.SearchSettings_exercise.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedexerciseHS = new HashSet<int?>(selectedexercise);
           //get the values for this members searchsettings exercise
           //var SearchSettingsexercise = new HashSet<int?>(currentsearchsetting.exercise.Select(c => c.id));
           foreach (var exercise in db.lu_exercise)
           {
               if (selectedexercise.Contains(exercise))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.exercises.Any(p => p.id == exercise.id))
                   {

                       //SearchSettings_exercise.exerciseID = exercise.exerciseID;
                       var temp = new searchsetting_exercise();
                       temp.id = exercise.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_exercise.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.exercises.Any(p => p.id == exercise.id))
                   {
                       var temp = db.searchsetting_exercise.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == exercise.id).First();
                       db.searchsetting_exercise.Remove(temp);

                   }
               }
           }
       }
       //hobby
       private void updatesearchsettingshobby(List<lu_hobby> selectedhobby, searchsetting currentsearchsetting)
       {
           if (selectedhobby == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_hobby  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_hobby CurrentSearchSettings_hobby = db.SearchSettings_hobby.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedhobbyHS = new HashSet<int?>(selectedhobby);
           //get the values for this members searchsettings hobby
           //var SearchSettingshobby = new HashSet<int?>(currentsearchsetting.hobby.Select(c => c.id));
           foreach (var hobby in db.lu_hobby)
           {
               if (selectedhobby.Contains(hobby))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.hobbies.Any(p => p.id == hobby.id))
                   {

                       //SearchSettings_hobby.hobbyID = hobby.hobbyID;
                       var temp = new searchsetting_hobby();
                       temp.id = hobby.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_hobby.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.hobbies.Any(p => p.id == hobby.id))
                   {
                       var temp = db.searchsetting_hobby.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == hobby.id).First();
                       db.searchsetting_hobby.Remove(temp);

                   }
               }
           }
       }
       //humor
       private void updatesearchsettingshumor(List<lu_humor> selectedhumor, searchsetting currentsearchsetting)
       {
           if (selectedhumor == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_humor  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_humor CurrentSearchSettings_humor = db.SearchSettings_humor.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedhumorHS = new HashSet<int?>(selectedhumor);
           //get the values for this members searchsettings humor
           //var SearchSettingshumor = new HashSet<int?>(currentsearchsetting.humor.Select(c => c.id));
           foreach (var humor in db.lu_humor)
           {
               if (selectedhumor.Contains(humor))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.humors.Any(p => p.id == humor.id))
                   {

                       //SearchSettings_humor.humorID = humor.humorID;
                       var temp = new searchsetting_humor();
                       temp.id = humor.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_humor.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.humors.Any(p => p.id == humor.id))
                   {
                       var temp = db.searchsetting_humor.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == humor.id).First();
                       db.searchsetting_humor.Remove(temp);

                   }
               }
           }
       }
       //political view
       private void updatesearchsettingspoliticalview(List<lu_politicalview> selectedpoliticalview, searchsetting currentsearchsetting)
       {
           if (selectedpoliticalview == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_politicalview  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_politicalview CurrentSearchSettings_politicalview = db.SearchSettings_politicalview.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedpoliticalviewHS = new HashSet<int?>(selectedpoliticalview);
           //get the values for this members searchsettings politicalview
           //var SearchSettingspoliticalview = new HashSet<int?>(currentsearchsetting.politicalview.Select(c => c.id));
           foreach (var politicalview in db.lu_politicalview)
           {
               if (selectedpoliticalview.Contains(politicalview))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.politicalviews.Any(p => p.id == politicalview.id))
                   {

                       //SearchSettings_politicalview.politicalviewID = politicalview.politicalviewID;
                       var temp = new searchsetting_politicalview();
                       temp.id = politicalview.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_politicalview.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.politicalviews.Any(p => p.id == politicalview.id))
                   {
                       var temp = db.searchsetting_politicalview.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == politicalview.id).First();
                       db.searchsetting_politicalview.Remove(temp);

                   }
               }
           }
       }
       //relegion
       private void updatesearchsettingsreligion(List<lu_religion> selectedreligion, searchsetting currentsearchsetting)
       {
           if (selectedreligion == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_religion  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_religion CurrentSearchSettings_religion = db.SearchSettings_religion.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedreligionHS = new HashSet<int?>(selectedreligion);
           //get the values for this members searchsettings religion
           //var SearchSettingsreligion = new HashSet<int?>(currentsearchsetting.religion.Select(c => c.id));
           foreach (var religion in db.lu_religion)
           {
               if (selectedreligion.Contains(religion))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.religions.Any(p => p.id == religion.id))
                   {

                       //SearchSettings_religion.religionID = religion.religionID;
                       var temp = new searchsetting_religion();
                       temp.id = religion.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_religion.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.religions.Any(p => p.id == religion.id))
                   {
                       var temp = db.searchsetting_religion.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == religion.id).First();
                       db.searchsetting_religion.Remove(temp);

                   }
               }
           }
       }
       //relegious attendance
       private void updatesearchsettingsreligiousattendance(List<lu_religiousattendance> selectedreligiousattendance, searchsetting currentsearchsetting)
       {
           if (selectedreligiousattendance == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_religiousattendance  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_religiousattendance CurrentSearchSettings_religiousattendance = db.SearchSettings_religiousattendance.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedreligiousattendanceHS = new HashSet<int?>(selectedreligiousattendance);
           //get the values for this members searchsettings religiousattendance
           //var SearchSettingsreligiousattendance = new HashSet<int?>(currentsearchsetting.religiousattendance.Select(c => c.id));
           foreach (var religiousattendance in db.lu_religiousattendance)
           {
               if (selectedreligiousattendance.Contains(religiousattendance))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.religiousattendances.Any(p => p.id == religiousattendance.id))
                   {

                       //SearchSettings_religiousattendance.religiousattendanceID = religiousattendance.religiousattendanceID;
                       var temp = new searchsetting_religiousattendance();
                       temp.id = religiousattendance.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_religiousattendance.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.religiousattendances.Any(p => p.id == religiousattendance.id))
                   {
                       var temp = db.searchsetting_religiousattendance.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == religiousattendance.id).First();
                       db.searchsetting_religiousattendance.Remove(temp);

                   }
               }
           }
       }
       //sign
       private void updatesearchsettingssign(List<lu_sign> selectedsign, searchsetting currentsearchsetting)
       {
           if (selectedsign == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_sign  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_sign CurrentSearchSettings_sign = db.SearchSettings_sign.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedsignHS = new HashSet<int?>(selectedsign);
           //get the values for this members searchsettings sign
           //var SearchSettingssign = new HashSet<int?>(currentsearchsetting.sign.Select(c => c.id));
           foreach (var sign in db.lu_sign)
           {
               if (selectedsign.Contains(sign))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.signs.Any(p => p.id == sign.id))
                   {

                       //SearchSettings_sign.signID = sign.signID;
                       var temp = new searchsetting_sign();
                       temp.id = sign.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_sign.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.signs.Any(p => p.id == sign.id))
                   {
                       var temp = db.searchsetting_sign.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == sign.id).First();
                       db.searchsetting_sign.Remove(temp);

                   }
               }
           }
       }
       //smokes
       private void updatesearchsettingssmokes(List<lu_smokes> selectedsmokes, searchsetting currentsearchsetting)
       {
           if (selectedsmokes == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_smokes  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_smokes CurrentSearchSettings_smokes = db.SearchSettings_smokes.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedsmokesHS = new HashSet<int?>(selectedsmokes);
           //get the values for this members searchsettings smokes
           //var SearchSettingssmokes = new HashSet<int?>(currentsearchsetting.smokes.Select(c => c.id));
           foreach (var smokes in db.lu_smokes)
           {
               if (selectedsmokes.Contains(smokes))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.smokes.Any(p => p.id == smokes.id))
                   {

                       //SearchSettings_smokes.smokesID = smokes.smokesID;
                       var temp = new searchsetting_smokes();
                       temp.id = smokes.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_smokes.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.smokes.Any(p => p.id == smokes.id))
                   {
                       var temp = db.searchsetting_smokes.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == smokes.id).First();
                       db.searchsetting_smokes.Remove(temp);

                   }
               }
           }
       }
       //education level
       private void updatesearchsettingseducationlevel(List<lu_educationlevel> selectededucationlevel, searchsetting currentsearchsetting)
       {
           if (selectededucationlevel == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_educationlevel  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_educationlevel CurrentSearchSettings_educationlevel = db.SearchSettings_educationlevel.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectededucationlevelHS = new HashSet<int?>(selectededucationlevel);
           //get the values for this members searchsettings educationlevel
           //var SearchSettingseducationlevel = new HashSet<int?>(currentsearchsetting.educationlevel.Select(c => c.id));
           foreach (var educationlevel in db.lu_educationlevel)
           {
               if (selectededucationlevel.Contains(educationlevel))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.educationlevels.Any(p => p.id == educationlevel.id))
                   {

                       //SearchSettings_educationlevel.educationlevelID = educationlevel.educationlevelID;
                       var temp = new searchsetting_educationlevel();
                       temp.id = educationlevel.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_educationlevel.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.educationlevels.Any(p => p.id == educationlevel.id))
                   {
                       var temp = db.searchsetting_educationlevel.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == educationlevel.id).First();
                       db.searchsetting_educationlevel.Remove(temp);

                   }
               }
           }
       }
       //employment status
       private void updatesearchsettingsemploymentstatus(List<lu_employmentstatus> selectedemploymentstatus, searchsetting currentsearchsetting)
       {
           if (selectedemploymentstatus == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_employmentstatus  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_employmentstatus CurrentSearchSettings_employmentstatus = db.SearchSettings_employmentstatus.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedemploymentstatusHS = new HashSet<int?>(selectedemploymentstatus);
           //get the values for this members searchsettings employmentstatus
           //var SearchSettingsemploymentstatus = new HashSet<int?>(currentsearchsetting.employmentstatus.Select(c => c.id));
           foreach (var employmentstatus in db.lu_employmentstatus)
           {
               if (selectedemploymentstatus.Contains(employmentstatus))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.employmentstatus.Any(p => p.id == employmentstatus.id))
                   {

                       //SearchSettings_employmentstatus.employmentstatusID = employmentstatus.employmentstatusID;
                       var temp = new searchsetting_employmentstatus();
                       temp.id = employmentstatus.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_employmentstatus.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.employmentstatus.Any(p => p.id == employmentstatus.id))
                   {
                       var temp = db.searchsetting_employmentstatus.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == employmentstatus.id).First();
                       db.searchsetting_employmentstatus.Remove(temp);

                   }
               }
           }
       }
       //have kids
       private void updatesearchsettingshavekids(List<lu_havekids> selectedhavekids, searchsetting currentsearchsetting)
       {
           if (selectedhavekids == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_havekids  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_havekids CurrentSearchSettings_havekids = db.SearchSettings_havekids.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedhavekidsHS = new HashSet<int?>(selectedhavekids);
           //get the values for this members searchsettings havekids
           //var SearchSettingshavekids = new HashSet<int?>(currentsearchsetting.havekids.Select(c => c.id));
           foreach (var havekids in db.lu_havekids)
           {
               if (selectedhavekids.Contains(havekids))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.havekids.Any(p => p.id == havekids.id))
                   {

                       //SearchSettings_havekids.havekidsID = havekids.havekidsID;
                       var temp = new searchsetting_havekids();
                       temp.id = havekids.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_havekids.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.havekids.Any(p => p.id == havekids.id))
                   {
                       var temp = db.searchsetting_havekids.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == havekids.id).First();
                       db.searchsetting_havekids.Remove(temp);

                   }
               }
           }
       }
       //income level
       private void updatesearchsettingsincomelevel(List<lu_incomelevel> selectedincomelevel, searchsetting currentsearchsetting)
       {
           if (selectedincomelevel == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_incomelevel  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_incomelevel CurrentSearchSettings_incomelevel = db.SearchSettings_incomelevel.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedincomelevelHS = new HashSet<int?>(selectedincomelevel);
           //get the values for this members searchsettings incomelevel
           //var SearchSettingsincomelevel = new HashSet<int?>(currentsearchsetting.incomelevel.Select(c => c.id));
           foreach (var incomelevel in db.lu_incomelevel)
           {
               if (selectedincomelevel.Contains(incomelevel))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.incomelevels.Any(p => p.id == incomelevel.id))
                   {

                       //SearchSettings_incomelevel.incomelevelID = incomelevel.incomelevelID;
                       var temp = new searchsetting_incomelevel();
                       temp.id = incomelevel.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_incomelevel.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.incomelevels.Any(p => p.id == incomelevel.id))
                   {
                       var temp = db.searchsetting_incomelevel.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == incomelevel.id).First();
                       db.searchsetting_incomelevel.Remove(temp);

                   }
               }
           }
       }
       //living situation
       private void updatesearchsettingslivingsituation(List<lu_livingsituation> selectedlivingsituation, searchsetting currentsearchsetting)
       {
           if (selectedlivingsituation == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_livingsituation  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_livingsituation CurrentSearchSettings_livingsituation = db.SearchSettings_livingsituation.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedlivingsituationHS = new HashSet<int?>(selectedlivingsituation);
           //get the values for this members searchsettings livingsituation
           //var SearchSettingslivingsituation = new HashSet<int?>(currentsearchsetting.livingsituation.Select(c => c.id));
           foreach (var livingsituation in db.lu_livingsituation)
           {
               if (selectedlivingsituation.Contains(livingsituation))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.livingstituations.Any(p => p.id == livingsituation.id))
                   {

                       //SearchSettings_livingsituation.livingsituationID = livingsituation.livingsituationID;
                       var temp = new searchsetting_livingstituation ();
                       temp.id = livingsituation.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_livingstituation.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.livingstituations.Any(p => p.id == livingsituation.id))
                   {
                       var temp = db.searchsetting_livingstituation.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == livingsituation.id).First();
                       db.searchsetting_livingstituation .Remove(temp);

                   }
               }
           }
       }
       //lookingfor
       private void updatesearchsettingslookingfor(List<lu_lookingfor> selectedlookingfor, searchsetting currentsearchsetting)
       {
           if (selectedlookingfor == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_lookingfor  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_lookingfor CurrentSearchSettings_lookingfor = db.SearchSettings_lookingfor.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedlookingforHS = new HashSet<int?>(selectedlookingfor);
           //get the values for this members searchsettings lookingfor
           //var SearchSettingslookingfor = new HashSet<int?>(currentsearchsetting.lookingfor.Select(c => c.id));
           foreach (var lookingfor in db.lu_lookingfor)
           {
               if (selectedlookingfor.Contains(lookingfor))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.lookingfor.Any(p => p.id == lookingfor.id))
                   {

                       //SearchSettings_lookingfor.lookingforID = lookingfor.lookingforID;
                       var temp = new searchsetting_lookingfor();
                       temp.id = lookingfor.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_lookingfor.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.lookingfor.Any(p => p.id == lookingfor.id))
                   {
                       var temp = db.searchsetting_lookingfor.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == lookingfor.id).First();
                       db.searchsetting_lookingfor.Remove(temp);

                   }
               }
           }
       }
       //maritial status
       private void updatesearchsettingsmaritalstatus(List<lu_maritalstatus> selectedmaritalstatus, searchsetting currentsearchsetting)
       {
           if (selectedmaritalstatus == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_maritalstatus  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_maritalstatus CurrentSearchSettings_maritalstatus = db.SearchSettings_maritalstatus.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedmaritalstatusHS = new HashSet<int?>(selectedmaritalstatus);
           //get the values for this members searchsettings maritalstatus
           //var SearchSettingsmaritalstatus = new HashSet<int?>(currentsearchsetting.maritalstatus.Select(c => c.id));
           foreach (var maritalstatus in db.lu_maritalstatus)
           {
               if (selectedmaritalstatus.Contains(maritalstatus))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.maritalstatuses.Any(p => p.id == maritalstatus.id))
                   {

                       //SearchSettings_maritalstatus.maritalstatusID = maritalstatus.maritalstatusID;
                       var temp = new searchsetting_maritalstatus();
                       temp.id = maritalstatus.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_maritalstatus.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.maritalstatuses.Any(p => p.id == maritalstatus.id))
                   {
                       var temp = db.searchsetting_maritalstatus.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == maritalstatus.id).First();
                       db.searchsetting_maritalstatus.Remove(temp);

                   }
               }
           }
       }
       //profession
       private void updatesearchsettingsprofession(List<lu_profession> selectedprofession, searchsetting currentsearchsetting)
       {
           if (selectedprofession == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_profession  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_profession CurrentSearchSettings_profession = db.SearchSettings_profession.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedprofessionHS = new HashSet<int?>(selectedprofession);
           //get the values for this members searchsettings profession
           //var SearchSettingsprofession = new HashSet<int?>(currentsearchsetting.profession.Select(c => c.id));
           foreach (var profession in db.lu_profession)
           {
               if (selectedprofession.Contains(profession))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.professions.Any(p => p.id == profession.id))
                   {

                       //SearchSettings_profession.professionID = profession.professionID;
                       var temp = new searchsetting_profession();
                       temp.id = profession.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_profession.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.professions.Any(p => p.id == profession.id))
                   {
                       var temp = db.searchsetting_profession.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == profession.id).First();
                       db.searchsetting_profession.Remove(temp);

                   }
               }
           }
       }
       //wants kids
       private void updatesearchsettingswantskids(List<lu_wantskids> selectedwantskids, searchsetting currentsearchsetting)
       {
           if (selectedwantskids == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_wantskids  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_wantskids CurrentSearchSettings_wantskids = db.SearchSettings_wantskids.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedwantskidsHS = new HashSet<int?>(selectedwantskids);
           //get the values for this members searchsettings wantskids
           //var SearchSettingswantskids = new HashSet<int?>(currentsearchsetting.wantskids.Select(c => c.id));
           foreach (var wantskids in db.lu_wantskids)
           {
               if (selectedwantskids.Contains(wantskids))
               {
                   //does not exist so we will add it
                   if (!currentsearchsetting.wantkids.Any(p => p.id == wantskids.id))
                   {

                       //SearchSettings_wantskids.wantskidsID = wantskids.wantskidsID;
                       var temp = new searchsetting_wantkids();
                       temp.id = wantskids.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_wantkids. Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentsearchsetting.wantkids.Any(p => p.id == wantskids.id))
                   {
                       var temp = db.searchsetting_wantkids.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == wantskids.id).First();
                       db.searchsetting_wantkids.Remove(temp);

                   }
               }
           }
       }
       #endregion

       #region "Checkbox Update Functions for profiledata many to many"


       //profiledata ethnicity
       private void updatprofilemetatdataethnicity(List<lu_ethnicity> selectedethnicity, profilemetadata currentprofilemetadata)
       {
           if (selectedethnicity == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_showme  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_showme CurrentSearchSettings_showme = db.SearchSettings_showme.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedshowmeHS = new HashSet<int?>(selectedshowme);
           //get the values for this members searchsettings showme
           //var SearchSettingsshowme = new HashSet<int?>(currentsearchsetting.showme.Select(c => c.id));
           foreach (var ethnicity in db.lu_ethnicity)
           {
               if (selectedethnicity.Contains(ethnicity))
               {
                   //does not exist so we will add it
                   if (!currentprofilemetadata.ethnicities.Any(p => p.id == ethnicity.id))
                   {

                       //SearchSettings_showme.showmeID = showme.showmeID;
                       var temp = new profiledata_ethnicity();
                       temp.id = ethnicity.id;
                       temp.profile_id = currentprofilemetadata.profile_id ;
                       db.ethnicities.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentprofilemetadata.ethnicities.Any(p => p.id == ethnicity.id))
                   {
                       var temp = db.ethnicities.Where(p => p.profile_id == currentprofilemetadata.profile_id  && p.ethnicty.id == ethnicity.id).First();
                       db.ethnicities.Remove(temp);

                   }
               }
           }
       }
       //profiledata hotfeature
       private void updatprofilemetatdatahotfeature(List<lu_hotfeature> selectedhotfeature, profilemetadata currentprofilemetadata)
       {
           if (selectedhotfeature == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_showme  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_showme CurrentSearchSettings_showme = db.SearchSettings_showme.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedshowmeHS = new HashSet<int?>(selectedshowme);
           //get the values for this members searchsettings showme
           //var SearchSettingsshowme = new HashSet<int?>(currentsearchsetting.showme.Select(c => c.id));
           foreach (var hotfeature in db.lu_hotfeature)
           {
               if (selectedhotfeature.Contains(hotfeature))
               {
                   //does not exist so we will add it
                   if (!currentprofilemetadata.ethnicities.Any(p => p.id == hotfeature.id))
                   {

                       //SearchSettings_showme.showmeID = showme.showmeID;
                       var temp = new profiledata_hotfeature();
                       temp.id = hotfeature.id;
                       temp.profile_id =currentprofilemetadata.profile_id;
                       db.hotfeatures.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentprofilemetadata.hotfeatures.Any(p => p.id == hotfeature.id))
                   {
                       var temp = db.hotfeatures.Where(p => p.profile_id == currentprofilemetadata.profile_id  && p.hotfeature.id == hotfeature.id).First();
                       db.hotfeatures.Remove(temp);

                   }
               }
           }
       }
       //profiledata hobby
       private void updatprofilemetatdatahobby(List<lu_hobby> selectedhobby, profilemetadata currentprofilemetadata)
       {
           if (selectedhobby == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_showme  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_showme CurrentSearchSettings_showme = db.SearchSettings_showme.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedshowmeHS = new HashSet<int?>(selectedshowme);
           //get the values for this members searchsettings showme
           //var SearchSettingsshowme = new HashSet<int?>(currentsearchsetting.showme.Select(c => c.id));
           foreach (var hobby in db.lu_hobby)
           {
               if (selectedhobby.Contains(hobby))
               {
                   //does not exist so we will add it
                   if (!currentprofilemetadata.ethnicities.Any(p => p.id == hobby.id))
                   {

                       //SearchSettings_showme.showmeID = showme.showmeID;
                       var temp = new profiledata_hobby();
                       temp.id = hobby.id;
                       temp.profile_id =currentprofilemetadata.profile_id;
                       db.hobbies .Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentprofilemetadata.hobbies.Any(p => p.id == hobby.id))
                   {
                       var temp = db.hobbies.Where(p => p.profile_id ==currentprofilemetadata.profile_id && p.hobby.id == hobby.id).First();
                       db.hobbies.Remove(temp);

                   }
               }
           }
       }
       //profiledata lookingfor
       private void updatprofilemetatdatalookingfor(List<lu_lookingfor> selectedlookingfor, profilemetadata currentprofilemetadata)
       {
           if (selectedlookingfor == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_showme  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           // int SearchSettingsID = currentsearchsetting.id;//ProfileDataToUpdate.searchsettings.FirstOrDefault().id;
           //SearchSettings_showme CurrentSearchSettings_showme = db.SearchSettings_showme.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           // var selectedshowmeHS = new HashSet<int?>(selectedshowme);
           //get the values for this members searchsettings showme
           //var SearchSettingsshowme = new HashSet<int?>(currentsearchsetting.showme.Select(c => c.id));
           foreach (var lookingfor in db.lu_lookingfor)
           {
               if (selectedlookingfor.Contains(lookingfor))
               {
                   //does not exist so we will add it
                   if (!currentprofilemetadata.ethnicities.Any(p => p.id == lookingfor.id))
                   {

                       //SearchSettings_showme.showmeID = showme.showmeID;
                       var temp = new profiledata_lookingfor();
                       temp.id = lookingfor.id;
                       temp.profile_id =currentprofilemetadata.profile_id;
                       db.lookingfor.Add(temp);

                   }
               }
               else
               { //exists means we want to remove it
                   if (currentprofilemetadata.lookingfor.Any(p => p.id == lookingfor.id))
                   {
                       var temp = db.lookingfor.Where(p => p.profile_id ==currentprofilemetadata.profile_id && p.lookingfor.id == lookingfor.id).First();
                       db.lookingfor.Remove(temp);

                   }
               }
           }
       }

       #endregion

       

      
       
    }
}
