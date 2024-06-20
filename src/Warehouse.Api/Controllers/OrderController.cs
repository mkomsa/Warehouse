using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.Orders.Commands.Create;
using Warehouse.Core.Orders.Commands.Delete;
using Warehouse.Core.Orders.Commands.Update;
using Warehouse.Core.Orders.Models;
using Warehouse.Core.Orders.Queries.GetOrderById;
using Warehouse.Core.Orders.Queries.GetOrders;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        GetOrdersQuery query = new GetOrdersQuery();

        IReadOnlyCollection<Order> result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("by-customer")]
    public async Task<IActionResult> GetOrdersByCustomer()
    {
        GetOrdersByCustomerQuery query = new GetOrdersByCustomerQuery();

        IReadOnlyCollection<CustomerOrders> result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid id)
    {
        GetOrderByIdQuery query = new GetOrderByIdQuery(id);

        Order result = await _mediator.Send(query);

        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Order order)
    {
        CreateOrderCommand command = new CreateOrderCommand()
        {
            Order = order
        };

        Guid result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPatch("{id}/change-status")]
    public async Task<IActionResult> ChangeStatus([FromRoute] Guid id, [FromBody] string updateStatus)
    {
        UpdateOrderStatusCommand command = new UpdateOrderStatusCommand()
        {
            Id = id,
            Status = updateStatus
        };

        Guid result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder([FromRoute] Guid id)
    {
        DeleteOrderCommand command = new DeleteOrderCommand()
        {
            Id = id
        };

        await mediator.Send(command);

        return NoContent();
    }
}
