using Domain.Entities.Sources;
using Domain.Repositories;
using Infrastructure.Repositories.BaseRepository;

namespace Infrastructure.Repositories
{
    public class ReferenceRepository : BaseRepository<SourceReference>, IReferenceRepository
    {
        public ReferenceRepository(string databasePath) 
            : base(databasePath, nameof(SourceReference))
        {
        }
    }
}
