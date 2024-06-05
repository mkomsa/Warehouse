using Warehouse.Core.Manufacturers.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class ManufacturerEntity
{
    public Guid Id { get; set; }
    public Guid AddressEntityId { get; set; }
    public AddressEntity AddressEntity { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Manufacturer ToManufacturer()
    {
        return new Manufacturer()
        {
            Id = Id,
            Address = AddressEntity.ToAddress(),
            Name = Name,
            Email = Email,
            PhoneNumber = PhoneNumber
        };
    }
}
