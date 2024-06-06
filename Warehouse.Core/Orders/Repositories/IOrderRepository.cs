using Warehouse.Core.Orders.Models;

namespace Warehouse.Core.Orders.Repositories;

public interface IOrderRepository
{
    Task<IReadOnlyCollection<Order>> GetOrdersAsync();
    Task<Order> GetOrderByIdAsync(Guid id);
    Task<Guid> CreateOrderAsync(Order order);
}
