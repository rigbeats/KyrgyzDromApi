using Library.Application.Interfaces;
using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance
{
	public class RecipeDbContext : DbContext, IDromDbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<UserVerificationCode> UserVerificationCodes { get; set; }

		public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			Database.Migrate();
		}
	}
}
