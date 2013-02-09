using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shell.MVC2.Domain.Entities.Anewluv.Chat
{

    public  class chatmodelbuilder
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


          //  modelBuilder.Entity<profile>().HasOptional(zm => zm.profilemetadata).WithRequired(r => r.profile );
           // modelBuilder.Entity<profile>().HasOptional(zm => zm.profiledata).WithRequired(r => r.profile );



            //11-23-2012 olawal added code to map profile_id's to profiledata  and profilemetadata
            //11-23-2012 olawal added code to map profile_id's to and profilemetadata
            modelBuilder.Entity<profiledata>()
            .HasKey(e => e.profile_id);
            modelBuilder.Entity<profiledata>()
                        .Property(e => e.profile_id)
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<profiledata>()
                        .HasRequired(e => e.profile)
                        .WithRequiredDependent(r => r.profiledata);

           // modelBuilder.Entity<profiledata>().HasOptional(zm => zm.profilemetadata).WithRequired(r => r.profiledata);



            //11-23-2012 olawal added code to map profile_id's to profiledata  an
            modelBuilder.Entity<profilemetadata>()
            .HasKey(e => e.profile_id );
            modelBuilder.Entity<profilemetadata>()
                        .Property(e => e.profile_id )
                        .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            modelBuilder.Entity<profilemetadata>()
                        .HasRequired(e => e.profile )
                        .WithRequiredDependent(r => r.profilemetadata );

           // modelBuilder.Entity<profilemetadata>()
             //         .HasRequired(e => e.profiledata )
              //       .WithRequiredDependent(r => r.profilemetadata);


           

    

        
        }

       
    }

  
}