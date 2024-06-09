using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.DAL.Entities;

namespace Warehouse.Infrastructure.DAL.Configurations;

internal class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        SetTableName(builder);
        SetPrimaryKey(builder);
        ConfigureColumns(builder);
    }

    private static void SetTableName(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.ToTable("product");
    }

    private static void SetPrimaryKey(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(e => e.ProductId);
    }

    private static void ConfigureColumns(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasOne(e => e.ParcelInfoEntity)
            .WithMany()
            .HasForeignKey(e => e.ParcelInfoId);

        builder.HasOne(e => e.ManufacturerEntity)
            .WithMany()
            .HasForeignKey(e => e.ManufacturerId);

        builder.Property(e => e.ProductId)
            .HasColumnName("product_id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        //builder.Property(e => e.CategoryId)
        //    .HasColumnName("category_id")
        //    .IsRequired();

        builder.Property(e => e.ParcelInfoId)
            .HasColumnName("parcel_info_id")
            .IsRequired();

        builder.Property(e => e.ManufacturerId)
            .HasColumnName("manufacturer_id")
            .IsRequired();

        builder.Property(e => e.Price)
            .HasColumnName("price")
            .IsRequired();

        builder.Property(e => e.AvailableAmount)
            .HasColumnName("available_amount")
            .IsRequired();
    }
}
