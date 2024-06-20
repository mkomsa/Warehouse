using Warehouse.Core.Products.Models;

namespace Warehouse.Core.Products.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyCollection<Product>> GetProductsAsync();
    Product GetProductByIdAsync(Guid productId);
    Task<Guid> AddProductAsync(Product product);
    Task DeleteProductAsync(Guid productId);
    Task UpdateProductAsync(Product product);
}
