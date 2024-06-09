using System.ComponentModel.DataAnnotations;
using Warehouse.Core.Customers.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class CustomerEntity
{
    [Key]
    public Guid CustomerId { get; set; }
    public Guid AddressId { get; set; }
    public AddressEntity AddressEntity { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public static CustomerEntity FromCustomer(Customer customer)
    {
        return new CustomerEntity()
        {
            CustomerId = customer.Id,
            AddressId = customer.AddressId,
            AddressEntity = AddressEntity.FromAddress(customer.Address),
            Name = customer.Name,
            FullName = customer.FullName,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
        };
    }

    public Customer ToCustomer()
    {
        return new Customer()
        {
            Id = CustomerId,
            Address = AddressEntity.ToAddress(),
            Name = Name,
            FullName = FullName,
            Email = Email,
            PhoneNumber = PhoneNumber,
        };
    }
}
