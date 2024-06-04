using MediatR;
using Warehouse.Core.Addresses.Models;
using Warehouse.Core.Addresses.Repositories;

namespace Warehouse.Core.Addresses.Queries.GetAddressById;

public record GetAddressByIdQuery(Guid Id) : IRequest<Address>;

internal class GetAddressByIdQueryHandler(IAddressRepository addressRepository) : IRequestHandler<GetAddressByIdQuery, Address>
{
    private readonly IAddressRepository _addressRepository = addressRepository;

    public async Task<Address> Handle(GetAddressByIdQuery query, CancellationToken cancellationToken)
    {
        return await _addressRepository.GetAddressById(query.Id);
    }
}
