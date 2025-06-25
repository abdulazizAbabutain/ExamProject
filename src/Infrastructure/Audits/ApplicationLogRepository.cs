using Domain.Auditing;
using Domain.Repositories;
using Infrastructure.Repositories.BaseRepository;
using System.Linq;

namespace Infrastructure.Audits
{
    internal class ApplicationLogRepository : BaseRepository<ApplicationLog>, IApplicationLogRepository
    {
        public ApplicationLogRepository(string databasePath)
            : base(databasePath, "logs")
        {
        }
    }
}
