using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{
    //Configure migrations
    //PM> enable-migrations
    // PM> add-migration -startupproject NotificationModel ddsd
    // Update-DataBase -startupproject NotificationModel -verbose

    public class NotificationContext : DbContext
    {
        public DbSet<address> address { get; set; }
        public DbSet<systemaddress> systemaddress { get; set; }
        public DbSet<lu_addresstype> lu_addresstype { get; set; }
        public DbSet<lu_messagetype> lu_messagetype { get; set; }
        public DbSet<lu_news> lu_news { get; set; }
        public DbSet<lu_systemaddresstype> lu_systemaddresses { get; set; }
        public DbSet<lu_template> lu_template { get; set; }
        public DbSet<lu_templatebody> lu_templatebody { get; set; }
        public DbSet<lu_templatesubject> lu_templatesubject { get; set; }     
        public DbSet<message> messages { get; set; }


       
       
    
       
        public DbSet<lu_systemaddresstype> lu_systemaddresstype { get; set; }
              
  
        public NotificationContext()
            : base("name=NotificationContext")
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
            notificationmodelbuilder.buildgeneralmodels(modelBuilder);
          
        }


        public class Initializer : IDatabaseInitializer<NotificationContext>
        {
            public void InitializeDatabase(NotificationContext context)
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