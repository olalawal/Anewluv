using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nmedia.Infrastructure.Domain.Data.ApiKey;


namespace Nmedia.Infrastructure.Domain
{
    public class SeedMethodsApiKeyModel
    {

        public static void seedgenerallookups(ApiKeyContext  context)
        {



            //application lookup
            var applicationqry = from applicationenum value in Enum.GetValues(typeof(applicationenum))
                                 //   where value != messageapplicationenum.NotSet
                                 orderby value // to sort by value; remove otherwise 
                                 select value;
            applicationqry.ToList().ForEach(kvp => context.lu_applications.AddOrUpdate(new lu_application()
            {
                id = (int)kvp,
                description = EnumExtensionMethods.ToDescription(kvp)

            }));
       
            //accesslevel lookup
            var accesslevelqry = from accesslevelsenum value in Enum.GetValues(typeof(accesslevelsenum))
                               //   where value != messageaccesslevelenum.NotSet
                                  orderby value // to sort by value; remove otherwise 
                                  select value;
              accesslevelqry.ToList().ForEach(kvp => context.lu_accesslevels.AddOrUpdate(new lu_accesslevel()
                {
                    id = (int)kvp,
                    description = EnumExtensionMethods.ToDescription(kvp)                 
                    
                }));

           
            //Add generic data I.e email system sender and a few email addresses to use to send stuff    
             //save this since the data is needed later for the temp
              EFUtils.SaveChanges(context);

            
            
              //add a few keys here
              //super admin anewluv site
              context.apikeys.AddOrUpdate(h => h.key, new apikey()              
              {
                  externalapplicationname = "Anewluvwebsite",
                   active = true ,
                key = Guid.Parse("bda11d91-7ade-4da1-855d-24adfe39d174") ,
                timestamp = DateTime.Now , 
                application  = context.lu_applications.Where(p => p.id == (int)applicationenum.anewluv).First(),
                accesslevel  = context.lu_accesslevels.Where(p => p.id == (int)accesslevelsenum.admin  ).First()

                 
              }
              );

            //read write anewluv site
              context.apikeys.AddOrUpdate(h => h.key, new apikey()
              {
                  active = true,
                  externalapplicationname = "AnewlvuIpadApplication",
                  key = Guid.Parse("460ad6f3-8216-469f-9b1c-52cffa5d812c"),
                  timestamp = DateTime.Now,
                  application = context.lu_applications.Where(p => p.id == (int)applicationenum.anewluv).First(),
                  accesslevel = context.lu_accesslevels.Where(p => p.id == (int)accesslevelsenum.admin).First()

              }
             );

            //ipdad read write
              context.apikeys.AddOrUpdate(h=>h.key, new apikey()              
              {
                  active =true ,
                  externalapplicationname = "AnewlvuIpadApplication",
                  key = Guid.Parse("460ad6f3-8216-469f-9b1c-52cffa5d812c"),
                  timestamp = DateTime.Now,                 
                  application = context.lu_applications.Where(p => p.id == (int)applicationenum.anewluvipad).First(),
                  accesslevel = context.lu_accesslevels.Where(p => p.id == (int)accesslevelsenum.admin).First()
                    
              }
              );


           

              EFUtils.SaveChanges(context);




              //use create some users here
              context.users.AddOrUpdate(h => h.username, new user()
              {
                  username = "pavankumar",
                  email = "pavankumark130@gmail.com",
                  active = true,
                  registeringapplication = "web development",
                  timestamp = DateTime.Now


              }
              );

              context.users.AddOrUpdate(h => h.username, new user()
              {
                  username = "ranapaul",
                  email = "rana.paul130@gmail.com",
                  active = true,
                  registeringapplication = "ipad devlopment",
                  timestamp = DateTime.Now


              }
             );


              context.users.AddOrUpdate(h => h.username, new user()
              {
                  username = "olawal",
                  email = "ola_lawal@yahoo.com",
                  active = true,
                  registeringapplication = "anewluvcore",
                  timestamp = DateTime.Now
              }
              );

            
             //link the user to the correct api key 
              EFUtils.SaveChanges(context);

          
              user user = context.users.Where(p => p.username == "pavankumar").FirstOrDefault();
              apikey apikey =  context.apikeys.Where(p => p.accesslevel.id   == (int)accesslevelsenum.readwriteuser && p.application.id == (int)applicationenum.anewluv).FirstOrDefault();
              user.apikeys.Add(apikey);

              user = context.users.Where(p => p.username == "ranapaul").FirstOrDefault();
              apikey = context.apikeys.Where(p => p.accesslevel.id == (int)accesslevelsenum.readwriteuser && p.application.id == (int)applicationenum.anewluvipad).FirstOrDefault();
              user.apikeys.Add(apikey);

               user = context.users.Where(p => p.username == "olawal").FirstOrDefault();
               apikey = context.apikeys.Where(p => p.accesslevel.id == (int)accesslevelsenum.admin).FirstOrDefault();
              user.apikeys.Add(apikey);
             



            //context.SaveChanges();
              EFUtils.SaveChanges(context);



          
        }


    }
}
