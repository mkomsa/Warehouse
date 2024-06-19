using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.Manufacturers.Models;
using Warehouse.Core.Manufacturers.Queries.GetManufacturers;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("api/manufacturer")]
public class ManufacturerController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        GetManufacturersQuery query = new();

        IReadOnlyCollection<Manufacturer> result = await mediator.Send(query);

        return Ok(result);
    }
}
