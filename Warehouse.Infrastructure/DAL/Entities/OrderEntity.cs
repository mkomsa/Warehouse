namespace Warehouse.Infrastructure.DAL.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerEntity Customer { get; set; }
    public Guid AddressId { get; set; }
    public AddressEntity Address { get; set; }
    public ICollection<OrderProductEntity> OrderProducts { get; set; } = new List<OrderProductEntity>();
    public Guid InvoiceId { get; set; }
    public InvoiceEntity Invoice { get; set; }
}
