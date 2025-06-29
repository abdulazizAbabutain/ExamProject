using Application.Commons.Managers;
using Application.Commons.Services;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Managers
{
    public class SystemManager(IConfiguration configuration) : ISystemManager
    {
        private readonly Lazy<ISystemService> _SystemService = new(() => new SystemService(configuration.GetConnectionString("Examiner")));

        public ISystemService SystemService => _SystemService.Value;
    }
}
