using Anewluv.Caching;
using Anewluv.Domain.Data;
using Anewluv.Domain.Data.ViewModels;
using Nmedia.Infrastructure.DTOs;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anewluv.DataExtentionMethods
{
    public static class searchsettingsextentions
    {

        //generic filtering function we can reuse, filters all search settings using profileid,searchname and other data
        public static searchsetting filtersearchsettings(this IRepository<searchsetting> repo, SearchSettingsModel searchmodel)
        {

            try
            {
                //This query assumes that one search is always called default and cannot be deleted dont like that
                List<searchsetting> allsearchsettings = new List<searchsetting>();
                searchsetting p = new searchsetting();

                //default handling for empty profile ID and other search data
                if (searchmodel == null) return p;

                allsearchsettings = repo.Query
                (z => (searchmodel.searchid != 0 && z.id == searchmodel.searchid) ||
                (searchmodel.profileid.Value != 0 && (z.profile_id == searchmodel.profileid.Value)))                 
                 .Include(x => x.profilemetadata)               
                .Include(x => x.profilemetadata.profile) 
                  .Include(x => x.profilemetadata.profile.profiledata)   
                .Include(y => y.details  )             
                .Include(y => y.locations)
                
               

                .Select().ToList();

                if (allsearchsettings.Count() > 0 & searchmodel.searchname != null)//|searchmodel.searchname != ""  )
                {
                    p = allsearchsettings.Where(z => z.searchname == searchmodel.searchname).FirstOrDefault();
                }
                else if (allsearchsettings.Count() > 0)
                {
                    p = allsearchsettings.OrderByDescending(z => z.creationdate).FirstOrDefault();  //get the first one thats probbaly the default.
                }

                return p;
            }
            catch (Exception ex)
            { throw ex; }
        }

        //generic filtering function we can reuse
        public static searchsetting getsearchsettingsbysearchid(this IRepository<searchsetting> repo,int? searchid)
        {

            try
            {
                //This query assumes that one search is always called default and cannot be deleted dont like that
                searchsetting mysearchsettings = new searchsetting();

                //default handling for empty profile ID and other search data

                mysearchsettings = repo.Query(z => z.id == searchid)
                .Include(x => x.profilemetadata)            
                .Include(x => x.profilemetadata.profile)
                  .Include(x => x.profilemetadata.profile.profiledata)
                .Include(y => y.details)
                .Include(y => y.locations)
                .Select().FirstOrDefault();



                return mysearchsettings;
            }
            catch (Exception ex)
            { throw ex; }
        }


        //all the search extentions for updates and gets here since the profile edit pages also have the search settings i.e the YOU part






        #region "private get methods for reuses"



        public static BasicSearchSettingsModel getbasicsearchsettings(BasicSearchSettingsModel SearchViewModel,searchsetting p, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            try
            {



                BasicSearchSettingsModel model = new BasicSearchSettingsModel();

                //populate values here ok ?
                // if (p == null) return null;

                // model. = p.searchname == null ? "Unamed Search" : p.searchname;
                //model.di = p.distancefromme == null ? 500 : p.distancefromme.GetValueOrDefault();
                // model.searchrank = p.searchrank == null ? 0 : p.searchrank.GetValueOrDefault();
                //populate ages select list here I guess
                //TODO get from app fabric
                // SharedRepository sharedrepository = new SharedRepository();
                //Ages = sharedrepository.AgesSelectList;


              

                model.agemin = p.agemin == null ? 18 : p.agemin.GetValueOrDefault();
                model.mygenderid = p.profilemetadata != null ? p.profilemetadata.profile.profiledata.gender_id.Value : (int?)null;
                model.agemax = p.agemax == null ? 99 : p.agemax.GetValueOrDefault();
                model.creationdate = p.creationdate == null ? (DateTime?)null : p.creationdate.GetValueOrDefault();
                model.distancefromme = p.distancefromme == null ? 500 : p.distancefromme.GetValueOrDefault();
                model.lastupdatedate = p.lastupdatedate == null ? DateTime.Now : p.lastupdatedate;
                model.searchname = p.searchname == null ? "Default" : p.searchname;
                model.searchrank = p.searchrank == null ? 1 : p.searchrank;
                model.myperfectmatch = p.myperfectmatch == null ? true : p.myperfectmatch;
                model.systemmatch = p.systemmatch == null ? false : p.systemmatch;
                //test of map the list items to the generic listitem object in order to clean up the models so no iselected item on them
                model.showmelist = SearchViewModel.showmelist;
                model.genderlist = SearchViewModel.genderlist;
                model.sortbylist = SearchViewModel.sortbylist;
                model.agelist = SearchViewModel.agelist;  //TO do have it use desction and IC as well instead of age object

                //update the list with the items that are selected.
                foreach (listitem showme in SearchViewModel.showmelist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.showme).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.showmelist.First(d => d.id == showme.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem gender in SearchViewModel.genderlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.gender).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.showmelist.First(d => d.id == gender.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (listitem sortbytype in SearchViewModel.sortbylist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.sortbytype).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.sortbylist.First(d => d.id == sortbytype.id).selected = true;
                }



                //Location does not match any list i think need to have this tweaked for now ignore
                //full location since it includes the city
                //for now UI only allows one but this code allows for many
                foreach (var item in p.locations)
                {
                    model.locationlist.Add(item);
                }


                return model;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       public static AppearanceSearchSettingsModel getappearancesearchsettings(AppearanceSearchSettingsModel SearchViewModel,searchsetting p, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            try
            {
                //searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().Queryable().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();

                AppearanceSearchSettingsModel model = new AppearanceSearchSettingsModel();

                //populate values here ok ?
                //if (p == null) return null;


                //get all the showmes so we can deterimine which are checked and which are not
   

              

                model.heightmax = p.heightmax == null ? 210 : p.heightmax;
                model.heightmin = p.heightmin == null ? 100 : p.heightmin;

                model.ethnicitylist = SearchViewModel.ethnicitylist;
                model.bodytypelist = SearchViewModel.bodytypelist;
                model.eyecolorlist = SearchViewModel.eyecolorlist;
                model.haircolorlist = SearchViewModel.haircolorlist;
                model.hotfeaturelist = SearchViewModel.hotfeaturelist;
                model.metricheightlist = SearchViewModel.metricheightlist;


                //pilot how to show the rest of the values 
                //sample of doing string values
                // var allhotfeature = _unitOfWorkAsync.lu_hotfeature;
                //model.hotfeaturelist =  p.profilemetadata.hotfeatures.ToList();
                //update the list with the items that are selected.
                foreach (listitem ethnicity in SearchViewModel.ethnicitylist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.ethnicity).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.ethnicitylist.First(d => d.id == ethnicity.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem bodytype in SearchViewModel.bodytypelist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.bodytype).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.bodytypelist.First(d => d.id == bodytype.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem eyecolor in SearchViewModel.eyecolorlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.eyecolor).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.eyecolorlist.First(d => d.id == eyecolor.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem haircolor in SearchViewModel.haircolorlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.haircolor).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.haircolorlist.First(d => d.id == haircolor.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem hotfeature in SearchViewModel.hotfeaturelist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.hotfeature).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.hotfeaturelist.First(d => d.id == hotfeature.id).selected = true;
                }

                return model;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       public static CharacterSearchSettingsModel getcharactersearchsettings(CharacterSearchSettingsModel SearchViewModel ,searchsetting p, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            try
            {
                //searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().Queryable().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();

                CharacterSearchSettingsModel model = new CharacterSearchSettingsModel();

                //populate values here ok ?
                // if (p == null) return null;


                model.humorlist = SearchViewModel.humorlist;
                model.dietlist = SearchViewModel.dietlist;

                model.hobbylist = SearchViewModel.hobbylist;
                model.drinkslist = SearchViewModel.drinkslist;
                model.exerciselist = SearchViewModel.exerciselist;
                model.smokeslist = SearchViewModel.smokeslist;
                model.signlist = SearchViewModel.signlist;
                model.politicalviewlist = SearchViewModel.politicalviewlist;
                model.religionlist = SearchViewModel.religionlist;
                model.religiousattendancelist = SearchViewModel.religiousattendancelist;


                //update the list with the items that are selected.


                //update the list with the items that are selected.
                //special handling for hobby, there is no Any value so for search since any is allowed make sure its selected if none selected
                foreach (listitem hobby in SearchViewModel.hobbylist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.hobby).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.hobbylist.First(d => d.id == hobby.id).selected = true;
                }

                //if no hobby is selected set any to true.
                if (!model.hobbylist.Any(z => z.selected == true))
                    model.hobbylist.First(f => f.description == "Any").selected = true;


                foreach (listitem humor in SearchViewModel.humorlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.humor).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.humorlist.First(d => d.id == humor.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (listitem diet in SearchViewModel.dietlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.diet).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.dietlist.First(d => d.id == diet.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (listitem drink in SearchViewModel.drinkslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.drink).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.drinkslist.First(d => d.id == drink.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (listitem exercise in SearchViewModel.exerciselist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.excercise).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.exerciselist.First(d => d.id == exercise.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (listitem smokes in SearchViewModel.smokeslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.smokes).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.smokeslist.First(d => d.id == smokes.id).selected = true;
                }


                //update the list with the items that are selected.
                foreach (listitem sign in SearchViewModel.signlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.sign).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.signlist.First(d => d.id == sign.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem politicalview in SearchViewModel.politicalviewlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.politicalview).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.politicalviewlist.First(d => d.id == politicalview.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem religion in SearchViewModel.religionlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.religion).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.religionlist.First(d => d.id == religion.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem religiousattendance in SearchViewModel.religiousattendancelist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.religiousattendance).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.religiousattendancelist.First(d => d.id == religiousattendance.id).selected = true;
                }


                return model;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

       public static LifeStyleSearchSettingsModel getlifestylesearchsettings(LifeStyleSearchSettingsModel SearchViewModel, searchsetting p, IUnitOfWorkAsync _unitOfWorkAsync)
        {
            try
            {
                // searchsetting p = _unitOfWorkAsync.Repository<searchsetting>().Queryable().Where(z => z.id == searchmodel.searchid || z.profile_id == searchmodel.profileid && (searchmodel.searchname == "" ? "Default" : searchmodel.searchname) == z.searchname).FirstOrDefault();
                LifeStyleSearchSettingsModel model = new LifeStyleSearchSettingsModel();


                model.educationlevellist = SearchViewModel.educationlevellist;
                model.lookingforlist = SearchViewModel.lookingforlist;
                model.employmentstatuslist = SearchViewModel.employmentstatuslist;
                model.havekidslist = SearchViewModel.havekidslist;
                model.incomelevellist = SearchViewModel.incomelevellist;
                model.livingsituationlist = SearchViewModel.livingsituationlist;
                model.maritalstatuslist = SearchViewModel.maritalstatuslist;
                model.professionlist = SearchViewModel.professionlist;
                model.wantskidslist = SearchViewModel.wantskidslist;


                //update the list with the items that are selected.
                foreach (listitem educationlevel in SearchViewModel. educationlevellist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.educationlevel).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.educationlevellist.First(d => d.id == educationlevel.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem lookingfor in SearchViewModel. lookingforlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.lookingfor).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.lookingforlist.First(d => d.id == lookingfor.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem employmentstatus in SearchViewModel. employmentstatuslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.employmentstatus).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.employmentstatuslist.First(d => d.id == employmentstatus.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem  imcomelevel in SearchViewModel.incomelevellist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.incomelevel).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.incomelevellist.First(d => d.id == imcomelevel.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem livingsituation in SearchViewModel. livingsituationlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.livingsituation).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.livingsituationlist.First(d => d.id == livingsituation.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem maritialstatus in SearchViewModel.maritalstatuslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.maritialstatus).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.maritalstatuslist.First(d => d.id == maritialstatus.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem profession in SearchViewModel. professionlist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.profession).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.professionlist.First(d => d.id == profession.id).selected = true;
                }

                //update the list with the items that are selected.
                foreach (listitem wantkids in SearchViewModel.wantskidslist.Where(c => p.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingdetailtypeEnum.wantskids).Any(f => f.value == c.id)))
                {
                    //update the value as checked here on the list
                    model.wantskidslist.First(d => d.id == wantkids.id).selected = true;

                }


                return model;

            }
            catch (Exception ex)
            {

               throw ex;
            }
        }

        #endregion


        #region "private update methods for reuse so we can also combine in one call"

        //TO DO add validation and pass back via messages , IE compare old settings to new i.e change nothing if nothing changed
       public static AnewluvMessages updatebasicsearchsettings(BasicSearchSettingsModel model, searchsetting p, AnewluvMessages messages , IUnitOfWorkAsync _unitOfWorkAsync)
        {

            bool nothingupdated = true;
            try
            {



                //create a new messages object
                if (p == null)
                {
                    messages.errormessages.Add("There is no search with this parameters");
                    return messages;
                }

                p.agemin = model.agemin;
                p.agemax = model.agemax;

                p.distancefromme = model.distancefromme;
                p.lastupdatedate = model.lastupdatedate;
                p.searchname = model.searchname;
                p.searchrank = model.searchrank;
                p.myperfectmatch = model.myperfectmatch;

                //checkbos item updates 
                if (model.genderlist.Count() > 0)
                    updatesearchsettingsdetail(model.genderlist, p, searchsettingdetailtypeEnum.gender, _unitOfWorkAsync);
                if (model.sortbylist.Count() > 0)
                    updatesearchsettingsdetail(model.sortbylist, p, searchsettingdetailtypeEnum.sortbytype, _unitOfWorkAsync);              
                if (model.showmelist.Count > 0)
                    updatesearchsettingsdetail(model.showmelist, p, searchsettingdetailtypeEnum.showme ,_unitOfWorkAsync);              
                if (model.locationlist.Count > 0)
                    updatesearchsettingslocation(model.locationlist, p ,_unitOfWorkAsync);
             


                _unitOfWorkAsync.Repository<searchsetting>().Update(p);
                var i = _unitOfWorkAsync.SaveChanges();

                // return messages;



            }
            catch (Exception ex)
            {
                //handle logging here
                var message = ex.Message;
                throw ex;

            }
            return messages;




        }
        //TO DO add validation and pass back via messages 

       public static AnewluvMessages updateappearancesearchsettings(AppearanceSearchSettingsModel model, searchsetting p, AnewluvMessages messages,IUnitOfWorkAsync _unitOfWorkAsync)
        {

            try
            {
                //  searchsetting p =searchsettingdetailtypeEnum.Repository<searchsetting>().Queryable().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
                //create a new messages object
                if (p == null)
                {
                    messages.errormessages.Add("There is no appearance search with this parameters");
                    return messages;
                }

                p.heightmax = model.heightmin;
                p.heightmax = model.heightmax;
                //checkbos item updates 
                if (model.ethnicitylist.Count > 0)
                    updatesearchsettingsdetail(model.ethnicitylist.ToList(), p, searchsettingdetailtypeEnum.ethnicity ,_unitOfWorkAsync);
                if (model.bodytypelist.Count > 0)
                    updatesearchsettingsdetail(model.bodytypelist.ToList(), p, searchsettingdetailtypeEnum.bodytype ,_unitOfWorkAsync);
                if (model.eyecolorlist.Count > 0)
                    updatesearchsettingsdetail(model.eyecolorlist.ToList(), p, searchsettingdetailtypeEnum.eyecolor ,_unitOfWorkAsync);
                if (model.haircolorlist.Count > 0)
                    updatesearchsettingsdetail(model.haircolorlist.ToList(), p, searchsettingdetailtypeEnum.haircolor ,_unitOfWorkAsync);
                if (model.hotfeaturelist.Count > 0)
                    updatesearchsettingsdetail(model.hotfeaturelist.ToList(), p, searchsettingdetailtypeEnum.hotfeature ,_unitOfWorkAsync);

                _unitOfWorkAsync.Repository<searchsetting>().Update(p);
                var i = _unitOfWorkAsync.SaveChanges();

                //  return messages;
            }
            catch (Exception ex)
            {
                //handle logging here
                var message = ex.Message;
                throw ex;

            }
            return messages;

        }

       public static AnewluvMessages updatecharactersearchsettings(CharacterSearchSettingsModel model, searchsetting p, AnewluvMessages messages,IUnitOfWorkAsync _unitOfWorkAsync)
        {

            try
            {


                // AnewluvMessages messages = new AnewluvMessages();
                //  searchsetting p =searchsettingdetailtypeEnum.Repository<searchsetting>().Queryable().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
                //create a new messages object
                if (p == null)
                {
                    messages.errormessages.Add("There is no search with this parameters");
                    return messages;
                }


                //checkbos item updates 
                if (model.dietlist.Count > 0)
                    updatesearchsettingsdetail(model.dietlist.ToList(), p, searchsettingdetailtypeEnum.diet ,_unitOfWorkAsync);

                if (model.humorlist.Count > 0)
                    updatesearchsettingsdetail(model.humorlist.ToList(), p, searchsettingdetailtypeEnum.humor ,_unitOfWorkAsync);

                if (model.hobbylist.Count > 0)
                    updatesearchsettingsdetail(model.hobbylist.ToList(), p, searchsettingdetailtypeEnum.hobby ,_unitOfWorkAsync);

                if (model.drinkslist.Count > 0)
                    updatesearchsettingsdetail(model.drinkslist.ToList(), p, searchsettingdetailtypeEnum.drink ,_unitOfWorkAsync);

                if (model.exerciselist.Count > 0)
                    updatesearchsettingsdetail(model.exerciselist.ToList(), p, searchsettingdetailtypeEnum.excercise ,_unitOfWorkAsync);

                if (model.smokeslist.Count > 0)
                    updatesearchsettingsdetail(model.smokeslist.ToList(), p, searchsettingdetailtypeEnum.smokes ,_unitOfWorkAsync);

                if (model.signlist.Count > 0)
                    updatesearchsettingsdetail(model.signlist.ToList(), p, searchsettingdetailtypeEnum.sign ,_unitOfWorkAsync);

                if (model.politicalviewlist.Count > 0)
                    updatesearchsettingsdetail(model.politicalviewlist.ToList(), p, searchsettingdetailtypeEnum.politicalview ,_unitOfWorkAsync);

                if (model.religionlist.Count > 0)
                    updatesearchsettingsdetail(model.religionlist.ToList(), p, searchsettingdetailtypeEnum.religion ,_unitOfWorkAsync);

                if (model.religiousattendancelist.Count > 0)
                    updatesearchsettingsdetail(model.religiousattendancelist.ToList(), p, searchsettingdetailtypeEnum.religiousattendance ,_unitOfWorkAsync);




                var i = _unitOfWorkAsync.SaveChanges();




            }
            catch (Exception ex)
            {
                //handle logging here
                var message = ex.Message;
                throw ex;

            }
            return messages;



        }

       public static AnewluvMessages updatelifestylesearchsettings(LifeStyleSearchSettingsModel model, searchsetting p, AnewluvMessages messages,IUnitOfWorkAsync _unitOfWorkAsync)
        {

            try
            {


                // AnewluvMessages messages = new AnewluvMessages();
                //searchsetting p = searchsettingdetailtypeEnum..Repository<searchsetting>().Queryable().Where(z => z.id == model.searchid || z.profile_id == model.profileid || z.searchname == model.searchname).FirstOrDefault();
                //create a new messages object
                if (p == null)
                {
                    messages.errormessages.Add("There is no search with this parameters, lifestype searchs");
                    return messages;
                }


                //checkbos item updates 
                if (model.educationlevellist.Count > 0)
                    updatesearchsettingsdetail(model.educationlevellist, p, searchsettingdetailtypeEnum.educationlevel ,_unitOfWorkAsync);

                if (model.lookingforlist.Count > 0)
                    updatesearchsettingsdetail(model.lookingforlist.ToList(), p, searchsettingdetailtypeEnum.lookingfor ,_unitOfWorkAsync);

                if (model.employmentstatuslist.Count > 0)
                    updatesearchsettingsdetail(model.employmentstatuslist.ToList(), p, searchsettingdetailtypeEnum.employmentstatus ,_unitOfWorkAsync);

                if (model.havekidslist.Count > 0)
                    updatesearchsettingsdetail(model.havekidslist.ToList(), p, searchsettingdetailtypeEnum.havekids ,_unitOfWorkAsync);

                if (model.incomelevellist.Count > 0)
                    updatesearchsettingsdetail(model.incomelevellist.ToList(), p, searchsettingdetailtypeEnum.incomelevel ,_unitOfWorkAsync);

                if (model.livingsituationlist.Count > 0)
                    updatesearchsettingsdetail(model.livingsituationlist.ToList(), p, searchsettingdetailtypeEnum.livingsituation ,_unitOfWorkAsync);

                if (model.maritalstatuslist.Count > 0)
                    updatesearchsettingsdetail(model.maritalstatuslist.ToList(), p, searchsettingdetailtypeEnum.maritialstatus ,_unitOfWorkAsync);

                if (model.professionlist.Count > 0)
                    updatesearchsettingsdetail(model.professionlist.ToList(), p, searchsettingdetailtypeEnum.profession ,_unitOfWorkAsync);

                if (model.wantskidslist.Count > 0)
                    updatesearchsettingsdetail(model.wantskidslist.ToList(), p, searchsettingdetailtypeEnum.wantskids ,_unitOfWorkAsync);


                var i = _unitOfWorkAsync.SaveChanges();



            }
            catch (Exception ex)
            {
                //handle logging here
                var message = ex.Message;
                throw ex;

            }
            return messages;



        }


        #endregion

        #region "PRIVATE Checkbox Update Functions for seaerch settings many to many"


        //Basic Checkbox settings updates
        //APPEARANCE checkboxes start  /////////////////////////
        //profiledata gender
       public static bool updatesearchsettingsdetail(List<listitem> searchitems, searchsetting currentsearchsettings, searchsettingdetailtypeEnum searchsettingtype,IUnitOfWorkAsync _unitOfWorkAsync)
        {

           bool profileupdated = false;
           if (searchitems != null && searchitems.Count() == 0) return false;

           //TODO add validation of the ids , need a method that returns the valid ids for the search setting type

            try
            {

                //only get the selected values 
                foreach (var item in searchitems)
                {
                    //new logic : if this item was selected and is not already in the search settings gender values add it 
                    if ((!currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingtype).Any(f => item.selected == true && f.value == item.id)))
                    {
                        //SearchSettings_showme.showmeID = showme.showmeID;
                        var temp = new searchsettingdetail();
                        temp.searchsetting_id = currentsearchsettings.id;
                        temp.value = item.id; //add the current gender value since its new.
                        temp.creationdate = DateTime.Now;
                        temp.searchsettingdetailtype_id = (int)searchsettingtype;
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Insert(temp);
                        profileupdated = true;
                    }
                    else
                    {
                        //we have an existing value and we want to remove it in this case since selected was false for sure
                        //we will be doing a remove either way

                        if ((currentsearchsettings.details.Where(m => m.searchsettingdetailtype_id == (int)searchsettingtype).Any(f => item.selected == false && f.value == item.id)))
                        {
                            var temp = _unitOfWorkAsync.Repository<searchsettingdetail>()
                           .Queryable()
                           .Where(p => p.searchsetting_id == currentsearchsettings.id && p.searchsettingdetailtype_id == (int)searchsettingtype && p.value == item.id).FirstOrDefault();
                            _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                            profileupdated = true;
                        }
                    }


                }
             }
             catch (Exception ex)
             {
                 throw ex;
             }
            return profileupdated;
        }

        //profiledata location
       public static void updatesearchsettingslocation(List<searchsetting_location> updatedlocations, searchsetting currentsearchsettings,IUnitOfWorkAsync _unitOfWorkAsync)
        {
            if (updatedlocations == null)
            {
                return;
            }

            //only get the selected values 
            foreach (var item in updatedlocations.Where(z => z.countryid != null))
            {
                //new logic : if this item was selected and is not already in the search settings gender values add it 
                if (currentsearchsettings.locations.Where(m => m.countryid == item.countryid && m.city == item.city && m.postalcode == item.postalcode).FirstOrDefault() == null)
                {
                    //SearchSettings_showme.showmeID = showme.showmeID;
                    var temp = new searchsetting_location();
                    temp.searchsetting_id = currentsearchsettings.id;
                    temp.countryid = item.countryid; //add the current gender value since its new.
                    temp.postalcode = item.postalcode;
                    temp.city = item.city;
                    _unitOfWorkAsync.Repository<searchsetting_location>().Insert(temp);

                }
                else
                {
                    //we have an existing value and we want to remove it in this case since selected was false for sure
                    //we will be doing a remove either way
                    var temp = _unitOfWorkAsync.Repository<searchsetting_location>().Queryable().Where(m => m.countryid == item.countryid && m.city == item.city && m.postalcode == item.postalcode).FirstOrDefault();
                    if (temp != null)
                        _unitOfWorkAsync.Repository<searchsettingdetail>().Delete(temp);
                }


            }



        }

        //END of Basic settings ///////////////////////


        #endregion

    }
}
