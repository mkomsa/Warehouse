using Warehouse.Core.ParcelsInfos.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class ParcelInfoEntity
{
    public Guid Id { get; set; }
    public IReadOnlyCollection<ProductEntity> Products { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }

    public ParcelInfo ToParcelInfo()
    {
        return new ParcelInfo()
        {
            Id = Id,
            Products = Products
                .Select(p => p.ToProduct())
                .ToList(),
            Length = Length,
            Width = Width,
            Height = Height,
            Weight = Weight
        };
    }
}
