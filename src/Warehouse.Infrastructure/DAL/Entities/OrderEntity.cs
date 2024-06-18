using System.ComponentModel.DataAnnotations;
using Warehouse.Core.Orders.Models;
using Warehouse.Core.Products.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class OrderEntity
{
    [Key]
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public CustomerEntity CustomerEntity { get; set; } = new();
    public Guid AddressId { get; set; }
    public AddressEntity AddressEntity { get; set; } = new();
    public ICollection<OrderProductEntity> OrderProducts { get; set; } = new List<OrderProductEntity>();
    public Guid InvoiceId { get; set; }
    public InvoiceEntity InvoiceEntity { get; set; } = new();
    public string Status { get; set; } = String.Empty;

    public static OrderEntity FromOrder(Order order)
    {
        return new OrderEntity()
        {
            OrderId = order.Id,
            CustomerId = order.Customer.Id,
            AddressId = order.Address.Id,
            CustomerEntity = CustomerEntity.FromCustomer(order.Customer),
            AddressEntity = AddressEntity.FromAddress(order.Address),
            OrderProducts = ToOrderProducts(order),
            InvoiceId = order.Invoice.Id,
            InvoiceEntity = InvoiceEntity.FromInvoice(order.Invoice),
            Status = order.Status,
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
                OrderProductId = Guid.NewGuid(),
                OrderId = order.Id,
                ProductId = product.Id,
            };

            orderProducts.Add(newProduct);
        }

        return orderProducts;
    }

    public Order ToOrder()
    {
        return new Order()
        {
            Id = OrderId,
            Address = AddressEntity.ToAddress(),
            Customer = CustomerEntity.ToCustomer(),
            Invoice = InvoiceEntity.ToInvoice(),
            Products = OrderProducts.Select(op => new Product()
            {
                Id = op.ProductEntity?.ProductId ?? Guid.Empty,
                Name = op.ProductEntity.Name,
                Manufacturer = op.ProductEntity.ManufacturerEntity.ToManufacturer(),
                ParcelInfo = op.ProductEntity.ParcelInfoEntity.ToParcelInfo(),
                AvailableAmount = op.ProductEntity?.AvailableAmount ?? 0,
                Price = op.ProductEntity?.Price ?? 0,
            }).ToList(),
            Status = Status
        };
    }
}
