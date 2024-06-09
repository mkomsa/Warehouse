using Warehouse.Core.Addresses.Models;

namespace Warehouse.Core.Addresses.Repositories;

public interface IAddressRepository
{
    Task<IReadOnlyCollection<Address>> GetAddressesAsync();
    Task<Address> GetAddressById(Guid id);
}
