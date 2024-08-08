using KDrom.Domain.Interfaces.IRepositories;
using KDrom.Persistance.Repositories;
using Library.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Persistance
{
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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();

			return services;
        }

		private static IServiceCollection AddServices(this IServiceCollection services)
		{
			services
				.AddTransient<IEmailService, EmailService>();

			return services;
		}
	}
}
