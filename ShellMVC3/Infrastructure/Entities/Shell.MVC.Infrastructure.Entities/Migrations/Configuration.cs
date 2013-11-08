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
    using Nmedia.Infrastructure.Domain.Errorlog;
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
           // SeedMethodsApiKeyModel.seedcascadeddata(context);

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
            SeedMethodsNotificationModel.seedgenerallookups(context);

        }

       
    }

    internal sealed class ConfigurationCustomerrorLogModel : DbMigrationsConfiguration<ErrorlogModel.ErrorlogContext>
    {
        public ConfigurationCustomerrorLogModel()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsNamespace = "Nmedia.Infrastructure.Domain.Errorlog";
        }

        protected override void Seed(ErrorlogContext context)
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

            SeedMethodsErrorlogModel.seedgenerallookups(context);
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

