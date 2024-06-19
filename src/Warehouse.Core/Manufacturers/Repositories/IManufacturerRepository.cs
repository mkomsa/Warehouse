using Warehouse.Core.Manufacturers.Models;

namespace Warehouse.Core.Manufacturers.Repositories;

public interface IManufacturerRepository
{
    Task<IReadOnlyCollection<Manufacturer>> GetManufacturersAsync();
}
