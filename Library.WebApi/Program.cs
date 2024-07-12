using Library.Application;
using Library.Application.Common.Mappings;
using Library.Persistance;
using Library.Persistance.Configuration;
using Library.WebApi.Middlewares;
using System.Reflection;

namespace Library.WebApi;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		var persistanceConfiguration = ConfigurationLoader.Load();

		builder.Configuration.AddConfiguration(persistanceConfiguration);

		builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

		builder.Services
			.AddApplication()
			.AddSwaggerGen()
			.AddPersistance(builder.Configuration)
			.AddAutoMapper(config =>
				config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly())))
			.AddCors(options => options.AddPolicy("AllowAll", policy =>
			{
				policy.AllowAnyHeader()
					.AllowAnyMethod()
					.AllowAnyOrigin();
			}))
			.AddControllers();

		var app = builder.Build();

		app.UseSwagger();
		app.UseSwaggerUI(config =>
		{
			config.RoutePrefix = string.Empty;
			config.SwaggerEndpoint("swagger/v1/swagger.json", "Notes API");
		});

		app.UseMiddleware<ExceptionHandlingMiddleware>();
		app.UseHsts();
		app.UseHttpsRedirection();
		app.UseRouting();
		app.UseCors("AllowAll");
		app.MapControllers();

		app.Run();
	}
}