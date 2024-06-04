using Warehouse.Core.Addresses.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class AddressEntity
{
    public Guid Id { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string Apartment { get; set; }

    public Address ToAddress()
    {
        return new Address()
        {
            Id = Id,
            PostalCode = PostalCode,
            Street = Street,
            Apartment = Apartment
        };
    }
}
