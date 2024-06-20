using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
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

    public Product GetProductByIdAsync(Guid productId)
    {
        try
        {
            List<ProductView> products = dbContext.ProductViews.ToList();

            IEnumerable<Product> mappedProducts = MapViews(products)
                .Select(e => e.ToProduct())
                .ToList();

            return mappedProducts.FirstOrDefault(p => p.Id == productId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new DbOperationException("Get products failed.", ex);
        }
    }

    public async Task<Guid> AddProductAsync(Product product)
    {
        try
        {
            await dbContext.Database.ExecuteSqlInterpolatedAsync(
                $@"
                CALL add_product(
                    {product.Id},
                    {product.Name},
                    {product.ParcelInfo.Id},
                    {product.Manufacturer.Id},
                    {product.Price}
                );"
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new DbOperationException("Create product failed.", ex);
        }

        return product.Id;
    }

    public async Task UpdateProductAsync(Product product)
    {
        try
        {
            await dbContext.Database.ExecuteSqlInterpolatedAsync(
                            $@"
                CALL update_product(
                    {product.Id},
                    {product.Name},
                    {product.ParcelInfo.Id},
                    {product.Manufacturer.Id},
                    {product.Price},
                    {product.AvailableAmount}
                );"
                        );
        }
        catch (Exception ex)
        {
            throw new DbOperationException("Update product failed.", ex);
        }
    }

    public async Task DeleteProductAsync(Guid productId)
    {
        try
        {
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                string sqlDeleteOrder = @"
                DELETE FROM public.""product""
                WHERE ""product_id"" = @ProductId;";

                await dbContext.Database.ExecuteSqlRawAsync(
                    sqlDeleteOrder,
                    new NpgsqlParameter("@ProductId", productId)
                );

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new DbOperationException("Delete product failed.", ex);
            }
        }
        catch (Exception ex)
        {
            throw new DbOperationException("DeleteProductAsync failed.", ex);
        }
    }

    private IEnumerable<ProductEntity> MapViews(List<ProductView> productViews)
    {
        IEnumerable<ProductEntity> productEntities = productViews.Select(
            view => new ProductEntity
            {
                ProductId = view.ProductId,
                Name = view.Name,
                Price = view.Price,
                AvailableAmount = view.AvailableAmount,
                ParcelInfoEntity = new ParcelInfoEntity
                {
                    ParcelInfoId = view.ParcelInfoId,
                    Height = view.Height,
                    Length = view.Length,
                    Width = view.Width,
                    Weight = view.Weight
                },
                ManufacturerEntity = new ManufacturerEntity
                {
                    ManufacturerId = view.ManufacturerId,
                    Name = view.ManufacturerName,
                    Email = view.ManufacturerEmail,
                    PhoneNumber = view.ManufacturerPhone,
                    AddressEntity = new AddressEntity
                    {
                        AddressId = view.ManufacturerAddressId,
                        Apartment = view.ManufacturerApartment,
                        PostalCode = view.ManufacturerPostalCode,
                        Street = view.ManufacturerStreet
                    }
                }
            }
        );
        return productEntities;
    }
}
