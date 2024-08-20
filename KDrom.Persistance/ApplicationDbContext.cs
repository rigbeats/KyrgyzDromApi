using KDrom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Bcpg.OpenPgp;

namespace KDrom.Persistance
{
    public class ApplicationDbContext : DbContext
	{
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

		public DbSet<VerificationCode> UserVerificationCodes { get; set; }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Model> Models { get; set; }

        public DbSet<ModelGeneration> ModelGenerations { get; set; }

        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql(_configuration.GetConnectionString("RecipeConnectionString"));
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("RecipeConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
		}
	}
}
