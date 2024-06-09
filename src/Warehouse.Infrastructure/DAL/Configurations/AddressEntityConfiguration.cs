using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.DAL.Entities;

namespace Warehouse.Infrastructure.DAL.Configurations;

internal class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        SetTableName(builder);
        SetPrimaryKey(builder);
        ConfigureColumns(builder);
    }
    private static void SetTableName(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.ToTable("address");
    }

    private static void SetPrimaryKey(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.HasKey(e => e.AddressId);
    }

    private static void ConfigureColumns(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.Property(e => e.AddressId)
            .HasColumnName("address_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.PostalCode)
            .HasColumnName("postal_code")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.Street)
            .HasColumnName("street")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.Apartment)
            .HasColumnName("apartment")
            .HasMaxLength(50)
            .IsRequired();
    }
}
