using KDrom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDrom.Persistance.EntityTypeConfiguration;

public class CarModelGenerationTypeConfiguration : IEntityTypeConfiguration<CarModelGeneration>
{
    public void Configure(EntityTypeBuilder<CarModelGeneration> builder)
    {
        builder.Property(mg => mg.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(mg => mg.CarModel)
            .WithMany(m => m.CarModelGenerations)
            .IsRequired();

        builder.Property(mg => mg.StartDate)
            .IsRequired();

        builder.Property(mg => mg.EndDate)
            .IsRequired();
    }
}
