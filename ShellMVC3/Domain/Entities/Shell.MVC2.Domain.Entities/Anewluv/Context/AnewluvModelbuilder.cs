using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{

    public  class anewluvmodelbuilder
    {

        public static void buildgeneralmodels(DbModelBuilder modelBuilder)
        {

            
            // modelBuilder.Entity<message>().ToTable("messages", schemaName: "Logging");
            //modelBuilder.Entity<address>().ToTable("messageAddresses", schemaName: "Logging");
            // modelBuilder.Entity<systemAddress>().ToTable("messageSystemAddresses", schemaName: "Logging");
            // modelBuilder.Entity<lu_template>().ToTable("lu_messageTemplate", schemaName: "Logging");
            // modelBuilder.Entity<lu_messageType>().ToTable("lu_messageType", schemaName: "Logging");
            // modelBuilder.Entity<lu_systemAddressType>().ToTable("lu_messageSystemAddressType", schemaName: "Logging");

            // map required relationships abusereport
            //**************************************
            modelBuilder.Entity<abusereport>().HasRequired(p => p.abuser).WithMany()
           .HasForeignKey(p => p.abuser_id).WillCascadeOnDelete(false);

            //reporter required
            modelBuilder.Entity<abusereport>().HasRequired(p => p.abusereporter).WithMany()
            .HasForeignKey(p => p.abusereporter_id).WillCascadeOnDelete(false);

            // map required relationships abusereportnotes
            //**************************************
            //map abusereport notes to abuse report and set as required
            modelBuilder.Entity<abusereportnotes>().HasRequired(p => p.abusereport).WithMany(z => z.notes)
            .HasForeignKey(p => p.abusereport_id).WillCascadeOnDelete(false);

            //abuse report notes also requires a person creating it i.e profiledata
            modelBuilder.Entity<abusereportnotes>().HasRequired(p => p.profiledata)
            .WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);


            //map requierd  relationshipds for block
            //**************************************
            //blocked  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<block>().HasRequired(t => t.blockedprofiledata).WithMany(z => z.blocks)
            .HasForeignKey(p => p.blockprofile_id).WillCascadeOnDelete(false);

            //blocker required
            modelBuilder.Entity<block>().HasRequired(t => t.profiledata).WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);


            //  //map requierd  relationshipds for CommunicationQoutas
            //  //**************************************
            modelBuilder.Entity<communicationquota>()
           .HasRequired(t => t.updaterprofiledata);

            //  //map requierd  relationshipds for favorite
            //  //**************************************
            //favorite  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<favorite>().HasRequired(t => t.favoriteprofiledata).WithMany(z => z.favorites)
           .HasForeignKey(p => p.favoriteprofile_id).WillCascadeOnDelete(false);

            //favorite sender required
            modelBuilder.Entity<favorite>().HasRequired(t => t.profiledata).WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);

            //  //map requierd  relationshipds for friend
            //  //**************************************
            //friend  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<friend>().HasRequired(t => t.friendprofiledata).WithMany(z => z.friends)
           .HasForeignKey(p => p.friendprofile_id).WillCascadeOnDelete(false);

            //friend sender required
            modelBuilder.Entity<friend>().HasRequired(t => t.profiledata).WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);

            //  //map requierd  relationshipds for hotlist
            //  //**************************************
            //hotlist  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<hotlist>().HasRequired(t => t.hotlistprofiledata).WithMany(z => z.hotlists)
           .HasForeignKey(p => p.hotlistprofile_id).WillCascadeOnDelete(false);

            //hotlist sender required
            modelBuilder.Entity<hotlist>().HasRequired(t => t.profiledata).WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);


            //  //map requierd  relationshipds for interest
            //  //**************************************
            //interest  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<interest>().HasRequired(t => t.interestprofiledata).WithMany(z => z.interests)
           .HasForeignKey(p => p.interestprofile_id).WillCascadeOnDelete(false);

            //interest sender required
            modelBuilder.Entity<interest>().HasRequired(t => t.profiledata).WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);

            //  //map requierd  relationshipds for like
            //  //**************************************
            //like  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<like>().HasRequired(t => t.likeprofiledata).WithMany(z => z.likes)
           .HasForeignKey(p => p.likeprofile_id).WillCascadeOnDelete(false);

            //like sender required
            modelBuilder.Entity<like>().HasRequired(t => t.profiledata).WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);


            //  //map requierd  relationshipds for friend
            //  //**************************************
            //friend  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<friend>().HasRequired(t => t.friendprofiledata).WithMany(z => z.friends)
           .HasForeignKey(p => p.friendprofile_id).WillCascadeOnDelete(false);

            //friend sender required
            modelBuilder.Entity<friend>().HasRequired(t => t.profiledata).WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);


            //  //map requierd  relationshipds for peek
            //  //**************************************
            //peek  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<peek>().HasRequired(t => t.peekprofiledata).WithMany(z => z.peeks)
           .HasForeignKey(p => p.peekprofile_id).WillCascadeOnDelete(false);

            //peek sender required
            modelBuilder.Entity<peek>().HasRequired(t => t.profiledata).WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);

            //mailupdatefreqency
            //  //map requierd  relationshipds for mailupdatefreqency
            //  //**************************************
            modelBuilder.Entity<mailupdatefreqency>()
           .HasRequired(t => t.profiledata);

            //membersinrole
            //  //map requierd  relationshipds for membersinrole
            //  //**************************************
            modelBuilder.Entity<membersinrole>()
           .HasRequired(t => t.profile);

            //openid
            //  //map requierd  relationshipds for openid
            //  //**************************************
            modelBuilder.Entity<openid>()
           .HasRequired(t => t.profile);

            //photo complex model requered field mappings
            // map required relationships abusereport
            //**************************************
            modelBuilder.Entity<photo>()
            .HasRequired(p => p.profiledata);

            // map required relationships photoalbum -none needed
            //**************************************

            //photoconversion model requered field mappings
            // map required relationships photoconversion
            //**************************************
            modelBuilder.Entity<photoconversion>()
            .HasRequired(p => p.photo);
            //type is requred too
            modelBuilder.Entity<photoconversion>()
           .HasRequired(p => p.type);


            //photoreviewstatus model requered field mappings
            // map required relationships photoreviewstatus
            //**************************************
            modelBuilder.Entity<photoreviewstatus>()
            .HasRequired(p => p.reviewerprofiledata);
            //photo is requred too
            modelBuilder.Entity<photoreviewstatus>()
           .HasRequired(p => p.photo);
            //wonder if we should have a review status type as required



            //Profile
            // Specify one-to-one association between profile and profiledata
            //*****************************************************************
            modelBuilder.Entity<profile>()
           .HasRequired(t => t.profiledata).WithRequiredPrincipal(t => t.profile);
            //status is required         
            modelBuilder.Entity<profile>()
           .HasRequired(p => p.status);

            //Profiledata
            //Specify one-to-one association between profile and profiledata
            modelBuilder.Entity<profiledata>()
           .HasOptional(t => t.visibilitysettings).WithOptionalPrincipal(t => t.profiledata);
            //gender is required         
            modelBuilder.Entity<profiledata>()
           .HasRequired(p => p.gender);

            //set one to many relation ship with searchsettings                 
            modelBuilder.Entity<profiledata>()
           .HasMany(p => p.searchsettings);

            //profiledata_ethnicity complex model requered field mappings
            // map required relationships profiledata_ethnicity
            //**************************************
            modelBuilder.Entity<profiledata_ethnicity>()
            .HasRequired(p => p.profiledata);

            //profiledata_hobby complex model requered field mappings
            // map required relationships profiledata_hobby
            //**************************************
            modelBuilder.Entity<profiledata_hobby>()
            .HasRequired(p => p.profiledata);

            //profiledata_hotfeature complex model requered field mappings
            // map required relationships profiledata_hotfeature
            //**************************************
            modelBuilder.Entity<profiledata_hotfeature>()
            .HasRequired(p => p.profiledata);

            //profiledata_lookingfor complex model requered field mappings
            // map required relationships profiledata_lookingfor
            //**************************************
            modelBuilder.Entity<profiledata_lookingfor>()
            .HasRequired(p => p.profiledata);


            //rating value 
            //rating complex model requered field mappings
            // map required relationships rating value
            //**************************************
            modelBuilder.Entity<ratingvalue>()
           .HasRequired(p => p.rating); //.WithMany(z=>z.ratingvalues).HasForeignKey(p=>p.id).WillCascadeOnDelete(false);
            
            //  //map requierd  relationshipds for favorite
            //  //**************************************
            //favorite  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<ratingvalue>().HasRequired(t => t.rateeprofiledata ).WithMany(z => z.ratingvalues )
           .HasForeignKey(p => p.rateeprofile_id ).WillCascadeOnDelete(false);

            //favorite sender required
            modelBuilder.Entity<ratingvalue>().HasRequired(t => t.profiledata).WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);

            //userlogtime
            modelBuilder.Entity<userlogtime>()
<<<<<<< HEAD
         .HasRequired(p => p.profile);

            //visiblitysetting
         //   modelBuilder.Entity<visiblitysetting>()
         // .HasRequired(p => p.profiledata);


            //  //map requierd  relationshipds for friend
            //  //**************************************
            //  //favorites profiledata  reqired
            //  modelBuilder.Entity<friend>()
            // .HasRequired(t => t.friendprofiledata)
            // .WithRequiredPrincipal();
            //  //member sendings friend profiledata required
            //  modelBuilder.Entity<friend>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();


            //  //map hobby required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<hobby>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map hotfeature required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<hotfeature>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map requierd  relationshipds for hotlist
            //  //**************************************
            //  //favorites profiledata  reqired
            //  modelBuilder.Entity<hotlist>()
            // .HasRequired(t => t.hotlistprofiledata )
            // .WithRequiredPrincipal();
            //  //member sendings hotlist profiledata required
            //  modelBuilder.Entity<hotlist>()
            // .HasRequired(t => t.profile )
            // .WithRequiredPrincipal();

            //  //map requierd  relationshipds for Like
            //  //**************************************
            //  //favorites profiledata  reqired
            //  modelBuilder.Entity<like>()
            // .HasRequired(t => t.likeprofiledata )
            // .WithRequiredPrincipal();
            //  //member sendings like profiledata required
            //  modelBuilder.Entity<like>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map loginGeodata required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<loginGeodata>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map lookingfor required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<lookingfor>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map lookingfor required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<lookingfor>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map mailupdatefreqency required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<mailupdatefreqency >()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map membersinrole required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<membersinrole>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();
            //  //role required
            //  modelBuilder.Entity<membersinrole>()
            // .HasRequired(t => t.role )
            // .WithRequiredPrincipal();

            //  //map openid required realtionships
            //  //profile  reqired
            //  modelBuilder.Entity<openid>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //map peek required realtionships
            //  //peek profiledata  reqired
            //  modelBuilder.Entity<peek>()
            // .HasRequired(t => t.peekprofiledata )
            // .WithRequiredPrincipal();
            //  //peeker profiledata required
            //  modelBuilder.Entity<peek>()
            // .HasRequired(t => t.profile)
            // .WithRequiredPrincipal();

            //  //fluent config for photo table
            //  //*************************************
            //  // Specifify the generation of guid fields 
            //  modelBuilder.Entity<photo>().Property(o => o.id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //  //set required feild
            //  modelBuilder.Entity<photo>()
            //  .HasRequired(t => t.profile)
            //  .WithRequiredPrincipal()
            //  ;

            //  //map photoconversions required realtionships
            //  //photoconversions profiledata  reqired
            //  modelBuilder.Entity<photoconversion>()
            // .HasRequired(t => t.photo)
            // .WithRequiredPrincipal();
            //  //peeker profiledata required
            //  modelBuilder.Entity<photoconversion>()
            // .HasRequired(t => t.type)
            // .WithRequiredPrincipal();


            //  //fluent config for photoreviewstatus table
            //  //*************************************
            //  modelBuilder.Entity<photoreviewstatus>()
            //  .HasRequired(t => t.reviewerprofiledata)
            //   .WithRequiredPrincipal();

            //  //profile and profiel data are at top

            //  //fluent confg for ratingvalues
            //  //map ratingvalues required realtionships

            // //rating tie back required 
            //  modelBuilder.Entity<ratingvalue>()
            // .HasRequired(t => t.rating )
            // .WithRequiredPrincipal();

            //  //rater profiledata  reqired
            //  modelBuilder.Entity<ratingvalue >()
            // .HasRequired(t => t.raterprofiledata)
            // .WithRequiredPrincipal();
            //  //member being rated profiledata required
            //  modelBuilder.Entity<ratingvalue>()
            // .HasRequired(t => t.rateeprofiledata )
            // .WithRequiredPrincipal();

            //  //user logtime fluent mapping
            //  //profile  reqired
            //  modelBuilder.Entity<userlogtime>()
            // .HasRequired(t => t.profile )
            // .WithRequiredPrincipal();

            //  //visibilitysetting fluent mapping
            //  //profiledata  reqired
            //  //modelBuilder.Entity<visiblitysetting>()
            // //.HasRequired(t => t.profile)
            //// .WithRequiredPrincipal();

=======
           .HasRequired(p => p.profile);
>>>>>>> updated code first DB

        
        }

        public static void buildsearchsettingsmodels(DbModelBuilder modelBuilder)
        {

            //searchsetting
            //  //map requierd  relationshipds for openid
            //  //**************************************
            modelBuilder.Entity<searchsetting>()
           .HasRequired(t => t.profiledata);

            //bodytype
            //**********************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_bodytype>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_bodytype>()
           .HasRequired(t => t.bodytype);


            //diet
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_diet>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_diet>()
           .HasRequired(t => t.diet);

            //drink
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_drink>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_drink>()
           .HasRequired(t => t.drink);

            //educationlevel
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_educationlevel>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_educationlevel>()
           .HasRequired(t => t.educationlevel);

            //employmentstatus
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_employmentstatus>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_employmentstatus>()
           .HasRequired(t => t.employmentstatus);

            //ethnicity
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_ethnicity>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_ethnicity>()
           .HasRequired(t => t.ethnicity);

            //exercise
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_exercise>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_exercise>()
           .HasRequired(t => t.exercise);

            //eyecolor
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_eyecolor>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_eyecolor>()
           .HasRequired(t => t.eyecolor);

            //genders
            //***********************************
            //all the other related tables  now          
           // modelBuilder.Entity<searchsetting>()
           //.HasMany(t => t.genders);

            //TO DO for some reason gender will not do the many part !?
            //all the other related tables  now          
           // modelBuilder.Entity<searchsetting_gender>()
           //.HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_gender>()
           .HasRequired(t => t.gender);

            //haircolor
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_haircolor>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_haircolor>()
           .HasRequired(t => t.haircolor);

            //havekids
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_havekids>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_havekids>()
           .HasRequired(t => t.havekids);

            //hobby
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_hobby>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_hobby>()
           .HasRequired(t => t.hobby);

            //hotfeature
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_hotfeature>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_hotfeature>()
           .HasRequired(t => t.hotfeature);

            //humor
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_humor>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_humor>()
           .HasRequired(t => t.humor);

            //income
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_incomelevel>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_incomelevel>()
           .HasRequired(t => t.incomelevel);

            //livingsituation
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_livingstituation>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_livingstituation>()
           .HasRequired(t => t.livingsituation);

            //location
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_location>()
           .HasRequired(t => t.searchsetting);

            //lookingfor
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_lookingfor>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_lookingfor>()
           .HasRequired(t => t.lookingfor);

            //maritalstatus
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_maritalstatus>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_maritalstatus>()
           .HasRequired(t => t.maritalstatus);

            //politicalview
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_politicalview>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_politicalview>()
           .HasRequired(t => t.politicalview);

            //lookingfor
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_lookingfor>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_lookingfor>()
           .HasRequired(t => t.lookingfor);


            //profession
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_profession>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_profession>()
           .HasRequired(t => t.profession);



            //religion
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_religion>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_religion>()
           .HasRequired(t => t.religion);



            //religiousattendance
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_religiousattendance>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_religiousattendance>()
           .HasRequired(t => t.religiousattendance);



            //showme
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_showme>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_showme>()
           .HasRequired(t => t.showme);



            //sign
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_sign>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_sign>()
           .HasRequired(t => t.sign);



            //smokes
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_smokes>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_smokes>()
           .HasRequired(t => t.smokes);



            //sortbytype
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_sortbytype>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_sortbytype>()
           .HasRequired(t => t.sortbytype );



            //wantkids
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_wantkids>()
           .HasRequired(t => t.searchsetting);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_wantkids>()
           .HasRequired(t => t.wantskids);


        }
    }

  
}