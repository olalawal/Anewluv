namespace NotificationModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Data.Entity.Validation;
    using System.Text;



    internal sealed class Configuration : DbMigrationsConfiguration<NotificationModel.NotificationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
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
                AddressTypeList.ForEach(kvp => context.MessageAddressTypeLookup.AddOrUpdate(new MessageAddressTypeLookup()
                {
                   MessageAddressTypeLookupID= kvp.Key,
                    Description = kvp.Value
                }));


                //populate the lookup table for System Address Types
                var SystemAddressTypeList = Enum.GetValues(typeof(MessageSystemAddressTypeEnum))
                  .Cast<MessageSystemAddressTypeEnum>()
                   .ToDictionary(t => (int)t, t => t.ToString()).ToList();
                // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<LogSeverityEnum >().ToList(); 
                SystemAddressTypeList.ForEach(kvp => context.MessageSystemAddressTypeLookup.AddOrUpdate(new MessageSystemAddressTypeLookup()
                {
                   MessageSystemAddressTypeLookupID = kvp.Key,
                    Description = kvp.Value
                }));


                //PopulateMessage Type lookups
                //populate the lookup table for System Address Types  , use the new create method
                //we need a later step to add the body of the string format values or the razor template
                var MessageTemplateList = Enum.GetValues(typeof(MessageTemplateEnum))
                  .Cast<MessageTemplateEnum>()
                   .ToDictionary(t => (int)t, t => t.ToString()).ToList();
                // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<LogSeverityEnum >().ToList(); 
                MessageTemplateList.ForEach(kvp => context.MessageTemplateLookup.AddOrUpdate(MessageTemplateLookup.Create(c =>
                {
                    c.MessageTemplateLookupId = kvp.Key;
                    c.Description = kvp.Value;

                }
                )));

                //PopulateMessage Type lookups
                //populate the lookup table for System Address Types  , use the new create method
                var MessageTypeList = Enum.GetValues(typeof(MessageTypeEnum))
                  .Cast<MessageTypeEnum>()
                   .ToDictionary(t => (int)t, t => t.ToString()).ToList();
                // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<LogSeverityEnum >().ToList(); 
                MessageTypeList.ForEach(kvp => context.MessageTypeLookup.AddOrUpdate(MessageTypeLookup.Create(c =>
                 {
                     c.MessageTypeLookupID = kvp.Key;
                     c.Description = kvp.Value;

                 }
                )));

             




               //Add generic data I.e email system sender and a few email addresses to use to send stuff               

                context.MessageAddress.AddOrUpdate(p=>p.EmailAddress,  MessageAddress.Create (c =>
               {
                   c.EmailAddress  = "olawal@medtox.com";
                   c.OtherIdentifer = "ola_lawal";
                   c.Username = "mtxdomain'\'olawal";
                   c.MessageAddressTypeLookupID  = (int)MessageAddressTypeEnum.Developer;
               }              
               ));

                context.MessageAddress.AddOrUpdate(p => p.EmailAddress, MessageAddress.Create(c =>
                {
                    c.EmailAddress = "mbrown@medtox.com";
                    c.OtherIdentifer = "Mike Brown";
                    c.Username = "mtxdomain'\'Mbrown";
                    c.MessageAddressTypeLookupID = (int)MessageAddressTypeEnum.ProjectLead ;
                }
               ));


                //use create the System email addresses here 
                context.MessageSystemAddresses.AddOrUpdate(p => p.EmailAddress, MessageSystemAddress.Create(c =>
                {
                    c.EmailAddress = "DoNotReply@nmedia.com";
                    c.HostIp  = "192.168.0.114";
                    c.CreatedBy  = "olawal";
                    c.HostName  = "";
                    c.MessageSystemAddressTypeLookupID = (int)MessageSystemAddressTypeEnum.DoNotReplyAddress;
                }
                ));

              //add a few templates 
              //TO DO use razor 
                //use create the System email addresses here 
                

                //context.SaveChanges();
                SaveChanges(context);

            
        }

        public void SaveChanges(DbContext context)
        {

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb.ToString());
            }
        }
    }
}
