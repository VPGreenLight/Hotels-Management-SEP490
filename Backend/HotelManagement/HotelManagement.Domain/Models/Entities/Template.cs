using System.ComponentModel.DataAnnotations;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Models.Entities;

public class Template : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    
    [Required]
    public string Content { get; set; } = null!; // HTML/Rich text content
    
    public TemplateType Type { get; set; } // Contract, Email, Report
    
    public string? Variables { get; set; } // JSON string chứa biến template
    
    public bool IsActive { get; set; } = true;
    
    public virtual ICollection<Contract>? Contracts { get; set; }
}