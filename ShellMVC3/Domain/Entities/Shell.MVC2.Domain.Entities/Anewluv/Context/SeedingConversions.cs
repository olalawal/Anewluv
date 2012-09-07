using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shell.MVC2.Domain.Entities.Anewluv;
using Shell.MVC2.Infrastructure;
using Dating.Server.Data.Models;

namespace Shell.MVC2.Domain.Entities.Migrations
{
    class SeedingConversions
    {


        //synch up anew luv database with the new database 
        //add the old database model 
        public static void ConvertDatabase()
       {
           var olddb = new AnewluvFtsEntities();
           var context = new AnewluvContext();

          

       }
    }
}
