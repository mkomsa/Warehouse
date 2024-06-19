using MediatR;
using Warehouse.Core.Products.Models;
using Warehouse.Core.Products.Repositories;

namespace Warehouse.Core.Products.Commands.Create;

public class CreateProductCommand() : IRequest<Guid>
{
    public Product Product { get; set; }
}

internal class CreateProductCommandHandler(IProductRepository productRepository) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        return await productRepository.AddProductAsync(command.Product);
    }
}
