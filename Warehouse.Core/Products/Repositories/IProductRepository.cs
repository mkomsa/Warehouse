using Warehouse.Core.Products.Models;

namespace Warehouse.Core.Products.Repositories;

public interface IProductRepository
{
    Task<IReadOnlyCollection<Product>> GetProductsAsync();
}
