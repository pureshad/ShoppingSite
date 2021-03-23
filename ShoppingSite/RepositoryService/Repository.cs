using ShoppingSite.Models;
using ShoppingSite.RepositoryInfra;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ShoppingSite.RepositoryService
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<TEntity> table;

        public Repository()
        {
            this._dbContext = new ApplicationDbContext();
            //table = _dbContext.Set<T>();
        }

        public IQueryable<TEntity> Table => Entities;

        public void Delete(TEntity id)
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

        public TEntity GetById(TEntity id) => Entities.Find(id);

        public IEnumerable<TEntity> GetAll() => Entities.ToList();

        public void Insert(TEntity obj)
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

        public void Update(TEntity obj)
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

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (table == null)
                    table = _dbContext.Set<TEntity>();
                return table;
            }
        }
    }
}
