using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Models.Entities;

public class EntryExit : BaseEntity
{
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;

    [ForeignKey("Branch")]
    public Guid BranchId { get; set; }
    public virtual Branch Branch { get; set; } = null!;

    public DateTime EntryExitTime { get; set; }

    public EntryExitDirection Direction { get; set; }

    public string? VehiclePlate { get; set; }

    public string? Note { get; set; }
}