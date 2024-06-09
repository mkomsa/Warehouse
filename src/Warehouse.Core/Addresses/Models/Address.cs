namespace Warehouse.Core.Addresses.Models;

public class Address
{
    public Guid Id { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string Apartment { get; set; }
}
