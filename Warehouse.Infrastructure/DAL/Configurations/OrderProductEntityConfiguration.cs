using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.DAL.Entities;

namespace Warehouse.Infrastructure.DAL.Configurations;
internal class OrderProductEntityConfiguration : IEntityTypeConfiguration<OrderProductEntity>
{
    public void Configure(EntityTypeBuilder<OrderProductEntity> builder)
    {
        SetTableName(builder);
        SetPrimaryKey(builder);
        ConfigureColumns(builder);
    }

    public void SetTableName(EntityTypeBuilder<OrderProductEntity> builder)
    {
        builder.ToTable("order_product");
    }

    public void SetPrimaryKey(EntityTypeBuilder<OrderProductEntity> builder)
    {
        builder.HasKey(e => new { e.OrderId, e.ProductId });
    }

    public void ConfigureColumns(EntityTypeBuilder<OrderProductEntity> builder)
    {

        builder.HasOne(op => op.OrderEntity)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);

        builder.Property(e => e.OrderProductId)
            .HasColumnName("order_product_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.OrderId)
            .HasColumnName("order_id")
            .IsRequired();

        builder.Property(e => e.ProductId)
            .HasColumnName("product_id")
            .IsRequired();
    }
}
