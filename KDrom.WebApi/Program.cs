using KDrom.Application;
using KDrom.Persistance;
using KDrom.Persistance.Configuration;
using KDrom.WebApi;

namespace KDrom.WebApi;

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
        app.UseRouting();          
        app.UseCors("ÑorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();      
        app.UseHsts();
        app.UseHttpsRedirection();
        app.MapControllers();

        app.Run();
	}
}