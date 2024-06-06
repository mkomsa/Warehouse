using System.ComponentModel.DataAnnotations;
using Warehouse.Core.ParcelsInfos.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class ParcelInfoEntity
{
    [Key]
    public Guid ParcelInfoId { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }

    public ParcelInfo ToParcelInfo()
    {
        return new ParcelInfo()
        {
            Id = ParcelInfoId,
            Length = Length,
            Width = Width,
            Height = Height,
            Weight = Weight
        };
    }
}
