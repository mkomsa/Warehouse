using System.ComponentModel.DataAnnotations;
using Warehouse.Core.Manufacturers.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class ManufacturerEntity
{
    [Key]
    public Guid ManufacturerId { get; set; }
    public Guid AddressId { get; set; }
    public AddressEntity AddressEntity { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Manufacturer ToManufacturer()
    {
        return new Manufacturer()
        {
            Id = ManufacturerId,
            Address = AddressEntity.ToAddress(),
            Name = Name,
            Email = Email,
            PhoneNumber = PhoneNumber
        };
    }
}
