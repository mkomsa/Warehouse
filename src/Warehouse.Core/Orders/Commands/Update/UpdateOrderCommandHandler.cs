using MediatR;
using Warehouse.Core.Orders.Repositories;

namespace Warehouse.Core.Orders.Commands.Update;

public class UpdateOrderCommand() : IRequest<Guid>
{

}

internal class UpdateOrderCommandHandler(IOrderRepository orderRepository) : IRequestHandler<UpdateOrderCommand, Guid>
{
    public async Task<Guid> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        return await orderRepository.UpdateOrderAsync(new());
    }
}
