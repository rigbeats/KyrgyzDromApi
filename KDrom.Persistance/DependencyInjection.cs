﻿using KDrom.Domain.Interfaces.IRepositories;
using KDrom.Domain.Interfaces.IServices;
using KDrom.Domain.Interfaces.Services;
using KDrom.Persistance.Repositories;
using KDrom.Persistance.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KDrom.Persistance;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services)
    {
        services
            .AddDatabase()
            .AddServices()
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        return services.AddDbContext<ApplicationDbContext>();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

        services.AddScoped<IUserVerificationCodeRepository, UserVerificationCodeRepository>();
        services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}
