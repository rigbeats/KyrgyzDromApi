using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Entities;
using KDrom.Domain.Enums;
using KDrom.Domain.Interfaces.IRepositories;
using KDrom.Domain.Interfaces.IServices;
using KDrom.Domain.Interfaces.Services;
using KDrom.Utilities;
using MediatR;

namespace KDrom.Application.Auth.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
{
    private readonly IVerificationCodeRepository _userVerificationCodeRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IEmailService _emailService;

    public RegisterUserCommandHandler(IUserRepository userRepository,
        IVerificationCodeRepository userVerificationCodeRepository,
        IEmailService emailService, IPasswordHasher passwordHasherService,
        IRoleRepository roleRepository)
    {
        _userVerificationCodeRepository = userVerificationCodeRepository;
        _passwordHasher = passwordHasherService;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.IsLoginTaken(request.Login))
            throw new InnerException("Пользователь с таким логином уже зарегистрирован");

        if (await _userRepository.IsEmailTaken(request.Email))
            throw new InnerException("Пользователь с такой почтой уже зарегистрирован");

        var hashedPassword = _passwordHasher.Hash(request.Password);
        var role = await _roleRepository.FindByNameAsync(RoleTypes.Seller.ToString());

        var user = new User()
        {
            Id = Guid.NewGuid().ToString(),
            Firstname = request.FirstName,
            Lastname = request.LastName,
            Email = request.Email,
            Login = request.Login,
            PasswordHash = hashedPassword.PasswordHash,
            PasswordSalt = hashedPassword.Salt,
            IsEmailConfirmed = false,
            Role = role
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();
        await SendVerificationCode(user.Email, user.Id);

        return user.Id;
    }

    private async Task SendVerificationCode(string email, string userId)
    {
        var verificationCode = Generator.GenerateVerificationCode(6);

        var template = "Asdasd";
        if (template == null)
            throw new InnerException("Не найден шаблон для отправки сообщения");

        var subject = "Активация аккаунта";
        var body = template;

        var codeDb = new VerificationCode()
        {
            Id = Guid.NewGuid().ToString(),
            ExpiredAt = DateTime.UtcNow.AddMinutes(15),
            Code = verificationCode,
            UserId = userId,
        };

        await _userVerificationCodeRepository.AddAsync(codeDb);
        await _userVerificationCodeRepository.SaveChangesAsync();

        await _emailService.SendAsync(email, subject, body);
    }
}
