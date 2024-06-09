using System.ComponentModel.DataAnnotations;
using Warehouse.Core.Invoices.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class InvoiceEntity
{
    [Key]
    public Guid InvoiceId { get; set; }
    public DateTime TransactionDate { get; set; }
    public double NetValue { get; set; }
    public double GrossValue { get; set; }
    public string Status { get; set; }
    public double VatRate { get; set; }

    public static InvoiceEntity FromInvoice(Invoice invoice)
    {
        return new InvoiceEntity()
        {
            InvoiceId = invoice.Id,
            TransactionDate = invoice.TransactionDate,
            NetValue = invoice.NetValue,
            GrossValue = invoice.GrossValue,
            Status = invoice.Status,
            VatRate = invoice.VatRate
        };
    }
    public Invoice ToInvoice()
    {
        return new Invoice()
        {
            Id = InvoiceId,
            TransactionDate = TransactionDate,
            NetValue = NetValue,
            GrossValue = GrossValue,
            Status = Status,
            VatRate = VatRate
        };
    }
}
