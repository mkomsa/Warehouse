using Warehouse.Core.Products.Models;

namespace Warehouse.Core.ParcelsInfos.Models;

public class ParcelInfo
{
    public Guid Id { get; set; }
    public IReadOnlyCollection<Product> Products { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
}
