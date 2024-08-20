using KDrom.Application.MediatR.Auth.Dtos;
using MediatR;

namespace KDrom.Application.MediatR.Auth.Refresh;

public record RefreshTokenCommand(
    string Token) : IRequest<TokenDto>;
