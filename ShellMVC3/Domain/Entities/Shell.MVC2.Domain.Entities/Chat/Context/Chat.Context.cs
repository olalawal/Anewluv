using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace Shell.MVC2.Domain.Entities.Anewluv.Chat
{
    public partial  class ChatContext : DbContext
    {
        //add the obects alphabetically 

        
       // public DbSet<abuser> abusers { get; set; }
        public DbSet<ChatClient> ChatClients { get; set; } //Currect connected clients
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
       
        //lookup tables
        //
        public ChatContext()
            : base("name=ChatContext")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            //rebuild DB if schema is differnt
            //Initializer init = new Initializer();            
            // init.InitializeDatabase(this);
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<message>().ToTable("messages", schemaName: "Logging");
            //modelBuilder.Entity<address>().ToTable("messageAddresses", schemaName: "Logging");
            // modelBuilder.Entity<systemAddress>().ToTable("messageSystemAddresses", schemaName: "Logging");
            // modelBuilder.Entity<lu_template>().ToTable("lu_messageTemplate", schemaName: "Logging");
            // modelBuilder.Entity<lu_messageType>().ToTable("lu_messageType", schemaName: "Logging");
            // modelBuilder.Entity<lu_systemAddressType>().ToTable("lu_messageSystemAddressType", schemaName: "Logging");

            // map required relationships abusereport
            //**************************************

            //comment this out when sharing after generating the database
            //only share with sql scripts 

            chatmodelbuilder.buildgeneralmodels(modelBuilder);
            
        }

        public class Initializer : IDatabaseInitializer<AnewluvContext >
        {
            public void InitializeDatabase(ChatContext  context)
            {
                if (!context.Database.Exists() || !context.Database.CompatibleWithModel(false))
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
}
