using KDrom.Application.Common.Exceptions;
using KDrom.Domain.Interfaces.IRepositories;
using KDrom.Domain.Interfaces.IServices;
using MediatR;

namespace KDrom.Application.Auth.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, TokenVm>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasherService;
    private readonly IJwtProvider _jwtProvider;

    public LoginQueryHandler(IUserRepository userRepository, 
        IJwtProvider jwtProvider, 
        IPasswordHasher passwordHasherService)
    {
        _passwordHasherService = passwordHasherService;
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<TokenVm> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var userDb = await _userRepository.GetByEmail(request.Email);
        if (userDb is null)
            throw new InnerException("Неверный логин или пароль");

        var result = _passwordHasherService.Verify(request.Password, userDb.PasswordSalt, userDb.PasswordHash);
        if (!result)
            throw new InnerException("Неверный логин или пароль");

        var token = _jwtProvider.GenerateToken(userDb);

        return new TokenVm()
        {
            AccessToken = token
        };
    }
}
