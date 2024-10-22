using FluentValidation;

namespace KDrom.Application.MediatR.Auth.Register;

public class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(q => q.FirstName)
            .NotEmpty().WithMessage("Имя не должно быть пустым")
            .Matches(@"^[А-Я][а-я]*$").WithMessage("Имя должно содержать русские символы")
            .Matches(@"^[А-Я]").WithMessage("Имя должно начинаться с заглавной буквы");

        RuleFor(q => q.LastName)
            .NotEmpty().WithMessage("Фамилия должна быть пустым")
            .Matches(@"^[А-Я][а-я]*$").WithMessage("Фамилия должна начинаться с заглавной буквы");

        RuleFor(q => q.Email)
            .NotEmpty().WithMessage("Email не должен быть пустым")
            .EmailAddress().WithMessage("Некорректный адрес электронной почты");

        RuleFor(q => q.Password)
            .MinimumLength(8).WithMessage("Пароль должен содержать не менее 8-ми символов")
            .MaximumLength(40).WithMessage("Пароль не должен быть длинее 40-ка символов")
            .Matches(@"[0-9]").WithMessage("Пароль должен содержать не менее одной цифры")
            .Matches(@"[a-z]").WithMessage("Пароль должен содержать не менее одной строчной буквы")
            .Matches(@"[A-Z]").WithMessage("Пароль должен содержать не менее одной заглавной буквы");
    }
}
