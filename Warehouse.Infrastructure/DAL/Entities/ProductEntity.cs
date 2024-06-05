using Warehouse.Core.Products.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class ProductEntity
{
    public Guid Id { get; set; }
    public Guid ParcelInfoEntityId { get; set; }
    public ParcelInfoEntity ParcelInfoEntity { get; set; }
    public Guid ManufacturerEntityId { get; set; }
    public ManufacturerEntity ManufacturerEntity { get; set; }
    public double Price { get; set; }
    public int AvailableAmount { get; set; }
    public ICollection<OrderProductEntity> OrderProductEntities { get; set; } = new List<OrderProductEntity>();

    public Product ToProduct()
    {
        return new Product()
        {
            Id = Id,
            ParcelInfo = ParcelInfoEntity.ToParcelInfo(),
            Manufacturer = ManufacturerEntity.ToManufacturer(),
            AvailableAmount = AvailableAmount,
            Price = Price
        };
    }
}
