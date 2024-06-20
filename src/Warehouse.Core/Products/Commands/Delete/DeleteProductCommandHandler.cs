using MediatR;
using Warehouse.Core.Products.Repositories;

namespace Warehouse.Core.Products.Commands.Delete;

public class DeleteProductCommand() : IRequest
{
    public Guid ProductId { get; set; }
}

internal class DeleteProductCommandHandler(IProductRepository productRepository) : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        await productRepository.DeleteProductAsync(command.ProductId);
    }
}
