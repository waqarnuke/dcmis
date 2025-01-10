using System;

namespace Core.Entities;

public class CardDetail : BaseEntity
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public string? Title { get; set; } 
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Organization { get; set; }
    public string? Weblink1 { get; set; }
    public string? Weblink2 { get; set; }
    public string? Weblink3 { get; set; }
    public string? Weblink4 { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }
    public string? PictureUrl { get; set; }
}
