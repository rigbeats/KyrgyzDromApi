using Library.Application.Interfaces;
using Library.Domain.Services;
using Library.Persistance.Configuration;
using Library.Persistance.ServicesImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Persistance
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services
				.AddDatabase(configuration)
				.AddServices();

			return services;
		}

		private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("RecipeConnectionString");

			services
				.AddDbContext<RecipeDbContext>(options =>
					options.UseNpgsql(connectionString));

			services.AddScoped<IDromDbContext, RecipeDbContext>();

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
