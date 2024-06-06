using Warehouse.Core.Orders.Models;
using Warehouse.Core.Products.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public Guid CustomerEntityId { get; set; }
    public CustomerEntity CustomerEntity { get; set; } = new();
    public Guid AddressEntityId { get; set; }
    public AddressEntity AddressEntity { get; set; } = new();
    public ICollection<OrderProductEntity> OrderProducts { get; set; } = new List<OrderProductEntity>();
    public Guid InvoiceEntityId { get; set; }
    public InvoiceEntity InvoiceEntity { get; set; } = new();

    public static OrderEntity FromOrder(Order order)
    {
        return new OrderEntity()
        {
            Id = order.Id,
            CustomerEntityId = order.Customer.Id,
            AddressEntityId = order.Address.Id,
            CustomerEntity = CustomerEntity.FromCustomer(order.Customer),
            AddressEntity = AddressEntity.FromAddress(order.Address),
            OrderProducts = ToOrderProducts(order),
            InvoiceEntityId = order.Invoice.Id,
            InvoiceEntity = InvoiceEntity.FromInvoice(order.Invoice)
        };
    }

    private static ICollection<OrderProductEntity> ToOrderProducts(Order order)
    {
        List<OrderProductEntity> orderProducts = new List<OrderProductEntity>()
        {
        };

        foreach (var product in order.Products)
        {
            OrderProductEntity newProduct = new OrderProductEntity()
            {
                Id = Guid.NewGuid(),
                OrderEntityId = order.Id,
                ProductEntityId = product.Id,
            };

            orderProducts.Add(newProduct);
        }

        return orderProducts;
    }

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
                Id = op.ProductEntity?.Id ?? Guid.Empty,
                Manufacturer = op.ProductEntity.ManufacturerEntity.ToManufacturer(),
                ParcelInfo = op.ProductEntity.ParcelInfoEntity.ToParcelInfo(),
                AvailableAmount = op.ProductEntity?.AvailableAmount ?? 0,
                Price = op.ProductEntity?.Price ?? 0,
            }).ToList()
        };
    }
}
