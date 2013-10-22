namespace Shell.MVC2.Domain.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Shell.MVC2.Domain.Entities.Anewluv;
    using Shell.MVC2.Infrastructure;

    internal sealed class ConfigurationAnewluv : DbMigrationsConfiguration<Shell.MVC2.Domain.Entities.Anewluv.AnewluvContext>
    {
        public ConfigurationAnewluv()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsNamespace = "Shell.MVC2.Domain.Entities.Anewluv";
        }

        protected override void Seed(Shell.MVC2.Domain.Entities.Anewluv.AnewluvContext context)
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
            SeedMethods.seedgenerallookups(context);
            SeedMethods.seedcriterialookups(context);
            SeedMethods.seedapplicationlookups(context);


        }



    }

    internal sealed class ConfigurationChat : DbMigrationsConfiguration<Shell.MVC2.Domain.Entities.Anewluv.Chat.ChatContext >
    {
        public ConfigurationChat()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsNamespace = "Shell.MVC2.Domain.Entities.Anewluv.Chat";
        }

        protected override void Seed(Shell.MVC2.Domain.Entities.Anewluv.Chat.ChatContext  context)
        {
            ChatSeedMethods.seedgenerallookups(context);
            // SeedMethodsApiKeyModel.seedcascadeddata(context);

        }


    }
}
