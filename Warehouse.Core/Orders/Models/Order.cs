using Warehouse.Core.Addresses.Models;
using Warehouse.Core.Customers.Models;
using Warehouse.Core.Invoices.Models;
using Warehouse.Core.Products.Models;

namespace Warehouse.Core.Orders.Models;

public class Order
{
    public Guid Id { get; set; }
    public Customer Customer { get; set; }
    public Address Address { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
    public Invoice Invoice { get; set; }
}
