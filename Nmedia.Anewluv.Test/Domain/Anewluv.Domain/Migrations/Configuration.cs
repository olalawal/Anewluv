namespace Anewluv.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;   
   // using Nmedia.Infrastructure;

    internal sealed class ConfigurationAnewluv : DbMigrationsConfiguration<AnewluvContext>
    {
        public ConfigurationAnewluv()
        {
            AutomaticMigrationsEnabled = false;
           // MigrationsNamespace = "Anewluv.Domain";
        }

        protected override void Seed(AnewluvContext context)
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

    //internal sealed class ConfigurationChat : DbMigrationsConfiguration<Chat.ChatContext >
    //{
    //    public ConfigurationChat()
    //    {
    //        AutomaticMigrationsEnabled = true;
    //        MigrationsNamespace = "Anewluv.Domain.Chat";
    //    }

    //    protected override void Seed(Anewluv.Domain.Chat.ChatContext  context)
    //    {
    //        ChatSeedMethods.seedgenerallookups(context);
    //        // SeedMethodsApiKeyModel.seedcascadeddata(context);

    //    }


    //}
}
