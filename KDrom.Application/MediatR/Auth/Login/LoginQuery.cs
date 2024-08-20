using KDrom.Application.MediatR.Auth.Dtos;
using MediatR;

namespace KDrom.Application.MediatR.Auth.Login;

public record LoginQuery(
    string Login,
    string Email,
    string Password) : IRequest<TokenDto>;