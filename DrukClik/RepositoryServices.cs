using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DrukClik.Data.Repository;

namespace DrukClik
{
    public class RepositoryServices<T> : IRepository<T> where T: class
    {
        private static RepositoryServices<T> _instance = null;
        static readonly object Obj = new object();
        AplicationContext _aplicationContext = new AplicationContext();

        public static RepositoryServices<T> Instance 
        {
            get
            {
                if (_instance == null)
                {
                    lock (Obj)
                    {
                        _instance = new RepositoryServices<T>();
                    }
                }
                return _instance;
            }
        }
        private void RunRepositoryMethod(Action<IRepository<T>> action)
        {
            try
            {
                IRepository<T> repository = null;
                repository = new Repository<T>(_aplicationContext);
                Console.WriteLine("Invoke Method {0}", action.Method);
                action(repository);
                repository.SaveChange();
                repository = null;
                _aplicationContext = null;
                _instance = null;
            }
            catch (Exception ex)
            {             
                Console.WriteLine("Error catched when invoked service action {0} Error message: {1}", action.Method, ex);
            }
        }
        public T AddEntity(T entity)
        {
            T result = null; RunRepositoryMethod(x => { result = x.AddEntity(entity); });
            return result;
        }
        public T DeleteEntity(T entity)
        {
            T result = null; RunRepositoryMethod(x => { result = x.DeleteEntity(entity); });
            return result;
        }
        public T GetEntityById(int primarykey)
        {
            T result = null; RunRepositoryMethod(x => { result = x.GetEntityById(primarykey); });
            return result;
        }
        public void SaveChange()
        {         
        }
        public IEnumerable<T> GetList()
        {
            IEnumerable<T> result = null; RunRepositoryMethod(x => { result = x.GetList(); });
            return result;
        }
        public IQueryable<T> GetList(Expression<Func<T, bool>> query)
        {
            throw new NotImplementedException();
        }
    }
}
