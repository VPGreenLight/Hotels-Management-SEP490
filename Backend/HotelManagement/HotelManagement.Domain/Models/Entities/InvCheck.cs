using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Models.Entities;

public class InvCheck : BaseEntity
{
    [ForeignKey("Room")]
    public Guid RoomId { get; set; }
    public virtual Room Room { get; set; } = null!;

    [ForeignKey("CheckedBy")]
    public Guid CheckedById { get; set; }
    public virtual User CheckedBy { get; set; } = null!;

    public DateTime CheckDate { get; set; }

    public string? ItemName { get; set; }

    public bool IsMissing { get; set; }

    public InvCheckStatus Status { get; set; }
}