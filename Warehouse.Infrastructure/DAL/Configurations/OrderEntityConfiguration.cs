using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.DAL.Entities;

namespace Warehouse.Infrastructure.DAL.Configurations;

internal class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        SetTableName(builder);
        SetPrimaryKey(builder);
        ConfigureColumns(builder);
    }

    private static void SetTableName(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.ToTable("order");
    }

    private static void SetPrimaryKey(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(e => e.OrderId);
    }

    private static void ConfigureColumns(EntityTypeBuilder<OrderEntity> builder)
    {

        builder.HasOne(e => e.AddressEntity)
            .WithMany()
            .HasForeignKey(e => e.AddressId);

        builder.HasOne(e => e.CustomerEntity)
            .WithMany()
            .HasForeignKey(e => e.CustomerId);

        builder.HasOne(e => e.InvoiceEntity)
            .WithOne()
            .HasForeignKey<OrderEntity>(e => e.InvoiceId);

        builder.Property(e => e.OrderId)
            .HasColumnName("order_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.CustomerId)
            .HasColumnName("customer_id")
            .IsRequired();

        builder.Property(e => e.AddressId)
            .HasColumnName("address_id")
            .IsRequired();

        //builder.Property(e => e.OrderProducts)
        //    .HasColumnName("product_id")
        //    .IsRequired();

        //builder.HasOne<ProductEntity>()
        //    .WithMany()
        //    .HasForeignKey(e => e.ProductId);

        builder.Property(e => e.InvoiceId)
            .HasColumnName("invoice_id")
            .IsRequired();
    }
}