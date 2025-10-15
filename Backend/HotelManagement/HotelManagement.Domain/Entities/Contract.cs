using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Domain.Entities;

public class Contract : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string ContractNumber { get; set; } = null!;

    [ForeignKey("Customer")]
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;

    public int GroupSize { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalValue { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? FileUrl { get; set; }

    [ForeignKey("Template")]
    public Guid? TemplateId { get; set; }
    public virtual Template? Template { get; set; }
}