namespace Warehouse.Infrastructure.DAL.Entities;

public class ProductEntity
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ParcelInfoId { get; set; }
    public Guid ManufacturerId { get; set; }
    public double Price { get; set; }
    public int AvailableAmount { get; set; }
}
