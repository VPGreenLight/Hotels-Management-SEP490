using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Models.Entities;

public class Lead : BaseEntity
{
    [Required]
    public string FullName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Source { get; set; }

    [ForeignKey("AssignedTo")]
    public Guid? AssignedToId { get; set; }
    public virtual User? AssignedTo { get; set; }

    public LeadStatus Status { get; set; }

    public DateTime ImportDate { get; set; }
}