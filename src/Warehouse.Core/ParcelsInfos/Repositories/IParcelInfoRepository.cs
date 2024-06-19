using Warehouse.Core.ParcelsInfos.Models;

namespace Warehouse.Core.ParcelsInfos.Repositories;
public interface IParcelInfoRepository
{
    Task<IReadOnlyCollection<ParcelInfo>> GetAllAsync();
}
