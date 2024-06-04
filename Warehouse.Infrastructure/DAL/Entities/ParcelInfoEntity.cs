using Warehouse.Core.ParcelsInfos.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class ParcelInfoEntity
{
    public Guid Id { get; set; }
    public IReadOnlyCollection<ProductEntity> ProductEntities { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }

    public ParcelInfo ToParcelInfo()
    {
        return new ParcelInfo()
        {
            Id = Id,
            Products = ProductEntities
                .Select(p => p.ToProduct())
                .ToList(),
            Length = Length,
            Width = Width,
            Height = Height,
            Weight = Weight
        };
    }
}
