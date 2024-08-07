using MediatR;

namespace KDrom.Application.Users.Commands.VerificateUser
{
    public class VerificateUserCommand : IRequest
    {
        public string UserId { get; set; }

        public string VerificationCode { get; set; }
    }
}
