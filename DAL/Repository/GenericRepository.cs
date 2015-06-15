using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private WebContext entities = null;

        //IObjectSet<T> _objectSet;
        DbSet<T> _objectSet;

        public GenericRepository(WebContext _entities)
        {
            entities = _entities;
            _objectSet = entities.Set<T>(); // entities.CreateObjectSet<T>();
        }

        public IEnumerable<T> GetAll(Func<T, bool> predicate = null)
        {
            if (predicate != null)
            {
                return _objectSet.Where(predicate);
            }

            return _objectSet.AsEnumerable();
        }

        public T Get(Func<T, bool> predicate)
        {
            return _objectSet.First(predicate);
        }

        public void Add(T entity)
        {
            _objectSet.Add(entity);
        }

        public void Attach(T entity)
        {
            _objectSet.Attach(entity);
        }

        public void Delete(T entity)
        {

            _objectSet.Remove(entity);
        }
    }
}
