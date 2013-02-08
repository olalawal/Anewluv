using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Shell.MVC2.Infrastructure.Entities.ApiKeyModel
{
    //Configure migrations
    //PM> enable-migrations
    // PM> add-migration -startupproject NotificationModel ddsd
    // Update-DataBase -startupproject NotificationModel -verbose

    public class ApiKeyContext : DbContext
    {
        public DbSet<apikey> apikeys { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<lu_accesslevel > lu_accesslevels { get; set; }
        public DbSet<lu_application_apikey > lu_applications { get; set; }


        public ApiKeyContext()
            : base("name=ApiKeyContext")
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
            ApiKeymodelbuilder.buildgeneralmodels(modelBuilder);
          
        }


        public class Initializer : IDatabaseInitializer<ApiKeyContext>
        {
            public void InitializeDatabase(ApiKeyContext context)
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