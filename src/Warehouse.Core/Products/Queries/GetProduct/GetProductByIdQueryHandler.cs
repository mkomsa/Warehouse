using MediatR;
using Warehouse.Core.Products.Models;
using Warehouse.Core.Products.Repositories;

namespace Warehouse.Core.Products.Queries.GetProduct;

public class GetProductByIdQuery() : IRequest<Product>
{
    public Guid ProductId { get; set; }
}

internal class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, Product>
{
    public async Task<Product> Handle(GetProductByIdQuery command, CancellationToken cancellationToken)
    {
        return productRepository.GetProductByIdAsync(command.ProductId);
    }
}
