using System.ComponentModel.DataAnnotations;

namespace Warehouse.Infrastructure.DAL.Entities;

public class OrderProductEntity
{
    [Key]
    public Guid OrderProductId { get; set; }
    public Guid OrderId { get; set; }
    public OrderEntity OrderEntity { get; set; }
    public Guid ProductId { get; set; }
    public ProductEntity ProductEntity { get; set; }
}
