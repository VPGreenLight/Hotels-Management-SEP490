using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Domain.Entities;

public class MaintenancePolicy : BaseEntity
{
    [Required] public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int IntervalDays { get; set; }

    [ForeignKey("Branch")] 
    public Guid? BranchId { get; set; }
    public virtual Branch? Branch { get; set; }

    public string? ContactPersonName { get; set; }

    public string? ContactPersonPhone { get; set; }
}