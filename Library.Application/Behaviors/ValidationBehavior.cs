using FluentValidation;
using MediatR;

namespace Library.Application.Behaviors
{
	public class ValidationBehavior<TRequest, TReasponse>
		: IPipelineBehavior<TRequest, TReasponse> where TRequest : IRequest<TReasponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}

		public Task<TReasponse> Handle(TRequest request, RequestHandlerDelegate<TReasponse> next, CancellationToken cancellationToken)
		{
			var context = new ValidationContext<TRequest>(request);
			var failures = _validators
				.Select(v => v.Validate(context))
				.SelectMany(result => result.Errors)
				.Where(failure => failure != null)
				.ToList();

			if (failures.Count != 0)
			{
				throw new ValidationException(failures);
			}

			return next();
		}
	}
}
