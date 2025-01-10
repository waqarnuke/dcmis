using System;

namespace Core.Entities;

public class Photo :BaseEntity
{
    public string? PublicId { get; set; }
    public string? Url {get; set;}
    public bool? IsMain { get; set; }
    public string? UserId { get; set; }
}
