using MediatR;
using Warehouse.Core.Orders.Models;
using Warehouse.Core.Orders.Repositories;

namespace Warehouse.Core.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(Guid Id) : IRequest<Order>;

public class GetOrderByIdQueryHandler(IOrderRepository orderRepository) : IRequestHandler<GetOrderByIdQuery, Order>
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<Order> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetOrderByIdAsync(query.Id);
    }
}
