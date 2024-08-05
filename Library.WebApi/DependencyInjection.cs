using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace Library.WebApi
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddWebApiServices(this IServiceCollection services)
		{
			services.AddControllers();

			return services
				.AddSwaggerGen()
				.AddCorsPolicy();
		}

		private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("СorsPolicy", builder =>
				{
					builder.AllowAnyHeader()
						   .AllowAnyMethod()
						   .AllowAnyOrigin();
				});
			});

			return services;
		}

		private static IServiceCollection AddJwtToken(this IServiceCollection services)
		{
			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(opt =>
				{
					opt.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuer = true,
						ValidateAudience = false,
						ValidateLifetime = true,
						ValidIssuer = ,
						IssuerSigningKey =
					};
				});

			services.AddAuthorization();
		}
	}
}
