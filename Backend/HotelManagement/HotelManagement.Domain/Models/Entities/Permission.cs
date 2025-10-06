using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Domain.Models.Entities;

public class Permission : BaseEntity
{
    [Required] 
    public string Name { get; set; } = null!;

    [Required] 
    [MaxLength(50)] 
    public string Code { get; set; } = null!;

    public string? Description { get; set; }
    
    public string? PolicyCondition { get; set; }
}