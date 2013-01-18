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
       
       public LifeStyleSettingsModel getlifestylesearchsettings(int searchid)
        {
            LifeStyleSettingsModel returnmodel = new LifeStyleSettingsModel();


            try
            {
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
