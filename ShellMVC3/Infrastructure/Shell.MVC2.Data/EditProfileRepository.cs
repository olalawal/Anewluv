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
   public  class EditProfileRepository : MemberRepositoryBase , IMemberRepository 
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

        public EditProfileRepository(AnewluvContext datingcontext, IMemberRepository membersrepository)
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
                        if (p.profilemetadata.profiledata.gender.id == 1)
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
                    var hotfeaturevalues = new HashSet<int>(p.hotfeature.Select(c => c.id.GetValueOrDefault()));
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
           newmodel = EditProfileBasicSettingsPage2Update(newmodel, profile);
          return newmodel;
       }
       private BasicSettingsViewModel EditProfileBasicSettingsPage1Update(BasicSettingsViewModel newmodel, profile profile, searchsetting oldsearchsettings)
       {

           // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

         

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
           //update gender values as well 
           //IEnumerable<int?> myEnumerable = SelectedGenderIds;

          // var GenderValues = myEnumerable != null ? new HashSet<int?>(myEnumerable) : null;

         //  foreach (var _Gender in  newGendersValues) //model.BasicSearchSettings.genderslist)
          // {
          //     _Gender.selected = GendersValues != null ? GendersValues.Contains(e=>e.id ==  _Gender.id) : false;
          // }

           //TO DO move this to client side validaton
           //now validate and update the page the page we are on
          // if (GendersValues == null)
         //  {
               //  ModelState.AddModelError("selectedId", "You have to select at least one gender!");
               //add errors to model
          //     model.CurrentErrors.Add("You have to select at least one gender!");
          //     return model;
        //   }



           //var ModelToUpdate = db.ProfileDatas
           //   .Where(i => i.ProfileID == membersmodel.profiledata.ProfileID)
           // .Select(p => new
           // {
           //     profiledata = p,
           //     SearchSettings = p.SearchSettings.Where(i => i.MyPerfectMatch == true).FirstOrDefault()
           // }).SingleOrDefault();

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
               this.UpdateSearchSettingsGenders(newmodel.genderslist.ToList(), oldsearchsettings);
               //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
               db.SaveChanges();
               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case

               //  CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID(_ProfileID, ProfileDataToUpdate);
               // model.CurrentErrors.Clear();
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
       private BasicSettingsViewModel EditProfileBasicSettingsPage2Update(BasicSettingsViewModel newmodel, profile profile)
       {



        

           // MembersViewModel membersmodel = GetMembersViewModelAddSearchSettingsAndUpdate(_ProfileID);

           var DistanceFromMe = model.BasicSearchSettings.DistanceFromMe;
           //re populate the models
           //build Basic Profile Settings from Submited view 
           // model.BasicProfileSettings. = AboutMe;
           model.BasicProfileSettings = new EditProfileBasicSettingsModel(ProfileDataToUpdate);

           //reload search settings since it seems the checkbox values are lost on postback
           //we really should just rebuild them from form collection imo
           model.BasicSearchSettings = new SearchModelBasicSettings(SearchSettingsToUpdate);
           //update the searchmodl settings with current settings on the UI
           model.BasicSearchSettings.DistanceFromMe = DistanceFromMe;


           //update show me and sortby with correct values from UI as well
           //this code is just for serv side validation does nothing atm

           //showme next
           IEnumerable<int?> myEnumerableShowmes = SelectedShowMeIds;

           var ShowMeTypeValues = myEnumerableShowmes != null ? new HashSet<int?>(myEnumerableShowmes) : null;

           foreach (var _ShowMeType in model.BasicSearchSettings.showmelist)
           {
               _ShowMeType.Selected = ShowMeTypeValues != null ? ShowMeTypeValues.Contains(_ShowMeType.ShowMeID) : false;
           }


           IEnumerable<int?> myEnumerableSortBys = SelectedSortByIds;

           var SortByTypeValues = myEnumerableSortBys != null ? new HashSet<int?>(myEnumerableSortBys) : null;

           foreach (var _SortByType in model.BasicSearchSettings.SortByList)
           {
               _SortByType.Selected = SortByTypeValues != null ? SortByTypeValues.Contains(_SortByType.SortByTypeID) : false;
           }




           try
           {
               //link the profiledata entities
               ProfileDataToUpdate.profile.ModificationDate = DateTime.Now;

               ////Add searchh settings as well if its not null
               //if (ModelToUpdate.SearchSettings == null)
               //{
               //    SearchSetting NewSearchSettings = new SearchSetting();
               //    ProfileDataToUpdate.SearchSettings.Add(NewSearchSettings);
               //}
               //else
               //{
               //    ProfileDataToUpdate.SearchSettings.Add(ModelToUpdate.SearchSettings);
               //}

               //detrmine if we are in edit or add mode for search settings for perfect match
               //if its null add a new entity  
               //noew update searchsettings text values
               SearchSettingsToUpdate.DistanceFromMe = model.BasicSearchSettings.DistanceFromMe;


               //TO DO move this code to searchssettings Repositoury
               UpdateSearchSettingsShowMe(SelectedShowMeIds, ProfileDataToUpdate);
               UpdateSearchSettingsSortByTypes(SelectedSortByIds, ProfileDataToUpdate);
               SearchSettingsToUpdate.LastUpdateDate = DateTime.Now;

               //db.Entry(ProfileDataToUpdate).State = EntityState.Modified;
               db.SaveChanges();

               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case
               //  membersmodel.profiledata = ProfileDataToUpdate;
               //  CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID,ProfileDataToUpdate  );

               model.CurrentErrors.Clear();
               return model;
           }
           catch (DataException)
           {
               //Log the error (add a variable name after DataException) 
               model.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               return model;
           }

       }
       #endregion

       //TO DO move to search setting repo i think

       #region "checkbox updated functions searchsettings values for all lists"


       //10-3-2012 oawlal made the functuon far more generic so it will work alot better for perefect match or any search setting 
       private void UpdateSearchSettingsGenders(List<lu_gender> selectedgenders, searchsetting currentsearchsetting)
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
       private void UpdateSearchSettingsShowMe(int?[] selectedShowMe, profilemetadata ProfileDataToUpdate)
       {
           if (selectedShowMe == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_ShowMe  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_ShowMe CurrentSearchSettings_ShowMe = db.SearchSettings_ShowMe.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedShowMeHS = new HashSet<int?>(selectedShowMe);
           //get the values for this members searchsettings ShowMe
           var SearchSettingsShowMe = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_ShowMe.Select(c => c.ShowMeID));
           foreach (var ShowMe in db.ShowMes)
           {
               if (selectedShowMeHS.Contains(ShowMe.ShowMeID))
               {
                   if (!SearchSettingsShowMe.Contains(ShowMe.ShowMeID))
                   {

                       //SearchSettings_ShowMe.ShowMeID = ShowMe.ShowMeID;
                       var temp = new SearchSettings_ShowMe();
                       temp.ShowMeID = ShowMe.ShowMeID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_ShowMe", temp);

                   }
               }
               else
               {
                   if (SearchSettingsShowMe.Contains(ShowMe.ShowMeID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_ShowMe.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //sort by
       private void UpdateSearchSettingsSortByTypes(int?[] selectedSortBy, profilemetadata ProfileDataToUpdate)
       {
           if (selectedSortBy == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_SortBy  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_SortBy CurrentSearchSettings_SortBy = db.SearchSettings_SortBy.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedSortByHS = new HashSet<int?>(selectedSortBy);
           //get the values for this members searchsettings SortBy
           var SearchSettingsSortBy = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_SortByType.Select(c => c.SortByTypeID));
           foreach (var SortBy in db.SortByTypes)
           {
               if (selectedSortByHS.Contains(SortBy.SortByTypeID))
               {
                   if (!SearchSettingsSortBy.Contains(SortBy.SortByTypeID))
                   {

                       //SearchSettings_SortBy.SortByID = SortBy.SortByID;
                       var temp = new SearchSettings_SortByType();
                       temp.SortByTypeID = SortBy.SortByTypeID;
                       temp.SearchSettingsID = SearchSettingsID;
                       //  var dd =   db.ProfileDatas.Where(p=>p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p=>p.SearchSettingsID == SearchSettingsID).First().SearchSettings_SortByType.First();

                       db.AddObject("SearchSettings_SortByType", temp);
                   }
               }
               else
               {
                   if (SearchSettingsSortBy.Contains(SortBy.SortByTypeID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_SortByType.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //body types
       private void UpdateSearchSettingsBodyTypes(int?[] selectedBodyTypes, profilemetadata ProfileDataToUpdate)
       {
           if (selectedBodyTypes == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_BodyTypes  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_BodyTypes CurrentSearchSettings_BodyTypes = db.SearchSettings_BodyTypes.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedBodyTypesHS = new HashSet<int?>(selectedBodyTypes);
           //get the values for this members searchsettings BodyTypes
           var SearchSettingsBodyTypes = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_BodyTypes.Select(c => c.BodyTypesID));
           foreach (var BodyTypes in db.CriteriaAppearance_Bodytypes)
           {
               if (selectedBodyTypesHS.Contains(BodyTypes.BodyTypesID))
               {
                   if (!SearchSettingsBodyTypes.Contains(BodyTypes.BodyTypesID))
                   {

                       //SearchSettings_BodyTypes.BodyTypesID = BodyTypes.BodyTypesID;
                       var temp = new SearchSettings_BodyTypes();
                       temp.BodyTypesID = BodyTypes.BodyTypesID;
                       temp.SearchSettingsID = SearchSettingsID;
                       //  temp.SearchSettings_BodyTypeID = BodyTypes.SearchSettings_BodyTypeID;
                       // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Ethnicity.Add(SearchSettings_BodyTypes);
                       // SearchSettingsGender.SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
                       //var dd =   db.ProfileDatas.Where(p=>p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p=>p.SearchSettingsID == SearchSettingsID).First().SearchSettings_BodyTypes.First();

                       db.AddObject("SearchSettings_BodyTypes", temp);


                       // You do not have to call the Load method to load the details for the order,
                       // because  lazy loading is set to true 
                       // by the constructor of the AdventureWorksEntities object. 
                       // With  lazy loading set to true the related objects are loaded when
                       // you access the navigation property. In this case SalesOrderDetails.

                       // Delete the first item in the order.




                   }
               }
               else
               {
                   if (SearchSettingsBodyTypes.Contains(BodyTypes.BodyTypesID))
                   {
                       //SearchSettings_BodyTypes.BodyTypesID  = BodyTypes.BodyTypesID;
                       // var temp = new SearchSettings_BodyTypes();
                       // temp.BodyTypesID = BodyTypes.BodyTypesID;
                       //temp.SearchSettingsID = BodyTypes.SearchSettingsID;
                       //temp.SearchSettings_BodyTypeID = BodyTypes.SearchSettings_BodyTypeID;
                       // temp.SearchSettings_BodyTypeID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_BodyTypes.Where(p => p.SearchSettingsID == 18 && p.BodyTypesID == BodyTypes.BodyTypesID).FirstOrDefault();
                       // temp.SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
                       //  var CurrentBodyType =  db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).SingleOrDefault().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).SingleOrDefault().SearchSettings_BodyTypes.Remove(temp);

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_BodyTypes.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //etnicity
       private void UpdateSearchSettingsEthnicity(int?[] selectedEthnicity, profilemetadata ProfileDataToUpdate)
       {
           if (selectedEthnicity == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Ethnicity  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_Ethnicity CurrentSearchSettings_Ethnicity = db.SearchSettings_Ethnicity.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedEthnicityHS = new HashSet<int?>(selectedEthnicity);
           //get the values for this members searchsettings Ethnicity
           var SearchSettingsEthnicity = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Ethnicity.Select(c => c.EthicityID));
           foreach (var Ethnicity in db.CriteriaAppearance_Ethnicity)
           {
               if (selectedEthnicityHS.Contains(Ethnicity.EthnicityID))
               {
                   if (!SearchSettingsEthnicity.Contains(Ethnicity.EthnicityID))
                   {

                       //SearchSettings_Ethnicity.EthnicityID = Ethnicity.EthnicityID;
                       var temp = new SearchSettings_Ethnicity();
                       temp.EthicityID = Ethnicity.EthnicityID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_Ethnicity", temp);
                   }
               }
               else
               {
                   if (SearchSettingsEthnicity.Contains(Ethnicity.EthnicityID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Ethnicity.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //hair color
       private void UpdateSearchSettingsHairColor(int?[] selectedHairColor, profilemetadata ProfileDataToUpdate)
       {
           if (selectedHairColor == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_HairColor  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_HairColor CurrentSearchSettings_HairColor = db.SearchSettings_HairColor.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedHairColorHS = new HashSet<int?>(selectedHairColor);
           //get the values for this members searchsettings HairColor
           var SearchSettingsHairColor = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_HairColor.Select(c => c.HairColorID));
           foreach (var HairColor in db.CriteriaAppearance_HairColor)
           {
               if (selectedHairColorHS.Contains(HairColor.HairColorID))
               {
                   if (!SearchSettingsHairColor.Contains(HairColor.HairColorID))
                   {

                       //SearchSettings_HairColor.HairColorID = HairColor.HairColorID;
                       var temp = new SearchSettings_HairColor();
                       temp.HairColorID = HairColor.HairColorID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_HairColor", temp);

                   }
               }
               else
               {
                   if (SearchSettingsHairColor.Contains(HairColor.HairColorID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_HairColor.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //eye color
       private void UpdateSearchSettingsEyeColor(int?[] selectedEyeColor, profilemetadata ProfileDataToUpdate)
       {
           if (selectedEyeColor == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_EyeColor  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_EyeColor CurrentSearchSettings_EyeColor = db.SearchSettings_EyeColor.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedEyeColorHS = new HashSet<int?>(selectedEyeColor);
           //get the values for this members searchsettings EyeColor
           var SearchSettingsEyeColor = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_EyeColor.Select(c => c.EyeColorID));
           foreach (var EyeColor in db.CriteriaAppearance_EyeColor)
           {
               if (selectedEyeColorHS.Contains(EyeColor.EyeColorID))
               {
                   if (!SearchSettingsEyeColor.Contains(EyeColor.EyeColorID))
                   {

                       //SearchSettings_EyeColor.EyeColorID = EyeColor.EyeColorID;
                       var temp = new SearchSettings_EyeColor();
                       temp.EyeColorID = EyeColor.EyeColorID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_EyeColor", temp);

                   }
               }
               else
               {
                   if (SearchSettingsEyeColor.Contains(EyeColor.EyeColorID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_EyeColor.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }

     //hot feature
       private void UpdateSearchSettingsHotFeature(int?[] selectedHotFeature, profilemetadata ProfileDataToUpdate)
       {
           if (selectedHotFeature == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_HotFeature  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_HotFeature CurrentSearchSettings_HotFeature = db.SearchSettings_HotFeature.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedHotFeatureHS = new HashSet<int?>(selectedHotFeature);
           //get the values for this members searchsettings HotFeature
           var SearchSettingsHotFeature = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_HotFeature.Select(c => c.HotFeatureID));
           foreach (var HotFeature in db.CriteriaCharacter_HotFeature)
           {
               if (selectedHotFeatureHS.Contains(HotFeature.HotFeatureID))
               {
                   if (!SearchSettingsHotFeature.Contains(HotFeature.HotFeatureID))
                   {

                       //SearchSettings_HotFeature.HotFeatureID = HotFeature.HotFeatureID;
                       var temp = new SearchSettings_HotFeature();
                       temp.HotFeatureID = HotFeature.HotFeatureID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_HotFeature", temp);

                   }
               }
               else
               {
                   if (SearchSettingsHotFeature.Contains(HotFeature.HotFeatureID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_HotFeature.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //diet
       private void UpdateSearchSettingsDiet(int?[] selectedDiet, profilemetadata ProfileDataToUpdate)
       {
           if (selectedDiet == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Diet  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_Diet CurrentSearchSettings_Diet = db.SearchSettings_Diet.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedDietHS = new HashSet<int?>(selectedDiet);
           //get the values for this members searchsettings Diet
           var SearchSettingsDiet = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Diet.Select(c => c.DietID));
           foreach (var Diet in db.CriteriaCharacter_Diet)
           {
               if (selectedDietHS.Contains(Diet.DietID))
               {
                   if (!SearchSettingsDiet.Contains(Diet.DietID))
                   {

                       //SearchSettings_Diet.DietID = Diet.DietID;
                       var temp = new SearchSettings_Diet();
                       temp.DietID = Diet.DietID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_Diet", temp);

                   }
               }
               else
               {
                   if (SearchSettingsDiet.Contains(Diet.DietID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Diet.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //drinks
       private void UpdateSearchSettingsDrinks(int?[] selectedDrinks, profilemetadata ProfileDataToUpdate)
       {
           if (selectedDrinks == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Drinks  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_Drinks CurrentSearchSettings_Drinks = db.SearchSettings_Drinks.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedDrinksHS = new HashSet<int?>(selectedDrinks);
           //get the values for this members searchsettings Drinks
           var SearchSettingsDrinks = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Drinks.Select(c => c.DrinksID));
           foreach (var Drinks in db.CriteriaCharacter_Drinks)
           {
               if (selectedDrinksHS.Contains(Drinks.DrinksID))
               {
                   if (!SearchSettingsDrinks.Contains(Drinks.DrinksID))
                   {

                       //SearchSettings_Drinks.DrinksID = Drinks.DrinksID;
                       var temp = new SearchSettings_Drinks();
                       temp.DrinksID = Drinks.DrinksID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_Drinks", temp);

                   }
               }
               else
               {
                   if (SearchSettingsDrinks.Contains(Drinks.DrinksID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Drinks.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //excerize
       private void UpdateSearchSettingsExercise(int?[] selectedExercise, profilemetadata ProfileDataToUpdate)
       {
           if (selectedExercise == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Exercise  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_Exercise CurrentSearchSettings_Exercise = db.SearchSettings_Exercise.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedExerciseHS = new HashSet<int?>(selectedExercise);
           //get the values for this members searchsettings Exercise
           var SearchSettingsExercise = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Exercise.Select(c => c.ExerciseID));
           foreach (var Exercise in db.CriteriaCharacter_Exercise)
           {
               if (selectedExerciseHS.Contains(Exercise.ExerciseID))
               {
                   if (!SearchSettingsExercise.Contains(Exercise.ExerciseID))
                   {

                       //SearchSettings_Exercise.ExerciseID = Exercise.ExerciseID;
                       var temp = new SearchSettings_Exercise();
                       temp.ExerciseID = Exercise.ExerciseID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_Exercise", temp);

                   }
               }
               else
               {
                   if (SearchSettingsExercise.Contains(Exercise.ExerciseID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Exercise.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //hobby
       private void UpdateSearchSettingsHobby(int?[] selectedHobby, profilemetadata ProfileDataToUpdate)
       {
           if (selectedHobby == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Hobby  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_Hobby CurrentSearchSettings_Hobby = db.SearchSettings_Hobby.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedHobbyHS = new HashSet<int?>(selectedHobby);
           //get the values for this members searchsettings Hobby
           var SearchSettingsHobby = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Hobby.Select(c => c.HobbyID));
           foreach (var Hobby in db.CriteriaCharacter_Hobby)
           {
               if (selectedHobbyHS.Contains(Hobby.HobbyID))
               {
                   if (!SearchSettingsHobby.Contains(Hobby.HobbyID))
                   {

                       //SearchSettings_Hobby.HobbyID = Hobby.HobbyID;
                       var temp = new SearchSettings_Hobby();
                       temp.HobbyID = Hobby.HobbyID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_Hobby", temp);

                   }
               }
               else
               {
                   if (SearchSettingsHobby.Contains(Hobby.HobbyID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Hobby.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //humor
       private void UpdateSearchSettingsHumor(int?[] selectedHumor, profilemetadata ProfileDataToUpdate)
       {
           if (selectedHumor == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Humor  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_Humor CurrentSearchSettings_Humor = db.SearchSettings_Humor.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedHumorHS = new HashSet<int?>(selectedHumor);
           //get the values for this members searchsettings Humor
           var SearchSettingsHumor = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Humor.Select(c => c.HumorID));
           foreach (var Humor in db.CriteriaCharacter_Humor)
           {
               if (selectedHumorHS.Contains(Humor.HumorID))
               {
                   if (!SearchSettingsHumor.Contains(Humor.HumorID))
                   {

                       //SearchSettings_Humor.HumorID = Humor.HumorID;
                       var temp = new SearchSettings_Humor();
                       temp.HumorID = Humor.HumorID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_Humor", temp);

                   }
               }
               else
               {
                   if (SearchSettingsHumor.Contains(Humor.HumorID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Humor.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //political view
       private void UpdateSearchSettingsPoliticalView(int?[] selectedPoliticalView, profilemetadata ProfileDataToUpdate)
       {
           if (selectedPoliticalView == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_PoliticalView  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_PoliticalView CurrentSearchSettings_PoliticalView = db.SearchSettings_PoliticalView.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedPoliticalViewHS = new HashSet<int?>(selectedPoliticalView);
           //get the values for this members searchsettings PoliticalView
           var SearchSettingsPoliticalView = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_PoliticalView.Select(c => c.PoliticalViewID));
           foreach (var PoliticalView in db.CriteriaCharacter_PoliticalView)
           {
               if (selectedPoliticalViewHS.Contains(PoliticalView.PoliticalViewID))
               {
                   if (!SearchSettingsPoliticalView.Contains(PoliticalView.PoliticalViewID))
                   {

                       //SearchSettings_PoliticalView.PoliticalViewID = PoliticalView.PoliticalViewID;
                       var temp = new SearchSettings_PoliticalView();
                       temp.PoliticalViewID = PoliticalView.PoliticalViewID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_PoliticalView", temp);

                   }
               }
               else
               {
                   if (SearchSettingsPoliticalView.Contains(PoliticalView.PoliticalViewID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_PoliticalView.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //relegion
       private void UpdateSearchSettingsReligion(int?[] selectedReligion, profilemetadata ProfileDataToUpdate)
       {
           if (selectedReligion == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Religion  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_Religion CurrentSearchSettings_Religion = db.SearchSettings_Religion.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedReligionHS = new HashSet<int?>(selectedReligion);
           //get the values for this members searchsettings Religion
           var SearchSettingsReligion = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Religion.Select(c => c.ReligionID));
           foreach (var Religion in db.CriteriaCharacter_Religion)
           {
               if (selectedReligionHS.Contains(Religion.religionID))
               {
                   if (!SearchSettingsReligion.Contains(Religion.religionID))
                   {

                       //SearchSettings_Religion.ReligionID = Religion.ReligionID;
                       var temp = new SearchSettings_Religion();
                       temp.ReligionID = Religion.religionID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_Religion", temp);

                   }
               }
               else
               {
                   if (SearchSettingsReligion.Contains(Religion.religionID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Religion.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //relegious attendance
       private void UpdateSearchSettingsReligiousAttendance(int?[] selectedReligiousAttendance, profilemetadata ProfileDataToUpdate)
       {
           if (selectedReligiousAttendance == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_ReligiousAttendance  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_ReligiousAttendance CurrentSearchSettings_ReligiousAttendance = db.SearchSettings_ReligiousAttendance.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedReligiousAttendanceHS = new HashSet<int?>(selectedReligiousAttendance);
           //get the values for this members searchsettings ReligiousAttendance
           var SearchSettingsReligiousAttendance = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_ReligiousAttendance.Select(c => c.ReligiousAttendanceID));
           foreach (var ReligiousAttendance in db.CriteriaCharacter_ReligiousAttendance)
           {
               if (selectedReligiousAttendanceHS.Contains(ReligiousAttendance.ReligiousAttendanceID))
               {
                   if (!SearchSettingsReligiousAttendance.Contains(ReligiousAttendance.ReligiousAttendanceID))
                   {

                       //SearchSettings_ReligiousAttendance.ReligiousAttendanceID = ReligiousAttendance.ReligiousAttendanceID;
                       var temp = new SearchSettings_ReligiousAttendance();
                       temp.ReligiousAttendanceID = ReligiousAttendance.ReligiousAttendanceID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_ReligiousAttendance", temp);

                   }
               }
               else
               {
                   if (SearchSettingsReligiousAttendance.Contains(ReligiousAttendance.ReligiousAttendanceID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_ReligiousAttendance.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //sign
       private void UpdateSearchSettingsSign(int?[] selectedSign, profilemetadata ProfileDataToUpdate)
       {
           if (selectedSign == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Sign  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_Sign CurrentSearchSettings_Sign = db.SearchSettings_Sign.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedSignHS = new HashSet<int?>(selectedSign);
           //get the values for this members searchsettings Sign
           var SearchSettingsSign = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Sign.Select(c => c.SignID));
           foreach (var Sign in db.CriteriaCharacter_Sign)
           {
               if (selectedSignHS.Contains(Sign.SignID))
               {
                   if (!SearchSettingsSign.Contains(Sign.SignID))
                   {

                       //SearchSettings_Sign.SignID = Sign.SignID;
                       var temp = new SearchSettings_Sign();
                       temp.SignID = Sign.SignID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_Sign", temp);

                   }
               }
               else
               {
                   if (SearchSettingsSign.Contains(Sign.SignID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Sign.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //smokes
       private void UpdateSearchSettingsSmokes(int?[] selectedSmokes, profilemetadata ProfileDataToUpdate)
       {
           if (selectedSmokes == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Smokes  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_Smokes CurrentSearchSettings_Smokes = db.SearchSettings_Smokes.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedSmokesHS = new HashSet<int?>(selectedSmokes);
           //get the values for this members searchsettings Smokes
           var SearchSettingsSmokes = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Smokes.Select(c => c.SmokesID));
           foreach (var Smokes in db.CriteriaCharacter_Smokes)
           {
               if (selectedSmokesHS.Contains(Smokes.SmokesID))
               {
                   if (!SearchSettingsSmokes.Contains(Smokes.SmokesID))
                   {

                       //SearchSettings_Smokes.SmokesID = Smokes.SmokesID;
                       var temp = new SearchSettings_Smokes();
                       temp.SmokesID = Smokes.SmokesID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_Smokes", temp);

                   }
               }
               else
               {
                   if (SearchSettingsSmokes.Contains(Smokes.SmokesID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Smokes.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //education level
       private void UpdateSearchSettingsEducationLevel(int?[] selectedEducationLevel, profilemetadata ProfileDataToUpdate)
       {
           if (selectedEducationLevel == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_EducationLevel  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_EducationLevel CurrentSearchSettings_EducationLevel = db.SearchSettings_EducationLevel.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedEducationLevelHS = new HashSet<int?>(selectedEducationLevel);
           //get the values for this members searchsettings EducationLevel
           var SearchSettingsEducationLevel = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_EducationLevel.Select(c => c.EducationLevelID));
           foreach (var EducationLevel in db.CriteriaLife_EducationLevel)
           {
               if (selectedEducationLevelHS.Contains(EducationLevel.EducationLevelID))
               {
                   if (!SearchSettingsEducationLevel.Contains(EducationLevel.EducationLevelID))
                   {

                       //SearchSettings_EducationLevel.EducationLevelID = EducationLevel.EducationLevelID;
                       var temp = new SearchSettings_EducationLevel();
                       temp.EducationLevelID = EducationLevel.EducationLevelID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_EducationLevel", temp);

                   }
               }
               else
               {
                   if (SearchSettingsEducationLevel.Contains(EducationLevel.EducationLevelID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_EducationLevel.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //employment status
       private void UpdateSearchSettingsEmploymentStatus(int?[] selectedEmploymentStatus, profilemetadata ProfileDataToUpdate)
       {
           if (selectedEmploymentStatus == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_EmploymentStatus  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_EmploymentStatus CurrentSearchSettings_EmploymentStatus = db.SearchSettings_EmploymentStatus.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedEmploymentStatusHS = new HashSet<int?>(selectedEmploymentStatus);
           //get the values for this members searchsettings EmploymentStatus
           var SearchSettingsEmploymentStatus = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_EmploymentStatus.Select(c => c.EmploymentStatusID));
           foreach (var EmploymentStatus in db.CriteriaLife_EmploymentStatus)
           {
               if (selectedEmploymentStatusHS.Contains(EmploymentStatus.EmploymentSatusID))
               {
                   if (!SearchSettingsEmploymentStatus.Contains(EmploymentStatus.EmploymentSatusID))
                   {

                       //SearchSettings_EmploymentStatus.EmploymentStatusID = EmploymentStatus.EmploymentStatusID;
                       var temp = new SearchSettings_EmploymentStatus();
                       temp.EmploymentStatusID = EmploymentStatus.EmploymentSatusID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_EmploymentStatus", temp);

                   }
               }
               else
               {
                   if (SearchSettingsEmploymentStatus.Contains(EmploymentStatus.EmploymentSatusID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_EmploymentStatus.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //have kids
       private void UpdateSearchSettingsHaveKids(int?[] selectedHaveKids, profilemetadata ProfileDataToUpdate)
       {
           if (selectedHaveKids == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_HaveKids  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_HaveKids CurrentSearchSettings_HaveKids = db.SearchSettings_HaveKids.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedHaveKidsHS = new HashSet<int?>(selectedHaveKids);
           //get the values for this members searchsettings HaveKids
           var SearchSettingsHaveKids = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_HaveKids.Select(c => c.HaveKidsID));
           foreach (var HaveKids in db.CriteriaLife_HaveKids)
           {
               if (selectedHaveKidsHS.Contains(HaveKids.HaveKidsId))
               {
                   if (!SearchSettingsHaveKids.Contains(HaveKids.HaveKidsId))
                   {

                       //SearchSettings_HaveKids.HaveKidsID = HaveKids.HaveKidsID;
                       var temp = new SearchSettings_HaveKids();
                       temp.HaveKidsID = HaveKids.HaveKidsId;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_HaveKids", temp);

                   }
               }
               else
               {
                   if (SearchSettingsHaveKids.Contains(HaveKids.HaveKidsId))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_HaveKids.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //income level
       private void UpdateSearchSettingsIncomeLevel(int?[] selectedIncomeLevel, profilemetadata ProfileDataToUpdate)
       {
           if (selectedIncomeLevel == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_IncomeLevel  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_IncomeLevel CurrentSearchSettings_IncomeLevel = db.SearchSettings_IncomeLevel.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedIncomeLevelHS = new HashSet<int?>(selectedIncomeLevel);
           //get the values for this members searchsettings IncomeLevel
           var SearchSettingsIncomeLevel = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_IncomeLevel.Select(c => c.ImcomeLevelID));
           foreach (var IncomeLevel in db.CriteriaLife_IncomeLevel)
           {
               if (selectedIncomeLevelHS.Contains(IncomeLevel.IncomeLevelID))
               {
                   if (!SearchSettingsIncomeLevel.Contains(IncomeLevel.IncomeLevelID))
                   {

                       //SearchSettings_IncomeLevel.IncomeLevelID = IncomeLevel.IncomeLevelID;
                       var temp = new SearchSettings_IncomeLevel();
                       temp.ImcomeLevelID = IncomeLevel.IncomeLevelID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_IncomeLevel", temp);

                   }
               }
               else
               {
                   if (SearchSettingsIncomeLevel.Contains(IncomeLevel.IncomeLevelID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_IncomeLevel.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //living situation
       private void UpdateSearchSettingsLivingSituation(int?[] selectedLivingSituation, profilemetadata ProfileDataToUpdate)
       {
           if (selectedLivingSituation == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_LivingSituation  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_LivingSituation CurrentSearchSettings_LivingSituation = db.SearchSettings_LivingSituation.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedLivingSituationHS = new HashSet<int?>(selectedLivingSituation);
           //get the values for this members searchsettings LivingSituation
           var SearchSettingsLivingSituation = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_LivingStituation.Select(c => c.LivingStituationID));
           foreach (var LivingSituation in db.CriteriaLife_LivingSituation)
           {
               if (selectedLivingSituationHS.Contains(LivingSituation.LivingSituationID))
               {
                   if (!SearchSettingsLivingSituation.Contains(LivingSituation.LivingSituationID))
                   {

                       //SearchSettings_LivingSituation.LivingSituationID = LivingSituation.LivingSituationID;
                       var temp = new SearchSettings_LivingStituation();
                       temp.LivingStituationID = LivingSituation.LivingSituationID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_LivingStituation", temp);

                   }
               }
               else
               {
                   if (SearchSettingsLivingSituation.Contains(LivingSituation.LivingSituationID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_LivingStituation.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //lookingfor
       private void UpdateSearchSettingsLookingFor(int?[] selectedLookingFor, profilemetadata ProfileDataToUpdate)
       {
           if (selectedLookingFor == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_LookingFor  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_LookingFor CurrentSearchSettings_LookingFor = db.SearchSettings_LookingFor.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedLookingForHS = new HashSet<int?>(selectedLookingFor);
           //get the values for this members searchsettings LookingFor
           var SearchSettingsLookingFor = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_LookingFor.Select(c => c.LookingForID));
           foreach (var LookingFor in db.CriteriaLife_LookingFor)
           {
               if (selectedLookingForHS.Contains(LookingFor.LookingForID))
               {
                   if (!SearchSettingsLookingFor.Contains(LookingFor.LookingForID))
                   {

                       //SearchSettings_LookingFor.LookingForID = LookingFor.LookingForID;
                       var temp = new SearchSettings_LookingFor();
                       temp.LookingForID = LookingFor.LookingForID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_LookingFor", temp);

                   }
               }
               else
               {
                   if (SearchSettingsLookingFor.Contains(LookingFor.LookingForID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_LookingFor.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //maritial status
       private void UpdateSearchSettingsMaritalStatus(int?[] selectedMaritalStatus, profilemetadata ProfileDataToUpdate)
       {
           if (selectedMaritalStatus == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_MaritalStatus  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_MaritalStatus CurrentSearchSettings_MaritalStatus = db.SearchSettings_MaritalStatus.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedMaritalStatusHS = new HashSet<int?>(selectedMaritalStatus);
           //get the values for this members searchsettings MaritalStatus
           var SearchSettingsMaritalStatus = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_MaritalStatus.Select(c => c.MaritalStatusID));
           foreach (var MaritalStatus in db.CriteriaLife_MaritalStatus)
           {
               if (selectedMaritalStatusHS.Contains(MaritalStatus.MaritalStatusID))
               {
                   if (!SearchSettingsMaritalStatus.Contains(MaritalStatus.MaritalStatusID))
                   {

                       //SearchSettings_MaritalStatus.MaritalStatusID = MaritalStatus.MaritalStatusID;
                       var temp = new SearchSettings_MaritalStatus();
                       temp.MaritalStatusID = MaritalStatus.MaritalStatusID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_MaritalStatus", temp);

                   }
               }
               else
               {
                   if (SearchSettingsMaritalStatus.Contains(MaritalStatus.MaritalStatusID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_MaritalStatus.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //profession
       private void UpdateSearchSettingsProfession(int?[] selectedProfession, profilemetadata ProfileDataToUpdate)
       {
           if (selectedProfession == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Profession  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_Profession CurrentSearchSettings_Profession = db.SearchSettings_Profession.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedProfessionHS = new HashSet<int?>(selectedProfession);
           //get the values for this members searchsettings Profession
           var SearchSettingsProfession = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_Profession.Select(c => c.ProfessionID));
           foreach (var Profession in db.CriteriaLife_Profession)
           {
               if (selectedProfessionHS.Contains(Profession.ProfessionID))
               {
                   if (!SearchSettingsProfession.Contains(Profession.ProfessionID))
                   {

                       //SearchSettings_Profession.ProfessionID = Profession.ProfessionID;
                       var temp = new SearchSettings_Profession();
                       temp.ProfessionID = Profession.ProfessionID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_Profession", temp);

                   }
               }
               else
               {
                   if (SearchSettingsProfession.Contains(Profession.ProfessionID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_Profession.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       //wants kids
       private void UpdateSearchSettingsWantsKids(int?[] selectedWantsKids, profilemetadata ProfileDataToUpdate)
       {
           if (selectedWantsKids == null)
           {
               // ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_WantsKids  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           int SearchSettingsID = ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettingsID;
           //SearchSettings_WantsKids CurrentSearchSettings_WantsKids = db.SearchSettings_WantsKids.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


           var selectedWantsKidsHS = new HashSet<int?>(selectedWantsKids);
           //get the values for this members searchsettings WantsKids
           var SearchSettingsWantsKids = new HashSet<int?>(ProfileDataToUpdate.SearchSettings.FirstOrDefault().SearchSettings_WantKids.Select(c => c.WantKidsID));
           foreach (var WantsKids in db.CriteriaLife_WantsKids)
           {
               if (selectedWantsKidsHS.Contains(WantsKids.WantsKidsID))
               {
                   if (!SearchSettingsWantsKids.Contains(WantsKids.WantsKidsID))
                   {

                       //SearchSettings_WantsKids.WantsKidsID = WantsKids.WantsKidsID;
                       var temp = new SearchSettings_WantKids();
                       temp.WantKidsID = WantsKids.WantsKidsID;
                       temp.SearchSettingsID = SearchSettingsID;

                       db.AddObject("SearchSettings_WantKids", temp);

                   }
               }
               else
               {
                   if (SearchSettingsWantsKids.Contains(WantsKids.WantsKidsID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().SearchSettings.Where(p => p.SearchSettingsID == SearchSettingsID).First().SearchSettings_WantKids.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }
       #endregion

       #region "Checkbox Update Functions for profiledata many to many"



       private void UpdateProfileDataEthnicity(int?[] selectedEthnicity, profilemetadata  ProfileDataToUpdate)
       {
           if (selectedEthnicity == null)
           {
               // ProfileDataToUpdate.profiledata.FirstOrDefault().ProfileData_Ethnicity  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           string ProfileDataID = ProfileDataToUpdate.ProfileID;
           //ProfileData_Ethnicity CurrentProfileData_Ethnicity = db.ProfileData_Ethnicity.Where(s => s.ProfileDataID == ProfileDataID).FirstOrDefault();


           var selectedEthnicityHS = new HashSet<int?>(selectedEthnicity);
           //get the values for this members profiledata Ethnicity
           var ProfileDataEthnicity = new HashSet<int?>(ProfileDataToUpdate.ProfileData_Ethnicity.Select(c => c.EthnicityID));
           foreach (var Ethnicity in db.CriteriaAppearance_Ethnicity)
           {
               if (selectedEthnicityHS.Contains(Ethnicity.EthnicityID))
               {
                   if (!ProfileDataEthnicity.Contains(Ethnicity.EthnicityID))
                   {

                       //ProfileData_Ethnicity.EthnicityID = Ethnicity.EthnicityID;
                       var temp = new ProfileData_Ethnicity();
                       temp.EthnicityID = Ethnicity.EthnicityID;
                       temp.ProfileID = ProfileDataID;

                       db.AddObject("ProfileData_Ethnicity", temp);
                   }
               }
               else
               {
                   if (ProfileDataEthnicity.Contains(Ethnicity.EthnicityID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().ProfileData_Ethnicity.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }

       private void UpdateProfileDataHotFeature(int?[] selectedHotFeature, profilemetadata  ProfileDataToUpdate)
       {
           if (selectedHotFeature == null)
           {
               // ProfileDataToUpdate.profiledata.FirstOrDefault().ProfileData_HotFeature  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           string ProfileDataID = ProfileDataToUpdate.ProfileID;
           //ProfileData_HotFeature CurrentProfileData_HotFeature = db.ProfileData_HotFeature.Where(s => s.ProfileDataID == ProfileDataID).FirstOrDefault();


           var selectedHotFeatureHS = new HashSet<int?>(selectedHotFeature);
           //get the values for this members profiledata HotFeature
           var ProfileDataHotFeature = new HashSet<int?>(ProfileDataToUpdate.ProfileData_HotFeature.Select(c => c.HotFeatureID));
           foreach (var HotFeature in db.CriteriaCharacter_HotFeature)
           {
               if (selectedHotFeatureHS.Contains(HotFeature.HotFeatureID))
               {
                   if (!ProfileDataHotFeature.Contains(HotFeature.HotFeatureID))
                   {

                       //ProfileData_HotFeature.HotFeatureID = HotFeature.HotFeatureID;
                       var temp = new ProfileData_HotFeature();
                       temp.HotFeatureID = HotFeature.HotFeatureID;
                       temp.ProfileID = ProfileDataID;

                       db.AddObject("ProfileData_HotFeature", temp);
                   }
               }
               else
               {
                   if (ProfileDataHotFeature.Contains(HotFeature.HotFeatureID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().ProfileData_HotFeature.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }

       private void UpdateProfileDataHobby(int?[] selectedHobby, profilemetadata  ProfileDataToUpdate)
       {
           if (selectedHobby == null)
           {
               // ProfileDataToUpdate.profiledata.FirstOrDefault().ProfileData_Hobby  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           string ProfileDataID = ProfileDataToUpdate.ProfileID;
           //ProfileData_Hobby CurrentProfileData_Hobby = db.ProfileData_Hobby.Where(s => s.ProfileDataID == ProfileDataID).FirstOrDefault();


           var selectedHobbyHS = new HashSet<int?>(selectedHobby);
           //get the values for this members profiledata Hobby
           var ProfileDataHobby = new HashSet<int?>(ProfileDataToUpdate.ProfileData_Hobby.Select(c => c.HobbyID));
           foreach (var Hobby in db.CriteriaCharacter_Hobby)
           {
               if (selectedHobbyHS.Contains(Hobby.HobbyID))
               {
                   if (!ProfileDataHobby.Contains(Hobby.HobbyID))
                   {

                       //ProfileData_Hobby.HobbyID = Hobby.HobbyID;
                       var temp = new ProfileData_Hobby();
                       temp.HobbyID = Hobby.HobbyID;
                       temp.ProfileID = ProfileDataID;

                       db.AddObject("ProfileData_Hobby", temp);
                   }
               }
               else
               {
                   if (ProfileDataHobby.Contains(Hobby.HobbyID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().ProfileData_Hobby.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }

       private void UpdateProfileDataLookingFor(int?[] selectedLookingFor, profilemetadata  ProfileDataToUpdate)
       {
           if (selectedLookingFor == null)
           {
               // ProfileDataToUpdate.profiledata.FirstOrDefault().ProfileData_LookingFor  = new List<gender>(); 
               return;
           }
           //build the search settings gender object
           string ProfileDataID = ProfileDataToUpdate.ProfileID;
           //ProfileData_LookingFor CurrentProfileData_LookingFor = db.ProfileData_LookingFor.Where(s => s.ProfileDataID == ProfileDataID).FirstOrDefault();


           var selectedLookingForHS = new HashSet<int?>(selectedLookingFor);
           //get the values for this members profiledata LookingFor
           var ProfileDataLookingFor = new HashSet<int?>(ProfileDataToUpdate.ProfileData_LookingFor.Select(c => c.LookingForID));
           foreach (var LookingFor in db.CriteriaLife_LookingFor)
           {
               if (selectedLookingForHS.Contains(LookingFor.LookingForID))
               {
                   if (!ProfileDataLookingFor.Contains(LookingFor.LookingForID))
                   {

                       //ProfileData_LookingFor.LookingForID = LookingFor.LookingForID;
                       var temp = new ProfileData_LookingFor();
                       temp.LookingForID = LookingFor.LookingForID;
                       temp.ProfileID = ProfileDataID;

                       db.AddObject("ProfileData_LookingFor", temp);
                   }
               }
               else
               {
                   if (ProfileDataLookingFor.Contains(LookingFor.LookingForID))
                   {

                       var Temp = db.ProfileDatas.Where(p => p.ProfileID == ProfileDataToUpdate.ProfileID).First().ProfileData_LookingFor.First();
                       db.DeleteObject(Temp);

                   }
               }
           }
       }



       #endregion

       

       #region "profile visisiblity settings update here"

       public bool UpdateProfileVisibilitySettings(ProfileVisiblitySetting model)
       {
           if (model.ProfileID != null)
           {
               datingservice.UpdateProfileVisiblitySetting(model);

               return true;
           }
           return false;
       }
       #endregion

       
    }
}
