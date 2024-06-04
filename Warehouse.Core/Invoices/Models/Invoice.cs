namespace Warehouse.Core.Invoices.Models;

public class Invoice
{
    public Guid Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public double NetValue { get; set; }
    public double GrossValue { get; set; }
    public string Status { get; set; }
    public double VatRate { get; set; }
}
