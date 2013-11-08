using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Domain.Entities.Anewluv.ViewModels;


using Shell.MVC2.Interfaces;
using Shell.MVC2.Infrastructure;
using System.Data;
using LoggingLibrary;
using Nmedia.Infrastructure.Domain.Errorlog;

namespace Shell.MVC2.Data
{

    /// <summary>
    /// we will move anything that updates search to public/private methdos here eventually
    /// </summary>
   public  class SearchEditRepository : MemberRepositoryBase ,ISearchEditRepository 
    {

       
       
        private  AnewluvContext db; // = new AnewluvContext();
       //private  PostalData2Entities postaldb; //= new PostalData2Entities();



        public SearchEditRepository(AnewluvContext datingcontext)
            : base(datingcontext)
        {
        }

        #region "searchsetting table get methods"      
      
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
                //log error mesasge
                new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, profileid, null, false);
               throw;
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
                new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, profileid, null, false);
                throw ;
            }
            catch (Exception ex)
            {
                //log error mesasge
                new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, profileid, null, false);
               throw;
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
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, profileid, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, profileid, null, false);
              throw;
           }

       }
    
        #endregion
        //basic settings  
       
       #region "Get methods here to return search settings values as well as the entire bound models as needed"
      
       
        public BasicSearchSettingsModel getbasicsearchsettings(int searchid)
        {
      

            try
            {


                searchsetting p = db.searchsetting.Where(z => z.id  == searchid).FirstOrDefault();
                BasicSearchSettingsModel model = new BasicSearchSettingsModel();

                //populate values here ok ?
                if (p != null)

                  model.distancefromme  = (p.distancefromme == 0 ||p.distancefromme  == null) ? p.distancefromme.GetValueOrDefault()  : 0;
                  model.agemin = p.agemax == null ? 18 : p.agemin.GetValueOrDefault();
                  model.agemax  = p.agemax == null ? 120 : p.agemax.GetValueOrDefault();

                  model.creationdate = p.creationdate == null ? null : p.creationdate;
                //this is done in inches and stuff , convert to metric as needed on UI
 
                  model.lastupdatedate  = p.lastupdatedate  == null ? null : p.lastupdatedate;
                  model.searchname = p.searchname  == "" ? "" : p.searchname ;    
                  model.searchrank =  p.searchrank  == null ? 0 : p.searchrank;   
                  model.myperfectmatch = p.myperfectmatch == null ? false : p.myperfectmatch.Value;
                  model.systemmatch = p.systemmatch == null ? false : p.systemmatch.Value;
                  model.savedsearch = p.savedsearch == null ? false : p.savedsearch.Value;

                  var showmevalues = new HashSet<int>(p.showme.Select(c => c.showme.id));
                  foreach (var _showme in db.lu_showme)
                  {
                      model.showmelist.Add(new lu_showme
                      {
                          id = _showme.id,
                          description = _showme.description,
                          selected = showmevalues.Contains(_showme.id)
                      });
                  }

                 

                  var sortbyvalues = new HashSet<int>(p.sortbytypes.Select(c => c.sortbytype.id));
                  foreach (var _sortby in db.lu_sortbytype)
                  {
                      model.sortbylist.Add(new lu_sortbytype 
                      {
                          id = _sortby.id,
                          description = _sortby.description,
                          selected = sortbyvalues.Contains(_sortby.id)
                      });
                  }

                //Different since theer is not a lookup for these.
                  var locationvalues = new HashSet<int>(p.locations .Select(c => c.id));
                  foreach (var _location in db.searchsetting_location )
                  {
                      model.locationlist.Add(new searchsetting_location 
                      {
                          id = _location.id,
                           city  = _location.city,
                            countryid = _location.countryid ,
                             postalcode = _location.postalcode ,
                          selected = locationvalues.Contains(_location.id)
                      });
                  }


                 var gendervalues = new HashSet<int>(p.genders.Select(c => c.gender.id));
                //set default if non selected
                // logic is if its a female then show them the male checked and vice versa
                if (gendervalues.Count  == 0)
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

                //now populate the checkboxes               
                foreach (var _genders in db.lu_gender)
                {
                    model.genderlist.Add(new lu_gender
                    {
                        id = _genders.id,
                        description = _genders.description,
                        selected = gendervalues.Contains(_genders.id)
                    });
                }


               return model;

            
            }
            catch (DataException dx)
            {
                //Log the error (add a variable name after DataException) 
                // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                // return model;
                //handle logging here
                var message = dx.Message;
                new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
                throw;
            }
            catch (Exception ex)
            {
                //log error mesasge
                new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
            }
        }       

        public AppearanceSearchSettingsModel getappearancesearchsettings(int searchid)
       {
          

           try
           {

               searchsetting p = db.searchsetting.Where(z => z.id == searchid).FirstOrDefault();
               AppearanceSearchSettingsModel model = new AppearanceSearchSettingsModel();

               //TO DO determine the users metric type here and get the min height in thier locale
               var usheightmin = "48";
               var usheightmax = "89";


               string heightmin = Convert.ToInt32(model.heightmin) == -1 ? usheightmin : model.heightmin;
               var heightmax = Convert.ToInt32(model.heightmax) == -1 ? usheightmax : model.heightmax;

               //pilot how to show the rest of the values 
               //sample of doing string values

               var ethncityvalues = new HashSet<int>(p.ethnicities.Select(c => c.ethnicity.id));
               foreach (var _ethnicity in db.lu_ethnicity)
               {
                   model.ethnicitylist.Add(new lu_ethnicity
                   {
                       id = _ethnicity.id,
                       description = _ethnicity.description,
                       selected = ethncityvalues.Contains(_ethnicity.id)
                   });
               }


               var bodytypesvalues = new HashSet<int>(p.bodytypes.Select(c => c.bodytype.id));
               foreach (var _bodytypes in db.lu_bodytype)
               {
                   model.bodytypeslist.Add(new lu_bodytype
                   {
                       id = _bodytypes.id,
                       description = _bodytypes.description,
                       selected = bodytypesvalues.Contains(_bodytypes.id)
                   });
               }


               var eyecolorvalues = new HashSet<int>(p.eyecolors.Select(c => c.eyecolor.id));
               foreach (var _eyecolor in db.lu_eyecolor)
               {
                   model.eyecolorlist.Add(new lu_eyecolor
                   {
                       id = _eyecolor.id,
                       description = _eyecolor.description,
                       selected = eyecolorvalues.Contains(_eyecolor.id)
                   });
               }


               var haircolorvalues = new HashSet<int>(p.haircolors.Select(c => c.haircolor.id));
               foreach (var _haircolor in db.lu_haircolor)
               {
                   model.haircolorlist.Add(new lu_haircolor
                   {
                       id = _haircolor.id,
                       description = _haircolor.description,
                       selected = haircolorvalues.Contains(_haircolor.id)
                   });
               }


               var hotfeaturevalues = new HashSet<int>(p.hotfeatures.Select(c => c.hotfeature.id));
               foreach (var _hotfeature in db.lu_hotfeature)
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
           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService ).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
              throw;
           }
       }

        public CharacterSearchSettingsModel getcharactersearchsettings(int searchid)
       {
    


           try
           {
               searchsetting p = db.searchsetting.Where(z => z.id == searchid).FirstOrDefault();
               CharacterSearchSettingsModel model = new CharacterSearchSettingsModel();
            
               #region "Diet"
               //Diet checkbox values populated here
               //pilot how to show the rest of the values 
               //sample of doing string values
               var dietvalues = new HashSet<int>(p.diets.Select(c => c.diet.id));
               foreach (var _diet in db.lu_diet)
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
               var humorvalues = new HashSet<int>(p.humors.Select(c => c.humor.id));
               foreach (var _humor in db.lu_humor)
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
               var hobbyvalues = new HashSet<int>(p.hobbies .Select(c => c.hobby.id));
               foreach (var _hobby in db.lu_hobby)
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
              //foreach (var item in p.drinks )
               //{
               //    (item.drinks.drinksName);
               //}
               var drinksvalues = new HashSet<int>(p.drinks.Select(c => c.drink.id));
               foreach (var _drinks in db.lu_drinks)
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
               var exercisevalues = new HashSet<int>(p.exercises.Select(c => c.exercise.id));
               foreach (var _exercise in db.lu_exercise)
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
               var smokesvalues = new HashSet<int>(p.smokes.Select(c => c.smoke.id));
               foreach (var _smokes in db.lu_smokes)
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
               var signvalues = new HashSet<int>(p.signs.Select(c => c.sign.id));
               foreach (var _sign in db.lu_sign)
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
               var politicalviewvalues = new HashSet<int>(p.politicalviews.Select(c => c.politicalview.id));
               foreach (var _politicalview in db.lu_politicalview)
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
               var religionvalues = new HashSet<int>(p.religions.Select(c => c.religion.id));
               foreach (var _religion in db.lu_religion)
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
               var religiousattendancevalues = new HashSet<int>(p.religiousattendances.Select(c => c.religiousattendance.id));
               foreach (var _religiousattendance in db.lu_religiousattendance)
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
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
              throw;
           }
       }       
     
       public LifeStyleSearchSettingsModel  getlifestylesearchsettings(int searchid)
        {
         

            try
            {
                searchsetting p = db.searchsetting.Where(z => z.id == searchid).FirstOrDefault();
                LifeStyleSearchSettingsModel model = new LifeStyleSearchSettingsModel();

                #region "EducationLevel"
                //EducationLevel checkbox values populated here
                //pilot how to show the rest of the values 
                //sample of doing string values
                var educationlevelvalues = new HashSet<int>(p.educationlevels.Select(c => c.educationlevel.id));
                foreach (var _educationlevel in db.lu_educationlevel)
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
                var employmentstatusvalues = new HashSet<int>(p.employmentstatus.Select(c => c.employmentstatus.id));
                foreach (var _employmentstatus in db.lu_employmentstatus)
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
                var incomelevelvalues = new HashSet<int>(p.incomelevels.Select(c => c.incomelevel.id));
                foreach (var _incomelevel in db.lu_incomelevel)
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
                var lookingforvalues = new HashSet<int>(p.lookingfor.Select(c => c.lookingfor.id));
                foreach (var _lookingfor in db.lu_lookingfor)
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
                var wantskidsvalues = new HashSet<int>(p.wantkids.Select(c => c.wantskids.id));
                foreach (var _wantskids in db.lu_wantskids)
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
                var professionvalues = new HashSet<int>(p.professions.Select(c => c.profession.id));
                foreach (var _profession in db.lu_profession)
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
                var maritalstatusvalues = new HashSet<int>(p.maritalstatuses.Select(c => c.maritalstatus.id));
                foreach (var _maritalstatus in db.lu_maritalstatus)
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
                var livingsituationvalues = new HashSet<int>(p.livingstituations.Select(c => c.livingsituation.id));
                foreach (var _livingsituation in db.lu_livingsituation)
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
                var havekidsvalues = new HashSet<int>(p.havekids.Select(c => c.havekids.id));
                foreach (var _havekids in db.lu_havekids)
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
            catch (DataException dx)
            {
                //Log the error (add a variable name after DataException) 
                // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                // return model;
                //handle logging here
                var message = dx.Message;
                new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
                throw;
            }
            catch (Exception ex)
            {
                //log error mesasge
                new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
            }

        }

        #endregion

       //remeber these values for the lists only send the new values i.e values they want to add
       #region "Push/Post methods that save changes to entire search settings or peice meal as needed"

       #region "Basic Search Settings Edit"
     
       public AnewluvMessages  editbasicsearchsettings(BasicSearchSettingsModel newmodel, int searchid)
       {
           //create a new messages object
           AnewluvMessages messages = new AnewluvMessages();

           //get the searchsettings
           searchsetting search = db.searchsetting.Where(d => d.id == searchid).First();

           messages = (updatebasicsearchsettings(newmodel, search, messages));
    


           if (messages.errormessages.Count > 0)
           {
               messages.message = "There was a problem Editing You Basic Settings, Please try again later";
               return messages;
           }
           messages.message = "Edit Basic Settings Successful";
           return messages;
       }
     
       private AnewluvMessages updatebasicsearchsettings(BasicSearchSettingsModel newmodel, searchsetting search, AnewluvMessages messages)
       {

            bool nothingupdated = true;

           try
           {
               //profile p = db.profiles.Where(z => z.id == profileid).First();
               //sample code for determining weather to edit an item or not or determin if a value changed'
               //nothingupdated = (newmodel.height  == search.height) ? false : true;

               //only update items that seem to be changing or are different
               var agemax = (newmodel.agemax  == search.agemax ) ? newmodel.agemax  : null;
               var agemin = (newmodel.agemin  == search.agemin ) ? newmodel.agemin  : null;
               var creationdate = (newmodel.creationdate  == search.creationdate ) ? newmodel.creationdate  : null;
               var distancefromme = (newmodel.distancefromme  == search.distancefromme) ? newmodel.distancefromme : null;
               var lastupdatedate = (newmodel.lastupdatedate  == search.lastupdatedate) ? newmodel.lastupdatedate : null;
               var myperfectmatch = (newmodel.myperfectmatch == search.myperfectmatch) ? newmodel.myperfectmatch : null;
               var savedsearch = (newmodel.savedsearch == search.savedsearch) ? newmodel.savedsearch : null;
               var searchname = (newmodel.searchname == search.searchname) ? newmodel.searchname : null;
               var searchrank = (newmodel.searchrank == search.searchrank) ? newmodel.searchrank : null;
               var systemmatch = (newmodel.systemmatch == search.systemmatch) ? newmodel.systemmatch : null;
               //TO DO test if anything changed
               var showmelist = newmodel.showmelist ;
               //TO DO test if anything changed
               var genderlist = newmodel.genderlist ;
                      //TO DO test if anything changed
               var sortbylist = newmodel.sortbylist;
                      //TO DO test if anything changed
               var locationlist = newmodel.locationlist;


               //update my settings 
               //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list

               if (agemax.HasValue == true) search.agemax  = agemax;
               if (agemin != null) search.agemin  = agemin;
               if (creationdate  != null) search.creationdate  = creationdate ;
               if (distancefromme  != null) search.distancefromme = distancefromme ;
               if (lastupdatedate  != null) search.lastupdatedate = lastupdatedate ;
               if (myperfectmatch   != null) search.myperfectmatch = myperfectmatch ;
               if (savedsearch  != null) search.savedsearch = savedsearch ;
               if (searchname  != null) search.searchname = searchname ;
               if (searchrank  != null) search.searchrank = searchrank ;
               if (systemmatch   != null) search.systemmatch  = systemmatch  ;
               if (showmelist.Count > 0)   this.updatesearchsettingsshowme(showmelist , search);
               if (genderlist.Count > 0)   this.updatesearchsettingsgenders(genderlist , search);
               if (sortbylist.Count > 0) this.updatesearchsettingssortbytype(sortbylist, search);
               if (locationlist.Count > 0) this.updatesearchsettingslocation(locationlist, search);
       

             //TO DO move this code to searchssettings Repositoury             


               search.lastupdatedate = DateTime.Now;

               //db.Entry(profiledata).State = EntityState.Modified;
               db.SaveChanges();
 

               
               //db.Entry(profiledata).State = EntityState.Modified;


               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();

               //TOD DO
               //wes should probbaly re-generate the members matches as well here but it too much overhead , only do it once when the user re-logs in and add a manual button to update thier mathecs when edit is complete
               //update session too just in case
               //membersmodel.profiledata = profiledata;               

               //   CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID,profiledata  );
               //model.CurrentErrors.Clear();
               // return model;
           }
           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
              throw;
           }
           return messages;

       }
  
       #endregion

       //#region "other editpages to implement"
       #region "Appeareance Settings  Search Edit"

       public AnewluvMessages editappearancesettings(AppearanceSearchSettingsModel newmodel, int searchid)
       {
           //create a new messages object
           AnewluvMessages messages = new AnewluvMessages();

                  // var newmodel2 = this.getAppearancesettingsviewmodel(profile.id);

           messages = updateappearancessettings(newmodel, searchid, messages);
       

           if (messages.errormessages.Count > 0)
           {
               messages.message = "There was a problem Editing You Appearance Settings, Please try again later";
               return messages;
           }
           messages.message = "Edit Appearance Settings Successful";
           return messages;
       }       

       private AnewluvMessages updateappearancessettings(AppearanceSearchSettingsModel newmodel, int searchid, AnewluvMessages messages)
       {
           bool nothingupdated = true;

           try
           {
               searchsetting searchsettingstoupdate = db.searchsetting.Where(d => d.id == searchid).First();

               //TO DO determine the users metric type here and get the min height in thier locale
               var usheightmin = "48";
               var usheightmax = "89";

               
               string heightmin = Convert.ToInt32(newmodel.heightmin) == -1 ? usheightmin  :  newmodel.heightmin ;
               var heightmax =  Convert.ToInt32(newmodel.heightmax) == -1 ? usheightmax  : newmodel.heightmax;
               //TO DO test if anything changed
               var bodytypelist = newmodel.bodytypeslist;
               //TO DO test if anything changed
               var haircolorlist = newmodel.haircolorlist;
               //TO DO test if anything changed
               var eyecolorlist = newmodel.eyecolorlist;
               //TO DO test if anything changed
               var hotfeaturelist = newmodel.hotfeaturelist;
               //TO DO test if anything changed
               var ethnicitylist = newmodel.ethnicitylist;

               //update my settings 
               //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list
              // nothingupdated = (newmodel.myheight == profile.profiledata.height) ? false : true;

               //profile.profiledata.bodytype = UiBodyType;  //TO DO look at this

               //now update the search settings 
          
               if (heightmin != "") searchsettingstoupdate.heightmin  = Convert.ToInt32(heightmin );
               if (heightmin != "") searchsettingstoupdate.heightmin = Convert.ToInt32(heightmin);

               //now update the lists if values were passed only!
               if (bodytypelist.Count > 0)
                   updatesearchsettingsbodytype(newmodel.bodytypeslist, searchsettingstoupdate);
               if (haircolorlist.Count > 0)
                   updatesearchsettingshaircolor(newmodel.haircolorlist , searchsettingstoupdate);
              if (eyecolorlist.Count > 0)
                   updatesearchsettingseyecolor(newmodel.eyecolorlist, searchsettingstoupdate);
              if (hotfeaturelist.Count > 0)
                  updatesearchsettingshotfeature(hotfeaturelist, searchsettingstoupdate);
              if (ethnicitylist.Count > 0)
                  updatesearchsettingsethnicity(ethnicitylist, searchsettingstoupdate);
                            
        
         
               searchsettingstoupdate.lastupdatedate = DateTime.Now;

               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();

               //TOD DO probbaly update cache stuff here as needed.                           

               //CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID,profiledata  );
               //model.CurrentErrors.Clear();
               // return model;
           }
           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
              throw;
           }
           return messages;



       }

       #endregion

       #region "Lifestyle settings "
       
       public AnewluvMessages editlifestylesettings(LifeStyleSearchSettingsModel  newmodel, int searchid)
       {
           //create a new messages object
           AnewluvMessages messages = new AnewluvMessages();

           // var newmodel2 = this.getAppearancesettingsviewmodel(profile.id);

           messages = (updatelifestylesettings(newmodel, searchid,  messages));          

           if (messages.errormessages.Count > 0)
           {
               messages.message = "There was a problem Editing You Lifestyle Search settings, Please try again later";
               return messages;
           }
           messages.message = "Edit Lifestyle Search settings Successful";
           return messages;
       }

       private AnewluvMessages updatelifestylesettings(LifeStyleSearchSettingsModel newmodel, int searchid, AnewluvMessages messages)
       {
           bool nothingupdated = true;

           try
           {

               searchsetting searchsettingstoupdate = db.searchsetting.Where(d => d.id == searchid).First();

               // profile p = db.profiles.Where(z => z.id == profileid).First();
               //sample code for determining weather to edit an item or not or determin if a value changed'
               //nothingupdated = (newmodel.educationlevel  == p.profiledata.educationlevel) ? false : true;

               //only update items that are not null
               var educationlevellist = newmodel.educationlevellist;
               var employmentstatuslist = newmodel.employmentstatuslist;
               var incomelevellist = newmodel.incomelevellist;
               var wantskidslist = newmodel.wantskidslist;
               var professionlist = newmodel.professionlist ;
               var maritalstatuslist = newmodel.maritalstatuslist;
               var livingsituationlist = newmodel.livingsituationlist;
               var havekidslist = newmodel.havekidslist;
               //TO DO test if anything changed
               var lookingforlist = newmodel.lookingforlist;


               //update my settings 
               //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list

               if (educationlevellist.Count > 0)
                   updatesearchsettingseducationlevel(educationlevellist, searchsettingstoupdate);
               if (employmentstatuslist.Count > 0)
                   updatesearchsettingsemploymentstatus(employmentstatuslist, searchsettingstoupdate);
               if (incomelevellist.Count > 0)
                   updatesearchsettingsincomelevel(incomelevellist, searchsettingstoupdate);
               if (wantskidslist.Count > 0)
                   updatesearchsettingswantskids(wantskidslist, searchsettingstoupdate);
               if (professionlist.Count > 0)
                   updatesearchsettingsprofession(professionlist , searchsettingstoupdate);
               if (maritalstatuslist.Count > 0)
                   updatesearchsettingsmaritalstatus(maritalstatuslist, searchsettingstoupdate);
               if (livingsituationlist.Count > 0)
                   updatesearchsettingslivingsituation(livingsituationlist, searchsettingstoupdate);
               if (havekidslist.Count > 0)
                   updatesearchsettingshavekids(havekidslist, searchsettingstoupdate);
               if (lookingforlist.Count > 0)
                   updatesearchsettingslookingfor(lookingforlist, searchsettingstoupdate);

               searchsettingstoupdate.lastupdatedate = DateTime.Now;
               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();
               
               //TOD DO probbaly update cache stuff here as needed.  
               //CachingFactory.MembersViewModelHelper.UpdateMemberProfileDataByProfileID (_ProfileID,profiledata  );
               //model.CurrentErrors.Clear();
               // return model;

           }
           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }
           return messages;



       }
      

       #endregion

       public AnewluvMessages editcharactersettings(CharacterSearchSettingsModel  newmodel, int searchid)
       {
           //create a new messages object
           AnewluvMessages messages = new AnewluvMessages();

           messages = updatecharacterssettings(newmodel, searchid, messages);

           if (messages.errormessages.Count > 0)
           {
               messages.message = "There was a problem Editing You character Settings, Please try again later";
               return messages;
           }
           messages.message = "Edit character Settings Successful";
           return messages;
       }
     
       private AnewluvMessages updatecharacterssettings(CharacterSearchSettingsModel newmodel, int searchid, AnewluvMessages messages)
       {
           bool nothingupdated = true;

           try
           {
               searchsetting searchsettingstoupdate = db.searchsetting.Where(d => d.id == searchid).First();



               //only update items that are not null
               var dietlist = newmodel.dietlist;
               var humorlist = newmodel.humorlist;
               var drinkinglist = newmodel.drinkslist;
               var excerciselist = newmodel.exerciselist;
               var smokinglist = newmodel.smokeslist;
               var signlist = newmodel.signlist;
               var politicalviewlist = newmodel.politicalviewlist;
               var religionlist = newmodel.religionlist;
               var religiousattendancelist = newmodel.religiousattendancelist; //TO DO test if anything changed
               var hobylistlist = newmodel.hobbylist;




               //update my settings 
               //this does nothing but we shoul verify that items changed before updating anything so have to test each input and list
               if (dietlist.Count > 0)
                   updatesearchsettingsdiet (dietlist, searchsettingstoupdate);
               if (humorlist.Count > 0)
                   updatesearchsettingshumor (humorlist, searchsettingstoupdate);
               if (drinkinglist.Count > 0)
                   updatesearchsettingsdrinks (drinkinglist, searchsettingstoupdate);
               if (excerciselist.Count > 0)
                   updatesearchsettingsexercise (excerciselist, searchsettingstoupdate);
               if (smokinglist.Count > 0)
                   updatesearchsettingssmokes (smokinglist, searchsettingstoupdate);
               if (signlist.Count > 0)
                   updatesearchsettingssign(signlist, searchsettingstoupdate);
               if (politicalviewlist.Count > 0)
                   updatesearchsettingspoliticalview(politicalviewlist, searchsettingstoupdate);
               if (religionlist.Count > 0)
                   updatesearchsettingsreligion(religionlist, searchsettingstoupdate);
               if (religiousattendancelist.Count > 0)
                   updatesearchsettingsreligiousattendance (religiousattendancelist, searchsettingstoupdate);
               if (hobylistlist.Count > 0)
                   updatesearchsettingshobby(hobylistlist, searchsettingstoupdate);

               
               searchsettingstoupdate.lastupdatedate = DateTime.Now;
               //db.Entry(profiledata).State = EntityState.Modified;
               int changes = db.SaveChanges();




           }
           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }
           return messages;



       }


       #endregion

  

       //index of what page we are looking at i.e we want to split up this model into diff partial views
       #region "checkbox updated functions searchsettings values for all lists"


       //10-3-2012 oawlal made the functuon far more generic so it will work alot better for perefect match or any search setting 
       private void updatesearchsettingsgenders(List<lu_gender> genders, searchsetting currentsearchsetting)
       {
          
           try
           {
               if (genders == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_Genders  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
               //SearchSettings_Genders CurrentSearchSettings_Genders = db.SearchSettings_Genders.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


               // var selectedGendersHS = new HashSet<int?>(selectedGenders);
               //get the values for this members searchsettings Genders
               //var SearchSettingsGenders = new HashSet<int?>(currentsearchsetting.genders.Select(c => c.id));
               foreach (var gender in genders)
               {
                   //new logic : if this item was selected and is not already in the search settings gender values add it 
                   if ((gender.selected == true && !currentsearchsetting.genders.Where(z => z.gender.id == gender.id).Any()))
                   {
                       //SearchSettings_Genders.GendersID = Genders.GendersID;
                       var temp = new searchsetting_gender();
                       temp.id = gender.id;
                       temp.searchsetting.id = currentsearchsetting.id;
                       db.searchsetting_gender.Add(temp);
                   }
                   else
                   {
                       //we have an existing value and we want to remove it in this case since selected was false for sure
                       //we will be doing a remove either way
                       var Temp = db.searchsetting_gender.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == gender.id).First();
                       if (Temp != null)
                           db.searchsetting_gender.Remove(Temp);

                   }
               }


           }

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

         
       }

       //show me
       private void updatesearchsettingsshowme(List<lu_showme> selectedshowme, searchsetting currentsearchsetting)
       {
          

           try
           {
               if (selectedshowme == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_showme  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
               //SearchSettings_showme CurrentSearchSettings_showme = db.SearchSettings_showme.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


               // var selectedshowmeHS = new HashSet<int?>(selectedshowme);
               //get the values for this members searchsettings showme
               //var SearchSettingsshowme = new HashSet<int?>(currentsearchsetting.showme.Select(c => c.id));
               foreach (var showme in db.lu_showme)
               {
                   if (selectedshowme.Contains(showme))
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //sort by
       private void updatesearchsettingssortbytype(List<lu_sortbytype> selectedsortbytype, searchsetting currentsearchsetting)
       {
          
           try
           {
               if (selectedsortbytype == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_sortbytype  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //body types
       private void updatesearchsettingsbodytype(List<lu_bodytype> selectedbodytype, searchsetting currentsearchsetting)
       {
          

           try
           {
               if (selectedbodytype == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_bodytype  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //etnicity
       private void updatesearchsettingsethnicity(List<lu_ethnicity> selectedethnicity, searchsetting currentsearchsetting)
       {
          
           try
           {
               if (selectedethnicity == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_ethnicity  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
               //SearchSettings_ethnicity CurrentSearchSettings_ethnicity = db.SearchSettings_ethnicity.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


               // var selectedethnicityHS = new HashSet<int?>(selectedethnicity);
               //get the values for this members searchsettings ethnicity
               //var SearchSettingsethnicity = new HashSet<int?>(currentsearchsetting.ethnicity.Select(c => c.id));
               foreach (var ethnicity in db.lu_ethnicity)
               {
                   if (selectedethnicity.Contains(ethnicity))
                   {
                       //does not exist so we will add it
                       if (!currentsearchsetting.ethnicities.Any(p => p.id == ethnicity.id))
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
                       if (currentsearchsetting.ethnicities.Any(p => p.id == ethnicity.id))
                       {
                           var temp = db.searchsetting_ethnicity.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == ethnicity.id).First();
                           db.searchsetting_ethnicity.Remove(temp);

                       }
                   }
               }


           }

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //hair color
       private void updatesearchsettingshaircolor(List<lu_haircolor> selectedhaircolor, searchsetting currentsearchsetting)
       {
         

           try
           {
               if (selectedhaircolor == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_haircolor  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //eye color
       private void updatesearchsettingseyecolor(List<lu_eyecolor> selectedeyecolor, searchsetting currentsearchsetting)
       {
          


           try
           {
               if (selectedeyecolor == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_eyecolor  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //hot feature
       private void updatesearchsettingshotfeature(List<lu_hotfeature> selectedhotfeature, searchsetting currentsearchsetting)
       {
          

           try
           {
               if (selectedhotfeature == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_hotfeature  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //diet
       private void updatesearchsettingsdiet(List<lu_diet> selecteddiet, searchsetting currentsearchsetting)
       {
          

           try
           {
               if (selecteddiet == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_diet  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //drinks
       private void updatesearchsettingsdrinks(List<lu_drinks> selecteddrinks, searchsetting currentsearchsetting)
       {
          

           try
           {
               if (selecteddrinks == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_drinks  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //excerize
       private void updatesearchsettingsexercise(List<lu_exercise> selectedexercise, searchsetting currentsearchsetting)
       {
          
           try
           {
               if (selectedexercise == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_exercise  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //hobby
       private void updatesearchsettingshobby(List<lu_hobby> selectedhobby, searchsetting currentsearchsetting)
       {
          

           try
           {

               if (selectedhobby == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_hobby  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //humor
       private void updatesearchsettingshumor(List<lu_humor> selectedhumor, searchsetting currentsearchsetting)
       {
          
           try
           {
               if (selectedhumor == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_humor  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //political view
       private void updatesearchsettingspoliticalview(List<lu_politicalview> selectedpoliticalview, searchsetting currentsearchsetting)
       {
          

           try
           {
               if (selectedpoliticalview == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_politicalview  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //relegion
       private void updatesearchsettingsreligion(List<lu_religion> selectedreligion, searchsetting currentsearchsetting)
       {
           

           try
           {
               if (selectedreligion == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_religion  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //relegious attendance
       private void updatesearchsettingsreligiousattendance(List<lu_religiousattendance> selectedreligiousattendance, searchsetting currentsearchsetting)
       {
         

           try
           {

               if (selectedreligiousattendance == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_religiousattendance  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //sign
       private void updatesearchsettingssign(List<lu_sign> selectedsign, searchsetting currentsearchsetting)
       {


           try
           {
               if (selectedsign == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_sign  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //smokes
       private void updatesearchsettingssmokes(List<lu_smokes> selectedsmokes, searchsetting currentsearchsetting)
       {
       


           try
           {
               if (selectedsmokes == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_smokes  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //education level
       private void updatesearchsettingseducationlevel(List<lu_educationlevel> selectededucationlevel, searchsetting currentsearchsetting)
       {
          

           try
           {
               if (selectededucationlevel == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_educationlevel  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //employment status
       private void updatesearchsettingsemploymentstatus(List<lu_employmentstatus> selectedemploymentstatus, searchsetting currentsearchsetting)
       {
          
           try
           {

               if (selectedemploymentstatus == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_employmentstatus  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //have kids
       private void updatesearchsettingshavekids(List<lu_havekids> selectedhavekids, searchsetting currentsearchsetting)
       {
          
           try
           {

               if (selectedhavekids == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_havekids  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //income level
       private void updatesearchsettingsincomelevel(List<lu_incomelevel> selectedincomelevel, searchsetting currentsearchsetting)
       {

           try
           {


               if (selectedincomelevel == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_incomelevel  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //living situation
       private void updatesearchsettingslivingsituation(List<lu_livingsituation> selectedlivingsituation, searchsetting currentsearchsetting)
       {
          

           try
           {
               if (selectedlivingsituation == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_livingsituation  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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
                           var temp = new searchsetting_livingstituation();
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
                           db.searchsetting_livingstituation.Remove(temp);

                       }
                   }
               }

           }

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //lookingfor
       private void updatesearchsettingslookingfor(List<lu_lookingfor> selectedlookingfor, searchsetting currentsearchsetting)
       {
          
           try
           {
               if (selectedlookingfor == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_lookingfor  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //maritial status
       private void updatesearchsettingsmaritalstatus(List<lu_maritalstatus> selectedmaritalstatus, searchsetting currentsearchsetting)
       {
          
           try
           {
               if (selectedmaritalstatus == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_maritalstatus  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //profession
       private void updatesearchsettingsprofession(List<lu_profession> selectedprofession, searchsetting currentsearchsetting)
       {
       


           try
           {
               if (selectedprofession == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_profession  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
       //wants kids
       private void updatesearchsettingswantskids(List<lu_wantskids> selectedwantskids, searchsetting currentsearchsetting)
       {
    

           try
           {
               if (selectedwantskids == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_wantskids  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
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
                           db.searchsetting_wantkids.Add(temp);

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

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }

       //location added 1/25/2012
       private void updatesearchsettingslocation(List<searchsetting_location> locations, searchsetting currentsearchsetting)
       {
         

           try
           {

               if (locations == null)
               {
                   // profiledata.SearchSettings.FirstOrDefault().SearchSettings_location  = new List<gender>(); 
                   return;
               }
               //build the search settings gender object
               // int SearchSettingsID = currentsearchsetting.id;//profiledata.searchsettings.FirstOrDefault().id;
               //SearchSettings_location CurrentSearchSettings_location = db.SearchSettings_location.Where(s => s.SearchSettingsID == SearchSettingsID).FirstOrDefault();


               // var selectedlocationHS = new HashSet<int?>(selectedlocation);
               //get the values for this members searchsettings location
               //var SearchSettingslocation = new HashSet<int?>(currentsearchsetting.location.Select(c => c.id));
               foreach (var location in db.searchsetting_location)
               {
                   if (locations.Contains(location))
                   {
                       //does not exist so we will add it
                       if (!currentsearchsetting.locations.Any(p => p.id == location.id))
                       {

                           //SearchSettings_location.locationID = location.locationID;
                           var temp = new searchsetting_location();
                           temp.id = location.id;
                           temp.searchsetting.id = currentsearchsetting.id;
                           db.searchsetting_location.Add(temp);

                       }
                   }
                   else
                   { //exists means we want to remove it
                       if (currentsearchsetting.locations.Any(p => p.id == location.id))
                       {
                           var temp = db.searchsetting_location.Where(p => p.searchsetting.id == currentsearchsetting.id && p.id == location.id).First();
                           db.searchsetting_location.Remove(temp);

                       }
                   }
               }
           }

           catch (DataException dx)
           {
               //Log the error (add a variable name after DataException) 
               // newmodel.CurrentErrors.Add("Unable to save changes. Try again, and if the problem persists, see your system administrator.");
               // return model;
               //handle logging here
               var message = dx.Message;
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, dx, null, null, false);
               throw;
           }
           catch (Exception ex)
           {
               //log error mesasge
               new ErroLogging(applicationEnum.EditSearchService).WriteSingleEntry(logseverityEnum.Warning, ex, null, null, false);
               throw;
           }

       }
     
     
       #endregion

    }
}
