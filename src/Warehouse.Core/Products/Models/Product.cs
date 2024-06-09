using Warehouse.Core.Manufacturers.Models;
using Warehouse.Core.ParcelsInfos.Models;

namespace Warehouse.Core.Products.Models;

public class Product
{
    public Guid Id { get; set; }
    public ParcelInfo ParcelInfo { get; set; }
    public Manufacturer Manufacturer { get; set; }
    public double Price { get; set; }
    public int AvailableAmount { get; set; }
}
