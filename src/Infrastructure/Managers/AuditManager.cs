using Application.Commons.Managers;
using Application.Commons.Services;
using Domain.Repositories;
using Infrastructure.Audits;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Managers
{
    public class AuditManager(IConfiguration configuration) : IAuditManager
    {
        private readonly Lazy<IApplicationLogRepository> _ApplicationLogRepository = new(() => new ApplicationLogRepository(configuration.GetConnectionString("Examiner")));
        private readonly Lazy<IAuditTrailService> _AuditTrailService = new(() => new AuditTrailService(configuration.GetConnectionString("Examiner")));

        public IApplicationLogRepository ApplicationLogRepository => _ApplicationLogRepository.Value;

        public IAuditTrailService AuditTrailService => _AuditTrailService.Value;
    }
}
