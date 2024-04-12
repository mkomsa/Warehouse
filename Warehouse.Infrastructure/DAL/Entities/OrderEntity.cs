namespace Warehouse.Infrastructure.DAL.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public CustomerEntity Customer { get; set; }
    public Guid CustomerId { get; set; }
    public Guid AddressId { get; set; }
    public ProductEntity Product { get; set; }
    public Guid ProductId { get; set; }
    public Guid InvoiceId { get; set; }
}
