using Application.Commons.Managers;
using Domain.Repositories;
using Infrastructure.Audits;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Managers
{
    public class AuditManager : IAuditManager
    {
        private readonly Lazy<IApplicationLogRepository> _ApplicationLogRepository;

        public AuditManager(IConfiguration configuration)
        {
            _ApplicationLogRepository = new Lazy<IApplicationLogRepository>(() => new ApplicationLogRepository(configuration.GetConnectionString("Examiner")));
        }
        public IApplicationLogRepository ApplicationLogRepository => _ApplicationLogRepository.Value;

    }
}
