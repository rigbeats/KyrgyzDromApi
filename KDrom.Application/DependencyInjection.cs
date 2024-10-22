using FluentValidation;
using KDrom.Application.Behaviors;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KDrom.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        AddMapping();

        return services
            .AddMediatR()
            .AddValidation();
    }

    private static IServiceCollection AddMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    private static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

        services.AddTransient(typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }

    private static void AddMapping()
    {
        //TypeAdapterConfig.GlobalSettings.Scan(typeof(MappingConfig).Assembly);
    }
}
