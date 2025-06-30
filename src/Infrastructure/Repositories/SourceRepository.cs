using Domain.Entities.Sources;
using Domain.Repositories;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories
{
    public class SourceRepository : BaseRepository<Source>, ISourceRepository
    {
        public SourceRepository(string databasePath) 
            : base(databasePath, nameof(Source))
        {
        }

        public bool IsExist(Guid id)
        {
            return _collection.Exists(x => x.Id == id);
        }

        public bool IsNotExist(Guid id)
        {
            return !_collection.Exists(x => x.Id == id);
        }
    }
}
