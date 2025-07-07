using Application.Commons.Managers;
using Domain.Managers;
using Infrastructure.Factories;
using Infrastructure.Managers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Infrastructure
{
    public static class InfrastructureRegisterServices
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.AddLocalization(option => option.ResourcesPath = "Resources");
            services.RegisterServices();
            services.AddDistributedMemoryCache();
            return services;
        }

        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddScoped<IAuditManager, AuditManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<ISystemManager, SystemManager>();
        }
    }
}
