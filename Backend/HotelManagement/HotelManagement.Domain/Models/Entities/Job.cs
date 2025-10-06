using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Models.Entities;

public class Job : BaseEntity
{
    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public JobType JobType { get; set; }

    [ForeignKey("AssignedTo")]
    public Guid AssignedToId { get; set; }
    public virtual User AssignedTo { get; set; } = null!;

    [ForeignKey("Branch")]
    public Guid BranchId { get; set; }
    public virtual Branch Branch { get; set; } = null!;

    public JobStatus Status { get; set; }

    public JobPriority Priority { get; set; }

    public DateTime? DueDate { get; set; }
}