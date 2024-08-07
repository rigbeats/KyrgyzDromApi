using KDrom.Domain.Interfaces.IRepositories;
using KDrom.Utilities;
using Library.Application.Common.Exceptions;
using Library.Domain.Dto;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Services;
using Library.Utilities.Security;
using MediatR;

namespace Library.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
	{
		private readonly IUserRepository _userRepository;
		private readonly IVerificationCodeRepository _userVerificationCodeRepository;
		private readonly IEmailService _emailService;

		public RegisterUserCommandHandler(IUserRepository userRepository, 
			IVerificationCodeRepository userVerificationCodeRepository, IEmailService emailService)
		{
			_userVerificationCodeRepository = userVerificationCodeRepository;
			_userRepository = userRepository;
			_emailService = emailService;
		}

		public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
            if (await _userRepository.IsLoginTaken(request.Login))
                throw new InnerException("Пользователь с таким логином уже существует");

            if (await _userRepository.IsEmailTaken(request.Email))
                throw new InnerException("Пользователь с такой почтой уже зарегистрирован");

            var hashedPassword = PasswordHasher.HashPassword(request.Password);
			var user = new User()
			{
				Id = Guid.NewGuid().ToString(),
				Firstname = request.FirstName,
				Lastname = request.LastName,
				Email = request.Email,
				Login = request.Login,
				PasswordHash = hashedPassword.PasswordHash,
				PasswordSalt = hashedPassword.Salt,
				IsEmailConfirmed = false,
				Role = UserRole.User
			};

			await _userRepository.AddAsync(user);
			await SendMessageToEmail(user.Email, user.Id, cancellationToken);
			await _userRepository.SaveChangesAsync();

			return user.Id;
		}

		private async Task SendMessageToEmail(string email, string userId, CancellationToken cancellationToken)
		{
			var verificationCode = Generator.GenerateVerificationCode(6);
			var template = "Asdasd";
			if (template == null)
				throw new InnerException("Не найден шаблон для отправки сообщения");

			var message = template;
			var dbCode = new VerificationCode()
			{
				Id = Guid.NewGuid().ToString(),
				ExpiredAt = DateTime.UtcNow.AddMinutes(15),
				Code = verificationCode,
				UserId = userId,
			};

			await _userVerificationCodeRepository.AddAsync(dbCode);
			await _userVerificationCodeRepository.SaveChangesAsync();

			await _emailService.SendAsync(new EmailDto()
			{
				Message = message,
				RecipientEmail = email,
				Subject = "Активация аккаунта"
			});
		}
	}
}
