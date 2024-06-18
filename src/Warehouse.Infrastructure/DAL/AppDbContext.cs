using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Warehouse.Infrastructure.DAL.Entities;
using Warehouse.Infrastructure.DAL.Views;

namespace Warehouse.Infrastructure.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<ManufacturerEntity> Manufacturers { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ParcelInfoEntity> ParcelsInfo { get; set; }
    public DbSet<InvoiceEntity> Invoices { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderProductEntity> OrdersProducts { get; set; }
    public DbSet<OrderView> OrderViews { get; set; }
    public DbSet<ProductView> ProductViews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<OrderView>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("OrderView", "public");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.CustomerAddressId).HasColumnName("customer_address_id");
            entity.Property(e => e.CustomerName).HasColumnName("customer_name");
            entity.Property(e => e.CustomerFullName).HasColumnName("customer_full_name");
            entity.Property(e => e.CustomerEmail).HasColumnName("customer_email");
            entity.Property(e => e.CustomerPhoneNumber).HasColumnName("customer_phone_number");
            entity.Property(e => e.AddressPostalCode).HasColumnName("address_postal_code");
            entity.Property(e => e.AddressStreet).HasColumnName("address_street");
            entity.Property(e => e.AddressApartment).HasColumnName("address_apartment");
            entity.Property(e => e.InvoiceTransactionDate).HasColumnName("invoice_transaction_date");
            entity.Property(e => e.InvoiceNetValue).HasColumnName("invoice_net_value");
            entity.Property(e => e.InvoiceGrossValue).HasColumnName("invoice_gross_value");
            entity.Property(e => e.InvoiceStatus).HasColumnName("invoice_status");
            entity.Property(e => e.InvoiceVatRate).HasColumnName("invoice_vat_rate");
            entity.Property(e => e.OrderProductId).HasColumnName("order_product_id");
            entity.Property(e => e.OrderProductOrderId).HasColumnName("order_product_order_id");
            entity.Property(e => e.OrderProductProductId).HasColumnName("order_product_product_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductName).HasColumnName("product_name");
            entity.Property(e => e.ProductManufacturerId).HasColumnName("product_manufacturer_id");
            entity.Property(e => e.ProductParcelInfoId).HasColumnName("product_parcel_info_id");
            entity.Property(e => e.ProductAvailableAmount).HasColumnName("product_available_amount");
            entity.Property(e => e.ProductPrice).HasColumnName("product_price");
            entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
            entity.Property(e => e.ManufacturerAddressId).HasColumnName("manufacturer_address_id");
            entity.Property(e => e.ParcelInfoId).HasColumnName("parcel_info_id");
            entity.Property(e => e.ParcelInfoWeight).HasColumnName("parcel_info_weight");
            entity.Property(e => e.ParcelInfoHeight).HasColumnName("parcel_info_height");
            entity.Property(e => e.ParcelInfoLength).HasColumnName("parcel_info_length");
            entity.ToView("order_view");
        });
        modelBuilder.Entity<ProductView>(entity =>
        {
            entity.HasNoKey();
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.AvailableAmount).HasColumnName("available_amount");
            entity.Property(e => e.ParcelInfoId).HasColumnName("parcel_info_id");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Width).HasColumnName("width");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Weight).HasColumnName("weight");
            entity.Property(e => e.ManufacturerId).HasColumnName("manufacturer_id");
            entity.Property(e => e.ManufacturerName).HasColumnName("manufacturer_name");
            entity.Property(e => e.ManufacturerEmail).HasColumnName("manufacturer_email");
            entity.Property(e => e.ManufacturerPhone).HasColumnName("manufacturer_phone");
            entity.Property(e => e.ManufacturerAddressId).HasColumnName("manufacturer_address_id");
            entity.Property(e => e.ManufacturerPostalCode).HasColumnName("manufacturer_postal_code");
            entity.Property(e => e.ManufacturerStreet).HasColumnName("manufacturer_street");
            entity.Property(e => e.ManufacturerApartment).HasColumnName("manufacturer_apartment");
            entity.ToView("product_view");
        });
    }
}
