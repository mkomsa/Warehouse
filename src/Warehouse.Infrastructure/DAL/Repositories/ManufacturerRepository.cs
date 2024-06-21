using Microsoft.EntityFrameworkCore;
using Warehouse.Core.Addresses.Models;
using Warehouse.Core.Manufacturers.Models;
using Warehouse.Core.Manufacturers.Repositories;

namespace Warehouse.Infrastructure.DAL.Repositories;

internal class ManufacturerRepository(AppDbContext dbContext) : IManufacturerRepository
{
    public async Task<IReadOnlyCollection<Manufacturer>> GetManufacturersAsync()
    {
        return await dbContext.ManufacturerViews
            .Select(m => new Manufacturer
            {
                Id = m.ManufacturerId,
                Name = m.Name,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber,
                Address = new Address
                {
                    Id = m.AddressId,
                    PostalCode = m.PostalCode,
                    Street = m.Street,
                    Apartment = m.Apartment
                }
            })
            .ToListAsync();
    }
}
