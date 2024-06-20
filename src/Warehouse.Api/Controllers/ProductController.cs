using MediatR;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Core.Manufacturers.Models;
using Warehouse.Core.ParcelsInfos.Models;
using Warehouse.Core.Products.Commands.Create;
using Warehouse.Core.Products.Commands.Delete;
using Warehouse.Core.Products.Commands.Update;
using Warehouse.Core.Products.Dtos;
using Warehouse.Core.Products.Models;
using Warehouse.Core.Products.Queries.GetProduct;
using Warehouse.Core.Products.Queries.GetProducts;

namespace Warehouse.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        GetProductsQuery query = new GetProductsQuery();
        IReadOnlyCollection<Product> result = await mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById([FromRoute] Guid id)
    {
        GetProductByIdQuery query = new GetProductByIdQuery()
        {
            ProductId = id
        };

        Product result = await mediator.Send(query);

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


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct([FromRoute] Guid id, [FromBody] ProductDto dto)
    {
        UpdateProductCommand command = new UpdateProductCommand()
        {
            ProductId = id,
            Product = dto
        };

        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
    {
        DeleteProductCommand command = new DeleteProductCommand()
        {
            ProductId = id
        };

        await mediator.Send(command);
        return NoContent();
    }
}
