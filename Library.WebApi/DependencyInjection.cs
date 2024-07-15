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

		public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
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
	}
}
