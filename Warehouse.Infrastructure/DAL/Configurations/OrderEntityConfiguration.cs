using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.DAL.Entities;

namespace Warehouse.Infrastructure.DAL.Configurations;

internal class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        SetPrimaryKey(builder);
        SetTableName(builder);
        ConfigureColumns(builder);
    }

    private static void SetPrimaryKey(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(e => e.Id);
    }

    private static void SetTableName(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("order");
    }

    private static void ConfigureColumns(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.CustomerId)
            .HasColumnName("customer_id")
            .IsRequired();

        builder.HasOne<CustomerEntity>()
            .WithMany()
            .HasForeignKey(e => e.CustomerId);

        builder.Property(e => e.AddressId)
            .HasColumnName("address_id")
            .IsRequired();

        builder.Property(e => e.OrderProducts)
            .HasColumnName("product_id")
            .IsRequired();

        //builder.HasOne<ProductEntity>()
        //    .WithMany()
        //    .HasForeignKey(e => e.ProductId);

        builder.Property(e => e.InvoiceId)
            .HasColumnName("invoice_id")
            .IsRequired();

        builder.HasOne<AddressEntity>()
            .WithMany()
            .HasForeignKey(e => e.AddressId);

        builder.HasOne<CustomerEntity>()
            .WithMany()
            .HasForeignKey(e => e.CustomerId);

        builder.HasOne<InvoiceEntity>()
            .WithOne()
            .HasForeignKey<OrderEntity>(e => e.InvoiceId);

    }
}