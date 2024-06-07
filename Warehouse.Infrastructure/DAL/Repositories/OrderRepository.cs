using Microsoft.EntityFrameworkCore;
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

            //List<OrderEntity> ordersFromView = await _dbContext.Orders
            //    .ToListAsync();

            //IIncludableQueryable<OrderEntity, ParcelInfoEntity> baseQuery = _dbContext.Orders
            //    .Include(o => o.CustomerEntity)
            //        .ThenInclude(ce => ce.AddressEntity)
            //    .Include(o => o.AddressEntity)
            //    .Include(o => o.InvoiceEntity)
            //    .Include(o => o.OrderProducts)
            //        .ThenInclude(op => op.ProductEntity)
            //            .ThenInclude(p => p.ManufacturerEntity)
            //               .ThenInclude(m => m.AddressEntity)
            //   .Include(o => o.OrderProducts)
            //       .ThenInclude(op => op.ProductEntity)
            //           .ThenInclude(p => p.ParcelInfoEntity);

            //List<Order> orders = await baseQuery
            //    .Select(e => e.ToOrder())
            //.ToListAsync();

            //if (!orders.Any())
            //{
            //    return new List<Order>()
            //    {
            //        new Order()
            //    };
            //}

            //var orderViews = dbContext.OrderViews.ToList();

            //var entities = orderViews.Select(ov => new OrderEntity()
            //{
            //    AddressId = ov.AddressId,
            //    AddressEntity = new AddressEntity()
            //    {
            //        AddressId = ov.AddressId,
            //        Apartment = ov.AddressApartment,
            //        PostalCode = ov.AddressPostalCode,
            //        Street = ov.AddressStreet
            //    },
            //    CustomerId = ov.CustomerId,
            //    CustomerEntity = new CustomerEntity()
            //    {
            //        CustomerId = ov.CustomerId,
            //        AddressEntity = new AddressEntity()
            //        {
            //            AddressId = ov.AddressId,
            //            Apartment = ov.AddressApartment,
            //            PostalCode = ov.AddressPostalCode,
            //            Street = ov.AddressStreet
            //        },
            //        AddressId = ov.AddressId,
            //        Email = ov.CustomerEmail,
            //        PhoneNumber = ov.CustomerPhoneNumber,
            //        FullName = ov.CustomerFullName,
            //        Name = ov.CustomerName,
            //    },
            //    InvoiceId = ov.InvoiceId,
            //    InvoiceEntity = new InvoiceEntity()
            //    {
            //        InvoiceId = ov.InvoiceId,
            //        TransactionDate = ov.InvoiceTransactionDate,
            //        GrossValue = ov.InvoiceGrossValue,
            //        NetValue = ov.InvoiceNetValue,
            //        Status = ov.InvoiceStatus,
            //        VatRate = ov.InvoiceVatRate,
            //    },
            //    OrderId = ov.OrderId,
            //    OrderProducts = GetProducts(ov),

            //});

            // Fetch the raw data from the view
            var orderViews = dbContext.OrderViews.ToList();

            // Group by OrderId to handle duplicates
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
            var orderEntitiesFromView = groupedOrderViews.Select(group => new OrderEntity
            {
                OrderId = group.OrderView.OrderId,
                CustomerId = group.OrderView.CustomerId,
                AddressId = group.OrderView.AddressId,
                InvoiceId = group.OrderView.InvoiceId,
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

            //return orderEntities;
            var orders = orderEntitiesFromView.Select(o => o.ToOrder()).ToList();
            return orders;
        }
        catch (Exception ex)
        {
            throw new DbOperationException("GetAddresses failed.", ex);
        }

        List<OrderProductEntity> GetProducts(OrderView ov)
        {
            return new List<OrderProductEntity>()
            {

            };
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

            var sql = @"
            SELECT
                o.order_id AS ""OrderId"",
                o.customer_id AS ""CustomerId"",
                o.address_id AS ""OrderAddressId"",
                o.invoice_id AS ""InvoiceId"",
                ce.customer_id AS ""CustomerEntity_Id"",
                ce.address_id AS ""CustomerEntity_AddressEntityId"",
                ce.name AS ""CustomerEntity_Name"",
                ce.full_name AS ""CustomerEntity_FullName"",
                ce.email AS ""CustomerEntity_Email"",
                ce.phone_number AS ""CustomerEntity_PhoneNumber"",
                ca.address_id AS ""CustomerAddressEntity_Id"",
                ca.postal_code AS ""CustomerAddressEntity_PostalCode"",
                ca.street AS ""CustomerAddressEntity_Street"",
                ca.apartment AS ""CustomerAddressEntity_Apartment"",
                ia.address_id AS ""OrderAddressEntity_Id"",
                ia.postal_code AS ""OrderAddressEntity_PostalCode"",
                ia.street AS ""OrderAddressEntity_Street"",
                ia.apartment AS ""OrderAddressEntity_Apartment"",
                ie.invoice_id AS ""InvoiceEntity_Id"",
                ie.transaction_date AS ""InvoiceEntity_TransactionDate"",
                ie.net_value AS ""InvoiceEntity_NetValue"",
                ie.gross_value AS ""InvoiceEntity_GrossValue"",
                ie.status AS ""InvoiceEntity_Status"",
                ie.vat_rate AS ""InvoiceEntity_VatRate"",
                op.order_product_id AS ""OrderProduct_Id"",
                op.order_id AS ""OrderProduct_OrderEntityId"",
                op.product_id AS ""OrderProduct_ProductEntityId"",
                p.product_id AS ""ProductEntity_Id"",
                p.manufacturer_id AS ""ProductEntity_ManufacturerEntityId"",
                p.parcel_info_id AS ""ProductEntity_ParcelInfoEntityId"",
                p.available_amount AS ""ProductEntity_AvailableAmount"",
                p.price AS ""ProductEntity_Price"",
                ma.manufacturer_id AS ""ManufacturerEntity_Id"",
                ma.address_id AS ""ManufacturerEntity_AddressEntityId"",
                ma.name AS ""ManufacturerEntity_Name"",
                ma.email AS ""ManufacturerEntity_Email"",
                ma.phone_number AS ""ManufacturerEntity_PhoneNumber"",
                ma_address.address_id AS ""ManufacturerAddressEntity_Id"",
                ma_address.postal_code AS ""ManufacturerAddressEntity_PostalCode"",
                ma_address.street AS ""ManufacturerAddressEntity_Street"",
                ma_address.apartment AS ""ManufacturerAddressEntity_Apartment"",
                pa.parcel_info_id AS ""ParcelInfoEntity_Id"",
                pa.weight AS ""ParcelInfoEntity_Weight"",
                pa.height AS ""ParcelInfoEntity_Height"",
                pa.length AS ""ParcelInfoEntity_Length"",
                pa.width AS ""ParcelInfoEntity_Width""
                FROM
                    ""public"".""order"" AS o
                LEFT JOIN
                    ""public"".""customer"" AS ce ON o.customer_id = ce.customer_id
                LEFT JOIN
                    ""public"".""address"" AS ca ON ce.address_id = ca.address_id
                LEFT JOIN
                    ""public"".""address"" AS ia ON o.address_id = ia.address_id
                LEFT JOIN
                    ""public"".""invoice"" AS ie ON o.invoice_id = ie.invoice_id
                LEFT JOIN
                    ""public"".""order_product"" AS op ON o.order_id = op.order_id
                LEFT JOIN
                    ""public"".""product"" AS p ON op.product_id = p.product_id
                LEFT JOIN
                    ""public"".""manufacturer"" AS ma ON p.manufacturer_id = ma.manufacturer_id
                LEFT JOIN
                    ""public"".""address"" AS ma_address ON ma.address_id = ma_address.address_id
                LEFT JOIN
                    ""public"".""parcel_info"" AS pa ON p.parcel_info_id = pa.parcel_info_id
                WHERE
                    o.order_id = @orderId";

            var xd = _dbContext.Database.ExecuteSqlRawAsync(sql,
                new Npgsql.NpgsqlParameter("@OrderId", id));

            //var haha = _dbContext.Database.SqlQueryRaw<OrderEntity>(sql, new Npgsql.NpgsqlParameter("@OrderId", id)).ToList();

            var haha = _dbContext.Orders.FromSqlRaw(sql, new Npgsql.NpgsqlParameter("@OrderId", id)).ToList();

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
    INSERT INTO public.""address"" (""address_id"", ""postal_code"", ""street"", ""apartment"")
    VALUES (@Id, @PostalCode, @Street, @Apartment);";

                await _dbContext.Database.ExecuteSqlRawAsync(sqlAddress,
                    new Npgsql.NpgsqlParameter("@Id", order.Address.Id),
                    new Npgsql.NpgsqlParameter("@PostalCode", order.Address.PostalCode),
                    new Npgsql.NpgsqlParameter("@Street", order.Address.Street),
                    new Npgsql.NpgsqlParameter("@Apartment", order.Address.Apartment)
                );

                // Inserting InvoiceEntity
                string sqlInvoice = @"
    INSERT INTO public.""invoice"" (""invoice_id"", ""transaction_date"", ""net_value"", ""gross_value"", ""status"", ""vat_rate"")
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
