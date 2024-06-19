using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.Manufacturers.Models;
using Warehouse.Core.ParcelsInfos.Models;
using Warehouse.Core.Products.Commands.Create;
using Warehouse.Core.Products.Dtos;
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

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
    {
        CreateProductCommand command = new CreateProductCommand()
        {
            Product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Manufacturer = new Manufacturer()
                {
                    Id = product.ManufacturerId,
                },
                ParcelInfo = new ParcelInfo()
                {
                    Id = product.ParcelInfoId
                },
                Price = product.Price,
            }
        };

        Guid result = await mediator.Send(command);

        return Ok(result);
    }
}
