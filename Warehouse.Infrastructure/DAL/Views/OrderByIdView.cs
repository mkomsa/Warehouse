namespace Warehouse.Infrastructure.DAL.Views;

internal class OrderByIdView
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid OrderAddressId { get; set; }
    public Guid InvoiceId { get; set; }
    public Guid CustomerEntity_Id { get; set; }
    public Guid CustomerEntity_AddressEntityId { get; set; }
    public string CustomerEntity_Name { get; set; }
    public string CustomerEntity_FullName { get; set; }
    public string CustomerEntity_Email { get; set; }
    public string CustomerEntity_PhoneNumber { get; set; }
    public Guid CustomerAddressEntity_Id { get; set; }
    public string CustomerAddressEntity_PostalCode { get; set; }
    public string CustomerAddressEntity_Street { get; set; }
    public string CustomerAddressEntity_Apartment { get; set; }
    public Guid OrderAddressEntity_Id { get; set; }
    public string OrderAddressEntity_PostalCode { get; set; }
    public string OrderAddressEntity_Street { get; set; }
    public string OrderAddressEntity_Apartment { get; set; }
    public Guid InvoiceEntity_Id { get; set; }
    public DateTime InvoiceEntity_TransactionDate { get; set; }
    public double InvoiceEntity_NetValue { get; set; }
    public double InvoiceEntity_GrossValue { get; set; }
    public string InvoiceEntity_Status { get; set; }
    public double InvoiceEntity_VatRate { get; set; }
    public Guid OrderProduct_Id { get; set; }
    public Guid OrderProduct_OrderEntityId { get; set; }
    public Guid OrderProduct_ProductEntityId { get; set; }
    public Guid ProductEntity_Id { get; set; }
    public Guid ProductEntity_ManufacturerEntityId { get; set; }
    public Guid ProductEntity_ParcelInfoEntityId { get; set; }
    public int ProductEntity_AvailableAmount { get; set; }
    public double ProductEntity_Price { get; set; }
    public Guid ManufacturerEntity_Id { get; set; }
    public Guid ManufacturerEntity_AddressEntityId { get; set; }
    public string ManufacturerEntity_Name { get; set; }
    public string ManufacturerEntity_Email { get; set; }
    public string ManufacturerEntity_PhoneNumber { get; set; }
    public Guid ManufacturerAddressEntity_Id { get; set; }
    public string ManufacturerAddressEntity_PostalCode { get; set; }
    public string ManufacturerAddressEntity_Street { get; set; }
    public string ManufacturerAddressEntity_Apartment { get; set; }
    public Guid ParcelInfoEntity_Id { get; set; }
    public double ParcelInfoEntity_Weight { get; set; }
    public double ParcelInfoEntity_Height { get; set; }
    public double ParcelInfoEntity_Length { get; set; }
    public double ParcelInfoEntity_Width { get; set; }
}
