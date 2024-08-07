using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance
{
    public class AppDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<VerificationCode> UserVerificationCodes { get; set; }

		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			Database.Migrate();
		}
	}
}
