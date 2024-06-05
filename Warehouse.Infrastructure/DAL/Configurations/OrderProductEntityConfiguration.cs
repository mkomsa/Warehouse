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
        builder.HasKey(e => new { e.OrderEntityId, e.ProductEntityId });
    }

    public void ConfigureColumns(EntityTypeBuilder<OrderProductEntity> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.OrderEntityId)
            .HasColumnName("order_id")
            .IsRequired();

        builder.Property(e => e.ProductEntityId)
            .HasColumnName("product_id")
            .IsRequired();

        builder.HasOne(op => op.OrderEntity)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderEntityId);
    }
}
