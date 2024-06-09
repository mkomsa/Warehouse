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
        builder.HasKey(e => e.InvoiceId);
    }

    private static void ConfigureColumns(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.Property(e => e.InvoiceId)
            .HasColumnName("invoice_id")
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
            .HasPrecision(18, 2)
            .HasComputedColumnSql("gross_value * ((100 - vat_rate)/100)", true)
            .HasConversion(v => double.Round(2, MidpointRounding.AwayFromZero), v => v)
            .ValueGeneratedOnAddOrUpdate()
            .HasMaxLength(16)
            .IsRequired();

        builder.Property(e => e.VatRate)
            .HasColumnName("vat_rate")
            .HasMaxLength(4)
            .IsRequired();
    }

    //private static void ConfigureIndexes(EntityTypeBuilder<InvoiceEntity> builder)
    //{
    //    builder.HasIndex(e => EF.Functions.(e.NetValue, builder.Property(nameof(InvoiceEntity.GrossValue)).Metadata.Name + ' ' + builder.Property(nameof(InvoiceEntity.VatRate)).Metadata.Name)).HasDatabaseName("IX_invoice_net_value");
    //}
}
