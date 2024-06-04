using MediatR;
using Warehouse.Core.Addresses.Models;
using Warehouse.Core.Addresses.Repositories;

namespace Warehouse.Core.Addresses.Queries.GetAddresses;

public class GetAddressesQuery() : IRequest<IReadOnlyCollection<Address>>
{
}

internal class GetAddressesQueryHandler(IAddressRepository addressRepository) : IRequestHandler<GetAddressesQuery, IReadOnlyCollection<Address>>
{
    private readonly IAddressRepository _addressRepository = addressRepository;

    public async Task<IReadOnlyCollection<Address>> Handle(GetAddressesQuery query, CancellationToken cancellationToken)
    {
        return await _addressRepository.GetAddressesAsync();
    }
}
