using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Models.Entities;

public class Branch : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    [ForeignKey("Manager")]
    public Guid? ManagerId { get; set; }
    public virtual User? Manager { get; set; }

    public BranchStatus Status { get; set; }

    public int TotalRooms { get; set; }

    public int ParkingSlotsCount { get; set; }

    public DateTime CreatedAt { get; set; }
}