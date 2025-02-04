using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationRegisterServices
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.RegisterMediatR();
            return services;
        }



        public static void RegisterMediatR(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
        }
    }
}
