using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Warehouse.Infrastructure.DAL.Entities;

namespace Warehouse.Infrastructure.DAL.Configurations;

internal class InvoiceEntityConfiguration : IEntityTypeConfiguration<InvoiceEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
    {
        SetTableName(builder);
        SetPrimaryKey(builder);
        ConfigureColumns(builder);
    }

    private static void SetTableName(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.ToTable("invoice");
    }

    private static void SetPrimaryKey(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.HasKey(e => e.Id);
    }

    private static void ConfigureColumns(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.TransactionDate)
            .HasColumnName("transaction_date")
            .HasColumnType("timestamptz")
            .IsRequired();

        builder.Property(e => e.NetValue)
            .HasColumnName("net_value")
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(e => e.GrossValue)
            .HasColumnName("gross_value")
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(e => e.Status)
            .HasColumnName("status")
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(e => e.NetValue)
            .HasColumnName("net_value")
            .HasMaxLength(4)
            .IsRequired();
    }
}
