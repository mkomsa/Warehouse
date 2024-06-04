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
}
