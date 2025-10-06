using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Models.Entities;

public class Request : BaseEntity
{
    public RequestType RequestType { get; set; }

    public PriorityLevel Priority { get; set; }

    public RequestStatus Status { get; set; }

    public string? Description { get; set; }

    [ForeignKey("RequestedBy")]
    public Guid RequestedById { get; set; }
    public virtual User RequestedBy { get; set; } = null!;

    [ForeignKey("Branch")]
    public Guid BranchId { get; set; }
    public virtual Branch Branch { get; set; } = null!;

    [ForeignKey("ApprovedBy")]
    public Guid? ApprovedById { get; set; }
    public virtual User? ApprovedBy { get; set; }

    public string? AttachmentUrl { get; set; }
    
    public virtual ICollection<Job>? Jobs { get; set; }
}