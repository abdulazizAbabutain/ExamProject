using Application.Commons.Managers;
using Domain.Managers;
using Infrastructure.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureRegisterServices
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.RegisterServices();
            return services;
        }

        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IAuditManager, AuditManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ISystemManager, SystemManager>();
        }
    }
}
