using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{

    public  class customerrorlogmodelbuilder
    {

        public static void buildgeneralmodels(DbModelBuilder modelBuilder)
        {

          //  modelBuilder.Entity<errorlog>().ToTable("CustomErrorLogs", schemaName: "Logging");
           // modelBuilder.Entity<lu_application>().ToTable("lu_application", schemaName: "Logging");
         //   modelBuilder.Entity<lu_logseverity>().ToTable("lu_logseverity", schemaName: "Logging");
          //  modelBuilder.Entity<lu_logseverityinternal>().ToTable("lu_logseverityinternal", schemaName: "Logging");

            //setup FK relationsships 
            modelBuilder.Entity<errorlog>().HasRequired(p => p.application).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<errorlog>().HasRequired(p => p.logseverity).WithMany().WillCascadeOnDelete(false);
             modelBuilder.Entity<errorlog>()
            .HasOptional(m => m.logseverityinternal).WithOptionalDependent().WillCascadeOnDelete(false);
            


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




        }
    }

  
}