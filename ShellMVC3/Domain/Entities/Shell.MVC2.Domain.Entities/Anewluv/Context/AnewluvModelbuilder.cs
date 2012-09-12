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

            // map one to many relation shipds for metadatata table from its side 
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //abusereports
            modelBuilder.Entity<profilemetadata>().HasMany(p=>p.abusereportadded)
           .WithRequired(z=>z.abusereporter ).HasForeignKey(t=>t.abusereporter_id).WillCascadeOnDelete(false);
           //.HasForeignKey(p => p.abuser_id ).WillCascadeOnDelete(false);
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.abusereports)
            .WithRequired(z => z.abuser).HasForeignKey(t => t.abuser_id ).WillCascadeOnDelete(false);


            // map required relationships abusereportnotes
            //**************************************
            //map abusereport notes to abuse report and set as required
            modelBuilder.Entity<abusereportnotes>().HasRequired(p => p.abusereport).WithMany(z => z.notes)
            .HasForeignKey(p => p.abusereport_id).WillCascadeOnDelete(false);

            //abuse report notes also requires a person creating it i.e profiledata
            modelBuilder.Entity<abusereportnotes>().HasRequired(p => p.profilemetadata)
            .WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);


            //  //map requierd  relationshipds for favorite
            //  //**************************************
            //favorite  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.favoritesadded )
           .WithRequired(z => z.profilemetadata ).HasForeignKey(t => t.profile_id ).WillCascadeOnDelete(false);
            //.HasForeignKey(p => p.abuser_id ).WillCascadeOnDelete(false);
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.favorites)
            .WithRequired(z => z.favoriteprofilemetadata  ).HasForeignKey(t => t.favoriteprofile_id).WillCascadeOnDelete(false);

           
            //  //map requierd  relationshipds for hotlist
            //  //**************************************
            //hotlist  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.hotlistsadded)
            .WithRequired(z => z.profilemetadata).HasForeignKey(t => t.profile_id).WillCascadeOnDelete(false);
            //.HasForeignKey(p => p.abuser_id ).WillCascadeOnDelete(false);
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.hotlists)
            .WithRequired(z => z.hotlistprofilemetadata ).HasForeignKey(t => t.hotlistprofile_id).WillCascadeOnDelete(false);


            //  //map requierd  relationshipds for interest
            //  //**************************************
            //interest  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.interestsadded)
           .WithRequired(z => z.profilemetadata).HasForeignKey(t => t.profile_id).WillCascadeOnDelete(false);
            //.HasForeignKey(p => p.abuser_id ).WillCascadeOnDelete(false);
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.interests )
            .WithRequired(z => z.interestprofilemetadata).HasForeignKey(t => t.interestprofile_id).WillCascadeOnDelete(false);

            //  //map requierd  relationshipds for like
            //  //**************************************
            //like  reqired ,  first part has to sleetc the nav property in perant
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.likesadded)
            .WithRequired(z => z.profilemetadata).HasForeignKey(t => t.profile_id).WillCascadeOnDelete(false);
            //.HasForeignKey(p => p.abuser_id ).WillCascadeOnDelete(false);
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.likes )
            .WithRequired(z => z.likeprofilemetadata ).HasForeignKey(t => t.likeprofile_id).WillCascadeOnDelete(false);


            //  //map requierd  relationshipds for friend
            //  //**************************************
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.friendsadded)
              .WithRequired(z => z.profilemetadata).HasForeignKey(t => t.profile_id).WillCascadeOnDelete(false);
            //.HasForeignKey(p => p.abuser_id ).WillCascadeOnDelete(false);
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.friends)
            .WithRequired(z => z.friendprofilemetadata).HasForeignKey(t => t.friendprofile_id).WillCascadeOnDelete(false);

            //  //map requierd  relationshipds for peek
            //  //**************************************
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.peeksadded)
          .WithRequired(z => z.profilemetadata).HasForeignKey(t => t.profile_id).WillCascadeOnDelete(false);
            //.HasForeignKey(p => p.abuser_id ).WillCascadeOnDelete(false);
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.peeks)
            .WithRequired(z => z.peekprofilemetadata ).HasForeignKey(t => t.peekprofile_id).WillCascadeOnDelete(false);

            
            //map requierd  relationshipds for block
            //**************************************
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.blocksadded )
             .WithRequired(z => z.profilemetadata).HasForeignKey(t => t.profile_id ).WillCascadeOnDelete(false);
            //.HasForeignKey(p => p.abuser_id ).WillCascadeOnDelete(false);
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.blocks )
            .WithRequired(z => z.blockedprofilemetadata).HasForeignKey(t => t.blockprofile_id).WillCascadeOnDelete(false);

            // map required relationships blocknotes
            //**************************************
            //map block notes to abuse report and set as required
            modelBuilder.Entity<blocknotes>().HasRequired(p => p.block).WithMany(z => z.notes)
            .HasForeignKey(p => p.block_id).WillCascadeOnDelete(false);

            //abuse report notes also requires a person creating it i.e profiledata
            modelBuilder.Entity<blocknotes>().HasRequired(p => p.profilemetadata)
            .WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);





            //  //map requierd  relationshipds for CommunicationQoutas
            //  //**************************************
            modelBuilder.Entity<communicationquota>()
           .HasRequired(t => t.updaterprofiledata);
           

            //mailupdatefreqency
            //  //map requierd  relationshipds for mailupdatefreqency
            //  //**************************************
            modelBuilder.Entity<mailupdatefreqency>()
           .HasRequired(t => t.profilemetadata );

            //membersinrole
            //  //map requierd  relationshipds for membersinrole
            //  //**************************************
         //   modelBuilder.Entity<membersinrole>()
          // .HasRequired(t => t.profile);

            //openid
            //  //map requierd  relationshipds for openid
            //  //**************************************
         //   modelBuilder.Entity<openid>()
         //  .HasRequired(t => t.profile);

            //photo complex model requered field mappings
            // map required relationships to profile metadata
            //**************************************
          //  modelBuilder.Entity<photo>().HasRequired(t => t.profilemetadata).WithMany(z => z.photos )
           //.HasForeignKey(p => p.profile_id ).WillCascadeOnDelete(false);


            modelBuilder.Entity<profilemetadata>().HasMany(p => p.photos )
            .WithRequired(z => z.profilemetadata ).HasForeignKey(t => t.profile_id ).WillCascadeOnDelete(false);

            //map photo conversions relation ship with photo      
            //**************************************
            modelBuilder.Entity<photo>().HasMany(p => p.conversions)
              .WithRequired(z => z.photo ).HasForeignKey(t => t.photo_id ).WillCascadeOnDelete(false);

            //phto conversion type is requred too
            modelBuilder.Entity<photoconversion>()
           .HasRequired(p => p.type);
           
            //map photo relation ship with the given photo security level
            modelBuilder.Entity<photo>().HasMany(p => p.securitylevels);
          

            // map required relationships photoalbum -many to many with photo is automatcic
            //**************************************
            modelBuilder.Entity<photoalbum>().HasRequired(t => t.profilemetadata).WithMany(z => z.photoalbums )
           .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);

                   //map photoalbum relation ship with the given photo security level
            modelBuilder.Entity<photoalbum>().HasMany(p => p.securitylevels);


          


            //photoreviewstatus model requered field mappings
            // map required relationships photoreviewstatus
            //**************************************
            modelBuilder.Entity<photoreviewstatus>()
            .HasRequired(p => p.reviewerprofiledata).WithMany().HasForeignKey(z=>z.reviewerprofile_id);
            //photo is requred too
            modelBuilder.Entity<photoreviewstatus>()
           .HasRequired(p => p.photo).WithMany().HasForeignKey(z=>z.photo_id);
            //wonder if we should have a review status type as required

            //Mailbox relation ships 
            //*********************************************************
            //*********************************************************

            //Setup composite table first
            modelBuilder.Entity<mailboxmessagefolder>().HasKey(c => new { c.mailboxfolder_id, c.mailboxmessage_id });

            modelBuilder.Entity<mailboxmessagefolder >().HasRequired(p => p.mailboxfolder)
            .WithMany(c => c.mailboxmessagesfolder).HasForeignKey(p => new { p.mailboxfolder_id, p.mailboxmessage_id });

            modelBuilder.Entity<mailboxmessagefolder>().HasRequired(p => p.mailboxmessage)
            .WithMany(c => c.mailboxmessagesfolder).HasForeignKey(p => new { p.mailboxfolder_id, p.mailboxmessage_id }); 



            //Messages link to profilemetadata , set this from profilemetatdata
            //======================================================
            //sent messages link to metatdata
           // modelBuilder.Entity<mailboxmessage>().HasRequired(t => t.sender).WithMany(z => z.sentmailboxmessages)
           //.HasForeignKey(p => p.sender_id ).WillCascadeOnDelete(false);
           // //revived mailbox messages link
           // modelBuilder.Entity<mailboxmessage>().HasRequired(t => t.recipeint).WithMany(z => z.receivedmailboxmessages)
           //.HasForeignKey(p => p.recipient_id ).WillCascadeOnDelete(false);

            modelBuilder.Entity<profilemetadata>().HasMany(p => p.sentmailboxmessages)
          .WithRequired(z => z.sender).HasForeignKey(t => t.sender_id ).WillCascadeOnDelete(false);
            //.HasForeignKey(p => p.abuser_id ).WillCascadeOnDelete(false);
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.receivedmailboxmessages)
            .WithRequired(z => z.recipeint  ).HasForeignKey(t => t.recipient_id ).WillCascadeOnDelete(false);


            //setup link to mailbox messages folder links, 
            //======================================================
           // modelBuilder.Entity<mailboxfolder>().HasRequired(t => t.profilemetadata).WithMany(z => z.mailboxfolders )
           //.HasForeignKey(p => p.profiled_id ).WillCascadeOnDelete(false);

           // modelBuilder.Entity<mailboxfolder>().HasRequired(t => t.foldertype).WithMany()
           //.HasForeignKey(p => p.foldertype_id ).WillCascadeOnDelete(false);

            modelBuilder.Entity<profilemetadata>().HasMany(p => p.mailboxfolders )
           .WithRequired(z => z.profilemetadata).HasForeignKey(t => t.profiled_id ).WillCascadeOnDelete(false);
            //.HasForeignKey(p => p.abuser_id ).WillCascadeOnDelete(false);

         

            //Profile
            // Specify one-to-one association between profile and profiledata
            //*****************************************************************

            //sets up a one to one relation ship with same primary key
            modelBuilder.Entity<profile>().HasOptional(zm => zm.profiledata);
            modelBuilder.Entity<profile >().HasKey(zmt => zmt.id);
            modelBuilder.Entity<profile>().HasRequired(zmt => zmt.profiledata).WithRequiredDependent(zm => zm.profile );

            //same thing for profilemetadata
            modelBuilder.Entity<profile>().HasOptional(zm => zm.profilemetadata );
            modelBuilder.Entity<profile>().HasKey(zmt => zmt.id);
            modelBuilder.Entity<profile>().HasRequired(zmt => zmt.profilemetadata).WithRequiredDependent(zm => zm.profile);

            //TO DO this might go away since we kind of want profile to be the base 
            //this allows loading of profiledata withoute profile info ?
            //now link profile metatadata and profiledata
            modelBuilder.Entity<profiledata>().HasOptional(zm => zm.profilemetadata);
            modelBuilder.Entity<profiledata>().HasKey(zmt => zmt.id);
            modelBuilder.Entity<profiledata>().HasRequired(zmt => zmt.profilemetadata ).WithRequiredDependent(zm => zm.profiledata);
        //http://stackoverflow.com/questions/9434245/how-do-i-code-an-optional-one-to-one-relationship-in-ef-4-1-code-first-with-lazy


            //map the remanining one to many  relationshipds for metadata tables
            //  //**************************************
     
          


           // modelBuilder.Entity<profile>()
           //.HasRequired(t => t.profiledata).WithRequiredPrincipal(t => t.profile);
           // //status is required         
           // modelBuilder.Entity<profile>()
           //.HasRequired(p => p.status);

           // //Profiledata
           // //Specify one-to-one association between profile and profiledata
           // modelBuilder.Entity<profiledata>()
           //.HasOptional(t => t.visibilitysettings).WithOptionalPrincipal(t => t.profiledata);
           // //gender is required         
           // modelBuilder.Entity<profiledata>()
           //.HasRequired(p => p.gender);

           // //set one to many relation ship with searchsettings                 
           // modelBuilder.Entity<profiledata>()
           //.HasMany(p => p.searchsettings);

            //profiledata_ethnicity complex model requered field mappings
            // map required relationships profiledata_ethnicity
            //**************************************
            modelBuilder.Entity<profilemetadata>().HasMany(t => t.ethnicities)
           .WithRequired(p=>p.profilemetadata).HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);

            //profiledata_hobby complex model requered field mappings
            // map required relationships profiledata_hobby
            //**************************************
            modelBuilder.Entity<profilemetadata>().HasMany(t => t.hobbies )
            .WithRequired(p => p.profilemetadata).HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);

            //profiledata_hotfeature complex model requered field mappings
            // map required relationships profiledata_hotfeature
            //**************************************
            modelBuilder.Entity<profilemetadata>().HasMany(t => t.hotfeatures )
                  .WithRequired(p => p.profilemetadata).HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false); ;

            //profiledata_lookingfor complex model requered field mappings
            // map required relationships profiledata_lookingfor
            //**************************************
            modelBuilder.Entity<profilemetadata>().HasMany(t => t.lookingfor )
              .WithRequired(p => p.profilemetadata).HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);

            //rating value 
            //rating complex model requered field mappings
            // map required relationships rating value
            //**************************************

            modelBuilder.Entity<profilemetadata>().HasMany(p => p.ratingvaluesadded )
           .WithRequired(z => z.profilemetatadata ).HasForeignKey(t => t.profile_id ).WillCascadeOnDelete(false);
         
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.ratingvalues)
            .WithRequired(z => z.rateeprofilemetadata).HasForeignKey(t => t.rateeprofile_id ).WillCascadeOnDelete(false);


            modelBuilder.Entity<ratingvalue>()
           .HasRequired(p => p.rating).WithMany().HasForeignKey(z=>z.rating_id); //.WithMany(z=>z.ratingvalues).HasForeignKey(p=>p.id).WillCascadeOnDelete(false);
            
            //  //map requierd  relationshipds for favorite
            //  //**************************************
            //favorite  reqired ,  first part has to sleetc the nav property in perant
           // modelBuilder.Entity<ratingvalue>().HasRequired(t => t.rateeprofilemetadata   ).WithMany(z => z.ratingvalues )
          // .HasForeignKey(p => p.rateeprofile_id ).WillCascadeOnDelete(false);

            //favorite sender required
         //   modelBuilder.Entity<ratingvalue>().HasRequired(t => t.profilemetatadata ).WithMany()
        //    .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);


            //add collection values tied to profile
            //userlogtime
            modelBuilder.Entity<profile>().HasMany(p => p.logontimes )
           .WithRequired(z => z.profile).HasForeignKey(t => t.profile_id).WillCascadeOnDelete(false);

            modelBuilder.Entity<profile>().HasMany(p => p.activitylogs)
          .WithRequired(z => z.profile).HasForeignKey(t => t.profile_id).WillCascadeOnDelete(false);

            modelBuilder.Entity<profile>().HasMany(p => p.openids)
          .WithRequired(z => z.profile).HasForeignKey(t => t.profile_id).WillCascadeOnDelete(false);

            modelBuilder.Entity<profile>().HasMany(p => p.roles)
          .WithRequired(z => z.profile).HasForeignKey(t => t.profile_id).WillCascadeOnDelete(false);


                        //Profiledata
            //Specify one-to-one association between profile and profiledata
            modelBuilder.Entity<visiblitysetting>()
           .HasRequired(t => t.profiledata ).WithMany().HasForeignKey(z=>z.profile_id);

            
       

         



        
        }

        public static void buildsearchsettingsmodels(DbModelBuilder modelBuilder)
        {

            //searchsetting
            //  //map requierd  relationshipds for openid
            //  //**************************************
            modelBuilder.Entity<searchsetting>()
           .HasRequired(t => t.profilemetadata );

            //bodytype
            //**********************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_bodytype>()
           .HasRequired(t => t.bodytype);

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id ).WillCascadeOnDelete(false);

           

            //diet
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_diet>()
           .HasRequired(t => t.diet);

            //drink
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_drink>()
           .HasRequired(t => t.drink);

            //educationlevel
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_educationlevel>()
           .HasRequired(t => t.educationlevel);

            //employmentstatus
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_employmentstatus>()
           .HasRequired(t => t.employmentstatus);

            //ethnicity
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_ethnicity>()
           .HasRequired(t => t.ethnicity);

            //exercise
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_exercise>()
           .HasRequired(t => t.exercise);

            //eyecolor
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


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

            modelBuilder.Entity<searchsetting>().HasMany(p => p.genders)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_gender>()
           .HasRequired(t => t.gender);

            //haircolor
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_haircolor>()
           .HasRequired(t => t.haircolor);

            //havekids
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_havekids>()
           .HasRequired(t => t.havekids);

            //hobby
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_hobby>()
           .HasRequired(t => t.hobby);

            //hotfeature
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_hotfeature>()
           .HasRequired(t => t.hotfeature);

            //humor
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_humor>()
           .HasRequired(t => t.humor);

            //income
            //***********************************
            //all the other related tables  now          

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_incomelevel>()
           .HasRequired(t => t.incomelevel);

            //livingsituation
            //***********************************
            //all the other related tables  now         

            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_livingstituation>()
           .HasRequired(t => t.livingsituation);

            //location
            //***********************************
            //all the other related tables  now  
            modelBuilder.Entity<searchsetting>().HasMany(p => p.locations)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //lookingfor
            //***********************************
            //all the other related tables  now       
            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_lookingfor>()
           .HasRequired(t => t.lookingfor);

            //maritalstatus
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
         .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_maritalstatus>()
           .HasRequired(t => t.maritalstatus);

            //politicalview
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
           .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_politicalview>()
           .HasRequired(t => t.politicalview);

            //lookingfor
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
        .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_lookingfor>()
           .HasRequired(t => t.lookingfor);


            //profession
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
          .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_profession>()
           .HasRequired(t => t.profession);



            //religion
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
                   .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_religion>()
           .HasRequired(t => t.religion);



            //religiousattendance
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
          .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_religiousattendance>()
           .HasRequired(t => t.religiousattendance);



            //showme
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
             .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_showme>()
           .HasRequired(t => t.showme);



            //sign
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
             .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_sign>()
           .HasRequired(t => t.sign);



            //smokes
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
          .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_smokes>()
           .HasRequired(t => t.smokes);



            //sortbytype
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
       .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_sortbytype>()
           .HasRequired(t => t.sortbytype );



            //wantkids
            //***********************************
            //all the other related tables  now          
            modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
          .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

            //all the other related tables  now          
            modelBuilder.Entity<searchsetting_wantkids>()
           .HasRequired(t => t.wantskids);


        }
    }

  
}