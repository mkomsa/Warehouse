using MediatR;
using Warehouse.Core.Products.Models;
using Warehouse.Core.Products.Repositories;

namespace Warehouse.Core.Products.Queries.GetProducts;

public record GetProductsQuery() : IRequest<IReadOnlyCollection<Product>>;

public class GetProductsQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductsQuery, IReadOnlyCollection<Product>>
{
    public async Task<IReadOnlyCollection<Product>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        return await productRepository.GetProductsAsync();
    }
}
