using Library.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDrom.Persistance.EntityTypeConfiguration
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Firstname)
                .HasMaxLength(50);

            builder.Property(u => u.Lastname)
                .HasMaxLength(50);

            builder.Property(u => u.Login)
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .HasMaxLength(50);

            builder.Property(u => u.PasswordSalt)
                .HasMaxLength(40);

            builder.Property(u => u.PasswordHash)
                .HasMaxLength(65);

            builder.Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(50);
        }
    }
}
