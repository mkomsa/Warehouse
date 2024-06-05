using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.DAL.Entities;

namespace Warehouse.Infrastructure.DAL.Configurations;

internal class ProductEntityConfiguration
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
        builder.HasKey(e => e.Id);
    }

    private static void ConfigureColumns(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        //builder.Property(e => e.CategoryId)
        //    .HasColumnName("category_id")
        //    .IsRequired();

        builder.Property(e => e.ParcelInfoEntityId)
            .HasColumnName("parcel_info_id")
            .IsRequired();

        builder.HasOne<ParcelInfoEntity>()
            .WithMany()
            .HasForeignKey(e => e.ParcelInfoEntityId);

        builder.Property(e => e.ManufacturerEntityId)
            .HasColumnName("manufacturer_id")
            .IsRequired();

        builder.HasOne<ManufacturerEntity>()
            .WithMany()
            .HasForeignKey(e => e.ManufacturerEntityId);

        builder.Property(e => e.Price)
            .HasColumnName("price")
            .IsRequired();

        builder.Property(e => e.AvailableAmount)
            .HasColumnName("available_amount")
            .IsRequired();
    }
}
