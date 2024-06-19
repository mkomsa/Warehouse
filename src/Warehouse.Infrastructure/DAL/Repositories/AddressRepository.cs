using Microsoft.EntityFrameworkCore;
using Warehouse.Core.Addresses.Models;
using Warehouse.Core.Addresses.Repositories;
using Warehouse.Infrastructure.DAL.Entities;
using Warehouse.Infrastructure.DAL.Exceptions;

namespace Warehouse.Infrastructure.DAL.Repositories;

internal class AddressRepository(AppDbContext dbContext) : IAddressRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyCollection<Address>> GetAddressesAsync()
    {
        try
        {
            Address[] addressEntities = await _dbContext.Addresses
                .Select(e => e.ToAddress())
                .ToArrayAsync();

            if (!addressEntities.Any())
            {
                return new List<Address>
                {
                    new()
                };
            }

            return addressEntities;
        }
        catch (Exception ex)
        {
            throw new DbOperationException("GetAddresses failed.", ex);
        }
    }

    public async Task<Address> GetAddressById(Guid id)
    {
        try
        {
            AddressEntity? addressEntity = await dbContext.Addresses.FirstOrDefaultAsync(e => e.AddressId == id);

            if (addressEntity == null)
            {
                return new Address();
            }

            return addressEntity.ToAddress();
        }
        catch (Exception ex)
        {
            throw new DbOperationException("GetAddresses failed.", ex);
        }
    }
}
