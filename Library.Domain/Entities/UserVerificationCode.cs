using System.ComponentModel.DataAnnotations;

namespace Library.Domain.Entities
{
	public class UserVerificationCode : EntityBase
	{
		public DateTime ExpiredAt { get; set; }

		[MaxLength(6)]
		public string VerificationCode { get; set; }

		public bool IsUsed { get; set; }

		[MaxLength(36)]
		public string UserId { get; set; }

		public User User { get; set; }
	}
}
