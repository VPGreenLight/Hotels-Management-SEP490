using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Entities;

public class Booking : BaseEntity
{
    [ForeignKey("Customer")]
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; } = null!;

    [ForeignKey("Receptionist")]
    public Guid ReceptionistId { get; set; }
    public virtual User Receptionist { get; set; } = null!;
    
    [ForeignKey("Room")]
    public Guid RoomId { get; set; }
    public virtual Room Room { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string BookingCode { get; set; } = null!;

    public DateTime CheckInDate { get; set; }

    public DateTime CheckOutDate { get; set; }

    public DateTime? ActualCheckIn { get; set; }

    public DateTime? ActualCheckOut { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    
    [Range(1, 20)]
    public int NumberOfGuests { get; set; }
    
    [Range(1, 365)]
    public int NumberOfNights { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 999999999.99)]
    public decimal RoomRate { get; set; } // Giá phòng tại thời điểm đặt
    
    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 999999999.99)]
    public decimal TotalRoomAmount { get; set; } // Tổng tiền phòng

    [MaxLength(1000)]
    public string? SpecialRequests { get; set; } // Yêu cầu đặc biệt
    
    [MaxLength(2000)]
    public string? Notes { get; set; }

    // Navigation properties
    public virtual ICollection<Invoice>? Invoices { get; set; }
    public virtual ICollection<ServiceUsage>? ServiceUsages { get; set; } // Dịch vụ sử dụng trong kỳ lưu trú
}