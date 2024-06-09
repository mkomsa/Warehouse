using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.Products.Models;
using Warehouse.Core.Products.Queries.GetProducts;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        GetProductsQuery query = new GetProductsQuery();
        IReadOnlyCollection<Product> result = await mediator.Send(query);

        return Ok(result);
    }
}
