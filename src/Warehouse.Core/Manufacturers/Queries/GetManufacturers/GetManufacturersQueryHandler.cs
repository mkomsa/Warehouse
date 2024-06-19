using MediatR;
using Warehouse.Core.Manufacturers.Models;
using Warehouse.Core.Manufacturers.Repositories;

namespace Warehouse.Core.Manufacturers.Queries.GetManufacturers;

public class GetManufacturersQuery() : IRequest<IReadOnlyCollection<Manufacturer>>
{

}

internal class GetManufacturersQueryHandler(IManufacturerRepository manufacturerRepository) : IRequestHandler<GetManufacturersQuery, IReadOnlyCollection<Manufacturer>>
{
    public async Task<IReadOnlyCollection<Manufacturer>> Handle(GetManufacturersQuery query, CancellationToken cancellationToken)
    {
        return await manufacturerRepository.GetManufacturersAsync();
    }
}
