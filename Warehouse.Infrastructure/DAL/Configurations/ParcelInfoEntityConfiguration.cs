using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.DAL.Entities;

namespace Warehouse.Infrastructure.DAL.Configurations;
internal class ParcelInfoEntityConfiguration : IEntityTypeConfiguration<ParcelInfoEntity>
{
    public void Configure(EntityTypeBuilder<ParcelInfoEntity> builder)
    {
        SetPrimaryKey(builder);
        SetTableName(builder);
        ConfigureColumns(builder);
    }

    private static void SetPrimaryKey(EntityTypeBuilder<ParcelInfoEntity> builder)
    {
        builder.HasKey(e => e.ProductId);
    }

    private static void SetTableName(EntityTypeBuilder<ParcelInfoEntity> builder)
    {
        builder.ToTable("id");
    }

    private static void ConfigureColumns(EntityTypeBuilder<ParcelInfoEntity> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(e => e.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        builder.HasOne<ProductEntity>()
            .WithMany()
            .HasForeignKey(e => e.ProductId);

        builder.Property(e => e.Length)
            .HasColumnName("length")
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(e => e.Width)
            .HasColumnName("width")
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(e => e.Height)
            .HasColumnName("height")
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(e => e.Weight)
            .HasColumnName("weight")
            .HasMaxLength(16)
            .IsRequired();
    }
}
