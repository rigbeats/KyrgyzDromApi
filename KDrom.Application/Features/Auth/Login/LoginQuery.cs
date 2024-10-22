using KDrom.Application.Abstractions.Query;
using KDrom.Application.Common.Exceptions;
using KDrom.Application.MediatR.Auth.Dtos;
using KDrom.Domain.Entities;
using KDrom.Domain.Interfaces.IRepositories;
using KDrom.Domain.Interfaces.IServices;

namespace KDrom.Application.MediatR.Auth.Login;

public record LoginQuery(
    string Login,
    string Email,
    string Password) : IQuery<TokenDto>;

public class LoginQueryHandler(IRepository<UserRefreshToken> userRefreshTokenRepository,
    IUserRepository userRepository,
    IPasswordHasher passwordHasherService,
    IJwtProvider jwtProvider) : IQueryHandler<LoginQuery, TokenDto>
{
    private readonly IRepository<UserRefreshToken> _userRefreshTokenRepository = userRefreshTokenRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IPasswordHasher _passwordHasherService = passwordHasherService;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<TokenDto> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var userDb = await _userRepository.GetByEmailWithRole(request.Email)
            ?? throw new InnerException("Неверный логин или пароль");

        if (!_passwordHasherService.Verify(request.Password, userDb.PasswordSalt, userDb.PasswordHash))
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