using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.DAL.Entities;

namespace Warehouse.Infrastructure.DAL.Configurations;

internal class ManufacturerEntityConfiguration : IEntityTypeConfiguration<ManufacturerEntity>
{
    public void Configure(EntityTypeBuilder<ManufacturerEntity> builder)
    {
        SetTableName(builder);
        SetPrimaryKey(builder);
        ConfigureColumns(builder);
    }

    private static void SetTableName(EntityTypeBuilder<ManufacturerEntity> builder)
    {
        builder.ToTable("manufacturer");
    }

    private static void SetPrimaryKey(EntityTypeBuilder<ManufacturerEntity> builder)
    {
        builder.HasKey(e => e.ManufacturerId);
    }

    private static void ConfigureColumns(EntityTypeBuilder<ManufacturerEntity> builder)
    {

        builder.HasOne(e => e.AddressEntity)
            .WithMany()
            .HasForeignKey(e => e.AddressId);

        builder.Property(e => e.ManufacturerId)
            .HasColumnName("manufacturer_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.AddressId)
            .HasColumnName("address_id")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.Email)
            .HasColumnName("email")
            .HasMaxLength(320)
            .IsRequired();

        builder.Property(e => e.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(16)
            .IsRequired();
    }
}
