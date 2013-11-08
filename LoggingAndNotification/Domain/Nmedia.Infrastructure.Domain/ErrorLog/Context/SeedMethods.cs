using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Shell.MVC2.Infrastructure;
using Nmedia.Infrastructure.Domain.Data.Errorlog;

namespace Nmedia.Infrastructure.Domain
{
    public class SeedMethodsErrorlogModel
    {

        public static void seedgenerallookups(ErrorlogContext  context)
        {


            //9-12-2012
            //added for profile activity type
            //application
            //filter an enum for not set since that is the zero value i.e  
            var applicationqry = from applicationEnum value in Enum.GetValues(typeof(applicationEnum))                              
                               orderby value // to sort by value; remove otherwise 
                               select value;
            applicationqry.ToList().ForEach(kvp => context.lu_application.AddOrUpdate(new lu_application()
            {
                id = (int)kvp,
                 description   = EnumExtensionMethods.ToDescription(kvp)
            }));

            //logseveritys
            //filter an enum for not set since that is the zero value i.e  
            var logseverityqry = from logseverityEnum value in Enum.GetValues(typeof(logseverityEnum))
                               orderby value // to sort by value; remove otherwise 
                               select value;       
            logseverityqry.ToList().ForEach(kvp => context.lu_logseverity.AddOrUpdate(new lu_logseverity()
            {
                id = (int)kvp,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


            //logseverityinternals
            //filter an enum for not set since that is the zero value i.e  
            var logseverityinternalqry = from logseverityinternalEnum value in Enum.GetValues(typeof(logseverityinternalEnum))
                                 orderby value // to sort by value; remove otherwise 
                                 select value;
            logseverityinternalqry.ToList().ForEach(kvp => context.lu_logseverityinternal.AddOrUpdate(new lu_logseverityinternal()
            {
                id = (int)kvp,
                description = EnumExtensionMethods.ToDescription(kvp)
            }));


          
        }


    }
}
