using KDrom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDrom.Persistance.EntityTypeConfiguration;

public class CarModelTypeConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.Property(m => m.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(m => m.Make)
            .WithMany(m => m.Models)
            .IsRequired();

        builder.HasMany(m => m.ModelGenerations)
            .WithOne(g => g.Model)
            .IsRequired();
    }
}
