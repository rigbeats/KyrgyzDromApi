using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Interfaces
{
	public interface IRecipeDbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<UserVerificationCode> UserVerificationCodes { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}
