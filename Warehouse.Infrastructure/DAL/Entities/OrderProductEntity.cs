namespace Warehouse.Infrastructure.DAL.Entities;

public class OrderProductEntity
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public OrderEntity OrderEntities { get; set; }
    public Guid ProductId { get; set; }
    public ProductEntity ProductEntity { get; set; }
}
