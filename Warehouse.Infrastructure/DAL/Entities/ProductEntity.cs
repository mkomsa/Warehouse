using Warehouse.Core.Products.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class ProductEntity
{
    public Guid Id { get; set; }
    public Guid ParcelInfoId { get; set; }
    public ParcelInfoEntity ParcelInfo { get; set; }
    public Guid ManufacturerId { get; set; }
    public ManufacturerEntity Manufacturer { get; set; }
    public double Price { get; set; }
    public int AvailableAmount { get; set; }
    public ICollection<OrderProductEntity> OrderProducts { get; set; } = new List<OrderProductEntity>();

    public Product ToProduct()
    {
        return new Product()
        {
            Id = Id,
            ParcelInfo = ParcelInfo.ToParcelInfo(),
            Manufacturer = Manufacturer.ToManufacturer(),
            AvailableAmount = AvailableAmount,
            Price = Price
        };
    }
}
