namespace Warehouse.Core.Products.Dtos;

public class ProductDto
{
    public string Name { get; set; }
    public Guid ManufacturerId { get; set; }
    public Guid ParcelInfoId { get; set; }
    public double Price { get; set; }
}