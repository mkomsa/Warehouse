using MediatR;
using Warehouse.Core.Orders.Repositories;

namespace Warehouse.Core.Orders.Commands.Update;

public class UpdateOrderStatusCommand() : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Status { get; set; } = String.Empty;
}

internal class UpdateOrderCommandHandler(IOrderRepository orderRepository) : IRequestHandler<UpdateOrderStatusCommand, Guid>
{
    public async Task<Guid> Handle(UpdateOrderStatusCommand command, CancellationToken cancellationToken)
    {
        return await orderRepository.UpdateOrderStatusAsync(command.Id, command.Status);
    }
}
