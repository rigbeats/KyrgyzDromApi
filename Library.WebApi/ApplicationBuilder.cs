using Library.WebApi.Middlewares;

namespace Library.WebApi
{
	public static class ApplicationBuilder
	{
		public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(config =>
			{
				config.RoutePrefix = string.Empty;
				config.SwaggerEndpoint("swagger/v1/swagger.json", "Drom API");
			});

			return app;
		}

		public static IApplicationBuilder UseExceptionMiddleware (this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionHandlingMiddleware>();

			return app;
		}
	}
}
