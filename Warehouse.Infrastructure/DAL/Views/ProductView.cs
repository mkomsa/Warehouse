namespace Warehouse.Infrastructure.DAL.Views;

public class ProductView
{
    public Guid ProductId { get; set; }
    public double Price { get; set; }
    public int AvailableAmount { get; set; }
    public Guid ParcelInfoId { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public Guid ManufacturerId { get; set; }
    public string ManufacturerName { get; set; }
    public string ManufacturerEmail { get; set; }
    public string ManufacturerPhone { get; set; }
    public Guid ManufacturerAddressId { get; set; }
    public string ManufacturerPostalCode { get; set; }
    public string ManufacturerStreet { get; set; }
    public string ManufacturerApartment { get; set; }
}
