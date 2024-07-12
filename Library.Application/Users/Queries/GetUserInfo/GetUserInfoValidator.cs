using FluentValidation;

namespace Library.Application.Users.Queries.GetUserInfo
{
	public class GetUserInfoValidator : AbstractValidator<GetUserInfoQuery>
	{
		public GetUserInfoValidator()
		{
			RuleFor(q => q.UserId).NotEmpty();
		}
	}
}
