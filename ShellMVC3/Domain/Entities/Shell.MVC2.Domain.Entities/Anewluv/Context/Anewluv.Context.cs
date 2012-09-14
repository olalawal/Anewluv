using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv
{
    public partial  class AnewluvContext : DbContext
    {
        //add the obects alphabetically 

        
       // public DbSet<abuser> abusers { get; set; }
        public DbSet<abusereport> abusereports { get; set; }
        public DbSet<abusereportnotes> abusereportnotes { get; set; }
        public DbSet<block> blocks { get; set; }
        public DbSet<blocknotes> blocknotes { get; set; }
        public DbSet<communicationquota> communicationquotas { get; set; }
        public DbSet<profiledata_ethnicity> ethnicities { get; set; }
        public DbSet<favorite> favorites { get; set; }
        public DbSet<friend> friends { get; set;}
        public DbSet<profiledata_hobby> hobbies { get; set; }
        public DbSet<profiledata_hotfeature> hotfeatures { get; set; }
        public DbSet<hotlist> hotlists { get; set; }
        public DbSet<interest> interests { get; set; }
        public DbSet<like> likes { get; set; }
        public DbSet<profileactivity> profileactivity { get; set; }
        public DbSet<profileactivitygeodata > profileactivitygeodata { get; set; }
        public DbSet<profiledata_lookingfor> lookingfor { get; set; }
        public DbSet<mailupdatefreqency> mailupdatefreqencies { get; set; }
        public DbSet<membersinrole> membersinroles { get; set; }
        public DbSet<openid> opendIds { get; set; }
        public DbSet<peek> peeks { get; set; }
        public DbSet<photo> photos { get; set; }
        public DbSet<photoalbum> photoalbums { get; set; }
        public DbSet<photoconversion> photoconversions { get; set; }
        public DbSet<photoreviewstatus > photoreviewstatuses { get; set; }
        public DbSet<profile> profiles { get; set; }
        public DbSet<profiledata> profiledata { get; set; }
        public DbSet<profilemetadata> profilemetadata { get; set; }
        public DbSet<rating> ratings { get; set; }
               
        public DbSet<systempagesetting> systempagesettings { get; set; }        
        public DbSet<userlogtime> userlogtimes { get; set; }
        public DbSet<visiblitysetting> visibilitysettings { get; set; }
      
        //mail
        //TO Do place mail in a different database as we get bigger
        //chat already does this 
        public DbSet<mailboxfolder> mailboxfolders { get; set; }
        public DbSet<mailboxfoldertype> mailboxfoldertypes { get; set; }
        public DbSet<mailboxmessage> mailboxmessages { get; set; }
        public DbSet<mailboxmessagefolder> mailboxmessagefolders { get; set; }

        //lookup tables
        //
        public AnewluvContext()
            : base("name=AnewluvContext")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            //rebuild DB if schema is differnt
            //Initializer init = new Initializer();            
            // init.InitializeDatabase(this);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<message>().ToTable("messages", schemaName: "Logging");
            //modelBuilder.Entity<address>().ToTable("messageAddresses", schemaName: "Logging");
            // modelBuilder.Entity<systemAddress>().ToTable("messageSystemAddresses", schemaName: "Logging");
            // modelBuilder.Entity<lu_template>().ToTable("lu_messageTemplate", schemaName: "Logging");
            // modelBuilder.Entity<lu_messageType>().ToTable("lu_messageType", schemaName: "Logging");
            // modelBuilder.Entity<lu_systemAddressType>().ToTable("lu_messageSystemAddressType", schemaName: "Logging");

            // map required relationships abusereport
            //**************************************

            //comment this out when sharing after generating the database
            //only share with sql scripts 
            anewluvmodelbuilder.buildgeneralmodels(modelBuilder);
            anewluvmodelbuilder.buildsearchsettingsmodels(modelBuilder);
        }

        public class Initializer : IDatabaseInitializer<AnewluvContext >
        {
            public void InitializeDatabase(AnewluvContext  context)
            {
                if (!context.Database.Exists() || !context.Database.CompatibleWithModel(false))
                {
                    context.Database.Create();
                    context.SaveChanges();
                }
                else if (!context.Database.CompatibleWithModel(false))
                {
                    //DO migrations here
                }

            }
        }

    }
}
