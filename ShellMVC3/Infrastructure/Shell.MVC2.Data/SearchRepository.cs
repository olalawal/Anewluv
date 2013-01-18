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

    /// <summary>
    /// we will move anything that updates search to public/private methdos here eventually
    /// </summary>
   public  class SearchRepository : MemberRepositoryBase ,ISearchRepository 
    {

       
       
        private  AnewluvContext db; // = new AnewluvContext();
       //private  PostalData2Entities postaldb; //= new PostalData2Entities();



        public SearchRepository(AnewluvContext datingcontext)
            : base(datingcontext)
        {
        }


        public searchsetting getsearchsetting(int profileid, string searchname, int? searchrank)
        {

                              
            try
            {
            //use the filter to search for items 
            // var searchesttings =    _datingcontext.searchsetting.Where(p => p.profile_id == profileid || searchname =="")  ;
            var searchesttings = (from x in _datingcontext.searchsetting.Where(p => p.profile_id == profileid)
                                    .WhereIf(searchname != "", z => z.searchname == searchname)
                                    .WhereIf(searchrank != null, z => z.searchrank == searchrank.GetValueOrDefault())
                                  select x).FirstOrDefault();
          
              
            return searchesttings; 
            }
            catch (DataException dx)
            {
                //Log the error (add a variable name after DataException) 
                // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                // return model;
                //handle logging here
                var message = dx.Message;
                throw dx;
            }
            catch (Exception ex)
            {
                //handle logging here
                var message = ex.Message;
                throw ex;

            }
            //return null;
        }
        public List<searchsetting> getsearchsettings(int profileid)
        {


            try
            {
                //use the filter to search for items 
                // var searchesttings =    _datingcontext.searchsetting.Where(p => p.profile_id == profileid || searchname =="")  ;
                var searchesttings = (from x in _datingcontext.searchsetting.Where(p => p.profile_id == profileid)
                                      select x);

                return searchesttings.ToList();
            }
            catch (DataException dx)
            {
                //Log the error (add a variable name after DataException) 
                // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                // return model;
                //handle logging here
                var message = dx.Message;
                throw dx;
            }
            catch (Exception ex)
            {
                //handle logging here
                var message = ex.Message;
                throw ex;

            }
            //return null;
        }
        public SearchSettingsViewModel getsearchsettingsviewmodel(int profileid,string searchname,int? searchrank )
        {

           try
           {
           //use the filter to search for items 
              // var searchesttings =    _datingcontext.searchsetting.Where(p => p.profile_id == profileid || searchname =="")  ;
               var searchesttings = getsearchsetting(profileid, searchname, searchrank.GetValueOrDefault());
               SearchSettingsViewModel returnmodel = new SearchSettingsViewModel();
               if (searchesttings != null)
               {
                 
                   returnmodel.basicsearchsettings = getbasicsearchsettings(searchesttings.id);
                   returnmodel.appearancesearchsettings = getappearancesearchsettings(searchesttings.id);
                   returnmodel.charactersearchsettings = getcharactersearchsettings(searchesttings.id);
                   returnmodel.lifestylesearchsettings = getlifestylesearchsettings(searchesttings.id);

                   return returnmodel;
               }


               returnmodel.currenterrors.Add("No search settings found");
               return returnmodel;


             //now we want to populate the rest of the values 

       }
           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               throw dx;
           }
           catch (Exception ex)
           {
               //handle logging here
               var message = ex.Message;
               throw ex;

           }

       }

        //basic settings  
       public BasicSearchSettingsModel getbasicsearchsettings(int searchid)
        {
            BasicSearchSettingsModel returnmodel = new BasicSearchSettingsModel();

            try
            {


                searchsetting p = db.searchsetting.Where(z => z.profile_id == intprofileid && z.myperfectmatch == true).FirstOrDefault();
                BasicSettings model = new BasicSettings();

                //populate values here ok ?
                if (p != null)


                    // model. = p.searchname == null ? "Unamed Search" : p.searchname;
                    //model.di = p.distancefromme == null ? 500 : p.distancefromme.GetValueOrDefault();
                    // model.searchrank = p.searchrank == null ? 0 : p.searchrank.GetValueOrDefault();

                    //populate ages select list here I guess
                    //TODO get from app fabric
                    // SharedRepository sharedrepository = new SharedRepository();
                    //Ages = sharedrepository.AgesSelectList;

                    model.br = p.agemax == null ? 99 : p.agemax.GetValueOrDefault();
                //  model.agemin = p.agemin == null ? 18 : p.agemin.GetValueOrDefault();




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

                return returnmodel;
            }
            catch (DataException dx)
            {
                //Log the error (add a variable name after DataException) 
                // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                // return model;
                //handle logging here
                var message = dx.Message;
                throw dx;
            }
            catch (Exception ex)
            {
                //handle logging here
                var message = ex.Message;
                throw ex;

            }

        }
       
       public AppearanceSearchSettingsModel getappearancesearchsettings(int searchid)
       {
           AppearanceSearchSettingsModel returnmodel = new AppearanceSearchSettingsModel();

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

               return returnmodel;
           }
           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               throw dx;
           }
           catch (Exception ex)
           {
               //handle logging here
               var message = ex.Message;
               throw ex;

           }
       }

       public CharacterSettingsModel getcharactersearchsettings(int searchid)
       {
           CharacterSettingsModel returnmodel = new CharacterSettingsModel();


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
               var hobbyvalues = new HashSet<int>(p.hobbies.Select(c => c.id.GetValueOrDefault()));
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
           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               throw dx;
           }
           catch (Exception ex)
           {
               //handle logging here
               var message = ex.Message;
               throw ex;

           }

       }
       
       public LifeStyleSettingsModel getlifestylesearchsettings(int searchid)
        {
            LifeStyleSettingsModel returnmodel = new LifeStyleSettingsModel();


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
                    model.educationlevellist.Add(new lu_educationlevel
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
                var lookingforvalues = new HashSet<int>(p.lookingfor.Select(c => c.id.GetValueOrDefault()));
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
                var livingsituationvalues = new HashSet<int>(p.livingstituations.Select(c => c.id.GetValueOrDefault()));
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
                return returnmodel;
            }
            catch (DataException dx)
            {
                //Log the error (add a variable name after DataException) 
                // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                // return model;
                //handle logging here
                var message = dx.Message;
                throw dx;
            }
            catch (Exception ex)
            {
                //handle logging here
                var message = ex.Message;
                throw ex;

            }

        }
       
    
        //index of what page we are looking at i.e we want to split up this model into diff partial views


    }
}
