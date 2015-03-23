using Anewluv.Domain;
using Anewluv.Domain.Data;
using AnewLuvFTS.DomainAndData.Models;
using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misc
{
    class Utils
    {

        public static void fixmissingprofiledataandmetadata(AnewluvFTSContext olddb, AnewluvContext context, string oldprofileid, int newprofileid)
        {


            var oldprofiledata = olddb.ProfileDatas.Where(p => p.ProfileID == oldprofileid).FirstOrDefault();
            var myprofile = context.profiles.Where(p => p.id == newprofileid).FirstOrDefault();
         

            var myopenids = new List<openid>();
            var mylogtimes = new List<userlogtime>();

            //if (oldprofiledata != null && myprofile.profilemetadata == null && myprofile != null)
            //{

            //    myprofile.profilemetadata = new profilemetadata { profile_id = myprofile.id, ObjectState = ObjectState.Added, profile = null, profiledata = null };
            //    context.SaveChanges();


            //}
            if (oldprofiledata != null && myprofile.profiledata == null && myprofile !=null)
            {
                try
                {

                    Console.WriteLine("attempting to to create missing profile data for   :" + oldprofileid + "to the new database with" + "new profileid : " + newprofileid.ToString());
                    //query the profile data                        
                    // Metadata classes are not meant to be instantiated.
                    context.profiledatas.Add(new profiledata
                    {
                        profile_id = newprofileid, //newprofileid;
                        age = oldprofiledata.Age,
                        birthdate = oldprofiledata.Birthdate,
                        city = oldprofiledata.City,
                        countryregion = oldprofiledata.Country_Region,
                        stateprovince = oldprofiledata.State_Province,
                        countryid = oldprofiledata.CountryID,
                        longitude = oldprofiledata.Longitude,
                        latitude = oldprofiledata.Latitude,
                        aboutme = oldprofiledata.AboutMe,
                        height = (long)oldprofiledata.Height.GetValueOrDefault(),
                        mycatchyintroLine = oldprofiledata.MyCatchyIntroLine,
                        phone = oldprofiledata.Phone,
                        postalcode = oldprofiledata.PostalCode,
                        lu_gender = context.lu_gender.Where(p => p.id == oldprofiledata.GenderID).FirstOrDefault()
                        ,
                        lu_bodytype = context.lu_bodytype.Where(p => p.id == oldprofiledata.BodyTypeID).FirstOrDefault()
                        ,
                        lu_eyecolor = context.lu_eyecolor.Where(p => p.id == oldprofiledata.EyeColorID).FirstOrDefault()
                        ,
                        lu_haircolor = context.lu_haircolor.Where(p => p.id == oldprofiledata.HairColorID).FirstOrDefault()
                        ,
                        lu_diet = context.lu_diet.Where(p => p.id == oldprofiledata.DietID).FirstOrDefault()
                        ,
                        lu_drinks = context.lu_drinks.Where(p => p.id == oldprofiledata.DrinksID).FirstOrDefault()
                        ,
                        lu_exercise = context.lu_exercise.Where(p => p.id == oldprofiledata.ExerciseID).FirstOrDefault()
                        ,
                        lu_humor = context.lu_humor.Where(p => p.id == oldprofiledata.HumorID).FirstOrDefault()
                        ,
                        lu_politicalview = context.lu_politicalview.Where(p => p.id == oldprofiledata.PoliticalViewID).FirstOrDefault()
                        ,
                        lu_religion = context.lu_religion.Where(p => p.id == oldprofiledata.ReligionID).FirstOrDefault()
                        ,
                        lu_religiousattendance = context.lu_religiousattendance.Where(p => p.id == oldprofiledata.ReligiousAttendanceID).FirstOrDefault()
                        ,
                        lu_sign = context.lu_sign.Where(p => p.id == oldprofiledata.SignID).FirstOrDefault()
                        ,
                        lu_smokes = context.lu_smokes.Where(p => p.id == oldprofiledata.SmokesID).FirstOrDefault()
                        ,
                        lu_educationlevel = context.lu_educationlevel.Where(p => p.id == oldprofiledata.EducationLevelID).FirstOrDefault()
                        ,
                        lu_employmentstatus = context.lu_employmentstatus.Where(p => p.id == oldprofiledata.EmploymentSatusID).FirstOrDefault()
                        ,
                        lu_havekids = context.lu_havekids.Where(p => p.id == oldprofiledata.HaveKidsId).FirstOrDefault()
                        ,
                        lu_incomelevel = context.lu_incomelevel.Where(p => p.id == oldprofiledata.IncomeLevelID).FirstOrDefault()
                        ,
                        lu_livingsituation = context.lu_livingsituation.Where(p => p.id == oldprofiledata.LivingSituationID).FirstOrDefault()
                        ,
                        lu_maritalstatus = context.lu_maritalstatus.Where(p => p.id == oldprofiledata.MaritalStatusID).FirstOrDefault()
                        ,
                        lu_profession = context.lu_profession.Where(p => p.id == oldprofiledata.ProfessionID).FirstOrDefault()
                        ,
                        lu_wantskids = context.lu_wantskids.Where(p => p.id == oldprofiledata.WantsKidsID).FirstOrDefault(),
                        ObjectState = ObjectState.Added
                    });
                    //visiblity settings was never implemented anyways.
                    // myprofiledata.visibilitysettings=  context.visibilitysettings.Where(p => p.id   == item.Prof).FirstOrDefault();     


                    //myprofilemetadata.profile_id = myprofile.id;


                    //call create mail here


                    //add openids

                    //mail stuff 





                    //add the two new objects to profile
                    //********************************
                    //myprofile.profiledata = myprofiledata;

                    myprofile.profilemetadata = new profilemetadata { profile_id = myprofile.id, ObjectState = ObjectState.Added, profile = null, profiledata = null };
                    context.SaveChanges();

                }
                catch (Exception ex)
                {

                    var dd = ex.ToString();
                }
                finally
                {
                    //iccrement on faliture too
                    // profilecount = profilecount + 1;
                    Console.WriteLine("profile repair complete ");
                }
            }
            else
            {
                Console.WriteLine("profile data already exists");
            }

            
        }



    }
}