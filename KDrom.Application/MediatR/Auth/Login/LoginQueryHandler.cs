using KDrom.Application.Common.Exceptions;
using KDrom.Application.MediatR.Auth.Dtos;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using KDrom.Domain.Interfaces.IServices;
using Mapster;
using MediatR;
using System.Reflection.Metadata.Ecma335;

namespace KDrom.Application.MediatR.Auth.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, TokenDto>
{
    private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;
    private readonly IPasswordHasher _passwordHasherService;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;

    public LoginQueryHandler(IPasswordHasher passwordHasherService,
        IUserRefreshTokenRepository userRefreshTokenRepository,
        IUserRepository userRepository,
        IJwtProvider jwtProvider)
    {
        _userRefreshTokenRepository = userRefreshTokenRepository;
        _passwordHasherService = passwordHasherService;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<TokenDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var userDb = await _userRepository.GetByEmailWithRole(request.Email);
        if (userDb is null)
            throw new InnerException("Неверный логин или пароль");

        var result = _passwordHasherService.Verify(request.Password, userDb.PasswordSalt, userDb.PasswordHash);
        if (!result)
            throw new InnerException("Неверный логин или пароль");

        var accessToken = _jwtProvider.GenerateAccessToken(userDb);
        var refreshToken = _jwtProvider.GenerateRefreshToken(userDb);

        await _userRefreshTokenRepository.AddAsync(new UserRefreshToken()
        {
            Id = Guid.NewGuid().ToString(),
            Token = refreshToken.Token,
            ExpiresAt = refreshToken.TokenExpiry,
            User = userDb,
            IsUsed = false
        });

        return new TokenDto()
        {
            AccessToken = accessToken.Token,
            AccessTokenExpiry = accessToken.TokenExpiry,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpiry = refreshToken.TokenExpiry,
        };
    }
}
