using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.ApiKeyModel
{

    public  class ApiKeymodelbuilder
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


            //systemddress
          //  modelBuilder.Entity<systemaddress>().HasRequired(a => a.systemaddresstype)
           // .WithMany().HasForeignKey(a => a.systemaddresstype_id  );

            //set up required fiel lookups
            modelBuilder.Entity<apikey>()
           .HasRequired(t => t.accesslevel );

            //set up required fiel lookups
            modelBuilder.Entity<apicall>()
           .HasRequired(t => t.apikey);
          

            modelBuilder.Entity<apikey >()
           .HasRequired(t => t.application );
            



           //many to many for address and recipeints
           modelBuilder.Entity<apikey>()
                .HasMany(p => p.registeredusers  )
                .WithMany(t => t.apikeys )
                .Map(mc =>
                   {
                       mc.ToTable("userapikey");
                       mc.MapLeftKey("apikey_id");
                       mc.MapRightKey("user_id");

                   });
            
         

            
        
        }

        
    }

  
}