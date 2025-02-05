using Domain.Repositories.RepositoryBase;
using LiteDB;

namespace Infrastructure.Repositories.BaseRepository
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly string _databasePath;
        private readonly string _collectionName;

        public BaseRepository(string databasePath, string collectionName)
        {
            _databasePath = databasePath;
            _collectionName = collectionName;
        }

        private LiteDatabase GetDatabase() => new LiteDatabase(_databasePath);

        public void Insert(T entity)
        {
            using var db = GetDatabase();
            var collection = db.GetCollection<T>(_collectionName);
            collection.Insert(entity);
        }

        public void Update(T entity)
        {
            using var db = GetDatabase();
            var collection = db.GetCollection<T>(_collectionName);
            collection.Update(entity);
        }

        public void Delete(int id)
        {
            using var db = GetDatabase();
            var collection = db.GetCollection<T>(_collectionName);
            collection.Delete(id);
        }

        public T GetById(Guid id)
        {
            using var db = GetDatabase();
            var collection = db.GetCollection<T>(_collectionName);
            return collection.FindById(id);
        }

        public IEnumerable<T> GetAll()
        {
            using var db = GetDatabase();
            var collection = db.GetCollection<T>(_collectionName);
            return collection.FindAll().ToList();
        }

        public IQueryable<T> GetAll(Func<T , bool> func, int pageNumber, int pageSize)
        {
            using var db = GetDatabase();
            var collection = db.GetCollection<T>(_collectionName);
            return collection.FindAll()
                .Where(func)
                .Skip(pageNumber)
                .Take(pageSize)
                .AsQueryable();
        }
        public IEnumerable<T> GetAll(int pageNumber, int pageSize)
        {
            using var db = GetDatabase();
            var collection = db.GetCollection<T>(_collectionName);
            return collection.FindAll()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }


        public ILiteCollection<T> GetCollection()
        {
            using var db = GetDatabase();
            return db.GetCollection<T>(_collectionName);
        }

        public int Count()
        {
            using var db = GetDatabase();
            var collection = db.GetCollection<T>(_collectionName);
            return collection.Count();
        }
    }
}
