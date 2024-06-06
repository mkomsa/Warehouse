using System.ComponentModel.DataAnnotations;
using Warehouse.Core.Addresses.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class AddressEntity
{
    [Key]
    public Guid AddressId { get; set; }
    public string PostalCode { get; set; }
    public string Street { get; set; }
    public string Apartment { get; set; }

    public static AddressEntity FromAddress(Address address)
    {
        return new AddressEntity()
        {
            AddressId = address.Id,
            PostalCode = address.PostalCode,
            Street = address.Street,
            Apartment = address.Apartment,
        };
    }

    public Address ToAddress()
    {
        return new Address()
        {
            Id = AddressId,
            PostalCode = PostalCode,
            Street = Street,
            Apartment = Apartment
        };
    }
}
