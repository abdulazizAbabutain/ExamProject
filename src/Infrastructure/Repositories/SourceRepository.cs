using Domain.Entities.EntityLookup;
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
    }
}
