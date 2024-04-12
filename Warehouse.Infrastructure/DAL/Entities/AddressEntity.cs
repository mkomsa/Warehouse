namespace Warehouse.Infrastructure.DAL.Entities;

public class AddressEntity
{
    public Guid Id { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string Apartment { get; set; }
}
