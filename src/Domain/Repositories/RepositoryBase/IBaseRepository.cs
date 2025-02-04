namespace Domain.Repositories.RepositoryBase
{
    public interface IBaseRepository<T> where T : class
    {
        void Insert(T entity);
        IEnumerable<T> GetAll();
        public T GetById(Guid id);

    }
}
