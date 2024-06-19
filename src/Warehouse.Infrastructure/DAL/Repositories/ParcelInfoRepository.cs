using Warehouse.Core.ParcelsInfos.Models;
using Warehouse.Core.ParcelsInfos.Repositories;

namespace Warehouse.Infrastructure.DAL.Repositories;

internal class ParcelInfoRepository(AppDbContext dbContext) : IParcelInfoRepository
{
    public async Task<IReadOnlyCollection<ParcelInfo>> GetAllAsync()
    {
        return dbContext.ParcelsInfo.Select(p => p.ToParcelInfo()).ToList();
    }
}
