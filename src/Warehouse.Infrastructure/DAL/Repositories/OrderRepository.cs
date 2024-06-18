﻿using Microsoft.EntityFrameworkCore;
using Warehouse.Core.Orders.Models;
using Warehouse.Core.Orders.Repositories;
using Warehouse.Infrastructure.DAL.Entities;
using Warehouse.Infrastructure.DAL.Exceptions;

namespace Warehouse.Infrastructure.DAL.Repositories;

internal class OrderRepository(AppDbContext dbContext) : IOrderRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<IReadOnlyCollection<Order>> GetOrdersAsync()
    {
        try
        {
            List<OrderView> orderViews = dbContext.OrderViews.ToList();
            IEnumerable<OrderEntity> orderEntitiesFromView = GroupAndMapViews(orderViews);

            List<Order> orders = orderEntitiesFromView.Select(o => o.ToOrder()).ToList();
            return orders;
        }
        catch (Exception ex)
        {
            throw new DbOperationException("GetOrders failed.", ex);
        }
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        try
        {
            Guid orderId = id;
            List<OrderView> orderViews = await _dbContext.OrderViews
                .FromSqlRaw("SELECT * FROM order_view WHERE \"order_id\" = @orderId",
                    new Npgsql.NpgsqlParameter("@orderId", orderId))
                .ToListAsync();

            var orderEntityFromView = GroupAndMapViews(orderViews)
                .Single()
                .ToOrder();

            if (orderEntityFromView == null)
            {
                return new Order();
            }

            return orderEntityFromView;
        }
        catch (Exception ex)
        {
            throw new DbOperationException("GetOrderById failed.", ex);
        }
    }

    public async Task<Guid> CreateOrderAsync(Order order)
    {
        try
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                // Inserting AddressEntity
                string sqlAddress = @"
                INSERT INTO public.""address"" (""address_id"", ""postal_code"", ""street"", ""apartment"")
                VALUES (@Id, @PostalCode, @Street, @Apartment);";

                await _dbContext.Database.ExecuteSqlRawAsync(sqlAddress,
                    new Npgsql.NpgsqlParameter("@Id", order.Address.Id),
                    new Npgsql.NpgsqlParameter("@PostalCode", order.Address.PostalCode),
                    new Npgsql.NpgsqlParameter("@Street", order.Address.Street),
                    new Npgsql.NpgsqlParameter("@Apartment", order.Address.Apartment)
                );

                // Inserting InvoiceEntity net_value,
                string sqlInvoice = @"
                INSERT INTO public.""invoice"" (""invoice_id"", ""transaction_date"", ""gross_value"", ""status"", ""vat_rate"")
                VALUES (@Id, @TransactionDate, @GrossValue, @Status, @VatRate);";

                await _dbContext.Database.ExecuteSqlRawAsync(sqlInvoice,
                    new Npgsql.NpgsqlParameter("@Id", order.Invoice.Id),
                    new Npgsql.NpgsqlParameter("@TransactionDate", order.Invoice.TransactionDate),
                    //new Npgsql.NpgsqlParameter("@NetValue", order.Invoice.NetValue),
                    new Npgsql.NpgsqlParameter("@GrossValue", order.Invoice.GrossValue),
                    new Npgsql.NpgsqlParameter("@Status", order.Invoice.Status),
                    new Npgsql.NpgsqlParameter("@VatRate", order.Invoice.VatRate)
                );

                // Inserting CustomerEntity
                string sqlCustomer = @"
                INSERT INTO public.""customer"" (""customer_id"", ""address_id"", ""name"", ""full_name"", ""email"", ""phone_number"")
                VALUES (@Id, @AddressEntityId, @Name, @FullName, @Email, @PhoneNumber);";

                await _dbContext.Database.ExecuteSqlRawAsync(sqlCustomer,
                    new Npgsql.NpgsqlParameter("@Id", order.Customer.Id),
                    new Npgsql.NpgsqlParameter("@AddressEntityId", order.Customer.AddressId),
                    new Npgsql.NpgsqlParameter("@Name", order.Customer.Name),
                    new Npgsql.NpgsqlParameter("@FullName", order.Customer.FullName),
                    new Npgsql.NpgsqlParameter("@Email", order.Customer.Email),
                    new Npgsql.NpgsqlParameter("@PhoneNumber", order.Customer.PhoneNumber)
                );

                // Inserting OrderEntity
                string sqlOrder = @"
                INSERT INTO ""public"".""order"" (""order_id"", ""customer_id"", ""address_id"", ""invoice_id"")
                VALUES (@Id, @CustomerEntityId, @AddressEntityId, @InvoiceEntityId);";

                await _dbContext.Database.ExecuteSqlRawAsync(sqlOrder,
                    new Npgsql.NpgsqlParameter("@Id", order.Id),
                    new Npgsql.NpgsqlParameter("@CustomerEntityId", order.Customer.Id),
                    new Npgsql.NpgsqlParameter("@AddressEntityId", order.Address.Id),
                    new Npgsql.NpgsqlParameter("@InvoiceEntityId", order.Invoice.Id)
                );

                // Inserting OrderProductEntity
                foreach (var product in order.Products)
                {
                    string sqlOrderProduct = @"
                    INSERT INTO ""public"".""order_product"" (""order_id"", ""product_id"", ""order_product_id"")
                    VALUES (@OrderId, @ProductId, @OrderProductId);";

                    await _dbContext.Database.ExecuteSqlRawAsync(sqlOrderProduct,
                        new Npgsql.NpgsqlParameter("@OrderId", order.Id),
                        new Npgsql.NpgsqlParameter("@ProductId", product.Id),
                        new Npgsql.NpgsqlParameter("@OrderProductId", Guid.NewGuid())
                    );
                }

                await transaction.CommitAsync();
                return order.Id;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw ex;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw new DbOperationException("CreateOrder failed.", ex);
        }
    }

    public async Task<Guid> UpdateOrderStatusAsync(Guid orderId, string status)
    {
        try
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                string sqlUpdateStatus = @"
                UPDATE public.""order""
                SET ""status"" = @Status
                WHERE ""order_id"" = @OrderId;";

                await _dbContext.Database.ExecuteSqlRawAsync(sqlUpdateStatus,
                    new Npgsql.NpgsqlParameter("@Status", status),
                    new Npgsql.NpgsqlParameter("@OrderId", orderId)
                );

                await transaction.CommitAsync();
                return orderId;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new DbOperationException("Update order status failed.", ex);
            }
        }
        catch (Exception ex)
        {
            throw new DbOperationException("UpdateOrderStatus failed.", ex);
        }
    }

    public async Task DeleteOrderAsync(Guid orderId)
    {
        try
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                string sqlDeleteOrder = @"
                DELETE FROM public.""order""
                WHERE ""order_id"" = @OrderId;";

                await _dbContext.Database.ExecuteSqlRawAsync(sqlDeleteOrder,
                    new Npgsql.NpgsqlParameter("@OrderId", orderId)
                );

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new DbOperationException("Delete order failed.", ex);
            }
        }
        catch (Exception ex)
        {
            throw new DbOperationException("DeleteOrderAsync failed.", ex);
        }
    }

    public IEnumerable<OrderEntity> GroupAndMapViews(List<OrderView> orderViews)
    {
        var groupedOrderViews = orderViews
                .GroupBy(ov => ov.OrderId)
                .Select(group => new
                {
                    OrderView = group.FirstOrDefault(),
                    OrderProducts = group.Select(ov => new OrderProductEntity
                    {
                        OrderProductId = ov.OrderProductId,
                        OrderId = ov.OrderProductOrderId,
                        ProductId = ov.OrderProductProductId,
                        ProductEntity = new ProductEntity
                        {
                            ProductId = ov.ProductId,
                            Name = ov.ProductName,
                            ManufacturerId = ov.ProductManufacturerId,
                            ParcelInfoId = ov.ProductParcelInfoId,
                            AvailableAmount = ov.ProductAvailableAmount,
                            Price = ov.ProductPrice,
                            ManufacturerEntity = new ManufacturerEntity
                            {
                                ManufacturerId = ov.ManufacturerId,
                                AddressId = ov.ManufacturerAddressId,
                                AddressEntity = new AddressEntity
                                {
                                    AddressId = ov.CustomerAddressId,
                                    Apartment = ov.AddressApartment,
                                    PostalCode = ov.AddressPostalCode,
                                    Street = ov.AddressStreet
                                },
                                Email = ov.CustomerEmail,
                                Name = ov.CustomerName,
                                PhoneNumber = ov.CustomerPhoneNumber
                            },
                            ParcelInfoEntity = new ParcelInfoEntity
                            {
                                ParcelInfoId = ov.ParcelInfoId,
                                Weight = ov.ParcelInfoWeight,
                                Height = ov.ParcelInfoHeight,
                                Length = ov.ParcelInfoLength
                            }
                        }
                    }).ToList()
                })
                .ToList();

        // Map grouped data to OrderEntity
        IEnumerable<OrderEntity> orderEntityFromView = groupedOrderViews.Select(group => new OrderEntity
        {
            OrderId = group.OrderView.OrderId,
            CustomerId = group.OrderView.CustomerId,
            AddressId = group.OrderView.AddressId,
            InvoiceId = group.OrderView.InvoiceId,
            Status = group.OrderView.Status,
            AddressEntity = new AddressEntity
            {
                AddressId = group.OrderView.AddressId,
                Apartment = group.OrderView.AddressApartment,
                PostalCode = group.OrderView.AddressPostalCode,
                Street = group.OrderView.AddressStreet
            },
            CustomerEntity = new CustomerEntity
            {
                CustomerId = group.OrderView.CustomerId,
                AddressId = group.OrderView.AddressId,
                Email = group.OrderView.CustomerEmail,
                PhoneNumber = group.OrderView.CustomerPhoneNumber,
                FullName = group.OrderView.CustomerFullName,
                Name = group.OrderView.CustomerName,
                AddressEntity = new AddressEntity
                {
                    AddressId = group.OrderView.CustomerAddressId,
                    Apartment = group.OrderView.AddressApartment,
                    PostalCode = group.OrderView.AddressPostalCode,
                    Street = group.OrderView.AddressStreet
                }
            },
            InvoiceEntity = new InvoiceEntity
            {
                InvoiceId = group.OrderView.InvoiceId,
                TransactionDate = group.OrderView.InvoiceTransactionDate,
                GrossValue = group.OrderView.InvoiceGrossValue,
                NetValue = group.OrderView.InvoiceNetValue,
                Status = group.OrderView.InvoiceStatus,
                VatRate = group.OrderView.InvoiceVatRate
            },
            OrderProducts = group.OrderProducts
        });

        return orderEntityFromView;
    }
}
