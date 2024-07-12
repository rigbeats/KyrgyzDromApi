﻿using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Library.Utilities.Security
{
	public class PasswordHasher
	{
		public static HashedPassword HashPassword(string password)
		{
			var salt = GenerateSalt();
			var saltedPassword = password + salt;
			var passwordHash = Sha256Hash(saltedPassword);

			return new HashedPassword()
			{
				PasswordHash = passwordHash,
				Salt = salt,
			};
		}

		public static bool VerifyPassword(string password, string salt, string hashedPassword)
		{
			var saltedPassword = password + salt;
			var computeHash = Sha256Hash(saltedPassword);

			return computeHash == hashedPassword;	
		}

		private static string GenerateSalt()
		{
			var saltBytes = new byte[16];

			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(saltBytes);
			}

			return Convert.ToBase64String(saltBytes);
		}

		private static string Sha256Hash(string password)
		{
			var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
			var sBuilder = new StringBuilder();

			foreach (var b in bytes)
				sBuilder.Append(b.ToString("X2"));

			return sBuilder.ToString();
		}
	}

	public class HashedPassword
	{
		public string PasswordHash { get; set; }

		public string Salt { get; set; }
	}
}