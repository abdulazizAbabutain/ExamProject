using Application.Commons.Behaviors;
using Application.Commons.MappingConfig;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationRegisterServices
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(MetricsBehavior<,>));
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            services.AddMapster(); 
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(TagMapper))
                .AddClasses(classes => classes.AssignableTo<IRegister>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            return services;
        }
    }
}
