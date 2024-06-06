using Warehouse.Core.Addresses.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class AddressEntity
{
    public Guid Id { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string Apartment { get; set; }

    public static AddressEntity FromAddress(Address address)
    {
        return new AddressEntity()
        {
            Id = address.Id,
            PostalCode = address.PostalCode,
            Street = address.Street,
            Apartment = address.Apartment,
        };
    }

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
