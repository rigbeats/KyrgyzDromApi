using Microsoft.Extensions.Configuration;

namespace Library.Persistance
{
	public class ConfigurationLoader
	{
		public static IConfiguration Load()
		{
			var currentDirectory = Directory.GetCurrentDirectory();
			var projectPath = Path.GetFullPath(Path.Combine(currentDirectory, @"..\", "Library.Persistance"));

			var configuraton = new ConfigurationBuilder()
				.SetBasePath(projectPath)
				.AddJsonFile("appsettings.persistance.json", optional: true, reloadOnChange: true)
				.Build();
				
			return configuraton;
		}
	}
}
