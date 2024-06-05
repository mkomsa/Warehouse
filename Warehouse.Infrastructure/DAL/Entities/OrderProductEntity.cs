namespace Warehouse.Infrastructure.DAL.Entities;

public class OrderProductEntity
{
    public Guid Id { get; set; }
    public Guid OrderEntityId { get; set; }
    public OrderEntity OrderEntity { get; set; }
    public Guid ProductEntityId { get; set; }
    public ProductEntity ProductEntity { get; set; }
}
