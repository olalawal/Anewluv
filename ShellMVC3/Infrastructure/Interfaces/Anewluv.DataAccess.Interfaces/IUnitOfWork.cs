using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;


namespace Anewluv.DataAccess.Interfaces
{
    //5-7-2013 completed changes to make this unit of work fully generic
    public interface IUnitOfWork: IDisposable 
    {
        //IDictionary<Type, object> repos { get; set; } 
       
        bool IsAuditEnabled { get; set; }
        bool DisableProxyCreation { get; set; }
        bool DisableLazyLoading { set; }
        bool SetIsolationToDefault { set; }
        
        //these all come from EF i think
        IDbSet<T> GetEntitySet<T>() where T : class;
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        void ChangeState<T>(T entity, EntityState state) where T : class;

        //End of EF implemenations 
        //custom IUnitOfWork Iitems
        void Add<T>(T entity) where T : class;
        //  bool AddAndAudit(T entity);
        void Update<T>(T entity) where T : class;
        // bool UpdateAndAudit(T entity);
        void Remove<T>(T entity) where T : class;
       

        /// <summary>
        /// olawal added on 6-3-2013
        /// Execute Stored Proc with result set returned.
        /// </summary>
        /// <param name="procName"></param>
        /// <returns>An object resultset</returns>
        List<T> ExecuteStoredProcedure<T>(string commandText, params object[] parameters) where T : class;
    


        DbTransaction BeginTransaction();
        ObjectContext ObjectContext { get; }
        int Commit();

      
        IRepository<TSet> GetRepository<TSet>() where TSet : class, new(); 
        //To use less custom code manually added the 
         //IRepository<TSet> GetRepository<TSet>() where TSet : class;
        //non generic implemenation, down the line abrract this out
       // IPromotionRepository GetPromotionRepository { get; }
       // IReviewRepository GetReviewRepository { get; }
        //IDeploymentRepository GetDeploymentRepository { get; }
        //ISurfRepository GetSurfRepository { get; } 
       // IRepository<promotionobject> GetPromotionRepository { get; }
       // IRepository<review> GetReviewRepository { get; }
      //  IRepository<deployment> GetDeploymentRepository { get; }
      //  IRepository<surf> GetSurfRepository { get; }
      //  IRepository<deploymenthistory > GetDeploymentHistoryRepository { get; }
    
       // DbTransaction BeginTransaction();
    }
}
