using KDrom.Application.Abstractions.Command;
using KDrom.Application.Common.Exceptions;
using KDrom.Application.MediatR.Auth.Dtos;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using KDrom.Domain.Interfaces.IServices;
using MediatR;
using System.Windows.Input;

namespace KDrom.Application.MediatR.Auth.Refresh;

public record RefreshTokenCommand(
    string Token) : ICommand<TokenDto>;

public class RefreshTokenCommandHandler(IUserRefreshTokenRepository userRefreshTokenRepository,
    IJwtProvider jwtProvider) : ICommandHandler<RefreshTokenCommand, TokenDto>
{
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository = userRefreshTokenRepository;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenDb = await _userRefreshTokenRepository.GetByToken(request.Token)
            ?? throw new InnerException("Токен недействителен");

        if (refreshTokenDb.IsUsed)
            throw new InnerException("Токен уже был использован. Пройдите авторизацию");

        if (refreshTokenDb.ExpiresAt > DateTime.UtcNow)
            throw new InnerException("Время жизни токена истекло. Пройдите авторизацию");

        var accessToken = _jwtProvider.GenerateAccessToken(refreshTokenDb.User);
        var refreshToken = _jwtProvider.GenerateRefreshToken(refreshTokenDb.User);

        await _userRefreshTokenRepository.AddAsync(new UserRefreshToken()
        {
            Id = Guid.NewGuid().ToString(),
            Token = refreshToken.Token,
            ExpiresAt = refreshToken.TokenExpiry,
            User = refreshTokenDb.User,
            IsUsed = false
        });

        refreshTokenDb.IsUsed = true;

        await _userRefreshTokenRepository.SaveChangesAsync();

        return new TokenDto()
        {
            AccessToken = accessToken.Token,
            AccessTokenExpiry = accessToken.TokenExpiry,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpiry = refreshToken.TokenExpiry,
        };
    }
}
