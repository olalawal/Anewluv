using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anewluv.Domain;
//using Nmedia.Infrastructure;
using Anewluv.Domain.Data;
using Nmedia.Infrastructure;
using Repository.Pattern.Infrastructure;

namespace Anewluv.Domain.Migrations
{
    public class SeedMethods
    {

        public static void seedgenerallookups(AnewluvContext context)
        {

            //abusetypes
            //filter an enum for not set since that is the zero value i.e  
            var abusetypeqry = from abusetypeEnum value in Enum.GetValues(typeof(abusetypeEnum))
                               where value != abusetypeEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            abusetypeqry.ToList().ForEach(kvp => context.lu_abusetype.Add(new lu_abusetype()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //actiontypes
            //filter an enum for not set since that is the zero value i.e  
            var actiontypeqry = from actiontypeEnum value in Enum.GetValues(typeof(actiontypeEnum))                              
                               orderby value // to sort by value; remove otherwise 
                               select value;
            actiontypeqry.ToList().ForEach(kvp => context.lu_actiontype.AddOrUpdate(new lu_actiontype()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            context.SaveChanges();


            //9-12-2012
            //added for profile activity type
            //activitytype
            //filter an enum for not set since that is the zero value i.e  
            var activitytypeqry = from activitytypeEnum value in Enum.GetValues(typeof(activitytypeEnum))
                                  where value != activitytypeEnum.NotSet
                                  orderby value // to sort by value; remove otherwise 
                                  select value;
            activitytypeqry.ToList().ForEach(kvp => context.lu_activitytype.AddOrUpdate(new lu_activitytype()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added, 
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

      
           
            //  //applicationtransfertypes
            ////filter an enum for not set since that is the zero value i.e  
            //var applicationtransfertypeqry = from  value in Enum.GetValues(typeof(applicationtransfertypeEnum))
            //                   where value != applicationtransfertypeEnum.NotSet
            //                   orderby value // to sort by value; remove otherwise 
            //                   select value;
            //applicationtransfertypeqry.ToList().ForEach(kvp => context.lu_applicationtransfertype.AddOrUpdate(new lu_applicationtransfertype()
            //{
            //    id = (int)kvp,  ObjectState = ObjectState.Added,
            //    description = EnumExtensionMethods.ToDescription(kvp)
            //}));


          


            //3-25-2013 olawal
            //added for open id
            //openidprovider
            //filter an enum for not set since that is the zero value i.e  
            var openidproviderqry = from openidproviderEnum value in Enum.GetValues(typeof(openidproviderEnum))                                 
                                  orderby value // to sort by value; remove otherwise 
                                  select value;
            openidproviderqry.ToList().ForEach(kvp => context.lu_openidprovider.AddOrUpdate(new lu_openidprovider()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //notetypes addded 9/11/2012
            //filter an enum for not set since that is the zero value i.e  
            var notetypeqry = from notetypeEnum value in Enum.GetValues(typeof(notetypeEnum))                          
                               orderby value // to sort by value; remove otherwise 
                               select value;
            notetypeqry.ToList().ForEach(kvp => context.lu_notetype.AddOrUpdate(new lu_notetype ()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //defaultmailboxfoldertypes
            //filter an enum for not set since that is the zero value i.e  
            var defaultmailboxfoldertypeqry = from defaultmailboxfoldertypeEnum value in Enum.GetValues(typeof(defaultmailboxfoldertypeEnum))
                               where value != defaultmailboxfoldertypeEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            defaultmailboxfoldertypeqry.ToList().ForEach(kvp => context.lu_defaultmailboxfolder.AddOrUpdate(new lu_defaultmailboxfolder()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


           //genders
            //filter an enum for not set since that is the zero value i.e  
            var genderqry = from genderEnum value in Enum.GetValues(typeof(genderEnum))
                               where value != genderEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            genderqry.ToList().ForEach(kvp => context.lu_gender.AddOrUpdate(new lu_gender()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));




            //flagyesno
            //filter an enum for not set since that is the zero value i.e  
            var flagyesnoqry = from flagyesnoEnum  value in Enum.GetValues(typeof(flagyesnoEnum))
                            where value != flagyesnoEnum.NotSet
                            orderby value // to sort by value; remove otherwise 
                            select value;
            flagyesnoqry.ToList().ForEach(kvp => context.lu_flagyesno.AddOrUpdate(new lu_flagyesno()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //photoapprovalstatuss
            //filter an enum for not set since that is the zero value i.e  
            var photoapprovalstatusqry = from photoapprovalstatusEnum value in Enum.GetValues(typeof(photoapprovalstatusEnum))
                               where value != photoapprovalstatusEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;       
            photoapprovalstatusqry.ToList().ForEach(kvp => context.lu_photoapprovalstatus.AddOrUpdate(new lu_photoapprovalstatus()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));



            // nonZeroProjectIds.ForEach(p => UrisByProject.Add(p, urisForTheImage[nonZeroProjectIds.FindIndex(t => t == p)]));

            List<string> rejectionreasons = new List<string>();
            List<string> rejectionreasonmessages = new List<string>();

            //photorejectionreasons
            //filter an enum for not set since that is the zero value i.e  
            var photorejectionreasonmessagesqry = from photorejectionreasonusermessageEnum value in Enum.GetValues(typeof(photorejectionreasonusermessageEnum))
                                          where value != photorejectionreasonusermessageEnum.NotSet
                                          orderby value // to sort by value; remove otherwise 
                                          select value;                                 

            var photorejectionreasonqry  = from photorejectionreasonEnum value in Enum.GetValues(typeof(photorejectionreasonEnum))
                               where value != photorejectionreasonEnum.NotSet
                               orderby value // to sort by value; remove otherwise
                               select value;

            foreach (var value in photorejectionreasonqry) 
            { 
                rejectionreasons.Add(EnumExtensionMethods.ToDescription(value));
                System.Diagnostics.Debug.WriteLine(EnumExtensionMethods.ToDescription(value));
            }


            foreach (var value in photorejectionreasonmessagesqry)
            {
                rejectionreasonmessages.Add(EnumExtensionMethods.ToDescription(value));
                System.Diagnostics.Debug.WriteLine(EnumExtensionMethods.ToDescription(value));
            }

            Dictionary<string, string> UrisByProject = new Dictionary<string, string>();
            rejectionreasons.ForEach(p => UrisByProject.Add(p, rejectionreasonmessages[rejectionreasons.FindIndex(t => t == p)]));

            var counter = 1;
            foreach (var value in UrisByProject)
            {
                lu_photorejectionreason newitem = new lu_photorejectionreason();
                newitem.id = counter;
                newitem.description = value.Key;
                newitem.userMessage = value.Value ;
                System.Diagnostics.Debug.WriteLine(value.Key + "," + value.Value);
                context.lu_photorejectionreason.AddOrUpdate(newitem);
                counter = counter + 1;
            }


            //End of photo rejection stuff
        

            //photosizes
            //filter an enum for not set since that is the zero value i.e  
            var photosizeqry = from photoImageresizerformatEnum value in Enum.GetValues(typeof(photoImageresizerformatEnum))
                               where value != photoImageresizerformatEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            photosizeqry.ToList().ForEach(kvp => context.lu_photoImagersizerformat.AddOrUpdate(new lu_photoImagersizerformat()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //has to come after image resizer format.
             //photoformats
            //filter an enum for not set since that is the zero value i.e  
            var photoformatqry = from photoformatEnum value in Enum.GetValues(typeof(photoformatEnum))
                               where value != photoformatEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            photoformatqry.ToList().ForEach(kvp => context.lu_photoformat.AddOrUpdate(new lu_photoformat()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp),
                photoImagersizerformat_id = (int)kvp  //this only works since these loookups have same order


            }));



            //photostatuss
            //filter an enum for not set since that is the zero value i.e  
            var photostatusqry = from photostatusEnum value in Enum.GetValues(typeof(photostatusEnum))
                               where value != photostatusEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            photostatusqry.ToList().ForEach(kvp => context.lu_photostatus.AddOrUpdate(new lu_photostatus()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //photoimagetypes
            //filter an enum for not set since that is the zero value i.e  
            var photoimagetypeqry = from photoimagetypeEnum value in Enum.GetValues(typeof(photoimagetypeEnum))
                                 where value != photoimagetypeEnum.NotSet
                                 orderby value // to sort by value; remove otherwise 
                                 select value;
            photoimagetypeqry.ToList().ForEach(kvp => context.lu_photoimagetype.AddOrUpdate(new lu_photoimagetype()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //profilestatuss
            //filter an enum for not set since that is the zero value i.e  
            var profilestatusqry = from profilestatusEnum value in Enum.GetValues(typeof(profilestatusEnum))
                               where value != profilestatusEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            profilestatusqry.ToList().ForEach(kvp => context.lu_profilestatus.AddOrUpdate(new lu_profilestatus()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //roles
            //filter an enum for not set since that is the zero value i.e  
            var roleqry = from roleEnum value in Enum.GetValues(typeof(roleEnum))
                               where value != roleEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            roleqry.ToList().ForEach(kvp => context.lu_role.AddOrUpdate(new lu_role()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //securityleveltypes
            //filter an enum for not set since that is the zero value i.e  
            var securityleveltypeqry = from securityleveltypeEnum value in Enum.GetValues(typeof(securityleveltypeEnum))
                               where value != securityleveltypeEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            securityleveltypeqry.ToList().ForEach(kvp => context.lu_securityleveltype.AddOrUpdate(new lu_securityleveltype()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


         
            //ssearchsettingdetail
            //filter an enum for not set since that is the zero value i.e  
            var searchsettingdetailtypeqry = from searchsettingdetailtypeEnum value in Enum.GetValues(typeof(searchsettingdetailtypeEnum))                             
                               orderby value // to sort by value; remove otherwise 
                               select value;
            searchsettingdetailtypeqry.ToList().ForEach(kvp => context.lu_searchsettingdetailtype.AddOrUpdate(new lu_searchsettingdetailtype()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


               //securityquestions
            //filter an enum for not set since that is the zero value i.e  
            var securityquestionqry = from securityquestionEnum value in Enum.GetValues(typeof(securityquestionEnum))                            
                               orderby value // to sort by value; remove otherwise 
                               select value;
            securityquestionqry.ToList().ForEach(kvp => context.lu_securityquestion.AddOrUpdate(new lu_securityquestion()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            

            //showmes
            //filter an enum for not set since that is the zero value i.e  
            var showmeqry = from showmeEnum value in Enum.GetValues(typeof(showmeEnum))
                               where value != showmeEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            showmeqry.ToList().ForEach(kvp => context.lu_showme.AddOrUpdate(new lu_showme()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //sortbytypes
            //filter an enum for not set since that is the zero value i.e  
            var sortbytypeqry = from sortbytypeEnum value in Enum.GetValues(typeof(sortbytypeEnum))
                            where value != sortbytypeEnum.NotSet
                            orderby value // to sort by value; remove otherwise 
                            select value;
            sortbytypeqry.ToList().ForEach(kvp => context.lu_sortbytype.AddOrUpdate(new lu_sortbytype()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));
          
        }

        public static void seedcriterialookups(AnewluvContext context)
        {

            //bodytypes
            //filter an enum for not set since that is the zero value i.e  
            var bodytypeqry = from bodytypeEnum value in Enum.GetValues(typeof(bodytypeEnum))
                                where value != bodytypeEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            bodytypeqry.ToList().ForEach(kvp => context.lu_bodytype.AddOrUpdate(new lu_bodytype()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //diets
            //filter an enum for not set since that is the zero value i.e  
            var dietqry = from dietEnum value in Enum.GetValues(typeof(dietEnum))
                                where value != dietEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            dietqry.ToList().ForEach(kvp => context.lu_diet.AddOrUpdate(new lu_diet()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //drinkss
            //filter an enum for not set since that is the zero value i.e  
            var drinksqry = from drinksEnum value in Enum.GetValues(typeof(drinksEnum))
                                where value != drinksEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            drinksqry.ToList().ForEach(kvp => context.lu_drinks.AddOrUpdate(new lu_drinks()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //educationlevels
            //filter an enum for not set since that is the zero value i.e  
            var educationlevelqry = from educationlevelEnum value in Enum.GetValues(typeof(educationlevelEnum))
                                where value != educationlevelEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            educationlevelqry.ToList().ForEach(kvp => context.lu_educationlevel.AddOrUpdate(new lu_educationlevel()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //employmentstatuss
            //filter an enum for not set since that is the zero value i.e  
            var employmentstatusqry = from employmentstatusEnum value in Enum.GetValues(typeof(employmentstatusEnum))
                                where value != employmentstatusEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            employmentstatusqry.ToList().ForEach(kvp => context.lu_employmentstatus.AddOrUpdate(new lu_employmentstatus()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //ethnicitys
            //filter an enum for not set since that is the zero value i.e  
            var ethnicityqry = from ethnicityEnum value in Enum.GetValues(typeof(ethnicityEnum))
                                where value != ethnicityEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            ethnicityqry.ToList().ForEach(kvp => context.lu_ethnicity.AddOrUpdate(new lu_ethnicity()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //exercises
            //filter an enum for not set since that is the zero value i.e  
            var exerciseqry = from exerciseEnum value in Enum.GetValues(typeof(exerciseEnum))
                               where value != exerciseEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            exerciseqry.ToList().ForEach(kvp => context.lu_exercise.AddOrUpdate(new lu_exercise()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //eyecolors
            //filter an enum for not set since that is the zero value i.e  
            var eyecolorqry = from eyecolorEnum value in Enum.GetValues(typeof(eyecolorEnum))
                                where value != eyecolorEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            eyecolorqry.ToList().ForEach(kvp => context.lu_eyecolor.AddOrUpdate(new lu_eyecolor()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //haircolors
            //filter an enum for not set since that is the zero value i.e  
            var haircolorqry = from haircolorEnum value in Enum.GetValues(typeof(haircolorEnum))
                                where value != haircolorEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            haircolorqry.ToList().ForEach(kvp => context.lu_haircolor.AddOrUpdate(new lu_haircolor()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //havekidss
            //filter an enum for not set since that is the zero value i.e  
            var havekidsqry = from havekidsEnum value in Enum.GetValues(typeof(havekidsEnum))
                                where value != havekidsEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            havekidsqry.ToList().ForEach(kvp => context.lu_havekids.AddOrUpdate(new lu_havekids()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //hobbys
            //filter an enum for not set since that is the zero value i.e  
            var hobbyqry = from hobbyEnum value in Enum.GetValues(typeof(hobbyEnum))
                                where value != hobbyEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            hobbyqry.ToList().ForEach(kvp => context.lu_hobby.AddOrUpdate(new lu_hobby()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //hotfeatures
            //filter an enum for not set since that is the zero value i.e  
            var hotfeatureqry = from hotfeatureEnum value in Enum.GetValues(typeof(hotfeatureEnum))
                                where value != hotfeatureEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            hotfeatureqry.ToList().ForEach(kvp => context.lu_hotfeature.AddOrUpdate(new lu_hotfeature()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //humors
            //filter an enum for not set since that is the zero value i.e  
            var humorqry = from humorEnum value in Enum.GetValues(typeof(humorEnum))
                                where value != humorEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            humorqry.ToList().ForEach(kvp => context.lu_humor.AddOrUpdate(new lu_humor()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //incomelevels
            //filter an enum for not set since that is the zero value i.e  
            var incomelevelqry = from incomelevelEnum value in Enum.GetValues(typeof(incomelevelEnum))
                                where value != incomelevelEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            incomelevelqry.ToList().ForEach(kvp => context.lu_incomelevel.AddOrUpdate(new lu_incomelevel()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //livingsituations
            //filter an enum for not set since that is the zero value i.e  
            var livingsituationqry = from livingsituationEnum value in Enum.GetValues(typeof(livingsituationEnum))
                                where value != livingsituationEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            livingsituationqry.ToList().ForEach(kvp => context.lu_livingsituation.AddOrUpdate(new lu_livingsituation()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //lookingfors
            //filter an enum for not set since that is the zero value i.e  
            var lookingforqry = from lookingforEnum value in Enum.GetValues(typeof(lookingforEnum))
                                where value != lookingforEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            lookingforqry.ToList().ForEach(kvp => context.lu_lookingfor.AddOrUpdate(new lu_lookingfor()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //maritalstatuss
            //filter an enum for not set since that is the zero value i.e  
            var maritalstatusqry = from maritalstatusEnum value in Enum.GetValues(typeof(maritalstatusEnum))
                                where value != maritalstatusEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            maritalstatusqry.ToList().ForEach(kvp => context.lu_maritalstatus.AddOrUpdate(new lu_maritalstatus()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //professions
            //filter an enum for not set since that is the zero value i.e  
            var professionqry = from professionEnum value in Enum.GetValues(typeof(professionEnum))
                                where value != professionEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            professionqry.ToList().ForEach(kvp => context.lu_profession.AddOrUpdate(new lu_profession()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //religions
            //filter an enum for not set since that is the zero value i.e  
            var religionqry = from religionEnum value in Enum.GetValues(typeof(religionEnum))
                                where value != religionEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            religionqry.ToList().ForEach(kvp => context.lu_religion.AddOrUpdate(new lu_religion()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));



            //politicals
            //filter an enum for not set since that is the zero value i.e  
            var politicalqry = from politicalviewEnum value in Enum.GetValues(typeof(politicalviewEnum))
                              where value != politicalviewEnum.NotSet
                              orderby value // to sort by value; remove otherwise 
                              select value;
            politicalqry.ToList().ForEach(kvp => context.lu_politicalview.AddOrUpdate(new lu_politicalview()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));



            //religiousattendances
            //filter an enum for not set since that is the zero value i.e  
            var religiousattendanceqry = from religiousattendanceEnum value in Enum.GetValues(typeof(religiousattendanceEnum))
                                where value != religiousattendanceEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            religiousattendanceqry.ToList().ForEach(kvp => context.lu_religiousattendance.AddOrUpdate(new lu_religiousattendance()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //signs
            //filter an enum for not set since that is the zero value i.e  
            var signqry = from signEnum value in Enum.GetValues(typeof(signEnum))
                                where value != signEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            signqry.ToList().ForEach(kvp => context.lu_sign.AddOrUpdate(new lu_sign()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //smokess
            //filter an enum for not set since that is the zero value i.e  
            var smokesqry = from smokesEnum value in Enum.GetValues(typeof(smokesEnum))
                                where value != smokesEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            smokesqry.ToList().ForEach(kvp => context.lu_smokes.AddOrUpdate(new lu_smokes()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //wantskidss
            //filter an enum for not set since that is the zero value i.e  
            var wantskidsqry = from wantskidsEnum value in Enum.GetValues(typeof(wantskidsEnum))
                                where value != wantskidsEnum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
            wantskidsqry.ToList().ForEach(kvp => context.lu_wantskids.AddOrUpdate(new lu_wantskids()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //2-13-2013 olawa lseed method for filter types addded late
            //profilefiltertypes
            //filter an enum for not set since that is the zero value i.e  
            var profilefiltertypeqry = from profilefiltertypeEnum value in Enum.GetValues(typeof(profilefiltertypeEnum))
                               where value != profilefiltertypeEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            profilefiltertypeqry.ToList().ForEach(kvp => context.lu_profilefiltertype.AddOrUpdate(new lu_profilefiltertype()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

          
        }

        public static void seedapplicationlookups(AnewluvContext context)
        {

            //has to come after image resizer format.
            //iconformats
            //filter an enum for not set since that is the zero value i.e  
            var iconformatqry = from iconformatEnum value in Enum.GetValues(typeof(iconformatEnum))
                                 where value != iconformatEnum.NotSet
                                 orderby value // to sort by value; remove otherwise 
                                 select value;
            iconformatqry.ToList().ForEach(kvp => context.lu_iconformat.AddOrUpdate(new lu_iconformat()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp),
                iconImagersizerformat_id = (int)kvp  //this only works since these loookups have same order


            }));



            //applicationpaymenttype
            //filter an enum for not set since that is the zero value i.e  
            var applicationpaymenttypeqry = from applicationpaymenttypeEnum value in Enum.GetValues(typeof(applicationpaymenttypeEnum))
                                            where value != applicationpaymenttypeEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            applicationpaymenttypeqry.ToList().ForEach(kvp => context.lu_applicationpaymenttype.AddOrUpdate(new lu_applicationpaymenttype()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //applicationitemtransfertype
            //filter an enum for not set since that is the zero value i.e  
            var applicationitemtransfertypeqry = from applicationitemtransfertypeEnum value in Enum.GetValues(typeof(applicationitemtransfertypeEnum))
                                           // where value != applicationitemtransfertypeEnum.NotSet
                                            orderby value // to sort by value; remove otherwise 
                                            select value;
            applicationitemtransfertypeqry.ToList().ForEach(kvp => context.lu_applicationtransfertype.AddOrUpdate(new lu_applicationtransfertype()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));



            //applicationtype
            //filter an enum for not set since that is the zero value i.e  
            var applicationtypeqry = from applicationtypeEnum value in Enum.GetValues(typeof(applicationtypeEnum))
                                            where value != applicationtypeEnum.NotSet
                                            orderby value // to sort by value; remove otherwise 
                                            select value;
            applicationtypeqry.ToList().ForEach(kvp => context.lu_applicationtype.AddOrUpdate(new lu_applicationtype()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //iconImageresizerformat
            //filter an enum for not set since that is the zero value i.e  
            var iconImageresizerformatqry = from iconImageresizerformatEnum value in Enum.GetValues(typeof(iconImageresizerformatEnum))
                                            where value != iconImageresizerformatEnum.NotSet
                                            orderby value // to sort by value; remove otherwise 
                                            select value;
            iconImageresizerformatqry.ToList().ForEach(kvp => context.lu_iconImagersizerformat.AddOrUpdate(new lu_iconImagersizerformat()
            {
                id = (int)kvp,  ObjectState = ObjectState.Added,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            context.SaveChanges();

        }
    }
}
