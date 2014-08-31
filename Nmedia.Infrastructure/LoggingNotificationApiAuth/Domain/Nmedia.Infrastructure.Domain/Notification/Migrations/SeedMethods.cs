using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Nmedia.Infrastructure.Domain.Data.Notification;
using Nmedia.Infrastructure.Domain;


namespace Nmedia.Infrastructure.Domain
{
    public class SeedMethodsNotificationModel
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


              //templatefilename lookup
              var templatefilenameqry = from templatefilenameenum value in Enum.GetValues(typeof(templatefilenameenum))
                                        //   where value != messagetemplatefilenameenum.NotSet
                                        orderby value // to sort by value; remove otherwise 
                                        select value;
              templatefilenameqry.ToList().ForEach(kvp => context.lu_templatefilename.AddOrUpdate(new lu_templatefilename()
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
              EFUtils.SaveChanges(context);

              //use create the System email addresses here 
              context.systemaddress.AddOrUpdate(h => h.emailaddress , new systemaddress()
              {

                  emailaddress = "AnewLuvDoNotReply@nmedia.com",
                  hostip = "192.168.0.114",
                  createdby = "olawal",
                  hostname = "",
                  credentialpassword = "kayode02",
                  creationdate = DateTime.Now,
                  //systemaddresstype =  context.lu_systemaddresstype.Where(z=>z.id ==(int)systemaddresstypeenum.DoNotReplyAddress).FirstOrDefault()
                  systemaddresstype_id = (int)systemaddresstypeenum.DoNotReplyAddress
              }
              );


              EFUtils.SaveChanges(context);


              var templateqry = from templateenum value in Enum.GetValues(typeof(templateenum))
                                //   where value != messagetemplateenum.NotSet
                                orderby value // to sort by value; remove otherwise 
                                select value;



              // var counter = 1;
              foreach (var value in templateqry)
              {
                  lu_template newitem = new lu_template();
                  //newitem.id = counter;
                  newitem.defaultlocation = "e:\\AppData\\NotificationService\\templates\\";
                  newitem.description = value.ToDescription();
                  newitem.active = true;
                  newitem.creationdate = DateTime.Now;
                  newitem.filename = context.lu_templatefilename.Where(z => z.id == (int)value).FirstOrDefault();
                  newitem.body = context.lu_templatebody.Where(z => z.id == (int)value).FirstOrDefault();
                  newitem.subject = context.lu_templatesubject.Where(z => z.id == (int)value).FirstOrDefault();
                  context.lu_template.AddOrUpdate(newitem);
                  //   counter = counter + 1;
              }





              ////add template names, to do maybe parse templates into Initial Catalog= from strings down the line
              ////
              //List<string> template = new List<string>();
              //List<string> templatefilename = new List<string>();

              //var templateqry = from templateenum value in Enum.GetValues(typeof(templateenum))
              //                  //   where value != messagetemplateenum.NotSet
              //                  orderby value // to sort by value; remove otherwise 
              //                  select value;

              //var templatefilenameqry = from templatefilenameenum value in Enum.GetValues(typeof(templatefilenameenum))
              //                  //   where value != messagetemplateenum.NotSet
              //                  orderby value // to sort by value; remove otherwise 
              //                  select value;


              //foreach (var value in templateqry)
              //{
              //    template.Add(EnumExtensionMethods.ToDescription(value));
              //    System.Diagnostics.Debug.WriteLine(EnumExtensionMethods.ToDescription(value));
              //}


              //foreach (var value in templatefilenameqry)
              //{
              //    templatefilename.Add(EnumExtensionMethods.ToDescription(value));
              //    System.Diagnostics.Debug.WriteLine(EnumExtensionMethods.ToDescription(value));
              //}


              //Dictionary<string, string> UrisByProject = new Dictionary<string, string>();
              //template.ForEach(p => UrisByProject.Add(p, templatefilename[template.FindIndex(t => t == p)]));

              //var counter = 1;
              //foreach (var value in UrisByProject)
              //{
              //     lu_template newitem = new lu_template();
              //    newitem.id = counter;
              //    newitem.description = value.Key;                  
              //    newitem.filename = value.Value + ".cshtml";
              //    newitem.active = true;
              //    newitem.creationdate = DateTime.Now;
              //    newitem.bodystring = context.lu_templatebody.Where(p => p.id == (int)counter).First();
              //    newitem.subjectstring = context.lu_templatesubject.Where(p => p.id == (int)counter).First();
              //    context.lu_template .AddOrUpdate(newitem);
              //    counter = counter + 1;
              //}


             //old way
              ////DO this last since it gets values from previous seeded body values
              ////template lookup
              //var templateqry = from templateenum value in Enum.GetValues(typeof(templateenum))
              //                  //   where value != messagetemplateenum.NotSet
              //                  orderby value // to sort by value; remove otherwise 
              //                  select value;
              //templateqry.ToList().ForEach(kvp => context.lu_template.AddOrUpdate(h => h.id, new lu_template()
              //{
              //    id = (int)kvp,
              //    description = EnumExtensionMethods.ToDescription(kvp),
              //    active = true,
              //    creationdate = DateTime.Now,
              //    bodystring = context.lu_templatebody.Where(p => p.id == (int)kvp).First(),
              //    subjectstring = context.lu_templatesubject.Where(p => p.id == (int)kvp).First()

              //}));


            //context.SaveChanges();
              EFUtils.SaveChanges(context);



          
        }


    }
}
