using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Entities;

public class ParkSlot : BaseEntity
{
    [Required]
    [MaxLength(20)]
    public string SlotNumber { get; set; } = null!;

    [ForeignKey("Branch")]
    public Guid BranchId { get; set; }
    public virtual Branch Branch { get; set; } = null!;

    public ParkSlotStatus Status { get; set; }

    public string? VehiclePlate { get; set; }

    public DateTime? AssignedTime { get; set; }
}