using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Linq.Expressions;
using Nmedia.DataAccess.Test;

using Nmedia.DataAccess.Test.Interfaces;


namespace Nmedia.DataAccess.Test
{


   
    //5-7-2013 completed changes to make this unit of work fully generic
    public interface IUnitOfWork<C>: IDisposable 
    {
        //bool disposed {get;set;}
      //  DbContext Context {get;set;} 
        //thread safe list of all the repos
      //  ConcurrentDictionary<Type, object> repos { get; set; }


        C GetContext { get; set; }
        EFRepository<IRepository<TSet>> GetRepository<TSet>() where TSet : class, new();
        void Save();

      //  void Dispose(bool disposing);


       // void  Dispose();
        
    }
}
