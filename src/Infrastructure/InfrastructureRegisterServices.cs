using Application.Commons.Managers;
using Application.Commons.Services;
using Domain.Managers;
using Infrastructure.Managers;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureRegisterServices
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
        {
            services.RegisterServices();


            //services.AddSingleton<LiteDatabase>(new LiteDatabase("Filename= database.db"));





            return services;
        }

        private static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
            services.AddScoped<IServiceManager, ServiceMangaer>();
        }
    }
}
