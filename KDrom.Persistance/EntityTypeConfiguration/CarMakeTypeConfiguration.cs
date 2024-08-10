using KDrom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KDrom.Persistance.EntityTypeConfiguration;

public class CarMakeTypeConfiguration : IEntityTypeConfiguration<CarMake>
{
    public void Configure(EntityTypeBuilder<CarMake> builder)
    {
        builder.Property(m => m.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(m => m.CarModels)
            .WithOne(m => m.CarMake)
            .IsRequired();
    }
}
