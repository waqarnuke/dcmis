using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class CardDetailDto
{
    public int Id { get; set; }
    [Required]
    public string? UserId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Title { get; set; } 
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Phone { get; set; }
    [Required]
    public string? Organization { get; set; }
    public string? Weblink1 { get; set; }
    public string? Weblink2 { get; set; }
    public string? Weblink3 { get; set; }
    public string? Weblink4 { get; set; }
    [Required]
    public string? Address { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    public string? State { get; set; }
    [Required]
    public string? ZipCode { get; set; }
    public PhotoDto? photo { get; set; }
}
