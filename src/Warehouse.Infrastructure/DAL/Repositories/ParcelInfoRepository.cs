using Microsoft.EntityFrameworkCore;
using Warehouse.Core.ParcelsInfos.Models;
using Warehouse.Core.ParcelsInfos.Repositories;

namespace Warehouse.Infrastructure.DAL.Repositories;

internal class ParcelInfoRepository(AppDbContext dbContext) : IParcelInfoRepository
{
    public async Task<IReadOnlyCollection<ParcelInfo>> GetAllAsync()
    {
        return await dbContext.ParcelInfoViews
            .Select(p => new ParcelInfo
            {
                Id = p.ParcelInfoId,
                Length = p.Length,
                Width = p.Width,
                Height = p.Height,
                Weight = p.Weight
            })
            .ToListAsync();
    }
}
