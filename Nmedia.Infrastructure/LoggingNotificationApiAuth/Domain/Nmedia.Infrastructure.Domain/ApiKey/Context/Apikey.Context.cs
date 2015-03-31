using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
//using Nmedia.DataAccess.Interfaces;


using System.Data.Common;
using System.Data;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;

using Nmedia.Infrastructure.Domain.Data.Apikey;
using Repository.Pattern.Ef6;
//using Nmedia.DataAccess;

namespace Nmedia.Infrastructure.Domain
{
    //Configure migrations
    //PM> enable-migrations
    // PM> add-migration -startupproject NotificationModel ddsd
    // Update-Database -startupproject NotificationModel -verbose

    public partial class ApiKeyContext : DataContext
    {
      
        
        static ApiKeyContext()
        {
            Database.SetInitializer<ApiKeyContext>(null);
        }


     


        public ApiKeyContext()
            : base("name=ApikeyContext")
        {


           
        }



        public DbSet<apikey> apikeys { get; set; }
        public DbSet<apicall> apicalls { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<lu_accesslevel > lu_accesslevels { get; set; }
        public DbSet<lu_application> lu_applications { get; set; }
  

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

            //comment this out when sharing after generating the Database
            //only share with sql scripts 
            ApiKeymodelbuilder.buildgeneralmodels(modelBuilder);
          
        }




    }
}