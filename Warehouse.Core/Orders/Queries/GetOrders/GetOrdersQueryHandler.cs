using MediatR;
using Warehouse.Core.Orders.Models;

namespace Warehouse.Core.Orders.Queries.GetOrders;

public class GetOrdersQuery() : IRequest<IReadOnlyCollection<Order>>
{
}

public class GetOrdersQueryHandler
{
}
