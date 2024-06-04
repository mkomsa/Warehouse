using Microsoft.EntityFrameworkCore;
using Warehouse.Core.Orders.Models;
using Warehouse.Infrastructure.DAL.Entities;
using Warehouse.Infrastructure.DAL.Exceptions;

namespace Warehouse.Infrastructure.DAL.Repositories;

internal class OrdersRepository(AppDbContext dbContext)
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyCollection<Order>> GetOrdersAsync()
    {
        try
        {
            Order[] orderEntities = await _dbContext.Orders.Include(e => e.CustomerEntity)
                .Include(e => e.AddressEntity)
                .Include(e => e.OrderProducts)
                    .ThenInclude(op => op.ProductEntity)
                .Select(e => e.ToOrder())
                .ToArrayAsync();

            if (!orderEntities.Any())
            {
                return new List<Order>()
                {
                    new Order()
                };
            }

            return orderEntities;
        }
        catch (Exception ex)
        {
            throw new DbOperationException("GetAddresses failed.", ex);
        }
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        try
        {
            OrderEntity? orderEntity = await dbContext.Orders.FirstOrDefaultAsync(e => e.Id == id);

            if (orderEntity == null)
            {
                return new Order();
            }

            return orderEntity
                .ToOrder();
        }
        catch (Exception ex)
        {
            throw new DbOperationException("GetAddresses failed.", ex);
        }
    }
}
