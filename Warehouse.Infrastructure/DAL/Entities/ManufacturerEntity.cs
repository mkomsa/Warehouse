namespace Warehouse.Infrastructure.DAL.Entities;

public class ManufacturerEntity
{
    public Guid Id { get; set; }
    public Guid AddressId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
