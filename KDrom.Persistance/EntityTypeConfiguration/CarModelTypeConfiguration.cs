using KDrom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDrom.Persistance.EntityTypeConfiguration;

public class CarModelTypeConfiguration : IEntityTypeConfiguration<CarModel>
{
    public void Configure(EntityTypeBuilder<CarModel> builder)
    {
        builder.Property(m => m.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasOne(m => m.CarMake)
            .WithMany(m => m.CarModels)
            .IsRequired();

        builder.HasMany(m => m.CarModelGenerations)
            .WithOne(g => g.CarModel)
            .IsRequired();
    }
}
