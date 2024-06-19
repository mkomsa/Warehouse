using Microsoft.EntityFrameworkCore;
using Warehouse.Core.Manufacturers.Models;
using Warehouse.Core.Manufacturers.Repositories;

namespace Warehouse.Infrastructure.DAL.Repositories;

internal class ManufacturerRepository(AppDbContext dbContext) : IManufacturerRepository
{
    public async Task<IReadOnlyCollection<Manufacturer>> GetManufacturersAsync()
    {
        return dbContext.Manufacturers
            .Include(e => e.AddressEntity)
            .Select(m => m.ToManufacturer())
            .ToList();
    }
}
