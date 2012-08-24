using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Shell.MVC2.Infrastructure.Entities.UserRepairModel
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

        /// <summary>
        /// added to make sure databse is in correct schema
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRepairLog>().ToTable("UserRepairLogs", schemaName: "Logging");
       
        }


        public class Initializer : IDatabaseInitializer<UserRepairLogContext>
        {
            public void InitializeDatabase(UserRepairLogContext context)
            {
                if (!context.Database.Exists() || !context.Database.CompatibleWithModel(false))
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
