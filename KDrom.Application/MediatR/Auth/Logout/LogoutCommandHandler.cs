using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Interfaces.IRepositories;
using MediatR;

namespace KDrom.Application.MediatR.Auth.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
{
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;

    public LogoutCommandHandler(IUserRefreshTokenRepository userRefreshTokenRepository)
    {
        _userRefreshTokenRepository = userRefreshTokenRepository;
    }

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenDb = await _userRefreshTokenRepository.GetByToken(request.Token)
            ?? throw new InnerException("Невалидный токен");

        if (refreshTokenDb.IsUsed || refreshTokenDb.ExpiresAt < DateTime.UtcNow)
            throw new InnerException("Токен уже использован или истек");

        refreshTokenDb.IsUsed = true;

        await _userRefreshTokenRepository.SaveChangesAsync();
    }
}
