using MediatR;

namespace KDrom.Application.MediatR.Auth.Logout;

public record LogoutCommand(
    string Token) : IRequest;
