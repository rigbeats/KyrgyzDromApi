using MediatR;

namespace KDrom.Application.MediatR.Auth.Verificate;

public record VerificateUserCommand(
    string UserId,
    string VerificationCode) : IRequest;