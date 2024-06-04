using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Warehouse.Core.Addresses.Repositories;
using Warehouse.Infrastructure.DAL;
using Warehouse.Infrastructure.DAL.Repositories;

namespace Warehouse.Infrastructure;

public static class ServiceExtension
{
    public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        services.AddScoped<IAddressRepository, AddressRepository>();
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            NpgsqlDataSource dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString)
                .EnableDynamicJson()
                .Build();

            services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(dataSourceBuilder)
            );
        }
    }
}
