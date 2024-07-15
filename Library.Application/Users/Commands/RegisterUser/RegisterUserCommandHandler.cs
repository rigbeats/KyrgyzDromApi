using Library.Application.Common.Exceptions;
using Library.Application.Interfaces;
using Library.Domain.Dto;
using Library.Domain.Entities;
using Library.Domain.Enums;
using Library.Domain.Services;
using Library.Utilities.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Library.Application.Users.Commands.RegisterUser
{
	public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
	{
		private readonly IRecipeDbContext _context;
		private readonly IEmailService _emailService;

		public RegisterUserCommandHandler(IRecipeDbContext context, IEmailService emailService)
		{
			_context = context;
			_emailService = emailService;
		}

		public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			var userDb = await _context.Users.FirstOrDefaultAsync(x => x.Login == request.Login 
				|| x.Email == request.Email, cancellationToken);

			//if (userDb != null)
			//{
			//	if (userDb.Login == request.Login)
			//		throw new AlreadyExistsException("Пользователь с таким логином уже существует");
//
//				if (userDb.Email == request.Email)
//					throw new AlreadyExistsException("Пользователь с такой почтой уже зарегистрирован");
//			}

			var hashedPassword = PasswordHasher.HashPassword(request.Password);
			var user = new User()
			{
				Id = Guid.NewGuid().ToString(),
				Firstname = request.FirstName,
				Lastname = request.LastName,
				Email = request.Email,
				Login = request.Login,
				Password = hashedPassword.PasswordHash,
				PasswordSalt = hashedPassword.Salt,
				IsActivated = false,
				Role = UserRole.User
			};

			//await _context.Users.AddAsync(user, cancellationToken);
			//await _context.SaveChangesAsync(cancellationToken);
			await SendMessageToEmail(user.Email, user.Id, cancellationToken);

			return user.Id;
		}

		private async Task SendMessageToEmail(string email, string userId, CancellationToken cancellationToken)
		{
			var verificationCode = GenerateVerificationCode(6);
			var template = "Asdasd";
			if (template == null)
				throw new NotFoundException("Не найден шаблон для отправки сообщения");

			var message = template;
			var dbCode = new UserVerificationCode()
			{
				Id = Guid.NewGuid().ToString(),
				ExpiredAt = DateTime.UtcNow.AddMinutes(15),
				VerificationCode = verificationCode,
				UserId = userId,
			};

			await _context.UserVerificationCodes.AddAsync(dbCode, cancellationToken);
			//await _context.SaveChangesAsync(cancellationToken);

			await _emailService.SendAsync(new EmailDto()
			{
				Message = message,
				RecipientEmail = email,
				Subject = "Активация аккаунта"
			});
		}

		private string GenerateVerificationCode(int codeLength)
		{
			using (var rng = RandomNumberGenerator.Create())
			{
				int randomCode = 0;
				byte[] randomNumber = new byte[4];

				int minValue = (int)Math.Pow(10, codeLength - 1);
				int maxValue = (int)Math.Pow(10, codeLength);

				while (randomCode < minValue || randomCode >= maxValue)
				{
					rng.GetBytes(randomNumber);
					randomCode = BitConverter.ToInt32(randomNumber, 0);
				}

				return randomCode.ToString().PadLeft(codeLength, '0');
			}
		}
	}
}
