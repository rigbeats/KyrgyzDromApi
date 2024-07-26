using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Interfaces
{
	public interface IDromDbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<UserVerificationCode> UserVerificationCodes { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
