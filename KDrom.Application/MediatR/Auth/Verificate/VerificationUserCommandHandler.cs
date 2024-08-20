using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Interfaces.IRepositories;
using MediatR;

namespace KDrom.Application.MediatR.Auth.Verificate;

public class VerificationUserCommandHandler : IRequestHandler<VerificateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IVerificationCodeRepository _verificationCodeRepository;

    public VerificationUserCommandHandler(IVerificationCodeRepository verificationCodeRepository,
        IUserRepository userRepository)
    {
        _verificationCodeRepository = verificationCodeRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(VerificateUserCommand request, CancellationToken cancellationToken)
    {
        var userDb = await _userRepository.GetByIdAsync(request.UserId);

        if (userDb is null)
            throw new InnerException("Пользователь не найден");

        var verificationCodeDb = await _verificationCodeRepository.GetByUserIdAsync(request.UserId);

        if (verificationCodeDb is null)
            throw new InnerException("Не найден код верификации");

        if (verificationCodeDb.ExpiredAt > DateTime.UtcNow)
            throw new InnerException("Истекло время использование кода активации");

        if (verificationCodeDb.Code != request.VerificationCode)
            throw new InnerException("Неверный код активации");

        if (verificationCodeDb.IsUsed)
            throw new InnerException("Код активации уже использован");

        userDb.IsEmailConfirmed = true;
        await _userRepository.SaveChangesAsync();
    }
}
