using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.Addresses.Models;
using Warehouse.Core.Addresses.Queries.GetAddressById;
using Warehouse.Core.Addresses.Queries.GetAddresses;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("api/address")]
public class AddressController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAddresses()
    {
        GetAddressesQuery query = new GetAddressesQuery();
        IReadOnlyCollection<Address> result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddressById([FromRoute] Guid id)
    {
        GetAddressByIdQuery query = new GetAddressByIdQuery(id);
        Address result = await _mediator.Send(query);

        return Ok(result);
    }
}
