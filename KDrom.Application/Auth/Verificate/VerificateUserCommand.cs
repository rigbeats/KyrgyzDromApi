using MediatR;

namespace KDrom.Application.Auth.Verificate;

public record VerificateUserCommand(
    string UserId,
    string VerificationCode) : IRequest;