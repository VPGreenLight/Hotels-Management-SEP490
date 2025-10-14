using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Entities;

public class Service : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public ServiceCategory Category { get; set; }

    [ForeignKey("Branch")]
    public Guid BranchId { get; set; }
    public virtual Branch Branch { get; set; } = null!;

    public bool IsAvailable { get; set; } = true;

    public string? Icon { get; set; } // URL hoặc tên icon

    public int? EstimatedDurationMinutes { get; set; }

    public bool RequiresBooking { get; set; } = false;

    public int? MaxCapacity { get; set; } // Sức chứa tối đa (cho spa, gym...)

    public string? OperatingHours { get; set; } // JSON: {"start": "08:00", "end": "22:00"}
    
    public bool IsChargeable { get; set; } = true; // Có tính phí hay không (miễn phí như wifi)
    
    [MaxLength(50)]
    public string? Unit { get; set; } // Đơn vị tính: lần, giờ, người, v.v.
    
    // Navigation properties
    public virtual ICollection<ServiceUsage>? ServiceUsages { get; set; }
}