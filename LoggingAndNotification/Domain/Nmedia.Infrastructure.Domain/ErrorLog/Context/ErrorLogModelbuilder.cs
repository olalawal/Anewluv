using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nmedia.Infrastructure.Domain.Data.errorlog;

namespace Nmedia.Infrastructure.Domain
{

    public partial class ErrorlogContext
    {

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<errorlog>().ToTable("errorlog_logs");
            modelBuilder.Entity<lu_logenviroment>().ToTable("errorlog_lu_enviroment");
            modelBuilder.Entity<lu_logapplication>().ToTable("errorlog_lu_application");
            modelBuilder.Entity<lu_logseverity>().ToTable("errorlog_lu_logseverity");
            modelBuilder.Entity<lu_logseverityinternal>().ToTable("errorlog_lu_logseverityinternal");

            //setup FK relationsships 
            modelBuilder.Entity<errorlog>().HasRequired(p => p.application).WithMany().WillCascadeOnDelete(false);
            modelBuilder.Entity<errorlog>().HasRequired(p => p.enviroment).WithMany().WillCascadeOnDelete(false);
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