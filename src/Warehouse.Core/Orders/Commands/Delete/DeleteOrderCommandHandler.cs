using MediatR;
using Warehouse.Core.Orders.Repositories;

namespace Warehouse.Core.Orders.Commands.Delete;

public class DeleteOrderCommand() : IRequest
{
    public Guid Id { get; set; }
}

internal class DeleteOrderCommandHandler(IOrderRepository orderRepository) : IRequestHandler<DeleteOrderCommand>
{
    public async Task Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        await orderRepository.DeleteOrderAsync(command.Id);
    }
}
