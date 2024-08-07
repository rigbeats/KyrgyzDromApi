using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Library.WebApi
{
    public static class DependencyInjection
	{
		public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddControllers();

			return services
				.AddJwtToken(configuration)
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

		private static IServiceCollection AddJwtToken(this IServiceCollection services, IConfiguration configuration)
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
						ValidIssuer = configuration["JwtOptions:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:Secret"]))
					};
				});

			services.AddAuthorization();

			return services;
		}
	}
}
