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
    public partial class ChatContext : DbContext, IUnitOfWork
    {


        private static readonly IDictionary<Type, object> repos = new Dictionary<Type, object>();


        public ChatContext()
            : base("name=ChatContext")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            //rebuild DB if schema is differnt
            //Initializer init = new Initializer();            
            // init.InitializeDatabase(this);
            this.Configuration.ValidateOnSaveEnabled = false;
            IsAuditEnabled = true;
            ObjectContext.SavingChanges += OnSavingChanges;
            Database.SetInitializer(
                 new DropCreateDatabaseIfModelChanges<AnewluvContext>());
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




        #region IContext Implementation

        public bool SetIsolationToDefault
        {
            set
            {
                if (value == true)
                {
                    var test = (this as IObjectContextAdapter).ObjectContext;
                    test.ExecuteStoreCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
                }
            }
        }


        public ObjectContext ObjectContext
        {
            get
            {
                return (this as IObjectContextAdapter).ObjectContext;
            }
        }



        //4-24-2013 olawal newly added
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }


        public bool IsAuditEnabled
        {
            get;
            set;
        }

        public bool DisableProxyCreation
        {

            set { ObjectContext.ContextOptions.ProxyCreationEnabled = !value; }
            get { return !ObjectContext.ContextOptions.ProxyCreationEnabled; }
        }

        public bool DisableLazyLoading
        {
            set { ObjectContext.ContextOptions.LazyLoadingEnabled = !value; }
        }

        public void ChangeState<T>(T entity, System.Data.Entity.EntityState state) where T : class
        {
            Entry<T>(entity).State = state;
        }

        public IDbSet<T> GetEntitySet<T>()
        where T : class
        {
            return Set<T>();
        }

        public virtual int Commit()
        {
            if (this.ChangeTracker.Entries().Any(IsChanged))
            {
                return this.SaveChanges();
            }
            return 0;
        }

        private static bool IsChanged(DbEntityEntry entity)
        {
            return IsStateEqual(entity, System.Data.Entity.EntityState.Added) ||
                   IsStateEqual(entity, System.Data.Entity.EntityState.Deleted) ||
                   IsStateEqual(entity, System.Data.Entity.EntityState.Modified);
        }

        private static bool IsStateEqual(DbEntityEntry entity, System.Data.Entity.EntityState state)
        {
            return (entity.State & state) == state;
        }

        public virtual DbTransaction BeginTransaction()
        {
            var connection = this.ObjectContext.Connection;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection
                .BeginTransaction(IsolationLevel.ReadCommitted);
        }
        #endregion

        #region "Code pulled from repostiory pattern since EF supports unit of work pattern no need of a separate repo?"





        //#endregion
        public void Add<T>(T entity)
        where T : class
        {
            this.GetEntitySet<T>().Add(entity);


        }
        //public bool AddAndAudit(T entity)
        //{
        //    //TO DO allow this to be flagable on the context instantiation side
        //    this.EnableAuditLog = true;
        //    using (var transaction = Context.BeginTransaction())
        //    {
        //        try
        //        {
        //            //Added via DI on service call
        //            //IRepository<review> repository = Context.GetRepository<T>();
        //            this.Add(entity);
        //            int i = Context.Commit();
        //            transaction.Commit();
        //            return (i > 0);
        //        }
        //        catch (Exception)
        //        {
        //            transaction.Rollback();
        //            throw;
        //        }

        //    }
        //}

        public void Update<T>(T entity)
        where T : class
        {
            this.ChangeState(entity, System.Data.Entity.EntityState.Modified);
        }

        public bool UpdateAndAudit<T>(T entity)
         where T : class
        {

            this.IsAuditEnabled = true;
            using (var transaction = this.BeginTransaction())
            {
                try
                {
                    //Added via DI on service call
                    //IRepository<review> repository = Context.GetRepository<T>();
                    this.Update(entity);
                    int i = this.Commit();
                    transaction.Commit();
                    return (i > 0);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }

        public void Remove<T>(T entity)
        where T : class
        {
            this.ChangeState(entity, System.Data.Entity.EntityState.Deleted);
        }

        // Method using Execute Store QUery
        public List<T> ExecuteStoredProcedure<T>(string commandText, params object[] parameters)
         where T : class
        {
            // List<T> myList = new List<T>();

            var groupData = this.ObjectContext.ExecuteStoreQuery<T>(commandText, parameters);

            return groupData.ToList();
        }

        public bool RemoveAndAudit<T>(T entity)
        where T : class
        {

            this.IsAuditEnabled = true;
            using (var transaction = this.BeginTransaction())
            {
                try
                {
                    //Added via DI on service call
                    //IRepository<review> repository = Context.GetRepository<T>();
                    this.Remove(entity);
                    int i = this.Commit();
                    transaction.Commit();
                    return (i > 0);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }


        public IRepository<T> GetRepository<T>() where T : class, new()
        {
            if (repos != null)
            {
                if (!repos.ContainsKey(typeof(T)))
                {
                    var repo = new EFRepository<T>(this);
                    try
                    {
                        repos.Add(typeof(T), repo);
                    }
                    catch
                    {
                        //no error really
                    }

                }
                return (EFRepository<T>)repos[typeof(T)];
            }
            else return null;
        }


        // public void Dispose() { this.Dispose(); }

        //public List<T> ExecuteStoredProcedure(string commandText, object[] parameters)
        //{
        //    // List<T> myList = new List<T>();
        //    //convert params to objectparamters 
        //    var obparameters = new ObjectParameter[] ;//{ new ObjectParameter("FirstName", "Bob") };

        //    foreach (object o in parameters)
        //    {


        //    }

        //    var groupData = Context.ObjectContext.ExecuteFunction<T>(commandText, parameters);

        //    return groupData.ToList();
        //}

        #endregion


        #region "Overides"

        //overide that allows clearing or repos
        protected override void Dispose(bool disposing)
        {
            repos.Clear();
            base.Dispose(disposing);
        }



        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //TO DO add logging
                DbEntityEntry entry = ex.Entries.FirstOrDefault();
                var objContext = ((IObjectContextAdapter)ObjectContext).ObjectContext;
                // Get failed entry
                objContext.AcceptAllChanges();
                //objContext.Refresh(RefreshMode.ClientWins, ex.Entries.Where(e => e.State == System.Data.Entity.EntityState.Added).Select(e => e.Entity));

                // Now call refresh on ObjectContext
                // objContext.Refresh(RefreshMode.ClientWins, entry.Entity);  
                return 1;
            }
            catch (DbEntityValidationException ex)
            {
                var sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                    ); // Add the original exception as the innerException
            }
            catch (DbUpdateException ex)
            {
                var sb = new StringBuilder();

                foreach (var badentries in ex.Entries)
                {
                    //  sb.AppendFormat("{0} failed validation\n", failure..Entity.GetType());
                    //  foreach (var error in failure.ValidationErrors)
                    //  {
                    sb.AppendFormat("- {0} : {1}", badentries.CurrentValues, badentries.State);
                    sb.AppendLine();
                    //}
                }

                throw new DbUpdateException(
                    "Entity Update Failed entiests with issues are " +
                    sb.ToString(), ex
                    ); // Add th

            }
        }

        #endregion

        #region Audit Imaplementation
        void OnSavingChanges(object sender, EventArgs e)
        {
            //TO DO differntiate between the diff types of tablles that need audi
            if (IsAuditEnabled)
            {
                var changeEntries = this.ChangeTracker.Entries().Where(p => p.State == System.Data.Entity.EntityState.Added
                    || p.State == System.Data.Entity.EntityState.Deleted
                    || p.State == System.Data.Entity.EntityState.Modified);

                if (null != changeEntries)
                {
                    foreach (var entity in changeEntries)
                    {
                        //error happens here on any saves 
                        //string modifedentiyname =  entity.Entity.GetType ()
                        //    var promotionobjectauditrecords = CreateAuditRecordsForPromotionObjectChanges(entity);
                        // var reviewsauditrecords = 


                        //TO DO maybe I think this is overkill though
                        //Add a method to do housekeeping and create surf data for mapping to deployments
                        //AddSurfs();

                        //only add records if we have some actual auit data for promotion objects to add
                        //if (promotionobjectauditrecords != null && promotionobjectauditrecords.Count > 0)
                        //{
                        //    foreach (var promotionobjecthistory in promotionobjectauditrecords)
                        //    {
                        //        this.promotionobjecthistory.Add(promotionobjecthistory);
                        //    }
                        //}
                    }
                }
            }
        }



        //private List<promotionobjecthistory> CreateAuditRecordsForPromotionObjectChanges(DbEntityEntry dbEntry)
        //{
        //    List<promotionobjecthistory> result = new List<promotionobjecthistory>();

        //    #region Generate Audit
        //    //determine audit time
        //    DateTime auditTime = DateTime.UtcNow;



        //    // Get the Table name by attribute
        //    TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
        //    string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;
        //    //make sure we are auditing the correct table
        //    if (!(tableName == "promotionobject")) return null;

        //    //TO DO check this !!!
        //    //5-1-2013  check to see if this is the inital promotion object added
        //    // promotionobject.initialpromotionobject = true;

        //    // Find Primiray key.
        //    string keyName = dbEntry.Entity.GetType().GetProperties().Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Name;

        //    if (dbEntry.State == System.Data.Entity.EntityState.Added)
        //    {


        //        result.Add(new promotionobjecthistory()
        //        {


        //            id = Guid.NewGuid()
        //            //   Id = Guid.NewGuid(),
        //            //   AuditDateInUTC = auditTime,
        //            //  AuditState = AuditState.Added,
        //            //   TableName = tableName,
        //            //  RecordID = dbEntry.CurrentValues.GetValue<object>(keyName).ToString(),  // Again, adjust this if you have a multi-column key
        //            //   NewValue = ToXmlString(dbEntry.CurrentValues.ToObject())
        //        }
        //            );
        //    }
        //    else if (dbEntry.State == System.Data.Entity.EntityState.Deleted)
        //    {
        //        result.Add(new promotionobjecthistory()
        //        {
        //            id = Guid.NewGuid(),
        //            // AuditDateInUTC = auditTime,
        //            //  AuditState = AuditState.Deleted,
        //            //  TableName = tableName,
        //            //   RecordID = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
        //            // NewValue = ToXmlString(dbEntry.OriginalValues.ToObject().ToString())
        //        }
        //            );
        //    }
        //    else if (dbEntry.State == System.Data.Entity.EntityState.Modified)
        //    {
        //        foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
        //        {
        //            if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
        //            {
        //                result.Add(new promotionobjecthistory()
        //                {
        //                    id = Guid.NewGuid(),
        //                    //AuditDateInUTC = auditTime,
        //                    // AuditState = AuditState.Modified,
        //                    //  TableName = tableName,
        //                    //   RecordID = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
        //                    //  ColumnName = propertyName,
        //                    //  OriginalValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ?
        //                    //  null
        //                    //  : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString(),
        //                    //
        //                    //   NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ?
        //                    //   null
        //                    //   : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString()
        //                }
        //               );
        //            }
        //        }
        //    }
        //    return result;

        //    #endregion
        //}

        //private static string ToXmlString(object value)
        //{
        //    var serializer = new DataContractSerializer(value.GetType());
        //    using (var backing = new System.IO.StringWriter())
        //    using (var writer = new System.Xml.XmlTextWriter(backing))
        //    {
        //        serializer.WriteObject(writer, value);
        //        return backing.ToString();
        //    }
        //}

        #endregion

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
