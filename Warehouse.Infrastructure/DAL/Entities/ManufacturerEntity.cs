﻿using Warehouse.Core.Manufacturers.Models;

namespace Warehouse.Infrastructure.DAL.Entities;

public class ManufacturerEntity
{
    public Guid Id { get; set; }
    public Guid AddressId { get; set; }
    public AddressEntity Address { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Manufacturer ToManufacturer()
    {
        return new Manufacturer()
        {
            Id = Id,
            Address = Address.ToAddress(),
            Name = Name,
            Email = Email,
            PhoneNumber = PhoneNumber
        };
    }
}
