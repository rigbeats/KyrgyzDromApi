using KDrom.Application.Common.Exceptions;
using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KDrom.Application.Users.Commands.VerificateUser
{
    public class VerificationUserCommandHandler : IRequestHandler<VerificateUserCommand>
    {
        private readonly IAppDbContext _context;

        public VerificationUserCommandHandler(IAppDbContext context)
        {
            _context = context;            
        }

        public async Task Handle(VerificateUserCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _context.Users.FindAsync(request.Id, cancellationToken);

            if (dbUser is null)
                throw new InnerException("Пользователь не найден");

            var dbVerificationCode = await _context.UserVerificationCodes
                .FirstOrDefaultAsync(x => x.UserId == request.Id, cancellationToken);

            if (dbVerificationCode is null)
                throw new InnerException("Код верификации не найден");

            if (dbVerificationCode.IsUsed)
                throw new ExpiredException("Токен активации уже использован");
        }
    }
}
