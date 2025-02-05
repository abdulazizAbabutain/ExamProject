using LiteDB;

namespace Domain.Repositories.RepositoryBase
{
    public interface IBaseRepository<T> where T : class
    {
        void Insert(T entity);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        IQueryable<T> GetAll(Func<T, bool> func, int pageNumber, int pageSize);
        IEnumerable<T> GetAll(int pageNumber, int pageSize);
        ILiteCollection<T> GetCollection();
        int Count();
    }
}
