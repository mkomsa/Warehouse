namespace Warehouse.Infrastructure.DAL.Views;

public class ManufacturerView
{
    public Guid ManufacturerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Guid AddressId { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string Apartment { get; set; }
}
