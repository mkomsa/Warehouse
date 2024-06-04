using Warehouse.Core.Addresses.Models;

namespace Warehouse.Core.Manufacturers.Models;

public class Manufacturer
{
    public Guid Id { get; set; }
    public Address Address { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
