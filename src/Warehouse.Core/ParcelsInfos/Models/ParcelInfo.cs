﻿namespace Warehouse.Core.ParcelsInfos.Models;

public class ParcelInfo
{
    public Guid Id { get; set; }
    public double Length { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
}