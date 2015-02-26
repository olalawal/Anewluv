using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity;

namespace Nmedia.DataAccess.Test.Interfaces
{

    //TO DO move the rest of the methods drop the IunitOfwork back into repo later using the dbset
    //5-2-2013 olawal moved the audting methods into service layer seems cleaner

    public interface IRepository<T>  where T : class
    {
       //  DbSet<T> dbSet {get;set;}

        /// <summary>
        /// The context for working with the EntityFramework. This is set in the constructor.
        /// </summary>
       // DbContext Context {get;set;}

     //   IRepository<TSet> GetRepository<TSet>() where TSet : class;   

     //   void Dispose();
        // bool RemoveAndAudit(T entity);
        T FindSingle(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);
        IQueryable<T> FindIncluding(params Expression<Func<T, object>>[] includeProperties);
        int Count(Expression<Func<T, bool>> predicate = null);
        bool Exist(Expression<Func<T, bool>> predicate = null);
       // RepositoryQuery<T> Query(); 


        //these all come from EF i think
       // IDbSet<T> GetEntitySet<T>() where T : class;
       // IDbSet<TEntity> Set<TEntity>() where TEntity : class;
      //  void ChangeState<T>(T entity, EntityState state) where T : class;

        //End of EF implemenations 
        //custom IUnitOfWork Iitems
       // void Add<T>(T entity) where T : class;
        //  bool AddAndAudit(T entity);
       // void Update<T>(T entity) where T : class;
        // bool UpdateAndAudit(T entity);
       // void Remove<T>(T entity) where T : class;

        /// <summary>
        /// olawal added on 6-3-2013
        /// Execute Stored Proc with result set returned.
        /// </summary>
        /// <param name="procName"></param>
        /// <returns>An object resultset</returns>
        List<Tentity> ExecuteStoredProcedure<Tentity>(string commandText, params object[] parameters) where Tentity : class;
        int ExecuteStoreCommand(string commandText, params object[] parameters); 

       // IContext Context { get; set; }
      //  void Add(T entity);
     //  //  bool AddAndAudit(T entity);
      //  void Update(T entity);
        // bool UpdateAndAudit(T entity);
      //  void Remove(T entity);
        // bool RemoveAndAudit(T entity);
      //  T FindSingle(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);
       // IQueryable<T> Find(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includes);
       // IQueryable<T> FindIncluding(params Expression<Func<T, object>>[] includeProperties);
       // int Count(Expression<Func<T, bool>> predicate = null);
       // bool Exist(Expression<Func<T, bool>> predicate = null);

             /// <summary>
             /// olawal added on 6-3-2013
        /// Execute Stored Proc with result set returned.
        /// </summary>
        /// <param name="procName"></param>
        /// <returns>An object resultset</returns>
     //   List<T> ExecuteStoredProcedure(string commandText, params object[] parameters);
    
        //to simplyfly pulled these fro, Iunite of work
       // bool EnableAuditLog { get; set; }
       // int Commit();
        //To use less custom code manually added the 
        // IRepository<TSet> GetRepository<TSet>() where TSet : class;
        //DbTransaction BeginTransaction();
    }
}
