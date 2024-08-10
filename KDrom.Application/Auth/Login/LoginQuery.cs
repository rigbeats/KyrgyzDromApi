using MediatR;

namespace KDrom.Application.Auth.Login;

public class LoginQuery : IRequest<TokenVm>
{
    public string Login { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
