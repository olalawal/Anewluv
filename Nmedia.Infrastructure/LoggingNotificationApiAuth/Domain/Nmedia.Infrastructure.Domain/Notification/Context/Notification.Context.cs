using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Nmedia.Infrastructure.Domain.Data.Notification;

using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Common;
using System.Data.Entity.Core.Objects;


//using Nmedia.DataAccess.Interfaces;
//using Nmedia.DataAccess;
using Repository.Pattern.Ef6;

namespace Nmedia.Infrastructure.Domain
{
    //Configure migrations
    //PM> enable-migrations
    // PM> add-migration -startupproject NotificationModel ddsd
    // Update-Database -startupproject NotificationModel -verbose

    public partial class NotificationContext : DataContext //DbContext, IUnitOfWork
    {
       
        public DbSet<address> address { get; set; }
        public DbSet<systemaddress> systemaddress { get; set; }
        public DbSet<lu_addresstype> lu_addresstype { get; set; }
        public DbSet<lu_messagetype> lu_messagetype { get; set; }
        public DbSet<lu_news> lu_news { get; set; }
        public DbSet<lu_systemaddresstype> lu_systemaddresstype { get; set; }
        public DbSet<lu_template> lu_template { get; set; }
        public DbSet<lu_templatefilename> lu_templatefilename { get; set; }
        public DbSet<lu_templatebody> lu_templatebody { get; set; }
        public DbSet<lu_templatesubject> lu_templatesubject { get; set; }
        public DbSet<message> messages { get; set; }

        public DbSet<lu_application> lu_application { get; set; }  //matches API application table



        static NotificationContext()
        {
            Database.SetInitializer<NotificationContext>(null);
        }


        public NotificationContext()
            : base("name=NotificationContext")
        {
         //   this.Configuration.ValidateOnSaveEnabled = false;
          //  IsAuditEnabled = true;
         //   ObjectContext.SavingChanges += OnSavingChanges;
         //   Database.SetInitializer(
         //   new DropCreateDatabaseIfModelChanges<NotificationContext>());
        }


        //public class Initializer : IDatabaseInitializer<NotificationContext>
        //{
        //    public void InitializeDatabase(NotificationContext context)
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