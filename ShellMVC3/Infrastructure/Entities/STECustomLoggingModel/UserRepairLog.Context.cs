using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace CustomLoggingModel
{
    public class UserRepairLogContext : DbContext 
    {
       
        public DbSet<UserRepairLog> UserRepairLogs { get; set; }


        public UserRepairLogContext()
            : base("name=UserRepairLogContext")

        {

            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            Initializer init = new Initializer();
            init.InitializeDatabase(this);
        }


        public class Initializer : IDatabaseInitializer<UserRepairLogContext>
        {
            public void InitializeDatabase(UserRepairLogContext context)
            {
                if (!context.Database.Exists()) // || !context.Database.CompatibleWithModel(false))
                {
                    context.Database.Create(); 
                    context.SaveChanges();
                }
                else
                {
                    //TO DO migrations here

                }


            }
        }



    }
}
