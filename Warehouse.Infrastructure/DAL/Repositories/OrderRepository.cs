using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Warehouse.Core.Orders.Models;
using Warehouse.Core.Orders.Repositories;
using Warehouse.Infrastructure.DAL.Entities;
using Warehouse.Infrastructure.DAL.Exceptions;

namespace Warehouse.Infrastructure.DAL.Repositories;

internal class OrderRepository(AppDbContext dbContext) : IOrderRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyCollection<Order>> GetOrdersAsync()
    {
        try
        {
            IIncludableQueryable<OrderEntity, ParcelInfoEntity> baseQuery = _dbContext.Orders
                .Include(o => o.CustomerEntity)
                    .ThenInclude(ce => ce.AddressEntity)
                .Include(o => o.AddressEntity)
                .Include(o => o.InvoiceEntity)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductEntity)
                        .ThenInclude(p => p.ManufacturerEntity)
                            .ThenInclude(m => m.AddressEntity)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductEntity)
                        .ThenInclude(p => p.ParcelInfoEntity);


            List<Order> orders = await baseQuery
                .Select(e => e.ToOrder())
                .ToListAsync();

            if (!orders.Any())
            {
                return new List<Order>()
                {
                    new Order()
                };
            }

            return orders;
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
            OrderEntity? orderEntity = await _dbContext.Orders
                .Include(o => o.CustomerEntity)
                    .ThenInclude(ce => ce.AddressEntity)
                .Include(o => o.AddressEntity)
                .Include(o => o.InvoiceEntity)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductEntity)
                        .ThenInclude(p => p.ManufacturerEntity)
                        .ThenInclude(m => m.AddressEntity)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductEntity)
                        .ThenInclude(p => p.ParcelInfoEntity)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (orderEntity == null)
            {
                return new Order();
            }

            return orderEntity.ToOrder();


        }
        catch (Exception ex)
        {
            throw new DbOperationException("GetAddresses failed.", ex);
        }
    }

    public async Task<Guid> CreateOrderAsync(Order order)
    {
        try
        {
            OrderEntity orderEntity = OrderEntity.FromOrder(order);
            await _dbContext.Orders.AddAsync(orderEntity);
            await _dbContext.SaveChangesAsync();

            return orderEntity.Id;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
