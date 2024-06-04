using Microsoft.Extensions.DependencyInjection;

namespace Warehouse.Core;

public static class ServiceExtension
{
    public static void RegisterCoreServices(this IServiceCollection services)
    {
        services.AddMediatR((config) =>
            config.RegisterServicesFromAssembly(typeof(ServiceExtension).Assembly)
            );
    }
}
