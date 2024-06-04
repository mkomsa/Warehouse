using Warehouse.Core.Customers.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class CustomerEntity
{
    public Guid Id { get; set; }
    public Guid AddressId { get; set; }
    public AddressEntity Address { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Customer ToCustomer()
    {
        return new Customer()
        {
            Id = Id,
            Address = Address.ToAddress(),
            Name = Name,
            FullName = FullName,
            Email = Email,
            PhoneNumber = PhoneNumber,
        };
    }
}
