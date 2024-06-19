using MediatR;
using Warehouse.Core.Orders.Models;
using Warehouse.Core.Orders.Repositories;

namespace Warehouse.Core.Orders.Queries.GetOrders;

public class GetOrdersByCustomerQuery() : IRequest<IReadOnlyCollection<CustomerOrders>>
{
}

internal class GetOrdersByCustomerQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrdersByCustomerQuery, IReadOnlyCollection<CustomerOrders>>
{
    public async Task<IReadOnlyCollection<CustomerOrders>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        return await orderRepository.GetCustomerOrdersAsync();
    }
}
