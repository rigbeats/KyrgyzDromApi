﻿using Library.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
	public class User : EntityBase
	{
		[MaxLength(25)]
		public string Firstname { get; set; }

		[MaxLength(35)]
		public string Lastname { get; set; }

		[MaxLength(50)]
		public string Email { get; set; }

		[MaxLength(50)]
		public string Login { get; set; }

		[MaxLength(65)]
		public string PasswordHash { get; set; }

		[MaxLength(40)]
		public string PasswordSalt { get; set; }

		public bool IsEmailConfirmed { get; set; }

		public UserRole Role { get; set; }
	}
}
