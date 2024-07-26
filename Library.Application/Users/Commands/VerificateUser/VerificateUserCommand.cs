using MediatR;

namespace KDrom.Application.Users.Commands.VerificateUser
{
    public class VerificateUserCommand : IRequest
    {
        public string Id { get; set; }

        public string VerificationCode { get; set; }
    }
}
