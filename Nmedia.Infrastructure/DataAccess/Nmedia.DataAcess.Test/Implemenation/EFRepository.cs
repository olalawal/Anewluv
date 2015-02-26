using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Nmedia.DataAccess.Test.Interfaces;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;

namespace Nmedia.DataAccess.Test
{
    //[Export(typeof(IRepository<>))]
    public class EFRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// This is set in the constructor and provides access to the underlying EntityFramework methods
        /// </summary>
        //private readonly DbSet<TEntity> IDbSet;
        internal IDbSet<T> IDbSet;

        /// <summary>
        /// The context for working with the EntityFramework. This is set in the constructor.
        /// </summary>
         internal DbContext Context;
        /// <summary>
        ///  transaction object
        /// </summary>
        private DbTransaction _transaction;      

        /// <summary>
        /// Private field to check if the context has been disposed
        /// </summary>
        //private bool _disposed;

        //hack for the fact we do not have a proper parameterless BASE class 
        public EFRepository()
        {

        }

        public EFRepository(DbContext _Context)
        {

            // _dataContext = Context.;
            this.Context = _Context;
            this.IDbSet = this.Context.Set<T>();

        }

        public T FindSingle(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            var set = FindIncluding(includes);
            return (predicate == null) ?
                   set.FirstOrDefault() :
                   set.FirstOrDefault(predicate);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes)
        {
            var set = FindIncluding(includes);
            return (predicate == null) ? set : set.Where(predicate);
        }
        public IQueryable<T> FindIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            var set = IDbSet; // this.Context.GetEntitySet<TEntity>();

            if (includeProperties != null)
            {
                foreach (var include in includeProperties)
                {
                    //Includes only work with the Load extention and are too slow right now
                    //for some reason lazy loading seems faster so use that for now till we figure out the dispose on context bug
                    //test the asqueryable part
                    set.Include(include).AsQueryable().Load();
                }
            }
            return set.AsQueryable();
        }
        public int Count(Expression<Func<T, bool>> predicate = null)
        {
            var set = IDbSet;// this.Context.GetEntitySet<TEntity>();
            return (predicate == null) ?
                   set.Count() :
                   set.Count(predicate);
        }
        public bool Exist(Expression<Func<T, bool>> predicate = null)
        {
            var set = IDbSet; // this.Context.GetEntitySet<TEntity>();
            return (predicate == null) ? set.Any() : set.Any(predicate);
        }


        //new clearner methods i think
        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = IDbSet;
            return query;
        }

        public IEnumerable<T> GetAll(string includeProperties)
        {
            IQueryable<T> query = IDbSet;
            return PerformInclusions(includeProperties, query);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> where,
                                   string includeProperties)
        {
            try
            {
                IQueryable<T> query = IDbSet;
                query = PerformInclusions(includeProperties, query);
                return query.Where(where);
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
        }

        public T Single(Expression<Func<T, bool>> where, string includeProperties)
        {
            try
            {
                IQueryable<T> query = IDbSet;
                query = PerformInclusions(includeProperties, query);
                return query.Single(where);
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
        }

        public T First(Expression<Func<T, bool>> where, string includeProperties)
        {
            try
            {
                IQueryable<T> query = IDbSet;
                query = PerformInclusions(includeProperties, query);
                return query.First(where);
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
        }

        public virtual void Attach(T entity)
        {
            IDbSet.Attach(entity);
        }

        public virtual T Insert(T entity)
        {
            return IDbSet.Add(entity);
        }

        public virtual void UpdateValues(T entity, T orig)
        {
            Context.Entry(orig).CurrentValues.SetValues(entity);
        }

        public virtual void Update(T entityToUpdate)
        {
            IDbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = IDbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                IDbSet.Attach(entityToDelete);
            }
            IDbSet.Remove(entityToDelete);
        }

        //INCLUDE COMPLEX PROPERTIES = VIRTUAL KEYWORD (PRIVATE)
        private static IQueryable<T> PerformInclusions(string includeProperties, IQueryable<T> query)
        {
            if (includeProperties != null && includeProperties.Length > 0)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            return query;
        }


        #region "code pulled from context base"


        //#endregion
        public void Add<TEntity>(T entity)
        where TEntity : class
        {
          //  this.GetEntitySet<TEntity>().Add(entity);


        }


        public void Update<TEntity>(T entity)
        where TEntity : class
        {
           // this.ChangeState(entity, System.Data.Entity.EntityState.Modified);
        }

  

        public void Remove<TEntity>(T entity)
        where TEntity : class
        {
           // Context.ChangeState(entity, System.Data.Entity.EntityState.Deleted);
        }

        // Method using Execute Store QUery
        public List<TEntity> ExecuteStoredProcedure<TEntity>(string commandText, params object[] parameters)
         where TEntity : class
        {
            // List<TEntity> myList = new List<TEntity>();
            var adapter = (IObjectContextAdapter)Context;
            var objectContext = adapter.ObjectContext;

            var groupData = objectContext.ExecuteStoreQuery<TEntity>(commandText, parameters);

            return groupData.ToList();
        }

        //method of excuting sql strings directly
        public int ExecuteStoreCommand(string commandText, params object[] parameters)
        {
            var adapter = (IObjectContextAdapter)Context;
            var objectContext = adapter.ObjectContext;

            var result = objectContext.ExecuteStoreCommand(commandText, parameters);

            return result;
        }


        //public bool RemoveAndAudit<TEntity>(T entity)
        //where T : class
        //{

        //    this.IsAuditEnabled = true;
        //    using (var transaction = this.BeginTransaction())
        //    {
        //        try
        //        {
        //            //Added via DI on service call
        //            //IRepository<review> repository = Context.GetRepository<TEntity>();
        //            this.Remove(entity);
        //            int i = this.Commit();
        //            // transaction.Commit();
        //            return (i > 0);
        //        }
        //        catch (Exception)
        //        {
        //            // transaction.Rollback();
        //            throw;
        //        }

        //    }
        //}
#endregion

    }

}
