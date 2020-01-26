using Shop.Core.Models;
using Shop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }
        public void Commit()
        {
            cache[className] = items;
        }

        public void Insert(T t)
        {
            items.Add(t);
        }
        public void Update(T t)
        {
            T tToUpdate = items.Find(i => i.Id == t.Id);
            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception("Item not found");
            }
        }
        public T Find(string id)
        {
            T t = items.FirstOrDefault(i => i.Id == id);
            if (t != null)
                return t;
            else
                throw new Exception("Item not found");
        }
        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }
        public void Delete(String id)
        {
            T t = items.Find(i => i.Id == id);
            if (t != null)
            {
                items.Remove(t);
            }
            else
            {
                throw new Exception("Item not found");
            }
        }
    }
}
