using Library.Application;
using Library.Persistance;
using Library.Persistance.Configuration;

namespace Library.WebApi;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

		builder.Services
			.AddPersistanceServices(builder.Configuration)
			.AddApplicationServices()
			.AddWebApiServices();
		
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