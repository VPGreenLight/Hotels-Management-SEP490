using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Models.Entities;

public class Booking : BaseEntity
{
    [ForeignKey("Customer")]
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("Receptionist")]
    public Guid ReceptionistId { get; set; }
    public virtual User Receptionist { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string BookingCode { get; set; } = null!;

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public DateTime? ActualCheckIn { get; set; }

    public DateTime? ActualCheckOut { get; set; }

    public BookingStatus Status { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    public virtual ICollection<BookItem>? BookItems { get; set; }

    public virtual ICollection<Invoice>? Invoices { get; set; }
}