using MediatR;

namespace KDrom.Application.Auth.Login;

public  record LoginQuery(
    string Login,
    string Email,
    string Password) : IRequest<TokenVm>;