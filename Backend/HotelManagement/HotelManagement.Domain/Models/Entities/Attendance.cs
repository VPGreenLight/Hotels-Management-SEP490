using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Domain.Models.Entities;

public class Attendance : BaseEntity
{
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;

    [ForeignKey("Branch")]
    public Guid BranchId { get; set; }
    public virtual Branch Branch { get; set; } = null!;

    public DateTime CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public TimeSpan? Duration { get; set; }

    public bool IsApproved { get; set; }
}