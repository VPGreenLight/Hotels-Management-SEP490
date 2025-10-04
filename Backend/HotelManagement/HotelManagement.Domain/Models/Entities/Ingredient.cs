using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Models.Entities;

public class Ingredient : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;

    public string? Unit { get; set; }

    public decimal QuantityInStock { get; set; }

    [ForeignKey("Branch")]
    public Guid BranchId { get; set; }
    public virtual Branch Branch { get; set; } = null!;

    public QaStatus LastQaStatus { get; set; }

    public DateTime? LastQaDate { get; set; }

    public string? SupplierName { get; set; }
}