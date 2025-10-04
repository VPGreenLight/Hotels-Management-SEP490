using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Models.Entities;

public class Invoice : BaseEntity
{
    [ForeignKey("Booking")]
    public Guid BookingId { get; set; }
    public virtual Booking Booking { get; set; } = null!;

    public InvoiceType InvoiceType { get; set; }

    public DateTime IssueDate { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;

    public string? PaymentMethod { get; set; }

    [ForeignKey("Contract")]
    public Guid? ContractId { get; set; }
    public virtual Contract? Contract { get; set; }
}