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

         
       
            //addresstype lookup
            var addresstypeqry = from addresstypeenum value in Enum.GetValues(typeof(addresstypeenum))
                               //   where value != messageaddresstypeenum.NotSet
                                  orderby value // to sort by value; remove otherwise 
                                  select value;
              addresstypeqry.ToList().ForEach(kvp => context.lu_addresstype.AddOrUpdate(new lu_addresstype()
                {
                    id = (int)kvp,
                    description = EnumExtensionMethods.ToDescription(kvp),
                    creationdate = DateTime.Now ,
                     active = true 
                    
                }));

            //message type lookup
              var messagetypeqry = from messagetypeenum value in Enum.GetValues(typeof(messagetypeenum))
                                   //   where value != messagemessagetypeenum.NotSet
                                   orderby value // to sort by value; remove otherwise 
                                   select value;
              messagetypeqry.ToList().ForEach(kvp => context.lu_messagetype.AddOrUpdate(new lu_messagetype()
              {
                  id = (int)kvp,
                  description = EnumExtensionMethods.ToDescription(kvp),
                    creationdate = DateTime.Now ,
                     active = true 
              }));


              //news lookup
              var newsqry = from newsenum value in Enum.GetValues(typeof(newsenum))
                                   //   where value != messagenewsenum.NotSet
                                   orderby value // to sort by value; remove otherwise 
                                   select value;
              newsqry.ToList().ForEach(kvp => context.lu_news.AddOrUpdate(new lu_news()
              {
                  id = (int)kvp,
                  description = EnumExtensionMethods.ToDescription(kvp),
                    creationdate = DateTime.Now ,
                     active = true 
              }));


              //systemaddresstype lookup
              var systemaddresstypeqry = from systemaddresstypeenum value in Enum.GetValues(typeof(systemaddresstypeenum))
                                   //   where value != messagesystemaddresstypeenum.NotSet
                                   orderby value // to sort by value; remove otherwise 
                                   select value;
              systemaddresstypeqry.ToList().ForEach(kvp => context.lu_systemaddresstype.AddOrUpdate(new lu_systemaddresstype()
              {
                  id = (int)kvp,
                  description = EnumExtensionMethods.ToDescription(kvp),                  
                  creationdate = DateTime.Now,
                  active = true 
                    
              }));

                        
              //templatebody lookup
              var templatebodyqry = from templatebodyenum value in Enum.GetValues(typeof(templatebodyenum))
                                   //   where value != messagetemplatebodyenum.NotSet
                                   orderby value // to sort by value; remove otherwise 
                                   select value;
              templatebodyqry.ToList().ForEach(kvp => context.lu_templatebody.AddOrUpdate(new lu_templatebody()
              {
                  id = (int)kvp,
                  description = EnumExtensionMethods.ToDescription(kvp),
                  creationdate = DateTime.Now,
                  active = true 
              }));


              //templatesubject lookup
              var templatesubjectqry = from templatesubjectenum value in Enum.GetValues(typeof(templatesubjectenum))
                                   //   where value != messagetemplatesubjectenum.NotSet
                                   orderby value // to sort by value; remove otherwise 
                                   select value;
              templatesubjectqry.ToList().ForEach(kvp => context.lu_templatesubject.AddOrUpdate(new lu_templatesubject()
              {
                  id = (int)kvp,
                  description = EnumExtensionMethods.ToDescription(kvp),
                  creationdate = DateTime.Now,
                  active = true 
              }));
            



            //Add generic data I.e email system sender and a few email addresses to use to send stuff               

             //save this since the data is needed later for the template
              Utils.SaveChanges(context);

              //use create the System email addresses here 
              context.systemaddress.AddOrUpdate( new systemaddress()
              {

                  emailaddress = "AnewLuvDoNotReply@nmedia.com",
                  hostip = "192.168.0.114",
                  createdby = "olawal",
                  hostname = "",
                  credentialpassword = "kayode02",
                  creationdate = DateTime.Now,
                  systemaddresstype =  context.lu_systemaddresstype.Where(z=>z.id ==(int)systemaddresstypeenum.DoNotReplyAddress).FirstOrDefault()

              }
              );

              //add a few templates 
              //TO DO use razor 
              //use create the System email addresses here 

              //DO this last since it gets values from previous seeded body values
              //template lookup
              var templateqry = from templateenum value in Enum.GetValues(typeof(templateenum))
                                //   where value != messagetemplateenum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;
              templateqry.ToList().ForEach(kvp => context.lu_template.AddOrUpdate(new lu_template()
              {
                  id = (int)kvp,
                  description = EnumExtensionMethods.ToDescription(kvp),
                  active = true,
                  creationdate = DateTime.Now,
                  bodystring = context.lu_templatebody.Where(p => p.id == (int)kvp).First(),
                  subjectstring = context.lu_templatesubject.Where(p => p.id == (int)kvp).First()

              }));


            //context.SaveChanges();
            Utils.SaveChanges(context);



          
        }


    }
}
