using Repositories.Repository;
using ShoppingSite.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repositories.Infra
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> table;

        public Repository()
        {
            this._dbContext = new ApplicationDbContext();
            table = _dbContext.Set<T>();
        }

        public void Delete(int id)
        {
            T exists = table.Find(id);
            table.Remove(exists);
        }

        public T GetById(int id) => table.Find(id);

        public IEnumerable<T> GetUsers() => table.ToList();

        public void Insert(T obj)
        {
            table.Add(obj);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(T obj)
        {
            table.Attach(obj);
            _dbContext.Entry(obj).State = EntityState.Modified;
        }
    }
}
