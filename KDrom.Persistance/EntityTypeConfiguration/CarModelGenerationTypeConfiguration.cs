using KDrom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDrom.Persistance.EntityTypeConfiguration;

public class CarModelGenerationTypeConfiguration : IEntityTypeConfiguration<ModelGeneration>
{
    public void Configure(EntityTypeBuilder<ModelGeneration> builder)
    {
        builder.Property(mg => mg.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(mg => mg.Model)
            .WithMany(m => m.ModelGenerations)
            .IsRequired();

        builder.Property(mg => mg.StartDate)
            .IsRequired();

        builder.Property(mg => mg.EndDate)
            .IsRequired();
    }
}
