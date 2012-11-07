using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Infrastructure;


namespace Shell.MVC2.Infrastructure.Entities.NotificationModel
{
    public class SeedMethods
    {

        public static void seedgenerallookups(NotificationContext  context)
        {

         
            var AddressTypeList = Enum.GetValues(typeof(messageaddresstypeenum))
           .Cast<messageaddresstypeenum>()
            .ToDictionary(t => (int)t, t => t.ToString()).ToList();
            // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<LogSeverityEnum >().ToList(); 
            AddressTypeList.ForEach(kvp => context.lu_addresstype.AddOrUpdate(new lu_addresstype()
            {
                id = kvp.Key,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            var messageaddresstypeqry = from messageaddresstypeenum value in Enum.GetValues(typeof(messageaddresstypeenum))
                               //   where value != messageaddresstypeenum.NotSet
                                  orderby value // to sort by value; remove otherwise 
                                  select value;
            messageaddresstypeqry.ToList().ForEach(kvp => context.lu_messageaddresstype.AddOrUpdate(new lu_messageaddresstype()
            {
                id = (int)kvp,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //populate the lookup table for System Address Types
            var SystemAddressTypeList = Enum.GetValues(typeof(messagesystemaddresstypeenum))
              .Cast<messagesystemaddresstypeenum>()
               .ToDictionary(t => (int)t, t => t.ToString()).ToList();
            // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<LogSeverityEnum >().ToList(); 
            SystemAddressTypeList.ForEach(kvp => context.lu_systemaddresstype.AddOrUpdate(new lu_systemaddresstype()
            {
                id = kvp.Key,
                description = kvp.Value
            }));


            //PopulateMessage Type lookups
            //populate the lookup table for System Address Types  , use the new create method
            //we need a later step to add the body of the string format values or the razor template
            var MessageTemplateList = Enum.GetValues(typeof(templateenum))
              .Cast<templateenum>()
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
            var MessageTypeList = Enum.GetValues(typeof(messagetypeenum))
              .Cast<messagetypeenum>()
               .ToDictionary(t => (int)t, t => t.ToString()).ToList();
            // var jobInstanceStateList = EnumExtensions.ConvertEnumToDictionary<LogSeverityEnum >().ToList(); 
            MessageTypeList.ForEach(kvp => context.lu_messagetype.AddOrUpdate(lu_messagetype.Create(c =>
            {
                c.id = kvp.Key;
                c.description = kvp.Value;

            }
            )));
            




            //Add generic data I.e email system sender and a few email addresses to use to send stuff               

          
            //use create the System email addresses here 
            context.systemAddresses.AddOrUpdate(p => p.emailAddress, systemAddress.Create(c =>
            {

                c.emailAddress = "AnewLuvDoNotReply@nmedia.com";
                c.hostIp = "192.168.0.114";
                c.createdBy = "olawal";
                c.hostName = "";
                c.credentialPassword = "kayode02";
                c.creationdate = DateTime.Now;
                c.id = (int)messagesystemaddresstypeenum.DoNotReplyAddress;
            }
            ));

            //add a few templates 
            //TO DO use razor 
            //use create the System email addresses here 


            //context.SaveChanges();
            Utils.SaveChanges(context);



            //9-12-2012
            //added for profile activity type
            //activitytype
            //filter an enum for not set since that is the zero value i.e  
            var activitytypeqry = from activitytypeenum value in Enum.GetValues(typeof(activitytypeEnum))
                               where value != activitytypeEnum.NotSet
                               orderby value // to sort by value; remove otherwise 
                               select value;
            activitytypeqry.ToList().ForEach(kvp => context.lu_activitytype.AddOrUpdate(new lu_activitytype()
            {
                id = (int)kvp,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

           




           
          
        }


    }
}
