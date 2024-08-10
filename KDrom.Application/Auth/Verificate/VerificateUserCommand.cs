using MediatR;

namespace KDrom.Application.Auth.Verificate;

public class VerificateUserCommand : IRequest
{
    public Guid UserId { get; set; }

    public string VerificationCode { get; set; }
}
