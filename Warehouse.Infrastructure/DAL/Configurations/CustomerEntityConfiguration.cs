using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.DAL.Entities;

namespace Warehouse.Infrastructure.DAL.Configurations;

internal class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
{
    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
        SetTableName(builder);
        SetPrimaryKey(builder);
        ConfigureColumns(builder);
    }

    private static void SetTableName(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.ToTable("customer");
    }

    private static void SetPrimaryKey(EntityTypeBuilder<CustomerEntity> builder)
    {
        builder.HasKey(e => e.CustomerId);
    }

    private static void ConfigureColumns(EntityTypeBuilder<CustomerEntity> builder)
    {

        builder.HasOne(e => e.AddressEntity)
            .WithMany()
            .HasForeignKey(e => e.AddressId);

        builder.Property(e => e.CustomerId)
            .HasColumnName("customer_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.AddressId)
            .HasColumnName("address_id")
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName("name");

        builder.Property(e => e.FullName)
            .HasColumnName("full_name")
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
