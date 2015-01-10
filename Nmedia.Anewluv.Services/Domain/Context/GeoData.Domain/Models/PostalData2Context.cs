using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using GeoData.Domain.Models.Mapping;
using Nmedia.DataAccess.Interfaces;
using System.Data.Entity.Core.Objects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel.DataAnnotations;

using Nmedia.DataAccess;
using System.Data.Common;
using System.Data;
using System.Data.Entity.Validation;

namespace GeoData.Domain.Models
{
    public partial class PostalData2Context : ContextBase
    {

        private static readonly IDictionary<Type, object> repos = new Dictionary<Type, object>();



        static PostalData2Context()
        {
            Database.SetInitializer<PostalData2Context>(null);
        }

        public PostalData2Context()
            : base("Name=PostalData2Context")
        {
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.AutoDetectChangesEnabled = true;
            this.DisableLazyLoading = true;
            //rebuild DB if schema is differnt
            //Initializer init = new Initializer();            
            // init.InitializeDatabase(this);
            this.Configuration.ValidateOnSaveEnabled = false;
            IsAuditEnabled = true;
           // ObjectContext.SavingChanges += OnSavingChanges;
            Database.SetInitializer(
             new DropCreateDatabaseIfModelChanges<PostalData2Context>());
        }



        //#region "Db objects"

        //public DbSet<Afghanistan> Afghanistans { get; set; }
        //public DbSet<AmericanSamoa> AmericanSamoas { get; set; }
        //public DbSet<Andorra> Andorras { get; set; }
        //public DbSet<Angola> Angolas { get; set; }
        //public DbSet<AntiguaandBarbuda> AntiguaandBarbudas { get; set; }
        //public DbSet<Argentina> Argentinas { get; set; }
        //public DbSet<Australia> Australias { get; set; }
        //public DbSet<Austria> Austrias { get; set; }
        //public DbSet<Azerbaijan> Azerbaijans { get; set; }
        //public DbSet<Bahama> Bahamas { get; set; }
        //public DbSet<Bahrain> Bahrains { get; set; }
        //public DbSet<Bangladesh> Bangladeshes { get; set; }
        //public DbSet<Barbado> Barbados { get; set; }
        //public DbSet<Belgium> Belgiums { get; set; }
        //public DbSet<Belize> Belizes { get; set; }
        //public DbSet<Bermuda> Bermudas { get; set; }
        //public DbSet<Brazil> Brazils { get; set; }
        //public DbSet<BritishVirginIsland> BritishVirginIslands { get; set; }
        //public DbSet<Bulgaria> Bulgarias { get; set; }
        //public DbSet<Canada> Canadas { get; set; }
        //public DbSet<CapeVerde> CapeVerdes { get; set; }
        //public DbSet<CaymanIsland> CaymanIslands { get; set; }
        //public DbSet<Chile> Chiles { get; set; }
        //public DbSet<China> Chinas { get; set; }
        //public DbSet<Colombia> Colombias { get; set; }
        //public DbSet<Costa_Rica> Costa_Ricas { get; set; }
        //public DbSet<Country_PostalCode_List> Country_PostalCode_List { get; set; }
        //public DbSet<Country_PostalCode_Region> Country_PostalCode_Region { get; set; }
        //public DbSet<CountryCode> CountryCodes { get; set; }
        //public DbSet<Croatia> Croatias { get; set; }
        //public DbSet<Cuba> Cubas { get; set; }
        //public DbSet<CustomRegion> CustomRegions { get; set; }
        //public DbSet<Cypru> Cyprus { get; set; }
        //public DbSet<CzechRepublic> CzechRepublics { get; set; }
        //public DbSet<Denmark> Denmarks { get; set; }
        //public DbSet<DominicanRepublic> DominicanRepublics { get; set; }
        //public DbSet<Egypt> Egypts { get; set; }
        //public DbSet<Eritrea> Eritreas { get; set; }
        //public DbSet<FalklandIsland> FalklandIslands { get; set; }
        //public DbSet<Fiji> Fijis { get; set; }
        //public DbSet<Finland> Finlands { get; set; }
        //public DbSet<France> Frances { get; set; }
        //public DbSet<FrenchGuiana> FrenchGuianas { get; set; }
        //public DbSet<FrenchPolynesia> FrenchPolynesias { get; set; }
        //public DbSet<Germany> Germanies { get; set; }
        //public DbSet<Ghana> Ghanas { get; set; }
        //public DbSet<Gibraltar> Gibraltars { get; set; }
        //public DbSet<Greenland> Greenlands { get; set; }
        //public DbSet<Guam> Guams { get; set; }
        //public DbSet<Guatemala> Guatemalas { get; set; }
        //public DbSet<Guernsey> Guernseys { get; set; }
        //public DbSet<Guyana> Guyanas { get; set; }
        //public DbSet<Haiti> Haitis { get; set; }
        //public DbSet<Hondura> Honduras { get; set; }
        //public DbSet<HongKong> HongKongs { get; set; }
        //public DbSet<Hungary> Hungaries { get; set; }
        //public DbSet<Iceland> Icelands { get; set; }
        //public DbSet<India> Indias { get; set; }
        //public DbSet<Indonesia> Indonesias { get; set; }
        //public DbSet<Iraq> Iraqs { get; set; }
        //public DbSet<Ireland> Irelands { get; set; }
        //public DbSet<IsleOfMan> IsleOfMen { get; set; }
        //public DbSet<Israel> Israels { get; set; }
        //public DbSet<Italy> Italies { get; set; }
        //public DbSet<IvoryCoast> IvoryCoasts { get; set; }
        //public DbSet<Jamaica> Jamaicas { get; set; }
        //public DbSet<Japan> Japans { get; set; }
        //public DbSet<Jersey> Jerseys { get; set; }
        //public DbSet<Jordan> Jordans { get; set; }
        //public DbSet<Kenya> Kenyas { get; set; }
        //public DbSet<Kiribati> Kiribatis { get; set; }
        //public DbSet<Lebanon> Lebanons { get; set; }
        //public DbSet<Liberia> Liberias { get; set; }
        //public DbSet<Liechtenstein> Liechtensteins { get; set; }
        //public DbSet<Luxembourg> Luxembourgs { get; set; }
        //public DbSet<Macedonia> Macedonias { get; set; }
        //public DbSet<Madagascar> Madagascars { get; set; }
        //public DbSet<Malaysia> Malaysias { get; set; }
        //public DbSet<Malta> Maltas { get; set; }
        //public DbSet<MarshallIsland> MarshallIslands { get; set; }
        //public DbSet<Martinique> Martiniques { get; set; }
        //public DbSet<Mayotte> Mayottes { get; set; }
        //public DbSet<Mexico> Mexicoes { get; set; }
        //public DbSet<Moldova> Moldovas { get; set; }
        //public DbSet<Monaco> Monacoes { get; set; }
        //public DbSet<Morocco> Moroccoes { get; set; }
        //public DbSet<Nepal> Nepals { get; set; }
        //public DbSet<Netherland> Netherlands { get; set; }
        //public DbSet<NetherlandsAntille> NetherlandsAntilles { get; set; }
        //public DbSet<NewZealand> NewZealands { get; set; }
        //public DbSet<Nigeria> Nigerias { get; set; }
        //public DbSet<NorthernMarianaIsland> NorthernMarianaIslands { get; set; }
        //public DbSet<Norway> Norways { get; set; }
        //public DbSet<Pakistan> Pakistans { get; set; }
        //public DbSet<PapuaNewGuinea> PapuaNewGuineas { get; set; }
        //public DbSet<Peru> Perus { get; set; }
        //public DbSet<Philippine> Philippines { get; set; }
        //public DbSet<Poland> Polands { get; set; }
        //public DbSet<Portugal> Portugals { get; set; }
        //public DbSet<PuertoRico> PuertoRicoes { get; set; }
        //public DbSet<Qatar> Qatars { get; set; }
        //public DbSet<Reunion> Reunions { get; set; }
        //public DbSet<Russia> Russias { get; set; }
        //public DbSet<SaintPierreandMiquelon> SaintPierreandMiquelons { get; set; }
        //public DbSet<SaintVincentandtheGrenadine> SaintVincentandtheGrenadines { get; set; }
        //public DbSet<Samoa> Samoas { get; set; }
        //public DbSet<SaudiArabia> SaudiArabias { get; set; }
        //public DbSet<Senegal> Senegals { get; set; }
        //public DbSet<Slovakia> Slovakias { get; set; }
        //public DbSet<Slovenia> Slovenias { get; set; }
        //public DbSet<SolomonIsland> SolomonIslands { get; set; }
        //public DbSet<SouthAfrica> SouthAfricas { get; set; }
        //public DbSet<Spain> Spains { get; set; }
        //public DbSet<SriLanka> SriLankas { get; set; }
        //public DbSet<Sweden> Swedens { get; set; }
        //public DbSet<Switzerland> Switzerlands { get; set; }
        //public DbSet<Syria> Syrias { get; set; }
        //public DbSet<sysdiagram> sysdiagrams { get; set; }
        //public DbSet<Taiwan> Taiwans { get; set; }
        //public DbSet<Tanzania> Tanzanias { get; set; }
        //public DbSet<Thailand> Thailands { get; set; }
        //public DbSet<TrinidadandTobago> TrinidadandTobagoes { get; set; }
        //public DbSet<Turkey> Turkeys { get; set; }
        //public DbSet<Uganda> Ugandas { get; set; }
        //public DbSet<Ukraine> Ukraines { get; set; }
        //public DbSet<UnitedArabEmirate> UnitedArabEmirates { get; set; }
        //public DbSet<UnitedKingdom> UnitedKingdoms { get; set; }
        //public DbSet<UnitedState> UnitedStates { get; set; }
        //public DbSet<USVirginIsland> USVirginIslands { get; set; }
        //public DbSet<Venezuela> Venezuelas { get; set; }
        //public DbSet<Vietnam> Vietnams { get; set; }
        //public DbSet<Yeman> Yemen { get; set; }

        //#endregion


        //#region IContext Implementation

        //public bool SetIsolationToDefault
        //{
        //    set
        //    {
        //        if (value == true)
        //        {
        //            var test = (this as IObjectContextAdapter).ObjectContext;
        //            test.ExecuteStoreCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
        //        }
        //    }
        //}


        //public ObjectContext ObjectContext
        //{
        //    get
        //    {
        //        return (this as IObjectContextAdapter).ObjectContext;
        //    }
        //}



        ////4-24-2013 olawal newly added
        //public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        //{
        //    return base.Set<TEntity>();
        //}


        //public bool IsAuditEnabled
        //{
        //    get;
        //    set;
        //}

        //public bool DisableProxyCreation
        //{

        //    set { ObjectContext.ContextOptions.ProxyCreationEnabled = !value; }
        //    get { return !ObjectContext.ContextOptions.ProxyCreationEnabled; }
        //}

        //public bool DisableLazyLoading
        //{
        //    set { ObjectContext.ContextOptions.LazyLoadingEnabled = !value; }
        //}

        //public void ChangeState<T>(T entity, System.Data.Entity.EntityState state) where T : class
        //{
        //    Entry<T>(entity).State = state;
        //}

        //public IDbSet<T> GetEntitySet<T>()
        //where T : class
        //{
        //    return Set<T>();
        //}

        //public virtual int Commit()
        //{
        //    if (this.ChangeTracker.Entries().Any(IsChanged))
        //    {
        //        return this.SaveChanges();
        //    }
        //    return 0;
        //}

        //private static bool IsChanged(DbEntityEntry entity)
        //{
        //    return IsStateEqual(entity, System.Data.Entity.EntityState.Added) ||
        //           IsStateEqual(entity, System.Data.Entity.EntityState.Deleted) ||
        //           IsStateEqual(entity, System.Data.Entity.EntityState.Modified);
        //}

        //private static bool IsStateEqual(DbEntityEntry entity, System.Data.Entity.EntityState state)
        //{
        //    return (entity.State & state) == state;
        //}

        //public virtual DbTransaction BeginTransaction()
        //{
        //    var connection = this.ObjectContext.Connection;
        //    if (connection.State != ConnectionState.Open)
        //    {
        //        connection.Open();
        //    }

        //    return connection
        //        .BeginTransaction(IsolationLevel.ReadCommitted);
        //}
        //#endregion

        //#region "Code pulled from repostiory pattern since EF supports unit of work pattern no need of a separate repo?"





        ////#endregion
        //public void Add<T>(T entity)
        //where T : class
        //{
        //    this.GetEntitySet<T>().Add(entity);


        //}
        ////public bool AddAndAudit(T entity)
        ////{
        ////    //TO DO allow this to be flagable on the context instantiation side
        ////    this.EnableAuditLog = true;
        ////    using (var transaction = Context.BeginTransaction())
        ////    {
        ////        try
        ////        {
        ////            //Added via DI on service call
        ////            //IRepository<review> repository = Context.GetRepository<T>();
        ////            this.Add(entity);
        ////            int i = Context.Commit();
        ////            transaction.Commit();
        ////            return (i > 0);
        ////        }
        ////        catch (Exception)
        ////        {
        ////            transaction.Rollback();
        ////            throw;
        ////        }

        ////    }
        ////}

        //public void Update<T>(T entity)
        //where T : class
        //{
        //    this.ChangeState(entity, System.Data.Entity.EntityState.Modified);
        //}

        //public bool UpdateAndAudit<T>(T entity)
        // where T : class
        //{

        //    this.IsAuditEnabled = true;
        //    using (var transaction = this.BeginTransaction())
        //    {
        //        try
        //        {
        //            //Added via DI on service call
        //            //IRepository<review> repository = Context.GetRepository<T>();
        //            this.Update(entity);
        //            int i = this.Commit();
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

        //public void Remove<T>(T entity)
        //where T : class
        //{
        //    this.ChangeState(entity, System.Data.Entity.EntityState.Deleted);
        //}

        //// Method using Execute Store QUery
        //public List<T> ExecuteStoredProcedure<T>(string commandText, params object[] parameters)
        // where T : class
        //{
        //    // List<T> myList = new List<T>();

        //    var groupData = this.ObjectContext.ExecuteStoreQuery<T>(commandText, parameters).ToList();

        //    return groupData.ToList();
        //}

        ////public T ExecuteStoredProcedureSingle<T>(string commandText, params object[] parameters)
        ////where T : class
        ////{
        ////    // List<T> myList = new List<T>();

        ////     return this.ObjectContext.ExecuteStoreQuery<T>(commandText, parameters).FirstOrDefault();

            
        ////}

        //public bool RemoveAndAudit<T>(T entity)
        //where T : class
        //{

        //    this.IsAuditEnabled = true;
        //    using (var transaction = this.BeginTransaction())
        //    {
        //        try
        //        {
        //            //Added via DI on service call
        //            //IRepository<review> repository = Context.GetRepository<T>();
        //            this.Remove(entity);
        //            int i = this.Commit();
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


        //public IRepository<T> GetRepository<T>() where T : class, new()
        //{
        //    if (repos != null)
        //    {
        //        if (!repos.ContainsKey(typeof(T)))
        //        {
        //            var repo = new EFRepository<T>(this);
        //            try
        //            {
        //                repos.Add(typeof(T), repo);
        //            }
        //            catch
        //            {
        //                //no error really
        //            }

        //        }
        //        return (EFRepository<T>)repos[typeof(T)];
        //    }
        //    else return null;
        //}


        //// public void Dispose() { this.Dispose(); }

        ////public List<T> ExecuteStoredProcedure(string commandText, object[] parameters)
        ////{
        ////    // List<T> myList = new List<T>();
        ////    //convert params to objectparamters 
        ////    var obparameters = new ObjectParameter[] ;//{ new ObjectParameter("FirstName", "Bob") };

        ////    foreach (object o in parameters)
        ////    {


        ////    }

        ////    var groupData = Context.ObjectContext.ExecuteFunction<T>(commandText, parameters);

        ////    return groupData.ToList();
        ////}

        //#endregion


        //#region "Overides"

        ////overide that allows clearing or repos
        //protected override void Dispose(bool disposing)
        //{
        //    repos.Clear();
        //    base.Dispose(disposing);
        //}



        //public override int SaveChanges()
        //{
        //    try
        //    {
        //        return base.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        //TO DO add logging
        //        DbEntityEntry entry = ex.Entries.FirstOrDefault();
        //        var objContext = ((IObjectContextAdapter)ObjectContext).ObjectContext;
        //        // Get failed entry
        //        objContext.AcceptAllChanges();
        //        //objContext.Refresh(RefreshMode.ClientWins, ex.Entries.Where(e => e.State == System.Data.Entity.EntityState.Added).Select(e => e.Entity));

        //        // Now call refresh on ObjectContext
        //        // objContext.Refresh(RefreshMode.ClientWins, entry.Entity);  
        //        return 1;
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        var sb = new StringBuilder();

        //        foreach (var failure in ex.EntityValidationErrors)
        //        {
        //            sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
        //            foreach (var error in failure.ValidationErrors)
        //            {
        //                sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
        //                sb.AppendLine();
        //            }
        //        }

        //        throw new DbEntityValidationException(
        //            "Entity Validation Failed - errors follow:\n" +
        //            sb.ToString(), ex
        //            ); // Add the original exception as the innerException
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        var sb = new StringBuilder();

        //        foreach (var badentries in ex.Entries)
        //        {
        //            //  sb.AppendFormat("{0} failed validation\n", failure..Entity.GetType());
        //            //  foreach (var error in failure.ValidationErrors)
        //            //  {
        //            sb.AppendFormat("- {0} : {1}", badentries.CurrentValues, badentries.State);
        //            sb.AppendLine();
        //            //}
        //        }

        //        throw new DbUpdateException(
        //            "Entity Update Failed entiests with issues are " +
        //            sb.ToString(), ex
        //            ); // Add th

        //    }
        //}

        //#endregion

        //#region Audit Imaplementation
        //void OnSavingChanges(object sender, EventArgs e)
        //{
        //    //TO DO differntiate between the diff types of tablles that need audi
        //    if (IsAuditEnabled)
        //    {
        //        var changeEntries = this.ChangeTracker.Entries().Where(p => p.State == System.Data.Entity.EntityState.Added
        //            || p.State == System.Data.Entity.EntityState.Deleted
        //            || p.State == System.Data.Entity.EntityState.Modified);

        //        if (null != changeEntries)
        //        {
        //            foreach (var entity in changeEntries)
        //            {
        //                //error happens here on any saves 
        //                //string modifedentiyname =  entity.Entity.GetType ()
        //                //    var promotionobjectauditrecords = CreateAuditRecordsForPromotionObjectChanges(entity);
        //                // var reviewsauditrecords = 


        //                //TO DO maybe I think this is overkill though
        //                //Add a method to do housekeeping and create surf data for mapping to deployments
        //                //AddSurfs();

        //                //only add records if we have some actual auit data for promotion objects to add
        //                //if (promotionobjectauditrecords != null && promotionobjectauditrecords.Count > 0)
        //                //{
        //                //    foreach (var promotionobjecthistory in promotionobjectauditrecords)
        //                //    {
        //                //        this.promotionobjecthistory.Add(promotionobjecthistory);
        //                //    }
        //                //}
        //            }
        //        }
        //    }
        //}



        ////private List<promotionobjecthistory> CreateAuditRecordsForPromotionObjectChanges(DbEntityEntry dbEntry)
        ////{
        ////    List<promotionobjecthistory> result = new List<promotionobjecthistory>();

        ////    #region Generate Audit
        ////    //determine audit time
        ////    DateTime auditTime = DateTime.UtcNow;



        ////    // Get the Table name by attribute
        ////    TableAttribute tableAttr = dbEntry.Entity.GetType().GetCustomAttributes(typeof(TableAttribute), false).SingleOrDefault() as TableAttribute;
        ////    string tableName = tableAttr != null ? tableAttr.Name : dbEntry.Entity.GetType().Name;
        ////    //make sure we are auditing the correct table
        ////    if (!(tableName == "promotionobject")) return null;

        ////    //TO DO check this !!!
        ////    //5-1-2013  check to see if this is the inital promotion object added
        ////    // promotionobject.initialpromotionobject = true;

        ////    // Find Primiray key.
        ////    string keyName = dbEntry.Entity.GetType().GetProperties().Single(p => p.GetCustomAttributes(typeof(KeyAttribute), false).Count() > 0).Name;

        ////    if (dbEntry.State == System.Data.Entity.EntityState.Added)
        ////    {


        ////        result.Add(new promotionobjecthistory()
        ////        {


        ////            id = Guid.NewGuid()
        ////            //   Id = Guid.NewGuid(),
        ////            //   AuditDateInUTC = auditTime,
        ////            //  AuditState = AuditState.Added,
        ////            //   TableName = tableName,
        ////            //  RecordID = dbEntry.CurrentValues.GetValue<object>(keyName).ToString(),  // Again, adjust this if you have a multi-column key
        ////            //   NewValue = ToXmlString(dbEntry.CurrentValues.ToObject())
        ////        }
        ////            );
        ////    }
        ////    else if (dbEntry.State == System.Data.Entity.EntityState.Deleted)
        ////    {
        ////        result.Add(new promotionobjecthistory()
        ////        {
        ////            id = Guid.NewGuid(),
        ////            // AuditDateInUTC = auditTime,
        ////            //  AuditState = AuditState.Deleted,
        ////            //  TableName = tableName,
        ////            //   RecordID = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
        ////            // NewValue = ToXmlString(dbEntry.OriginalValues.ToObject().ToString())
        ////        }
        ////            );
        ////    }
        ////    else if (dbEntry.State == System.Data.Entity.EntityState.Modified)
        ////    {
        ////        foreach (string propertyName in dbEntry.OriginalValues.PropertyNames)
        ////        {
        ////            if (!object.Equals(dbEntry.OriginalValues.GetValue<object>(propertyName), dbEntry.CurrentValues.GetValue<object>(propertyName)))
        ////            {
        ////                result.Add(new promotionobjecthistory()
        ////                {
        ////                    id = Guid.NewGuid(),
        ////                    //AuditDateInUTC = auditTime,
        ////                    // AuditState = AuditState.Modified,
        ////                    //  TableName = tableName,
        ////                    //   RecordID = dbEntry.OriginalValues.GetValue<object>(keyName).ToString(),
        ////                    //  ColumnName = propertyName,
        ////                    //  OriginalValue = dbEntry.OriginalValues.GetValue<object>(propertyName) == null ?
        ////                    //  null
        ////                    //  : dbEntry.OriginalValues.GetValue<object>(propertyName).ToString(),
        ////                    //
        ////                    //   NewValue = dbEntry.CurrentValues.GetValue<object>(propertyName) == null ?
        ////                    //   null
        ////                    //   : dbEntry.CurrentValues.GetValue<object>(propertyName).ToString()
        ////                }
        ////               );
        ////            }
        ////        }
        ////    }
        ////    return result;

        ////    #endregion
        ////}

        ////private static string ToXmlString(object value)
        ////{
        ////    var serializer = new DataContractSerializer(value.GetType());
        ////    using (var backing = new System.IO.StringWriter())
        ////    using (var writer = new System.Xml.XmlTextWriter(backing))
        ////    {
        ////        serializer.WriteObject(writer, value);
        ////        return backing.ToString();
        ////    }
        ////}

        //#endregion

    }
}
