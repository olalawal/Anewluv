using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace NotificationModel
{
   //Configure migrations
   //PM> enable-migrations
   // PM> add-migration -startupproject NotificationModel ddsd
   // Update-DataBase -startupproject NotificationModel -verbose

    public class NotificationContext :DbContext 
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageTypeLookup> MessageTypeLookup { get; set; }
        public DbSet<MessageTemplateLookup> MessageTemplateLookup { get; set; }       
        public DbSet<MessageAddress> MessageAddress { get; set; }
        public DbSet<MessageAddressTypeLookup> MessageAddressTypeLookup { get; set; }
        public DbSet<MessageSystemAddress> MessageSystemAddresses { get; set; }
        public DbSet<MessageSystemAddressTypeLookup> MessageSystemAddressTypeLookup { get; set; }


        public NotificationContext()
            : base("name=NotificationContext")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            //rebuild DB if schema is differnt
            //Initializer init = new Initializer();            
           // init.InitializeDatabase(this);
        }
    }

    public class Initializer : IDatabaseInitializer<NotificationContext>
    {
        public void InitializeDatabase(NotificationContext context)
        {
            if (!context.Database.Exists() )//|| !context.Database.CompatibleWithModel(false))
            {
                context.Database.Create();              
                context.SaveChanges();
            }
            else if (!context.Database.CompatibleWithModel(false))
            {
            //DO migrations here
            }

        }
    }
     
}
