namespace Shell.MVC2.Infrastructure.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Data.Entity.Validation;
    using System.Text;
     using Shell.MVC2.Infrastructure.Entities.ApiKeyModel ;
    using Shell.MVC2.Infrastructure.Entities.NotificationModel;
    using Shell.MVC2.Infrastructure.Entities.UserRepairModel;
    using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
    using Shell.MVC2.Infrastructure.Entities;


    internal sealed class ConfigurationApiKeyModel : DbMigrationsConfiguration<ApiKeyModel.ApiKeyContext>
    {
        public ConfigurationApiKeyModel()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsNamespace = "Shell.MVC2.Infrastructure.Entities.ApiKeyModel";
        }

        protected override void Seed(ApiKeyModel.ApiKeyContext context)
        {
            SeedMethodsApiKeyModel.seedgenerallookups(context);

        }


    }

    internal sealed class ConfigurationNotificationModel : DbMigrationsConfiguration<NotificationModel.NotificationContext>
    {
        public ConfigurationNotificationModel()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsNamespace = "Shell.MVC2.Infrastructure.Entities.NotificationModel";
        }

        protected override void Seed(NotificationModel.NotificationContext context)
        {
            SeedMethods.seedgenerallookups(context);

        }

       
    }

    internal sealed class ConfigurationCustomerrorLogModel : DbMigrationsConfiguration<CustomErrorLogModel.CustomErrorLogContext>
    {
        public ConfigurationCustomerrorLogModel()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsNamespace = "Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel";
        }

        protected override void Seed(CustomErrorLogContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //TO DO migrations here
            //populate the lookup table for log severity
            var LogSeverityList = Enum.GetValues(typeof(LogSeverityEnum))
           .Cast<LogSeverityEnum>()
            .ToDictionary(t => (int)t, t => t.ToString()).ToList();

            // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<LogSeverityEnum >().ToList(); 
            LogSeverityList.ForEach(kvp => context.lu_logSeverity.Add(new lu_logSeverity()
            {
                id = kvp.Key,
                Description = kvp.Value
            }));
            context.SaveChanges();

            //populate the lookup table for applications           
            var ApplicationList = Enum.GetValues(typeof(ApplicationEnum))
           .Cast<ApplicationEnum>()
            .ToDictionary(t => (int)t, t => t.ToString()).ToList();

            // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<ApplicationEnum >().ToList(); 
            ApplicationList.ForEach(kvp => context.lu_Application.Add(new lu_Application()
            {
                id = kvp.Key,
                ApplicationName = kvp.Value
            }));
            context.SaveChanges();
        }

        
    }

    internal sealed class ConfigurationUserRepairModel : DbMigrationsConfiguration<UserRepairModel.UserRepairLogContext>
    {
        public ConfigurationUserRepairModel()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsNamespace = "Shell.MVC2.Infrastructure.Entities.UserRepairModel";
        }

       
   
    }

}

