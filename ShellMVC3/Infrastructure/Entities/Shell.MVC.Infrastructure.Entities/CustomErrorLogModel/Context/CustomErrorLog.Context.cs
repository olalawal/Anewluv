using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Shell.MVC2.Infrastructure.Entities.CustomErrorLogModel
{
    public partial  class CustomErrorLogContext : DbContext 
    {
       
        public DbSet<errorlog> errorlogs { get; set; }
       

       public CustomErrorLogContext()
            : base("name=CustomErrorLogContext")

        {

            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            Initializer init = new Initializer();
            init.InitializeDatabase(this);
        }


       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {

           customerrorlogmodelbuilder.buildgeneralmodels(modelBuilder);
       }

      

       public class Initializer : IDatabaseInitializer<CustomErrorLogContext>
       {
           public void InitializeDatabase(CustomErrorLogContext context)
           {
               if (!context.Database.Exists())
               {
                   //  context.Database.Delete(); 
                   context.Database.Create();
                   context.SaveChanges();
               }
               else if ( !context.Database.CompatibleWithModel(false))
               {
                   //TO DO migrations here
                    context.Database.Delete();
                    context.Database.Create();
                    context.SaveChanges();
               }
           
               
           }
       }


    }
}
