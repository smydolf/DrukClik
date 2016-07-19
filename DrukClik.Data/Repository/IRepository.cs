using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DrukClik.Data.Repository
{
    public interface IRepository<T> where T : class
    {      
        T AddEntity(T entity);
        T DeleteEntity(T entity);
        IEnumerable<T> GetList();
        IQueryable<T> GetList(Expression<Func<T, bool>> query);
        T GetEntityById(int primarykey);
        void SaveChange();
    }
}
