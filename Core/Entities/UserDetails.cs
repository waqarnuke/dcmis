namespace Core.Entities;

public class UserDetails : BaseEntity
{
    public string? Name { get; set; } 
    public string? Email { get; set; }
    public string? PhoneNo { get; set; }
    public string? NickName { get; set; }
    public string? Address { get; set; }
    public string? CurrentOrg { get; set; }
    public string? IG { get; set; }
    public string? FB { get; set; }
    public string? WhatsApp { get; set; }
    public string? LinkedIn { get; set; }
    public int Age { get; set; }
    public string? Gender { get; set; }
    public Guid UserId { get; set; }
    public string? PictureUrl { get; set; }
}
