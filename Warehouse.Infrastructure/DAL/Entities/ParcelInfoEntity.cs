namespace Warehouse.Infrastructure.DAL.Entities;

public class ParcelInfoEntity
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
}
