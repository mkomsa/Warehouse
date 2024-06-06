using MediatR;
using Warehouse.Core.Orders.Models;
using Warehouse.Core.Orders.Repositories;

namespace Warehouse.Core.Orders.Commands.Create;

public class CreateOrderCommand() : IRequest<Guid>
{
    public Order Order { get; set; }
}

internal class CreateOrderCommandHandler(IOrderRepository orderRepository) : IRequestHandler<CreateOrderCommand, Guid>
{
    public async Task<Guid> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        return await orderRepository.CreateOrderAsync(command.Order);

    }
}
