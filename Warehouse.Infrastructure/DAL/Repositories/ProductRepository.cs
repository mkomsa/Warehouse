using Microsoft.EntityFrameworkCore;
using Warehouse.Core.Products.Models;
using Warehouse.Core.Products.Repositories;

namespace Warehouse.Infrastructure.DAL.Repositories;

internal class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<IReadOnlyCollection<Product>> GetProductsAsync()
    {
        return await dbContext.Products
            .Include(p => p.ParcelInfoEntity)
            .Include(p => p.ManufacturerEntity)
                .ThenInclude(m => m.AddressEntity)
            .Select(p => p.ToProduct())
            .ToListAsync();
    }
}
