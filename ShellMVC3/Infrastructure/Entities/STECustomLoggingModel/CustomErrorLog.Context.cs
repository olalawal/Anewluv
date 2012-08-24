using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace CustomLoggingModel
{
    public class CustomErrorLogContext : DbContext 
    {
       
        public DbSet<CustomErrorLog> CustomErrorLogs { get; set; }
        public DbSet<ApplicationLookup > ApplicationLookup { get; set; }
        public DbSet<LogSeverityLookup> LogSeverityLookup{ get; set; }

       public CustomErrorLogContext()
            : base("name=CustomErrorLogContext")

        {

            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            Initializer init = new Initializer();
            init.InitializeDatabase(this);
        }


       public class Initializer : IDatabaseInitializer<CustomErrorLogContext>
       {
           public void InitializeDatabase(CustomErrorLogContext context)
           {
               if (!context.Database.Exists()) // || !context.Database.CompatibleWithModel(false))
               {
                   //  context.Database.Delete(); 
                   context.Database.Create();
                   //populate the lookup table for log severity
                   var LogSeverityList = Enum.GetValues(typeof(LogSeverityEnum))
                  .Cast<LogSeverityEnum>()
                   .ToDictionary(t => (int)t, t => t.ToString()).ToList();

                   // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<LogSeverityEnum >().ToList(); 
                   LogSeverityList.ForEach(kvp => context.LogSeverityLookup.Add(new LogSeverityLookup()
                   {
                       LogSeverityLookupID = kvp.Key,
                       Description = kvp.Value
                   }));
                   context.SaveChanges();

                   //populate the lookup table for applications           
                   var ApplicationList = Enum.GetValues(typeof(ApplicationEnum))
                  .Cast<ApplicationEnum>()
                   .ToDictionary(t => (int)t, t => t.ToString()).ToList();

                   // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<ApplicationEnum >().ToList(); 
                   ApplicationList.ForEach(kvp => context.ApplicationLookup.Add(new ApplicationLookup()
                   {
                       ApplicationLookupID = kvp.Key,
                       ApplicationName = kvp.Value
                   }));
                   context.SaveChanges();
               }
               else
               {
                   //TO DO migrations here

               }
           
               
           }
       }


    }
}
