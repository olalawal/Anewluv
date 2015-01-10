using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using Nmedia.DataAccess.Interfaces;
using Nmedia.DataAccess;
using System.Data.Common;
using System.Data;
using System.Data.Entity.Validation;
using Anewluv.Domain.Data.Chat;


namespace Anewluv.Domain.Chat
{
    public partial class ChatContext :ContextBase
    {


        private static readonly IDictionary<Type, object> repos = new Dictionary<Type, object>();


        public ChatContext()
            : base("name=ChatContext")
        {
           // this.Configuration.ProxyCreationEnabled = true;
           // this.Configuration.AutoDetectChangesEnabled = true;
            //rebuild DB if schema is differnt
            //Initializer init = new Initializer();            
            // init.InitializeDatabase(this);
           // this.Configuration.ValidateOnSaveEnabled = false;
            IsAuditEnabled = true;
           // ObjectContext.SavingChanges += OnSavingChanges;
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<ChatContext>());
        }

        //add the obects alphabetically 


        #region "Db objects"
        
        // public DbSet<abuser> abusers { get; set; }
        public DbSet<ChatClient> ChatClients { get; set; } //Currect connected clients
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatRoom> ChatRooms { get; set; }
        public DbSet<ChatUser> ChatUsers { get; set; }
        #endregion
        //lookup tables
        //




      

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // modelBuilder.Entity<message>().ToTable("messages", schemaName: "Logging");
        //    //modelBuilder.Entity<address>().ToTable("messageAddresses", schemaName: "Logging");
        //    // modelBuilder.Entity<systemAddress>().ToTable("messageSystemAddresses", schemaName: "Logging");
        //    // modelBuilder.Entity<lu_template>().ToTable("lu_messageTemplate", schemaName: "Logging");
        //    // modelBuilder.Entity<lu_messageType>().ToTable("lu_messageType", schemaName: "Logging");
        //    // modelBuilder.Entity<lu_systemAddressType>().ToTable("lu_messageSystemAddressType", schemaName: "Logging");

        //    // map required relationships abusereport
        //    //**************************************

        //    //comment this out when sharing after generating the Database
        //    //only share with sql scripts 

        //    chatmodelbuilder.buildgeneralmodels(modelBuilder);
            
        //}

        //public class Initializer : IDatabaseInitializer<ChatContext>
        //{
        //    public void InitializeDatabase(ChatContext  context)
        //    {
        //        if (!context.Database.Exists() || !context.Database.CompatibleWithModel(false))
        //        {
        //            context.Database.Create();
        //            context.SaveChanges();
        //        }
        //        else if (!context.Database.CompatibleWithModel(false))
        //        {
        //            //DO migrations here
        //        }

        //    }
        //}

    }
}
