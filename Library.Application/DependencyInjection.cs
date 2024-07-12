using FluentValidation;
using Library.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Library.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddMediatR(config 
				=> config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			services
				.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
			services.AddTransient(typeof(IPipelineBehavior<,>),
				typeof(ValidationBehavior<,>));

			return services;
		}
	}
}
