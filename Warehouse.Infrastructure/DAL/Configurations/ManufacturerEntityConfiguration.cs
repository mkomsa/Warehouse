using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.DAL.Entities;

namespace Warehouse.Infrastructure.DAL.Configurations;

internal class ManufacturerEntityConfiguration
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
        builder.HasKey(e => e.Id);
    }

    private static void ConfigureColumns(EntityTypeBuilder<ManufacturerEntity> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.AddressEntityId)
            .HasColumnName("address_id")
            .IsRequired();

        builder.HasOne<AddressEntity>()
            .WithMany()
            .HasForeignKey(e => e.AddressEntityId);

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

        builder.HasOne<AddressEntity>()
            .WithMany()
            .HasForeignKey(e => e.AddressEntityId);
    }
}
