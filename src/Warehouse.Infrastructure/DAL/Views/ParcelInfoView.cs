namespace Warehouse.Infrastructure.DAL.Views;

public class ParcelInfoView
{
    public Guid ParcelInfoId { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
}
