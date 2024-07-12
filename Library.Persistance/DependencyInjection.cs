using Library.Application.Interfaces;
using Library.Domain.Services;
using Library.Persistance.ServicesImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Persistance
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
		{
			services
				.AddDatabase(configuration)
				.AddEmailService();

			return services;
		}

		private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("RecipeConnectionString");

			services
				.AddDbContext<RecipeDbContext>(options =>
					options.UseNpgsql(connectionString));

			services.AddScoped<IRecipeDbContext, RecipeDbContext>();

			return services;
		}

		private static IServiceCollection AddEmailService(this IServiceCollection services)
		{
			return services.AddTransient<IEmailService, EmailService>();
		}
	}
}
