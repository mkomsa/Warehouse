using MediatR;
using Warehouse.Core.Orders.Models;
using Warehouse.Core.Orders.Repositories;

namespace Warehouse.Core.Orders.Queries.GetOrders;

public class GetOrdersQuery() : IRequest<IReadOnlyCollection<Order>>
{
}

public class GetOrdersQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrdersQuery, IReadOnlyCollection<Order>>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<IReadOnlyCollection<Order>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetOrdersAsync();
    }
}
