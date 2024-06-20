namespace Warehouse.Core.Products.Dtos;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ManufacturerId { get; set; }
    public Guid ParcelInfoId { get; set; }
    public double Price { get; set; }
    public int AvailableAmount { get; set; }
}