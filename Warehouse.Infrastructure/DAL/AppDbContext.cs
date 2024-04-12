using Microsoft.EntityFrameworkCore;
using Warehouse.Infrastructure.DAL.Entities;

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
}
