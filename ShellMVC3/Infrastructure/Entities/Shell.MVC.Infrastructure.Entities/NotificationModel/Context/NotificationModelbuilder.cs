using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{

    public  class notificationmodelbuilder
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


       

         



        
        }

        
    }

  
}