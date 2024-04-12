namespace Warehouse.Infrastructure.DAL.Entities;

public class InvoiceEntity
{
    public Guid Id { get; set; }
    public DateTime TransactionDate { get; set; }
    public double NetValue { get; set; }
    public double GrossValue { get; set; }
    public string Status { get; set; }
    public double VatRate { get; set; }
}
