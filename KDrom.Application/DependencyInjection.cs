using FluentValidation;
using KDrom.Application.Behaviors;
using KDrom.Application.Common.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KDrom.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services
            .AddMediatR()
            .AddValidation()
            .AddMapping();
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

    private static IServiceCollection AddMapping(this IServiceCollection services)
    {
        return services.AddAutoMapper(config =>
            config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly())));
    }
}
