using LiteDB;
using Domain.Repositories.RepositoryBase;

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
    }
}
