namespace Warehouse.Infrastructure.DAL.Views;

public class CustomerOrdersView
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public double TotalSpent { get; set; }
}
