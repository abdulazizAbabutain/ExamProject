using Application.Commons.Managers;
using Domain.Managers;
using Infrastructure.Managers;
using Infrastructure.Services;
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
            services.AddTransient<IRepositoryManager, RepositoryManager>();
            services.AddTransient<IServiceManager, ServiceMangaer>();

        }
    }
}
