using System;

namespace API.DTOs;

public class PhotoDto
{
    public int Id { get; set; }
    public string? PublicId { get; set; }
    public string? Url {get; set;}
    public bool? IsMain { get; set; }
    public string? UserId { get; set; }
}
