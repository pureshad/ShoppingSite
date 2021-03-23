using System.Collections.Generic;

namespace Repositories.Repository
{
    public interface IRepository<T> where T : class
    {
        void Insert(T obj);
        IEnumerable<T> GetUsers();
        void Update(T obj);
        void Delete(int id);
        T GetById(int id);
        void Save();
    }
}
