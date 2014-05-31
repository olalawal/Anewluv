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
using Anewluv.Domain.Data;


namespace Anewluv.Domain
{
    public partial class AnewluvContext : DbContext, IUnitOfWork
    {


        private static readonly IDictionary<Type, object> repos = new Dictionary<Type, object>();



        public AnewluvContext()
            : base("name=AnewluvContext")
        {
            Database.SetInitializer<AnewluvContext>(null);
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

        #region "Db objects "


        public DbSet<abusereportnote> abusereportnotes { get; set; }
        public DbSet<abusereport> abusereports { get; set; }
        public DbSet<applicationiconconversion> applicationiconconversions { get; set; }
        public DbSet<applicationitem> applicationitems { get; set; }
        public DbSet<applicationrole> applicationroles { get; set; }
        public DbSet<application> applications { get; set; }
        public DbSet<blocknote> blocknotes { get; set; }
        public DbSet<block> blocks { get; set; }
        public DbSet<communicationquota> communicationquotas { get; set; }
        public DbSet<favorite> favorites { get; set; }
        public DbSet<friend> friends { get; set; }
        public DbSet<hotlist> hotlists { get; set; }
        public DbSet<interest> interests { get; set; }
        public DbSet<like> likes { get; set; }
        public DbSet<lu_abusetype> lu_abusetype { get; set; }
        public DbSet<lu_activitytype> lu_activitytype { get; set; }
        public DbSet<lu_applicationitempaymenttype> lu_applicationitempaymenttype { get; set; }
        public DbSet<lu_applicationitemtransfertype> lu_applicationitemtransfertype { get; set; }
        public DbSet<lu_applicationtype> lu_applicationtype { get; set; }
        public DbSet<lu_bodytype> lu_bodytype { get; set; }
        public DbSet<lu_defaultmailboxfolder> lu_defaultmailboxfolder { get; set; }
        public DbSet<lu_diet> lu_diet { get; set; }
        public DbSet<lu_drinks> lu_drinks { get; set; }
        public DbSet<lu_educationlevel> lu_educationlevel { get; set; }
        public DbSet<lu_employmentstatus> lu_employmentstatus { get; set; }
        public DbSet<lu_ethnicity> lu_ethnicity { get; set; }
        public DbSet<lu_exercise> lu_exercise { get; set; }
        public DbSet<lu_eyecolor> lu_eyecolor { get; set; }
        public DbSet<lu_flagyesno> lu_flagyesno { get; set; }
        public DbSet<lu_gender> lu_gender { get; set; }
        public DbSet<lu_haircolor> lu_haircolor { get; set; }
        public DbSet<lu_havekids> lu_havekids { get; set; }
        public DbSet<lu_height> lu_height { get; set; }
        public DbSet<lu_hobby> lu_hobby { get; set; }
        public DbSet<lu_hotfeature> lu_hotfeature { get; set; }
        public DbSet<lu_humor> lu_humor { get; set; }
        public DbSet<lu_iconformat> lu_iconformat { get; set; }
        public DbSet<lu_iconImagersizerformat> lu_iconImagersizerformat { get; set; }
        public DbSet<lu_incomelevel> lu_incomelevel { get; set; }
        public DbSet<lu_livingsituation> lu_livingsituation { get; set; }
        public DbSet<lu_lookingfor> lu_lookingfor { get; set; }
        public DbSet<lu_maritalstatus> lu_maritalstatus { get; set; }
        public DbSet<lu_notetype> lu_notetype { get; set; }
        public DbSet<lu_openidprovider> lu_openidprovider { get; set; }
        public DbSet<lu_photoapprovalstatus> lu_photoapprovalstatus { get; set; }
        public DbSet<lu_photoformat> lu_photoformat { get; set; }
        public DbSet<lu_photoImagersizerformat> lu_photoImagersizerformat { get; set; }
        public DbSet<lu_photoimagetype> lu_photoimagetype { get; set; }
        public DbSet<lu_photorejectionreason> lu_photorejectionreason { get; set; }
        public DbSet<lu_photostatus> lu_photostatus { get; set; }
        public DbSet<lu_photostatusdescription> lu_photostatusdescription { get; set; }
        public DbSet<lu_politicalview> lu_politicalview { get; set; }
        public DbSet<lu_profession> lu_profession { get; set; }
        public DbSet<lu_profilefiltertype> lu_profilefiltertype { get; set; }
        public DbSet<lu_profilestatus> lu_profilestatus { get; set; }
        public DbSet<lu_religion> lu_religion { get; set; }
        public DbSet<lu_religiousattendance> lu_religiousattendance { get; set; }
        public DbSet<lu_role> lu_role { get; set; }
        public DbSet<lu_securityleveltype> lu_securityleveltype { get; set; }
        public DbSet<lu_securityquestion> lu_securityquestion { get; set; }
        public DbSet<lu_showme> lu_showme { get; set; }
        public DbSet<lu_sign> lu_sign { get; set; }
        public DbSet<lu_smokes> lu_smokes { get; set; }
        public DbSet<lu_sortbytype> lu_sortbytype { get; set; }
        public DbSet<lu_wantskids> lu_wantskids { get; set; }
        public DbSet<mailboxfolder> mailboxfolders { get; set; }
        public DbSet<mailboxfoldertype> mailboxfoldertypes { get; set; }
        public DbSet<mailboxmessagefolder> mailboxmessagefolders { get; set; }
        public DbSet<mailboxmessage> mailboxmessages { get; set; }
        public DbSet<mailupdatefreqency> mailupdatefreqencies { get; set; }
        public DbSet<membersinrole> membersinroles { get; set; }
        public DbSet<openid> openids { get; set; }
        public DbSet<peek> peeks { get; set; }
        public DbSet<photo_securitylevel> photo_securitylevel { get; set; }
        public DbSet<photoalbum_securitylevel> photoalbum_securitylevel { get; set; }
        public DbSet<photoalbum> photoalbums { get; set; }
        public DbSet<photoconversion> photoconversions { get; set; }
        public DbSet<photoreview> photoreviews { get; set; }
        public DbSet<photo> photos { get; set; }
        public DbSet<profileactivity> profileactivities { get; set; }
        public DbSet<profileactivitygeodata> profileactivitygeodatas { get; set; }
        public DbSet<profiledata_ethnicity> profiledata_ethnicity { get; set; }
        public DbSet<profiledata_hobby> profiledata_hobby { get; set; }
        public DbSet<profiledata_hotfeature> profiledata_hotfeature { get; set; }
        public DbSet<profiledata_lookingfor> profiledata_lookingfor { get; set; }
        public DbSet<profiledata> profiledatas { get; set; }
        public DbSet<profilemetadata> profilemetadatas { get; set; }
        public DbSet<profile> profiles { get; set; }
        public DbSet<rating> ratings { get; set; }
        public DbSet<ratingvalue> ratingvalues { get; set; }
        public DbSet<searchsetting_bodytype> searchsetting_bodytype { get; set; }
        public DbSet<searchsetting_diet> searchsetting_diet { get; set; }
        public DbSet<searchsetting_drink> searchsetting_drink { get; set; }
        public DbSet<searchsetting_educationlevel> searchsetting_educationlevel { get; set; }
        public DbSet<searchsetting_employmentstatus> searchsetting_employmentstatus { get; set; }
        public DbSet<searchsetting_ethnicity> searchsetting_ethnicity { get; set; }
        public DbSet<searchsetting_exercise> searchsetting_exercise { get; set; }
        public DbSet<searchsetting_eyecolor> searchsetting_eyecolor { get; set; }
        public DbSet<searchsetting_gender> searchsetting_gender { get; set; }
        public DbSet<searchsetting_haircolor> searchsetting_haircolor { get; set; }
        public DbSet<searchsetting_havekids> searchsetting_havekids { get; set; }
        public DbSet<searchsetting_hobby> searchsetting_hobby { get; set; }
        public DbSet<searchsetting_hotfeature> searchsetting_hotfeature { get; set; }
        public DbSet<searchsetting_humor> searchsetting_humor { get; set; }
        public DbSet<searchsetting_incomelevel> searchsetting_incomelevel { get; set; }
        public DbSet<searchsetting_livingstituation> searchsetting_livingstituation { get; set; }
        public DbSet<searchsetting_location> searchsetting_location { get; set; }
        public DbSet<searchsetting_lookingfor> searchsetting_lookingfor { get; set; }
        public DbSet<searchsetting_maritalstatus> searchsetting_maritalstatus { get; set; }
        public DbSet<searchsetting_politicalview> searchsetting_politicalview { get; set; }
        public DbSet<searchsetting_profession> searchsetting_profession { get; set; }
        public DbSet<searchsetting_religion> searchsetting_religion { get; set; }
        public DbSet<searchsetting_religiousattendance> searchsetting_religiousattendance { get; set; }
        public DbSet<searchsetting_showme> searchsetting_showme { get; set; }
        public DbSet<searchsetting_sign> searchsetting_sign { get; set; }
        public DbSet<searchsetting_smokes> searchsetting_smokes { get; set; }
        public DbSet<searchsetting_sortbytype> searchsetting_sortbytype { get; set; }
        public DbSet<searchsetting_wantkids> searchsetting_wantkids { get; set; }
        public DbSet<searchsetting> searchsettings { get; set; }
        public DbSet<systempagesetting> systempagesettings { get; set; }
        public DbSet<userlogtime> userlogtimes { get; set; }
        public DbSet<visiblitysetting> visiblitysettings { get; set; }
        public DbSet<visiblitysettings_country> visiblitysettings_country { get; set; }
        public DbSet<visiblitysettings_gender> visiblitysettings_gender { get; set; }

               
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

        //    //comment this out when sharing after generating the database
        //    //only share with sql scripts 
        //    anewluvmodelbuilder.buildgeneralmodels(modelBuilder);
        //    anewluvmodelbuilder.buildsearchsettingsmodels(modelBuilder);
        //}

        //public class Initializer : IDatabaseInitializer<AnewluvContext >
        //{
        //    public void InitializeDatabase(AnewluvContext  context)
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
    }
}
