using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.ParcelsInfos.Models;
using Warehouse.Core.ParcelsInfos.Queries.GetParcelInfos;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("api/parcel-info")]
public class ParcelInfoController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        GetParcelInfosQuery query = new GetParcelInfosQuery();

        IReadOnlyCollection<ParcelInfo> result = await mediator.Send(query);

        return Ok(result);
    }
}
