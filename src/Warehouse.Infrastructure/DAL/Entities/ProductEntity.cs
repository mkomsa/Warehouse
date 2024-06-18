using System.ComponentModel.DataAnnotations;
using Warehouse.Core.Products.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class ProductEntity
{
    [Key]
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public Guid ParcelInfoId { get; set; }
    public ParcelInfoEntity ParcelInfoEntity { get; set; }
    public Guid ManufacturerId { get; set; }
    public ManufacturerEntity ManufacturerEntity { get; set; }
    public double Price { get; set; }
    public int AvailableAmount { get; set; }
    public ICollection<OrderProductEntity> OrderProductEntities { get; set; } = new List<OrderProductEntity>();

    public Product ToProduct()
    {
        return new Product()
        {
            Id = ProductId,
            Name = Name,
            ParcelInfo = ParcelInfoEntity.ToParcelInfo(),
            Manufacturer = ManufacturerEntity.ToManufacturer(),
            AvailableAmount = AvailableAmount,
            Price = Price
        };
    }
}
