using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.Orders.Commands.Create;
using Warehouse.Core.Orders.Models;
using Warehouse.Core.Orders.Queries.GetOrderById;
using Warehouse.Core.Orders.Queries.GetOrders;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("api/order")]
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
}
