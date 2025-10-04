using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;
using TaskStatus = HotelManagement.Domain.Models.Enums.TaskStatus;

namespace HotelManagement.Domain.Models.Entities;

public class Task : BaseEntity
{
    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public TaskType TaskType { get; set; }

    [ForeignKey("AssignedTo")]
    public Guid AssignedToId { get; set; }
    public virtual User AssignedTo { get; set; } = null!;

    [ForeignKey("Branch")]
    public Guid BranchId { get; set; }
    public virtual Branch Branch { get; set; } = null!;

    public TaskStatus Status { get; set; }

    public TaskPriority Priority { get; set; }

    public DateTime? DueDate { get; set; }
}