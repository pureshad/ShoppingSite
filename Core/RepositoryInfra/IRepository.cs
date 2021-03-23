using System.Collections.Generic;
using System.Linq;

namespace ShoppingSite.RepositoryInfra
{
    public interface IRepository<T> where T : class
    {
        void Insert(T obj);
        IEnumerable<T> GetAll();
        void Update(T obj);
        void Delete(T id);
        T GetById(T id);
        void Save();
        IQueryable<T> Table { get; }

    }
}
