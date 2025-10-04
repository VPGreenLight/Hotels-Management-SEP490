using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Domain.Models.Entities;

public class AuditLog : BaseEntity
{
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    public virtual User User { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    [Required]
    public string Action { get; set; } = null!;

    [Required]
    public string EntityType { get; set; } = null!;

    public Guid EntityId { get; set; }

    public string? Details { get; set; }
}
