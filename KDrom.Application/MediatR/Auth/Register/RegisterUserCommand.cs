using MediatR;

namespace KDrom.Application.MediatR.Auth.Register;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Login,
    string Password) : IRequest<string>;