using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Entities;

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
    
    public virtual ICollection<Room>? Rooms { get; set; }
    public virtual ICollection<Service>? Services { get; set; }
    public virtual ICollection<Job>? Jobs { get; set; }
    public virtual ICollection<Attendance>? Attendances { get; set; }
    public virtual ICollection<EntryExit>? EntryExits { get; set; }
}