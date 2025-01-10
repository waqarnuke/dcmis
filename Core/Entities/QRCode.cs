using System;

namespace Core.Entities;

public class QRCode : BaseEntity
{
    public string? QRCodeData  { get; set; }
    public string? Description { get; set; }
    public  string? UserId { get; set; }
}
