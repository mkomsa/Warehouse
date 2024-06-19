using MediatR;
using Warehouse.Core.ParcelsInfos.Models;
using Warehouse.Core.ParcelsInfos.Repositories;

namespace Warehouse.Core.ParcelsInfos.Queries.GetParcelInfos;

public class GetParcelInfosQuery() : IRequest<IReadOnlyCollection<ParcelInfo>> { }

internal class GetParcelInfosQueryHandler(IParcelInfoRepository parcelInfoRepository) : IRequestHandler<GetParcelInfosQuery, IReadOnlyCollection<ParcelInfo>>
{
    public async Task<IReadOnlyCollection<ParcelInfo>> Handle(GetParcelInfosQuery request, CancellationToken cancellationToken)
    {
        return await parcelInfoRepository.GetAllAsync();
    }
}
