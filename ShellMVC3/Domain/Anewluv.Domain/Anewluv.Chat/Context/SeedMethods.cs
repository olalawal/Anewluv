using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Anewluv.Domain.Chat;
using Shell.MVC2.Infrastructure;
using Anewluv.Domain.Data.Chat;

namespace Anewluv.Domain.Migrations
{
    public class ChatSeedMethods
    {

        public static void seedgenerallookups(ChatContext context)
        {


            //9-12-2012
            //added for profile activity type
            //roomstatus
            //filter an enum for not set since that is the zero value i.e  
            var roomstatusqry = from roomstatusEnum value in Enum.GetValues(typeof(roomstatusEnum))                              
                               orderby value // to sort by value; remove otherwise 
                               select value;
            roomstatusqry.ToList().ForEach(kvp => context.lu_roomstatus.AddOrUpdate(new lu_roomstatus()
            {
                id = (int)kvp,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));

            //userstatuss
            //filter an enum for not set since that is the zero value i.e  
            var userstatusqry = from userstatusEnum value in Enum.GetValues(typeof(userstatusEnum))                               
                               orderby value // to sort by value; remove otherwise 
                               select value;       
            userstatusqry.ToList().ForEach(kvp => context.lu_userstatus.AddOrUpdate(new lu_userstatus()
            {
                id = (int)kvp,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


          

          
        }


    }
}
