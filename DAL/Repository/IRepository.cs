using System;
namespace DAL.Repository
{
    public interface IRepository<T>
     where T : class
    {
        void Add(T entity);
        void Attach(T entity);
        void Delete(T entity);
        T Get(Func<T, bool> predicate);
        System.Collections.Generic.IEnumerable<T> GetAll(Func<T, bool> predicate = null);
    }
}
