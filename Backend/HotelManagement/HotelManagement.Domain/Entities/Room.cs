using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Entities;

public class Room : BaseEntity
{
    [Required, MaxLength(20)]
    public string RoomNumber { get; set; } = null!;

    [ForeignKey("Branch")]
    public Guid BranchId { get; set; }
    public virtual Branch Branch { get; set; } = null!;

    [ForeignKey("RoomCategory")]
    public Guid CategoryId { get; set; }
    public virtual RoomCategory RoomCategory { get; set; } = null!;

    public RoomStatus CurrentStatus { get; set; } = RoomStatus.Vacant;

    public int Floor { get; set; }

    public DateTime? LastCleanedAt { get; set; }

    [ForeignKey("LastCheckedBy")]
    public Guid? LastCheckedById { get; set; }
    public virtual User? LastCheckedBy { get; set; }
    
    public DateTime? LastInventoryCheckDate { get; set; }
    
    public bool HasInventoryIssues { get; set; } = false; // Flag nhanh để query
    
    // Navigation properties
    public virtual ICollection<Booking>? Bookings { get; set; }
    public virtual ICollection<RoomInventoryCheck>? InventoryChecks { get; set; }
    public virtual ICollection<ServiceUsage>? ServiceUsages { get; set; }
}