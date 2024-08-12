using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Dto;
using KDrom.Domain.Entities;
using KDrom.Domain.Enums;
using KDrom.Domain.Interfaces.IRepositories;
using KDrom.Domain.Interfaces.IServices;
using KDrom.Domain.Interfaces.Services;
using KDrom.Utilities;
using MediatR;

namespace KDrom.Application.Auth.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IVerificationCodeRepository _userVerificationCodeRepository;
    private readonly IEmailService _emailService;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IUserRepository userRepository,
        IVerificationCodeRepository userVerificationCodeRepository,
        IEmailService emailService, IPasswordHasher passwordHasherService)
    {
        _userVerificationCodeRepository = userVerificationCodeRepository;
        _passwordHasher = passwordHasherService;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.IsLoginTaken(request.Login))
            throw new InnerException("Пользователь с таким логином уже существует");

        if (await _userRepository.IsEmailTaken(request.Email))
            throw new InnerException("Пользователь с такой почтой уже зарегистрирован");

        var hashedPassword = _passwordHasher.Hash(request.Password);
        var user = new User()
        {
            Id = Guid.NewGuid(),
            Firstname = request.FirstName,
            Lastname = request.LastName,
            Email = request.Email,
            Login = request.Login,
            PasswordHash = hashedPassword.PasswordHash,
            PasswordSalt = hashedPassword.Salt,
            IsEmailConfirmed = false
        };

        await _userRepository.AddAsync(user);
        await SendMessageToEmail(user.Email, user.Id, cancellationToken);
        await _userRepository.SaveChangesAsync();

        return user.Id;
    }

    private async Task SendMessageToEmail(string email, Guid userId, CancellationToken cancellationToken)
    {
        var verificationCode = Generator.GenerateVerificationCode(6);
        var template = "Asdasd";
        if (template == null)
            throw new InnerException("Не найден шаблон для отправки сообщения");

        var message = template;
        var dbCode = new VerificationCode()
        {
            Id = Guid.NewGuid(),
            ExpiredAt = DateTime.UtcNow.AddMinutes(15),
            Code = verificationCode,
            UserId = userId,
        };

        await _userVerificationCodeRepository.AddAsync(dbCode);
        await _userVerificationCodeRepository.SaveChangesAsync();

        await _emailService.SendAsync(new EmailDto()
        {
            Message = message,
            RecipientEmail = email,
            Subject = "Активация аккаунта"
        });
    }
}
