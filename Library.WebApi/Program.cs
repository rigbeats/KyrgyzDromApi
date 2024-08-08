using KDrom.Persistance.Configuration;
using Library.Application;
using Library.Persistance;
using Library.Persistance.Configuration;

namespace Library.WebApi;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		var services = builder.Services;
		var configuration = builder.Configuration;

		services.Configure<SmtpOptions>(builder.Configuration.GetSection("SmtpSettings"));
		services.Configure<JwtTokenOptions>(builder.Configuration.GetSection("JwtOptions"));

		services
			.AddWebApiServices(configuration)
			.AddPersistanceServices()
			.AddApplicationServices();
		
		var app = builder.Build();

		app.UseCustomSwagger();
		app.UseExceptionMiddleware();
		app.UseHsts();
		app.UseHttpsRedirection();
		app.UseRouting();
		app.UseCors("ÑorsPolicy");
		app.MapControllers();

		app.Run();
	}
}