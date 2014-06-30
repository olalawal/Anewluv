using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;


namespace Nmedia.DataAccess.Interfaces
{
    public interface IContext : IDisposable
    {
        bool IsAuditEnabled { get; set; }
        bool DisableProxyCreation { get; set; }
        bool DisableLazyLoading { set; }
        bool SetIsolationToDefault { set; }
        IDbSet<T> GetEntitySet<T>() where T : class;
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        void ChangeState<T>(T entity, EntityState state) where T : class;
        DbTransaction BeginTransaction();
        ObjectContext ObjectContext { get; }
        int Commit();
    }
}
