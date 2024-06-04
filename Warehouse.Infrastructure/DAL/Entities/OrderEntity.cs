using Warehouse.Core.Orders.Models;
using Warehouse.Core.Products.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerEntity CustomerEntity { get; set; }
    public Guid AddressId { get; set; }
    public AddressEntity AddressEntity { get; set; }
    public ICollection<OrderProductEntity> OrderProducts { get; set; } = new List<OrderProductEntity>();
    public IReadOnlyCollection<ProductEntity> ProductEntities { get; set; }
    public Guid InvoiceId { get; set; }
    public InvoiceEntity InvoiceEntity { get; set; }

    public Order ToOrder()
    {
        return new Order()
        {
            Id = Id,
            Address = AddressEntity.ToAddress(),
            Customer = CustomerEntity.ToCustomer(),
            Invoice = InvoiceEntity.ToInvoice(),
            Products = OrderProducts.Select(op => new Product()
            {
                Id = op.ProductEntity.Id,
                Manufacturer = op.ProductEntity.ManufacturerEntity.ToManufacturer(),
                ParcelInfo = op.ProductEntity.ParcelInfoEntity.ToParcelInfo(),
                AvailableAmount = op.ProductEntity.AvailableAmount,
                Price = op.ProductEntity.Price,
            }).ToList()
        };
    }
}
