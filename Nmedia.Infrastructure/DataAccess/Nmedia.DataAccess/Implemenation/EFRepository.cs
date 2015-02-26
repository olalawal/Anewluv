using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
//using Nmedia.DataAccess.Interfaces;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity.Core.Objects;

namespace Nmedia.DataAccess
{
    //[Export(typeof(IRepository<>))]
    public class EFRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// This is set in the constructor and provides access to the underlying EntityFramework methods
        /// </summary>
        private readonly DbSet<T> dbSet;

        /// <summary>
        /// The context for working with the EntityFramework. This is set in the constructor.
        /// </summary>
        private readonly DbContext Context;


        /// <summary>
        ///  transaction object
        /// </summary>
        private DbTransaction _transaction;

        private static readonly IDictionary<Type, object> repos = new Dictionary<Type, object>();

        /// <summary>
        /// Private field to check if the context has been disposed
        /// </summary>
        private bool _disposed;

        //hack for the fact we do not have a proper parameterless BASE class 
        public EFRepository()
        {

        }

        public EFRepository(DbContext _Context)
        {

            // _dataContext = Context.;
            this.Context = _Context;
            this.dbSet = this.Context.Set<T>();

        }

        public T FindSingle(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            var set = FindIncluding(includes);
            return (predicate == null) ?
                   set.FirstOrDefault() :
                   set.FirstOrDefault(predicate);
        }
        public IQueryable<T> Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null, params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            var set = FindIncluding(includes);
            return (predicate == null) ? set : set.Where(predicate);
        }

        //TO DO Look at this
        //http://blog.longle.net/2013/05/11/genericizing-the-unit-of-work-pattern-repository-pattern-with-entity-framework-in-mvc/
        //internal IQueryable<TEntity> Get(
        //Expression<Func<TEntity, bool>> filter = null,
        //Func<IQueryable<TEntity>,
        //    IOrderedQueryable<TEntity>> orderBy = null,
        //List<Expression<Func<TEntity, object>>>
        //    includeProperties = null,
        //int? page = null,
        //int? pageSize = null)
        //{
        //    IQueryable<TEntity> query = DbSet;

        //    if (includeProperties != null)
        //        includeProperties.ForEach(i => { query = query.Include(i); });

        //    if (filter != null)
        //        query = query.Where(filter);

        //    if (orderBy != null)
        //        query = orderBy(query);

        //    if (page != null && pageSize != null)
        //        query = query
        //            .Skip((page.Value - 1) * pageSize.Value)
        //            .Take(pageSize.Value);

        //    return query;
        //} 



        public IQueryable<T> FindIncluding(params System.Linq.Expressions.Expression<Func<T, object>>[] includeProperties)
        {
            var set = dbSet; // this.Context.GetEntitySet<T>();

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
        public int Count(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null)
        {
            var set = dbSet;// this.Context.GetEntitySet<T>();
            return (predicate == null) ?
                   set.Count() :
                   set.Count(predicate);
        }
        public bool Exist(System.Linq.Expressions.Expression<Func<T, bool>> predicate = null)
        {
            var set = dbSet; // this.Context.GetEntitySet<T>();
            return (predicate == null) ? set.Any() : set.Any(predicate);
        }


        //public IRepository<T> GetRepository<T>() 
        //{
        //    if (repos != null)
        //    {
        //        if (!repos.ContainsKey(typeof(T)))
        //        {
        //            var repo = new EFRepository<T>(Context);
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

        public void Dispose()
        {
            // if (null != _transaction)
            //     _transaction.Dispose();
            //  _transaction = null;

            //if (null != _context)
            //    _context.Dispose();
            //     _context=null;

            Dispose(true);
            // Take yourself off the Finalization queue to prevent finalization code for object from executing a second time.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!_disposed)
            {
                // If disposing equals true, dispose all managed and unmanaged resources.
                if (disposing)
                {
                    //cleare repos !
                    //6-10-2013 big line missing here forgot to clear repos 
                    repos.Clear();
                    // Dispose managed resources.
                   
                    if (Context != null)
                    {
                        Context.Dispose();
                       // Context = null;
                    }
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }

                    //kill all the repos as well lol

                }
            }

            _disposed = true;
        }



    }

}
