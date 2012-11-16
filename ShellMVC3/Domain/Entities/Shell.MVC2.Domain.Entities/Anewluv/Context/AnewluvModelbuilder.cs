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


            //activitylog and geo data
            modelBuilder.Entity<profileactivity>().HasRequired(a => a.profileactivitygeodata )
           .WithMany().HasForeignKey(a => a.profileactivitygeodata_id ); 
   



            //abusereports
            modelBuilder.Entity<profilemetadata>().HasMany(p=>p.abusereportadded)
           .WithRequired(z=>z.abusereporter ).HasForeignKey(t=>t.abusereporter_id).WillCascadeOnDelete(false);
           //.HasForeignKey(p => p.abuser_id ).WillCascadeOnDelete(false);
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.abusereports)
            .WithRequired(z => z.abuser).HasForeignKey(t => t.abuser_id ).WillCascadeOnDelete(false);

            //11-17-2012 olawal added requured lookup
            modelBuilder.Entity<abusereport>()
            .HasRequired(t => t.abusetype);


            // map required relationships abusereportnotes
            //**************************************
            //map abusereport notes to abuse report and set as required
            modelBuilder.Entity<abusereportnotes>().HasRequired(p => p.abusereport).WithMany(z => z.notes)
            .HasForeignKey(p => p.abusereport_id).WillCascadeOnDelete(false);

            //abuse report notes also requires a person creating it i.e profiledata
            modelBuilder.Entity<abusereportnotes>().HasRequired(p => p.profilemetadata)
            .WithMany()
            .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);

            //11-17-2012 olawal added requured lookup
            modelBuilder.Entity<abusereportnotes >()
            .HasRequired(t => t.notetype );


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

            //Members in role required lookup
            //11-17-2012 olawal added requured lookup
            modelBuilder.Entity<membersinrole>()
            .HasRequired(t => t.role );





            modelBuilder.Entity<profilemetadata>().HasMany(p => p.photos )
            .WithRequired(z => z.profilemetadata ).HasForeignKey(t => t.profile_id ).WillCascadeOnDelete(false);

            //map photo      
            //**************************************

            //9-20-2012 added photo review
            modelBuilder.Entity<photo>().HasMany(p => p.reviews )
           .WithRequired(z => z.photo).HasForeignKey(t => t.photo_id).WillCascadeOnDelete(false);

            modelBuilder.Entity<photo>().HasMany(p => p.conversions)
            .WithRequired(z => z.photo ).HasForeignKey(t => t.photo_id ).WillCascadeOnDelete(false);

            //map photo relation ship with the given photo security level
            modelBuilder.Entity<photo>().HasMany(p => p.securitylevels);

            //phot conversions
            //conversions relation ship with photo 
            //phto conversion type is requred too
            //********************************************
            modelBuilder.Entity<photoconversion>()
           .HasRequired(p => p.formattype);

            //Map required photo to photo conversion
            modelBuilder.Entity<photoconversion>()
            .HasRequired(p => p.photo).WithMany().HasForeignKey(z => z.photo_id);

            


             //confgure 1 to 1 required relatonship woth photo type
            //********************************************
            modelBuilder.Entity<lu_photoformat>()
           .HasRequired(p => p.imageresizerformat).WithMany().HasForeignKey(z=>z.photoImagersizerformat_id);
           

            // map required relationships photoalbum -many to many with photo is automatcic
            //**************************************
            modelBuilder.Entity<photoalbum>().HasRequired(t => t.profilemetadata).WithMany(z => z.photoalbums )
           .HasForeignKey(p => p.profile_id).WillCascadeOnDelete(false);

            //map photoalbum relation ship with the given photo security level
            modelBuilder.Entity<photoalbum>().HasMany(p => p.securitylevels);

            
            //photoreview model requered field mappings
            // map required relationships photoreview
            //**************************************
            modelBuilder.Entity<photoreview >()
            .HasRequired(p => p.reviewerprofiledata).WithMany().HasForeignKey(z=>z.reviewerprofile_id);
            //photo is requred too
            modelBuilder.Entity<photoreview>()
           .HasRequired(p => p.photo).WithMany().HasForeignKey(z=>z.photo_id);
            //wonder if we should have a review status type as required

            //Mailbox relation ships 
            //*********************************************************
            //*********************************************************

            //Setup composite table first
            modelBuilder.Entity<mailboxmessagefolder>().HasKey(c => new { c.mailboxfolder_id, c.mailboxmessage_id });

            modelBuilder.Entity<mailboxmessagefolder >().HasRequired(p => p.mailboxfolder)
            .WithMany(c => c.mailboxmessagesfolders).HasForeignKey(p=>p.mailboxfolder_id);

           modelBuilder.Entity<mailboxmessagefolder>().HasRequired(p => p.mailboxmessage)
           .WithMany(c => c.mailboxmessagesfolders).HasForeignKey(p => p.mailboxmessage_id ); 



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

            //prfoile part is optional for profile activity since non registered  can log information as well
            modelBuilder.Entity<profile>().HasMany(p => p.profileactivity )
           .WithRequired(z => z.profile).HasForeignKey(t => t.profile_id).WillCascadeOnDelete(false);

            modelBuilder.Entity<profile>().HasMany(p => p.openids)
          .WithRequired(z => z.profile).HasForeignKey(t => t.profile_id).WillCascadeOnDelete(false);

            modelBuilder.Entity<profile>().HasMany(p => p.memberroles )
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
            modelBuilder.Entity<profilemetadata>().HasMany(p => p.searchsettings )
           .WithRequired(z => z.profilemetadata).HasForeignKey(t => t.profile_id).WillCascadeOnDelete(false);

     //       //bodytype
     //       //**********************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_bodytype>()
     //      .HasRequired(t => t.bodytype);
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id ).WillCascadeOnDelete(false);

     //       //wantkids
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_wantkids>()
     //      .HasRequired(t => t.wantskids);
     //       //   modelBuilder.Entity<searchsetting>().HasMany(p => p.wantkids)
     //       // .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


     //       //diet
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_diet>()
     //      .HasRequired(t => t.diet );
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.diets)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //drink
     //       //***********************************             
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_drink>()
     //      .HasRequired(t => t.drink );
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.drinks)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //educationlevel
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_educationlevel>()
     //      .HasRequired(t => t.educationlevel );
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.educationlevels)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //employmentstatus
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_employmentstatus>()
     //      .HasRequired(t => t.employmentstatus );
     //     //  modelBuilder.Entity<searchsetting>().HasMany(p => p.employmentstatus)
     //    //  .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //ethnicity
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_bodytype>()
     //      .HasRequired(t => t.bodytype);
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.bodytypes)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //exercise
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_ethnicity>()
     //      .HasRequired(t => t.ethnicity );
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.ethnicitys)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //eyecolor
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_eyecolor>()
     //      .HasRequired(t => t.eyecolor );
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.eyecolors )
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //genders
     //       //***********************************
     //       //all the other related tables  now          
     //       // modelBuilder.Entity<searchsetting>()
     //       //.HasMany(t => t.genders);

     //       //TO DO for some reason gender will not do the many part !?
     //       //all the other related tables  now          
     //       // modelBuilder.Entity<searchsetting_gender>()
     //       //.HasRequired(t => t.searchsetting);
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.genders)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_gender>()
     //      .HasRequired(t => t.gender);

     //       //haircolor
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_haircolor>()
     //      .HasRequired(t => t.haircolor );
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.haircolors )
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);
            
     //       //havekids
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_havekids>()
     //      .HasRequired(t => t.havekids);
     //     //  modelBuilder.Entity<searchsetting>().HasMany(p => p.havekids )
     //     // .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //hobby
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_hobby>()
     //      .HasRequired(t => t.hobby);
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.hobbies)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //hotfeature
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_hotfeature>()
     //      .HasRequired(t => t.hotfeature);
     //     //  modelBuilder.Entity<searchsetting>().HasMany(p => p.hotfeature )
     //     // .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //humor
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_humor>()
     //      .HasRequired(t => t.humor);
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.humors)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //incomelevel
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_incomelevel>()
     //      .HasRequired(t => t.incomelevel);
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.incomelevels)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //livingsituation
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_livingstituation>()
     //      .HasRequired(t => t.livingsituation );
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.livingstituations)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //location
     //       //***********************************
     //       //all the other related tables  now  
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.locations)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);


     //       //lookingfor
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_lookingfor>()
     //      .HasRequired(t => t.lookingfor);
     //  //     modelBuilder.Entity<searchsetting>().HasMany(p => p.lookingfor)
     ////      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //maritalstatus
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_maritalstatus>()
     //      .HasRequired(t => t.maritalstatus);
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.maritalstatuses )
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //politicalview
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_politicalview>()
     //      .HasRequired(t => t.politicalview);
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.politicalviews)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //profession
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_profession>()
     //      .HasRequired(t => t.profession );
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.professions)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);



     //       //religion
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_religion>()
     //      .HasRequired(t => t.religion);
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.religions )
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);
            
     //       //religiousattendance
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_religiousattendance>()
     //      .HasRequired(t => t.religiousattendance);
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.religiousattendances )
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //showme
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_showme>()
     //      .HasRequired(t => t.showme);
     ////       modelBuilder.Entity<searchsetting>().HasMany(p => p.showme )
     // //     .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //sign
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_sign>()
     //      .HasRequired(t => t.sign );
     //       modelBuilder.Entity<searchsetting>().HasMany(p => p.signs)
     //      .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //smokes
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_smokes>()
     //      .HasRequired(t => t.smoke);
     //   //    modelBuilder.Entity<searchsetting>().HasMany(p => p.smokes )
     //   //   .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

     //       //sortbytype
     //       //***********************************
     //       //all the other related tables  now          
     //       modelBuilder.Entity<searchsetting_sortbytype>()
     //      .HasRequired(t => t.sortbytype );
     //     //  modelBuilder.Entity<searchsetting>().HasMany(p => p.sortbytypes)
     //     // .WithRequired(z => z.searchsetting).HasForeignKey(t => t.searchsetting_id).WillCascadeOnDelete(false);

           


        }
    }

  
}