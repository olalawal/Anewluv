using Nmedia.DataAccess.Test;
using Nmedia.DataAccess.Test.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nmedia.DataAcess.Test
{
     public class UnitOfWork<C> : IUnitOfWork<C> where C : DbContext
    {
        private readonly C _dbcontext;

        public UnitOfWork(C dbcontext)
        {
            _dbcontext = dbcontext;
        }

      

        public C GetContext
        {
            get
            {
                return _dbcontext;
            }

        }
    

        public ConcurrentDictionary<Type,object> repos = null;
        //context is injected into the reposotry probbaly ?
      
    
        public EFRepository<IRepository<TEntity>> GetRepository<TEntity>() where TEntity : class, new()
        {

            var currentrepo =  (EFRepository<IRepository<TEntity>>)repos.GetOrAdd(
               typeof(TEntity),
               t => new EFRepository<IRepository<TEntity>>(this._dbcontext)
           );

            return currentrepo;

            //if (repos != null)
            //{
            //    if (!repos.ContainsKey(typeof(T)))
            //    {
            //        var repo = new EFRepository<T>(this.Context);
            //        try
            //        {
            //            repos.AddOrUpdate(typeof(T), repo);
            //        }
            //        catch
            //        {
            //            //no error really
            //        }

            //    }
            //    return (EFRepository<T>)repos[typeof(T)];
            //}
            //else return null;
        }

        public void Save()
        {
            try
            {
                this._dbcontext.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var m = ex.EntityValidationErrors;
                var l = 3;
                l++;
            }
            catch (DbUpdateException ex)
            {
                var m = ex.InnerException;
                var l = 3;
                l++;
            }
            catch (OptimisticConcurrencyException ex)
            {
                var m = ex.InnerException;
                var l = 3;
                l++;
            }
        }

        public bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._dbcontext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

    }
}
