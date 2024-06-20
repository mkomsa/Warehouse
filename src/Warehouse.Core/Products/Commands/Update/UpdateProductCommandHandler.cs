using MediatR;
using Warehouse.Core.Manufacturers.Models;
using Warehouse.Core.ParcelsInfos.Models;
using Warehouse.Core.Products.Dtos;
using Warehouse.Core.Products.Models;
using Warehouse.Core.Products.Repositories;

namespace Warehouse.Core.Products.Commands.Update;

public class UpdateProductCommand() : IRequest
{
    public Guid ProductId { get; set; }
    public ProductDto Product { get; set; }
}

internal class UpdateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        Product productToUpdate = new Product()
        {
            Id = command.ProductId,
            Name = command.Product.Name,
            Manufacturer = new Manufacturer()
            {
                Id = command.Product.ManufacturerId,
            },
            ParcelInfo = new ParcelInfo()
            {
                Id = command.Product.ParcelInfoId
            },
            Price = command.Product.Price,
            AvailableAmount = command.Product.AvailableAmount
        };

        await productRepository.UpdateProductAsync(productToUpdate);
    }
}
