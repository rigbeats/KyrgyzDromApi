﻿using MediatR;

namespace Library.Application.Users.Commands.RegisterUser
{
	public class RegisterUserCommand : IRequest<string>
	{ 
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }
	}
}
