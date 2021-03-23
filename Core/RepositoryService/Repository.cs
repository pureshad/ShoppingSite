using ShoppingSite.Models;
using ShoppingSite.RepositoryInfra;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ShoppingSite.RepositoryService
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<T> table;

        public Repository()
        {
            this._dbContext = new ApplicationDbContext();
            //table = _dbContext.Set<T>();
        }

        public IQueryable<T> Table => Entities;

        public void Delete(T id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            try
            {
                //T exists = table.Find(id);
                Entities.Remove(id);
                Save();
            }
            catch (DbUpdateException e)
            {
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(e), e);
            }
        }

        public T GetById(T id) => Entities.Find(id);

        public IEnumerable<T> GetAll() => Entities.ToList();

        public void Insert(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            try
            {
                Entities.Add(obj);
                Save();
            }
            catch (DbUpdateException e)
            {
                _dbContext.Dispose();
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(e), e);
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            try
            {
                Entities.Attach(obj);
                //_dbContext.Entry(obj).State = EntityState.Modified;
                Save();
            }
            catch (DbUpdateException e)
            {
                _dbContext.Dispose();
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(e), e);
            }

        }

        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            if (_dbContext is DbContext dbContext)
            {
                var entities = dbContext.ChangeTracker.Entries().
                    Where(w => w.State == EntityState.Added || w.State == EntityState.Modified).ToList();

                entities.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        //Ignored
                    }
                });
            }
            try
            {
                Save();
                return exception.ToString();
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        protected virtual DbSet<T> Entities
        {
            get
            {
                if (table == null)
                    table = _dbContext.Set<T>();
                return table;
            }
        }
    }
}
