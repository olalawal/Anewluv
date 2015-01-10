using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using Nmedia.Infrastructure.Domain.Data.Apikey;

namespace Nmedia.Infrastructure.Domain
{

    public  class ApiKeymodelbuilder
    {

        public static void buildgeneralmodels(DbModelBuilder modelBuilder)
        {


            //modelBuilder.Entity<apikey>().ToTable("api_apikeys");
            //modelBuilder.Entity<apicall>().ToTable("api_apicalls");
            //modelBuilder.Entity<user>().ToTable("api_users");
            //modelBuilder.Entity<lu_accesslevel>().ToTable("api_lu_accesslevel");
            //modelBuilder.Entity<lu_application>().ToTable("api_lu_application");
       


            // modelBuilder.Entity<message>().ToTable("messages", schemaName: "Logging");
            //modelBuilder.Entity<address>().ToTable("messageAddresses", schemaName: "Logging");
            // modelBuilder.Entity<systemAddress>().ToTable("messageSystemAddresses", schemaName: "Logging");
            // modelBuilder.Entity<lu_template>().ToTable("lu_messageTemplate", schemaName: "Logging");
            // modelBuilder.Entity<lu_messageType>().ToTable("lu_messageType", schemaName: "Logging");
            // modelBuilder.Entity<lu_systemAddressType>().ToTable("lu_messageSystemAddressType", schemaName: "Logging");

            // map one to many relation shipds for metadatata table from its side 
            //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            //systemddress
          //  modelBuilder.Entity<systemaddress>().HasRequired(a => a.systemaddresstype)
           // .WithMany().HasForeignKey(a => a.systemaddresstype_id  );


            //Apikey Mapping
            //set up required fiel lookups
           // modelBuilder.Entity<apikey>()
          // .HasRequired(t => t.accesslevel );         

          //  modelBuilder.Entity<apikey >()
         //  .HasRequired(t => t.application );

            //new added
            // FK Relationships
            modelBuilder.Entity<apikey>().HasRequired(t => t.application).WithMany()
                .HasForeignKey(u => u.application_id);
            ;

            modelBuilder.Entity<apikey>().HasRequired(t => t.accesslevel).WithMany()
               .HasForeignKey(u => u.accesslevel_id);
            ;

            modelBuilder.Entity<apikey>().HasOptional(t => t.user).WithMany()
               .HasForeignKey(u => u.user_id);
            ;

            modelBuilder.Entity<apikey>()
                               .HasOptional(p => p.user)
                               .WithMany(t => t.apikeys).HasForeignKey(p => p.user_id).WillCascadeOnDelete(false);
                               
                              



            //APicall mapping
            //set up required fiel lookups
            modelBuilder.Entity<apicall>()
           .HasRequired(t => t.apikey);
          




           ////many to many for user and apikeys
           //modelBuilder.Entity<apikey>()
           //     .HasMany(p => p.registeredusers  )
           //     .WithMany(t => t.apikeys )
           //     .Map(mc =>
           //        {
           //            mc.ToTable("user_apikey");
           //            mc.MapLeftKey("apikey_id");
           //            mc.MapRightKey("user_id");

           //        });
            

         

            
        
        }

        
    }

  
}