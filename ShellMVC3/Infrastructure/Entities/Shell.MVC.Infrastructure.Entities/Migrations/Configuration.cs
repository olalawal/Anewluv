namespace Shell.MVC2.Infrastructure.Entities.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Data.Entity.Validation;
    using System.Text;
    using Shell.MVC2.Infrastructure.Entities.NotificationModel;
    using Shell.MVC2.Infrastructure.Entities.UserRepairModel;
    using Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel;
    using Shell.MVC2.Infrastructure.Entities;


    internal sealed class ConfigurationNotificationModel : DbMigrationsConfiguration<NotificationModel.NotificationContext>
    {
        public ConfigurationNotificationModel()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsNamespace = "Shell.MVC2.Infrastructure.Entities.NotificationModel";
        }

        protected override void Seed(NotificationModel.NotificationContext context)
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

            //Put the data to be loaded here
            // if (!context.Database.Exists())//|| !context.Database.CompatibleWithModel(false))
            // {
            //context.Database.Delete();
            //context.Database.Create();
            //populate the lookup table for log sAddress Types
            var AddressTypeList = Enum.GetValues(typeof(MessageAddressTypeEnum))
           .Cast<MessageAddressTypeEnum>()
            .ToDictionary(t => (int)t, t => t.ToString()).ToList();
            // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<LogSeverityEnum >().ToList(); 
            AddressTypeList.ForEach(kvp => context.lu_addressType.AddOrUpdate(new lu_addressType()
            {
               id = kvp.Key,
                description = kvp.Value
            }));


            //populate the lookup table for System Address Types
            var SystemAddressTypeList = Enum.GetValues(typeof(MessageSystemAddressTypeEnum))
              .Cast<MessageSystemAddressTypeEnum>()
               .ToDictionary(t => (int)t, t => t.ToString()).ToList();
            // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<LogSeverityEnum >().ToList(); 
            SystemAddressTypeList.ForEach(kvp => context.lu_systemAddressType.AddOrUpdate(new lu_systemAddressType()
            {
               id = kvp.Key,
                description = kvp.Value
            }));


            //PopulateMessage Type lookups
            //populate the lookup table for System Address Types  , use the new create method
            //we need a later step to add the body of the string format values or the razor template
            var MessageTemplateList = Enum.GetValues(typeof(MessageTemplateEnum))
              .Cast<MessageTemplateEnum>()
               .ToDictionary(t => (int)t, t => t.ToString()).ToList();
            // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<LogSeverityEnum >().ToList(); 
            MessageTemplateList.ForEach(kvp => context.lu_template.AddOrUpdate(lu_template.Create(c =>
            {
                c.id = kvp.Key;
                c.description = kvp.Value;

            }
            )));

            //PopulateMessage Type lookups
            //populate the lookup table for System Address Types  , use the new create method
            var MessageTypeList = Enum.GetValues(typeof(MessageTypeEnum))
              .Cast<MessageTypeEnum>()
               .ToDictionary(t => (int)t, t => t.ToString()).ToList();
            // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<LogSeverityEnum >().ToList(); 
            MessageTypeList.ForEach(kvp => context.lu_messageType .AddOrUpdate(lu_messageType.Create(c =>
            {
                c.id = kvp.Key;
                c.description = kvp.Value;

            }
            )));






            //Add generic data I.e email system sender and a few email addresses to use to send stuff               

            context.address.AddOrUpdate(p => p.emailAddress, address.Create(c =>
            {
                c.emailAddress = "olawal@medtox.com";
                c.otherIdentifer = "ola_lawal";
                c.username = "mtxdomain'\'olawal";
                c.creationDate = DateTime.Now;
                c.id = (int)MessageAddressTypeEnum.Developer;
            }
           ));

            context.address.AddOrUpdate(p => p.emailAddress, address.Create(c =>
            {
                c.emailAddress = "mbrown@medtox.com";
                c.otherIdentifer = "Mike Brown";
                c.username = "mtxdomain'\'Mbrown";
                c.creationDate = DateTime.Now;
                c.id = (int)MessageAddressTypeEnum.ProjectLead;
            }
           ));


            //use create the System email addresses here 
            context.systemAddresses.AddOrUpdate(p => p.emailAddress, systemAddress.Create(c =>
            {
              
                c.emailAddress = "AnewLuvDoNotReply@nmedia.com";
                c.hostIp = "192.168.0.114";
                c.createdBy = "olawal";
                c.hostName = "";  
                c.credentialPassword ="kayode02";
                c.creationDate = DateTime.Now;
                c.id = (int)MessageSystemAddressTypeEnum.DoNotReplyAddress;
            }
            ));

            //add a few templates 
            //TO DO use razor 
            //use create the System email addresses here 


            //context.SaveChanges();
            Utils.SaveChanges(context);


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
            ApplicationList.ForEach(kvp => context.lu_application.Add(new lu_application()
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

