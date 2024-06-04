using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.Orders.Models;
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
    public async Task<IActionResult> GetOrders([FromRoute] Guid id)
    {
        GetOrdersQuery query = new GetOrdersQuery();

        IReadOnlyCollection<Order> result = await _mediator.Send(query);

        return Ok(result);
    }
}
