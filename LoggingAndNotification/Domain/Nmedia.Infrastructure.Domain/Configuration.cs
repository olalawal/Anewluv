using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nmedia.Infrastructure.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text;

    internal sealed class ConfigurationErrorlog : DbMigrationsConfiguration<ErrorlogContext>
    {
        public ConfigurationErrorlog()
        {
            AutomaticMigrationsEnabled = true;
            //TO DO comment this  out when model has data 
            AutomaticMigrationDataLossAllowed = true;

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

    internal sealed class ConfigurationNotification : DbMigrationsConfiguration<NotificationContext>
    {
        public ConfigurationNotification()
        {
            AutomaticMigrationsEnabled = false;
            // MigrationsNamespace = "SWellsFargo.Promotion.Domain.SurfManagement";
        }

        protected override void Seed(NotificationContext context)
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
            SeedMethodsNotificationModel.seedgenerallookups(context);
        }

    }
}
