using Library.Application.Interfaces;
using Library.Domain.Entities;
using Library.Persistance.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistance
{
	public class RecipeDbContext : DbContext, IRecipeDbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<UserVerificationCode> UserVerificationCodes { get; set; }

		public RecipeDbContext(DbContextOptions<RecipeDbContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new RoleConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}
}
