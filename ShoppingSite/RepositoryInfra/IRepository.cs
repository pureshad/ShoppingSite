using System.Collections.Generic;
using System.Linq;

namespace ShoppingSite.RepositoryInfra
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Insert(TEntity obj);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity obj);
        void Delete(TEntity id);
        TEntity GetById(TEntity id);
        void Save();
        IQueryable<TEntity> Table { get; }

    }
}
