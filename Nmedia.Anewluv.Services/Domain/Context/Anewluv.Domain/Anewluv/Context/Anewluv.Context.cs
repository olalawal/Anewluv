using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
//using Nmedia.DataAccess.Interfaces;
//using Nmedia.DataAccess;
using System.Data.Common;
using System.Data;
using System.Data.Entity.Validation;
using Anewluv.Domain.Data;
//using Nmedia.DataAccess.Test;
using Repository.Pattern.Ef6;


namespace Anewluv.Domain
{
    public partial class AnewluvContext : DataContext
    {



        static AnewluvContext()
        {
            Database.SetInitializer<AnewluvContext>(null);
        }



        public AnewluvContext()
            : base("name=AnewluvContext")
        {
         //  // Database.SetInitializer<AnewluvContext>(null);           
         //   this.Configuration.ProxyCreationEnabled = true;
         //   this.Configuration.AutoDetectChangesEnabled = true;
         //   this.DisableLazyLoading = true;
         //   //rebuild DB if schema is differnt
         //   //Initializer init = new Initializer();            
         //   // init.InitializeDatabase(this);
         //   this.Configuration.ValidateOnSaveEnabled = false;
         //   IsAuditEnabled = true;
         ////   ObjectContext.SavingChanges += OnSavingChanges;
         //  //Database.SetInitializer(
         // //  new DropCreateDatabaseIfModelChanges<AnewluvContext>());
          //  Database.SetInitializer(
         //  new DropCreateDatabaseIfModelChanges<AnewluvContext>());
        }


        //add the obects alphabetically 

        #region "Db objects "


        
        public DbSet<action> actions { get; set; }


        public DbSet<application> applications { get; set; }         
        public DbSet<applicationiconconversion> applicationiconconversions { get; set; }       
        public DbSet<applicationrole> applicationroles { get; set; }             
        public DbSet<communicationquota> communicationquotas { get; set; }      
        public DbSet<lu_abusetype> lu_abusetype { get; set; }



        public DbSet< lu_actiontype> lu_actiontype {get;set;}



        public DbSet<lu_activitytype> lu_activitytype { get; set; }
        public DbSet<lu_applicationpaymenttype> lu_applicationpaymenttype { get; set; }
        public DbSet<lu_applicationtransfertype> lu_applicationtransfertype { get; set; }
        public DbSet<lu_applicationtype> lu_applicationtype { get; set; }
        public DbSet<lu_bodytype> lu_bodytype { get; set; }
       // public DbSet<lu_defaultmailboxfolder> lu_defaultmailboxfolder { get; set; }
        public DbSet<lu_diet> lu_diet { get; set; }
        public DbSet<lu_drinks> lu_drinks { get; set; }
        public DbSet<lu_educationlevel> lu_educationlevel { get; set; }
        public DbSet<lu_employmentstatus> lu_employmentstatus { get; set; }
        public DbSet<lu_ethnicity> lu_ethnicity { get; set; }
        public DbSet<lu_exercise> lu_exercise { get; set; }
        public DbSet<lu_eyecolor> lu_eyecolor { get; set; }
        public DbSet<lu_flagyesno> lu_flagyesno { get; set; }
        public DbSet<lu_gender> lu_gender { get; set; }
        public DbSet<lu_haircolor> lu_haircolor { get; set; }
        public DbSet<lu_havekids> lu_havekids { get; set; }
        public DbSet<lu_height> lu_height { get; set; }
        public DbSet<lu_hobby> lu_hobby { get; set; }
        public DbSet<lu_hotfeature> lu_hotfeature { get; set; }
        public DbSet<lu_humor> lu_humor { get; set; }
        public DbSet<lu_iconformat> lu_iconformat { get; set; }
        public DbSet<lu_iconImagersizerformat> lu_iconImagersizerformat { get; set; }
        public DbSet<lu_incomelevel> lu_incomelevel { get; set; }
        public DbSet<lu_livingsituation> lu_livingsituation { get; set; }
        public DbSet<lu_lookingfor> lu_lookingfor { get; set; }
        public DbSet<lu_maritalstatus> lu_maritalstatus { get; set; }
        public DbSet<lu_notetype> lu_notetype { get; set; }
        public DbSet<lu_openidprovider> lu_openidprovider { get; set; }
        public DbSet<lu_photoapprovalstatus> lu_photoapprovalstatus { get; set; }
        public DbSet<lu_photoformat> lu_photoformat { get; set; }
        public DbSet<lu_photoImagersizerformat> lu_photoImagersizerformat { get; set; }
        public DbSet<lu_photoimagetype> lu_photoimagetype { get; set; }
        public DbSet<lu_photorejectionreason> lu_photorejectionreason { get; set; }
        public DbSet<lu_photostatus> lu_photostatus { get; set; }
        public DbSet<lu_photostatusdescription> lu_photostatusdescription { get; set; }
        public DbSet<lu_politicalview> lu_politicalview { get; set; }
        public DbSet<lu_profession> lu_profession { get; set; }
        public DbSet<lu_profilefiltertype> lu_profilefiltertype { get; set; }
        public DbSet<lu_profilestatus> lu_profilestatus { get; set; }
        public DbSet<lu_religion> lu_religion { get; set; }
        public DbSet<lu_religiousattendance> lu_religiousattendance { get; set; }
        public DbSet<lu_role> lu_role { get; set; }

         public DbSet< lu_searchsettingdetailtype> lu_searchsettingdetailtype {get;set;}


        public DbSet<lu_securityleveltype> lu_securityleveltype { get; set; }
        public DbSet<lu_securityquestion> lu_securityquestion { get; set; }
        public DbSet<lu_showme> lu_showme { get; set; }
        public DbSet<lu_sign> lu_sign { get; set; }
        public DbSet<lu_smokes> lu_smokes { get; set; }
        public DbSet<lu_sortbytype> lu_sortbytype { get; set; }
        public DbSet<lu_wantskids> lu_wantskids { get; set; }

        public DbSet<mailboxfolder> mailboxfolders { get; set; }
        public DbSet<lu_defaultmailboxfolder> lu_defaultmailboxfolder { get; set; }
        public DbSet<mailboxmessagefolder> mailboxmessagefolders { get; set; }
        public DbSet<mailboxmessage> mailboxmessages { get; set; }
        public DbSet<mailupdatefreqency> mailupdatefreqencies { get; set; }
        public DbSet<membersinrole> membersinroles { get; set; }
        public DbSet<openid> openids { get; set; }     
       
        
        public DbSet<note> notes { get; set; }     
      
            
        public DbSet<photo_securitylevel> photo_securitylevel { get; set; }
        public DbSet<photoalbum_securitylevel> photoalbum_securitylevel { get; set; }
        public DbSet<photophotoalbum> phototphotoalbums { get; set; }
        public DbSet<photo> photos { get; set; }
        public DbSet<photoconversion> photoconversions { get; set; }
        public DbSet<photoreview> photoreviews { get; set; }
        public DbSet<photoalbum> photoalbum { get; set; }

  
        public DbSet<profileactivity> profileactivities { get; set; }
        public DbSet<profileactivitygeodata> profileactivitygeodatas { get; set; }
        public DbSet<profiledata_ethnicity> profiledata_ethnicity { get; set; }
        public DbSet<profiledata_hobby> profiledata_hobby { get; set; }
        public DbSet<profiledata_hotfeature> profiledata_hotfeature { get; set; }
        public DbSet<profiledata_lookingfor> profiledata_lookingfor { get; set; }
        public DbSet<profiledata> profiledatas { get; set; }
        public DbSet<profilemetadata> profilemetadatas { get; set; }
        public DbSet<profile> profiles { get; set; }
        public DbSet<rating> ratings { get; set; }
        public DbSet<ratingvalue> ratingvalues { get; set; }      
        public DbSet<searchsetting_location> searchsetting_location { get; set; }    
        public DbSet<searchsetting> searchsettings { get; set; }

        public DbSet<searchsettingdetail> searchsettingdetails { get; set; }

        public DbSet<systempagesetting> systempagesettings { get; set; }
        public DbSet<userlogtime> userlogtimes { get; set; }
        public DbSet<visiblitysetting> visiblitysettings { get; set; }
        public DbSet<visiblitysettings_country> visiblitysettings_country { get; set; }
        public DbSet<visiblitysettings_gender> visiblitysettings_gender { get; set; }

               
        #endregion

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // modelBuilder.Entity<message>().ToTable("messages", schemaName: "Logging");
        //    //modelBuilder.Entity<address>().ToTable("messageAddresses", schemaName: "Logging");
        //    // modelBuilder.Entity<systemAddress>().ToTable("messageSystemAddresses", schemaName: "Logging");
        //    // modelBuilder.Entity<lu_template>().ToTable("lu_messageTemplate", schemaName: "Logging");
        //    // modelBuilder.Entity<lu_messageType>().ToTable("lu_messageType", schemaName: "Logging");
        //    // modelBuilder.Entity<lu_systemAddressType>().ToTable("lu_messageSystemAddressType", schemaName: "Logging");

        //    // map required relationships abusereport
        //    //**************************************

        //    //comment this out when sharing after generating the database
        //    //only share with sql scripts 
        //    anewluvmodelbuilder.buildgeneralmodels(modelBuilder);
        //    anewluvmodelbuilder.buildsearchsettingsmodels(modelBuilder);
        //}

        //public class Initializer : IDatabaseInitializer<AnewluvContext >
        //{
        //    public void InitializeDatabase(AnewluvContext  context)
        //    {
        //        if (!context.Database.Exists() || !context.Database.CompatibleWithModel(false))
        //        {
        //            context.Database.Create();
        //            context.SaveChanges();
        //        }
        //        else if (!context.Database.CompatibleWithModel(false))
        //        {
        //            //DO migrations here
        //        }

        //    }
        //}


       
    }
}
