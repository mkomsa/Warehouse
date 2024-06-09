using Warehouse.Core.Addresses.Models;

namespace Warehouse.Core.Customers.Models;

public class Customer
{
    public Guid Id { get; set; }
    public Guid AddressId { get; set; }
    public Address Address { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}
