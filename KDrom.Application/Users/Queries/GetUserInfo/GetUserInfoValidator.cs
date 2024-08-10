using FluentValidation;
using KDrom.Application.Users.Queries.GetUserInfo;

namespace Library.Application.Users.Queries.GetUserInfo;

public class GetUserInfoValidator : AbstractValidator<GetUserInfoQuery>
{
    public GetUserInfoValidator()
    {
        RuleFor(q => q.UserId)
            .NotEmpty().WithMessage("Идентификатор пользователя не должен быть пустым");
    }
}
