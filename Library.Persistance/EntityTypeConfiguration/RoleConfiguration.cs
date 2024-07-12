using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Persistance.EntityTypeConfiguration
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.HasKey(role => role.Id);
			builder.HasIndex(role => role.Id).IsUnique();
			builder.Property(role => role.Title).HasMaxLength(50);
		}
	}
}