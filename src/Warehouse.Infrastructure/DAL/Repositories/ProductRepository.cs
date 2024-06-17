using Warehouse.Core.Products.Models;
using Warehouse.Core.Products.Repositories;
using Warehouse.Infrastructure.DAL.Entities;
using Warehouse.Infrastructure.DAL.Exceptions;
using Warehouse.Infrastructure.DAL.Views;

namespace Warehouse.Infrastructure.DAL.Repositories;

internal class ProductRepository(AppDbContext dbContext) : IProductRepository
{
    public async Task<IReadOnlyCollection<Product>> GetProductsAsync()
    {
        //return await dbContext.Products
        //    .Include(p => p.ParcelInfoEntity)
        //    .Include(p => p.ManufacturerEntity)
        //        .ThenInclude(m => m.AddressEntity)
        //    .Select(p => p.ToProduct())
        //    .ToListAsync();
        try
        {
            List<ProductView> products = dbContext.ProductViews.ToList();

            return MapViews(products)
                .Select(e => e.ToProduct())
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new DbOperationException("Get products failed.", ex);
        }
    }

    private IEnumerable<ProductEntity> MapViews(List<ProductView> productViews)
    {
        IEnumerable<ProductEntity> productEntities = productViews.Select(view => new ProductEntity()
        {
            ProductId = view.ProductId,
            Price = view.Price,
            AvailableAmount = view.AvailableAmount,
            ParcelInfoEntity = new()
            {
                ParcelInfoId = view.ParcelInfoId,
                Height = view.Height,
                Length = view.Length,
                Width = view.Width,
                Weight = view.Weight,
            },
            ManufacturerEntity = new()
            {
                ManufacturerId = view.ManufacturerId,
                Name = view.ManufacturerName,
                Email = view.ManufacturerEmail,
                PhoneNumber = view.ManufacturerPhone,
                AddressEntity = new AddressEntity()
                {
                    AddressId = view.ManufacturerAddressId,
                    Apartment = view.ManufacturerApartment,
                    PostalCode = view.ManufacturerPostalCode,
                    Street = view.ManufacturerStreet
                }
            }
        });
        return productEntities;
    }
}
