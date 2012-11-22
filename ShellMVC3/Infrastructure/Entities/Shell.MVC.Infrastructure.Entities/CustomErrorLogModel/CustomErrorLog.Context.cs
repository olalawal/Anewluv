using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{
    public class CustomErrorLogContext : DbContext 
    {
       
        public DbSet<CustomErrorLog> CustomErrorLogs { get; set; }
        public DbSet<lu_Application> lu_Application { get; set; }
        public DbSet<lu_logSeverity> lu_logSeverity{ get; set; }

       public CustomErrorLogContext()
            : base("name=CustomErrorLogContext")

        {

            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            Initializer init = new Initializer();
            init.InitializeDatabase(this);
        }


       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {
           modelBuilder.Entity<CustomErrorLog>().ToTable("CustomErrorLogs", schemaName: "Logging");
           modelBuilder.Entity<lu_Application>().ToTable("lu_Application", schemaName: "Logging");
           modelBuilder.Entity<lu_logSeverity>().ToTable("lu_logSeverity", schemaName: "Logging");

           //setup FK relationsships 
           modelBuilder.Entity<CustomErrorLog>().HasRequired(p => p.Application).WithMany().HasForeignKey(p=>p.logseverity_id);
           modelBuilder.Entity<CustomErrorLog>().HasRequired(p => p.LogSeverity).WithMany().HasForeignKey(p=>p.application_id);

       }

      

       public class Initializer : IDatabaseInitializer<CustomErrorLogContext>
       {
           public void InitializeDatabase(CustomErrorLogContext context)
           {
               if (!context.Database.Exists())
               {
                   //  context.Database.Delete(); 
                   context.Database.Create();
                   context.SaveChanges();
               }
               else if ( !context.Database.CompatibleWithModel(false))
               {
                   //TO DO migrations here
                    context.Database.Delete();
                    context.Database.Create();
                    context.SaveChanges();
               }
           
               
           }
       }


    }
}
