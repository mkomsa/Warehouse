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
        Guid addressId = Guid.NewGuid();
        Guid customerId = Guid.NewGuid();

        command.Order.Address.Id = addressId;
        command.Order.Customer.AddressId = addressId;
        command.Order.Customer.Id = customerId;
        command.Order.Invoice.Id = Guid.NewGuid();

        return await orderRepository.CreateOrderAsync(command.Order);

    }
}
