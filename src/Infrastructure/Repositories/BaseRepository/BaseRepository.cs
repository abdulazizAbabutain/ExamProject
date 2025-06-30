using Domain.Auditing;
using Domain.Repositories.RepositoryBase;
using LiteDB;

namespace Infrastructure.Repositories.BaseRepository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly LiteDatabase _db;          
        protected readonly ILiteCollection<T> _collection;
        protected readonly string _collectionName;

        public BaseRepository(string databasePath, string collectionName)
        {
           _db = new LiteDatabase(databasePath);
           _collectionName = collectionName;
           _collection = _db.GetCollection<T>(_collectionName);
        }

        public virtual void Insert(T entity)
            => _collection.Insert(entity);

        public virtual void Insert(IEnumerable<T> entity)
         => _collection.Insert(entity);

        public virtual void Update(T entity)
            => _collection.Update(entity);

        public virtual void DeleteById(Guid id)
            => _collection.Delete(id);
        
        public virtual void DeleteAll()
            => _collection.DeleteAll(); 
        
        public virtual T GetById(Guid id)
            => _collection.FindById(id);

        public virtual IEnumerable<T> GetAll()
            => _collection.FindAll();

        public virtual IEnumerable<T> GetAll(Func<T , bool> func, int pageNumber, int pageSize)
            => _collection.FindAll()
                .Where(func)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


        public virtual Task<IEnumerable<T>> GetAllAsync(Func<T, bool> func, int pageNumber, int pageSize)
        => Task.Run(() =>
        {
            return _collection
                .FindAll()
                .Where(func)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        });
        

        public virtual IEnumerable<T> GetAll(int pageNumber, int pageSize)
            => _collection.FindAll()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

        public virtual ILiteCollection<T> GetCollection() 
            => _collection;

        public virtual int Count()
            => _collection.Count();


        public virtual Task<int> CountAsync()
             => Task.Run(() =>
             {
                 return _collection.Count();
             });
        public void Update(IEnumerable<T> entity)
            => _collection.Update(entity);

        public void Dispose()
        {
            _db?.Dispose();
        }



    }
}
