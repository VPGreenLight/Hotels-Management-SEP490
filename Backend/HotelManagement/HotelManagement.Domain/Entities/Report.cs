using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Entities;

public class Report : BaseEntity
{
    [Required] 
    public string Name { get; set; } = null!;

    public ReportType ReportType { get; set; }

    [ForeignKey("Branch")]
    public Guid? BranchId { get; set; }
    public virtual Branch? Branch { get; set; }

    public DateTime GeneratedDate { get; set; }

    public string? DataLink { get; set; }
}