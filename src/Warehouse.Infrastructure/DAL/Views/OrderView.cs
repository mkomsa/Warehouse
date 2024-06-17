public class OrderView
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid AddressId { get; set; }
    public Guid InvoiceId { get; set; }
    public string Status { get; set; }
    public Guid CustomerAddressId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerFullName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerPhoneNumber { get; set; }
    public string AddressPostalCode { get; set; }
    public string AddressStreet { get; set; }
    public string AddressApartment { get; set; }
    public DateTime InvoiceTransactionDate { get; set; }
    public double InvoiceNetValue { get; set; }
    public double InvoiceGrossValue { get; set; }
    public string InvoiceStatus { get; set; }
    public double InvoiceVatRate { get; set; }
    public Guid OrderProductId { get; set; }
    public Guid OrderProductOrderId { get; set; }
    public Guid OrderProductProductId { get; set; }
    public Guid ProductId { get; set; }
    public Guid ProductManufacturerId { get; set; }
    public Guid ProductParcelInfoId { get; set; }
    public int ProductAvailableAmount { get; set; }
    public double ProductPrice { get; set; }
    public Guid ManufacturerId { get; set; }
    public Guid ManufacturerAddressId { get; set; }
    public Guid ParcelInfoId { get; set; }
    public double ParcelInfoWeight { get; set; }
    public double ParcelInfoHeight { get; set; }
    public double ParcelInfoLength { get; set; }
}