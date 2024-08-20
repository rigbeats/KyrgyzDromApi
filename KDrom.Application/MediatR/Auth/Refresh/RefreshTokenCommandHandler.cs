using KDrom.Application.Common.Exceptions;
using KDrom.Application.MediatR.Auth.Dtos;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using KDrom.Domain.Interfaces.IServices;
using MediatR;
using System.Transactions;

namespace KDrom.Application.MediatR.Auth.Refresh;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
{
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
    private readonly IJwtProvider _jwtProvider;

    public RefreshTokenCommandHandler(IUserRefreshTokenRepository userRefreshTokenRepository, IJwtProvider jwtProvider)
    {
        _userRefreshTokenRepository = userRefreshTokenRepository;
        _jwtProvider = jwtProvider;
    }

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
