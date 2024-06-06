using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
            /// <summary>
            ///  just for testing, now view does the job
            /// //IIncludableQueryable<OrderEntity, ParcelInfoEntity> baseQuery = _dbContext.Orders
            ///    .Include(o => o.CustomerEntity)
            ///        .ThenInclude(ce => ce.AddressEntity)
            ///    .Include(o => o.AddressEntity)
            ///    .Include(o => o.InvoiceEntity)
            ///    .Include(o => o.OrderProducts)
            ///        .ThenInclude(op => op.ProductEntity)
            ///            .ThenInclude(p => p.ManufacturerEntity)
            ///                .ThenInclude(m => m.AddressEntity)
            ///    .Include(o => o.OrderProducts)
            ///        .ThenInclude(op => op.ProductEntity)
            ///            .ThenInclude(p => p.ParcelInfoEntity);
            /// 
            ///List<Order> orders = await baseQuery
            ///    .Select(e => e.ToOrder())
            ///    .ToListAsync();
            /// 
            ///var orders = dbContext.Orders
            ///    .FromSqlRaw(@"SELECT o.*, c.*, a.*, i.*
            ///       FROM public.""Orders"" o
            ///       INNER JOIN public.""Customers"" c ON o.CustomerEntityId = c.Id
            ///       INNER JOIN public.""Addresses"" a ON o.AddressEntityId = a.Id
            ///       INNER JOIN public.""Invoices"" i ON o.InvoiceEntityId = i.Id")
            ///    .ToList();
            ///var orders = dbContext.Orders
            ///    .FromSqlRaw(@"SELECT ""o"".*, ""c"".*, ""a"".*, ""i"".*
            ///       FROM public.""Orders"" AS o
            ///       INNER JOIN public.""Customers"" AS c ON o.""CustomerEntityId"" = c.""Id""
            ///       INNER JOIN public.""Addresses"" AS a ON o.""AddressEntityId"" = a.""Id""
            ///       INNER JOIN public.""Invoices"" AS i ON o.""InvoiceEntityId"" = i.""Id""")
            ///    .ToList();
            /// 
            ///var orders = dbContext.Orders
            ///    .FromSqlRaw(
            ///        @"SELECT o.""Id"" AS ""OrderId"", o.""CustomerEntityId"", o.""AddressEntityId"", o.""InvoiceEntityId"",
            ///             c.""Id"" AS ""CustomerId"", c.""AddressEntityId"" AS ""CustomerAddressId"", c.""Name"" AS ""CustomerName"", c.""FullName"" AS ""CustomerFullName"", c.""Email"" AS ""CustomerEmail"", c.""PhoneNumber"" AS ""CustomerPhoneNumber"",
            ///             a.""Id"" AS ""AddressId"", a.""PostalCode"", a.""Street"", a.""Apartment"",
            ///             i.""Id"" AS ""InvoiceId"", i.""TransactionDate"", i.""NetValue"", i.""GrossValue"", i.""Status"", i.""VatRate""
            ///       FROM public.""Orders"" AS o
            ///       INNER JOIN public.""Customers"" AS c ON o.""CustomerEntityId"" = c.""Id""
            ///       INNER JOIN public.""Addresses"" AS a ON o.""AddressEntityId"" = a.""Id""
            ///       INNER JOIN public.""Invoices"" AS i ON o.""InvoiceEntityId"" = i.""Id""");
            ///.ToList();
            /// 
            ///var orders = dbContext.Orders.FromSqlRaw(@"select *
            ///   FROM public.""Orders"" as o
            ///   INNER JOIN public.""Customers"" c ON o.""CustomerEntityId"" = c.""Id""
            ///   INNER JOIN public.""Addresses"" a ON o.""AddressEntityId"" = a.""Id""
            ///   INNER JOIN public.""Invoices"" i ON o.""InvoiceEntityId"" = i.""Id""").ToList();
            /// 
            ///var orders = dbContext.Orders
            ///    .FromSqlRaw(@"SELECT o.*, c.*, a.*, i.*
            ///       FROM public.""Orders"" AS o
            ///       INNER JOIN public.""Customers"" AS c ON o.""CustomerEntityId"" = c.""Id""
            ///       INNER JOIN public.""Addresses"" AS a ON o.""AddressEntityId"" = a.""Id""
            ///       INNER JOIN public.""Invoices"" AS i ON o.""InvoiceEntityId"" = i.""Id""")
            ///    .Include(o => o.OrderProducts) // Include any related entities if needed
            ///    .ToList();
            /// </summary>

            List<OrderEntity> ordersFromView = await _dbContext.Orders
                .ToListAsync();

            IIncludableQueryable<OrderEntity, ParcelInfoEntity> baseQuery = _dbContext.Orders
                .Include(o => o.CustomerEntity)
                    .ThenInclude(ce => ce.AddressEntity)
                .Include(o => o.AddressEntity)
                .Include(o => o.InvoiceEntity)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.ProductEntity)
                        .ThenInclude(p => p.ManufacturerEntity)
                           .ThenInclude(m => m.AddressEntity)
               .Include(o => o.OrderProducts)
                   .ThenInclude(op => op.ProductEntity)
                       .ThenInclude(p => p.ParcelInfoEntity);

            List<Order> orders = await baseQuery
                .Select(e => e.ToOrder())
            .ToListAsync();

            if (!orders.Any())
            {
                return new List<Order>()
                {
                    new Order()
                };
            }

            return orders;
        }
        catch (Exception ex)
        {
            throw new DbOperationException("GetAddresses failed.", ex);
        }
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        try
        {
            /// <summary>
            ///OrderEntity? orderEntity = await _dbContext.Orders
            ///    .Include(o => o.CustomerEntity)
            ///        .ThenInclude(ce => ce.AddressEntity)
            ///    .Include(o => o.AddressEntity)
            ///    .Include(o => o.InvoiceEntity)
            ///    .Include(o => o.OrderProducts)
            ///        .ThenInclude(op => op.ProductEntity)
            ///            .ThenInclude(p => p.ManufacturerEntity)
            ///            .ThenInclude(m => m.AddressEntity)
            ///   .Include(o => o.OrderProducts)
            ///        .ThenInclude(op => op.ProductEntity)
            ///            .ThenInclude(p => p.ParcelInfoEntity)
            ///    .FirstOrDefaultAsync(o => o.OrderId == id);
            /// <summary>

            var orderEntity = await _dbContext.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

            string sqlQuery = @"
                SELECT * 
                FROM public.""OrderDetailView""
                WHERE ""OrderId"" = @OrderId;
                ";

            var xd = _dbContext.Database.ExecuteSqlRawAsync(sqlQuery,
                new Npgsql.NpgsqlParameter("@OrderId", id));

            if (orderEntity == null)
            {
                return new Order();
            }

            return orderEntity.ToOrder();
        }
        catch (Exception ex)
        {
            throw new DbOperationException("GetAddresses failed.", ex);
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
                INSERT INTO public.""Addresses"" (""Id"", ""PostalCode"", ""Street"", ""Apartment"")
                VALUES (@Id, @PostalCode, @Street, @Apartment);";

                await _dbContext.Database.ExecuteSqlRawAsync(sqlAddress,
                    new Npgsql.NpgsqlParameter("@Id", order.Address.Id),
                    new Npgsql.NpgsqlParameter("@PostalCode", order.Address.PostalCode),
                    new Npgsql.NpgsqlParameter("@Street", order.Address.Street),
                    new Npgsql.NpgsqlParameter("@Apartment", order.Address.Apartment)
                );

                // Inserting InvoiceEntity
                string sqlInvoice = @"
                INSERT INTO public.""Invoices"" (""Id"", ""TransactionDate"", ""NetValue"", ""GrossValue"", ""Status"", ""VatRate"")
                VALUES (@Id, @TransactionDate, @NetValue, @GrossValue, @Status, @VatRate);";

                await _dbContext.Database.ExecuteSqlRawAsync(sqlInvoice,
                    new Npgsql.NpgsqlParameter("@Id", order.Invoice.Id),
                    new Npgsql.NpgsqlParameter("@TransactionDate", order.Invoice.TransactionDate),
                    new Npgsql.NpgsqlParameter("@NetValue", order.Invoice.NetValue),
                    new Npgsql.NpgsqlParameter("@GrossValue", order.Invoice.GrossValue),
                    new Npgsql.NpgsqlParameter("@Status", order.Invoice.Status),
                    new Npgsql.NpgsqlParameter("@VatRate", order.Invoice.VatRate)
                );

                // Inserting CustomerEntity
                string sqlCustomer = @"
                INSERT INTO public.""Customers"" (""Id"", ""AddressEntityId"", ""Name"", ""FullName"", ""Email"", ""PhoneNumber"")
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
                INSERT INTO ""public"".""Orders"" (""Id"", ""CustomerEntityId"", ""AddressEntityId"", ""InvoiceEntityId"")
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
                    INSERT INTO ""public"".""OrdersProducts"" (""Id"", ""OrderEntityId"", ""ProductEntityId"")
                    VALUES (@Id, @OrderEntityId, @ProductEntityId);";

                    await _dbContext.Database.ExecuteSqlRawAsync(sqlOrderProduct,
                        new Npgsql.NpgsqlParameter("@Id", Guid.NewGuid()),
                        new Npgsql.NpgsqlParameter("@OrderEntityId", order.Id),
                        new Npgsql.NpgsqlParameter("@ProductEntityId", product.Id)
                    );
                }

                await transaction.CommitAsync();
                return order.Id;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
