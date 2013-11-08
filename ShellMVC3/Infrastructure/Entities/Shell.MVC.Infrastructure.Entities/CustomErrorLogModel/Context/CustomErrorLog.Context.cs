using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Nmedia.Infrastructure.Domain.Errorlog
{
    public partial  class ErrorlogContext : DbContext 
    {
       
        public DbSet<Errorlog> Errorlogs { get; set; }
       

       public ErrorlogContext()
            : base("name=ErrorlogContext")

        {

            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            Initializer init = new Initializer();
            init.InitializeDatabase(this);
        }


       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {

           customErrorlogmodelbuilder.buildgeneralmodels(modelBuilder);
       }

      

       public class Initializer : IDatabaseInitializer<ErrorlogContext>
       {
           public void InitializeDatabase(ErrorlogContext context)
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
