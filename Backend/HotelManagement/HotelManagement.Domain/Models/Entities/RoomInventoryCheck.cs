using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Models.Entities;

public class RoomInventoryCheck : BaseEntity
{
    [ForeignKey("Room")] public Guid RoomId { get; set; }
    public virtual Room Room { get; set; } = null!;

    [ForeignKey("CheckedBy")] public Guid CheckedById { get; set; }
    public virtual User CheckedBy { get; set; } = null!;

    [ForeignKey("Booking")] public Guid? BookingId { get; set; }
    public virtual Booking? Booking { get; set; } // Liên kết với booking (kiểm kê sau khi trả phòng)

    public DateTime CheckDate { get; set; } = DateTime.UtcNow;

    public InventoryCheckType CheckType { get; set; } // CheckOut, Periodic, Incident

    public InventoryCheckStatus Status { get; set; } = InventoryCheckStatus.InProgress;

    public bool HasIssues { get; set; } = false; // Có vấn đề không

    public string? OverallNotes { get; set; }

    [ForeignKey("ApprovedBy")] public Guid? ApprovedById { get; set; }
    public virtual User? ApprovedBy { get; set; } // Người phê duyệt

    public DateTime? ApprovedDate { get; set; }

    // Navigation properties
    public virtual ICollection<RoomInventoryCheckDetail>? CheckDetails { get; set; }
}