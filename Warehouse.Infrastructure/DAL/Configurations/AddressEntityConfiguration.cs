using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.DAL.Entities;

namespace Warehouse.Infrastructure.DAL.Configurations;

internal class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        SetPrimaryKey(builder);
        SetTableName(builder);
        ConfigureColumns(builder);
    }

    private static void SetPrimaryKey(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.HasKey(e => e.Id);
    }

    private static void SetTableName(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.ToTable("address");
    }

    private static void ConfigureColumns(EntityTypeBuilder<AddressEntity> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("id")
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
