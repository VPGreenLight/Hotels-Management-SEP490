using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Domain.Models.Entities;

public class ServiceUsage : BaseEntity
{
    [ForeignKey("Booking")]
    public Guid BookingId { get; set; }
    public virtual Booking Booking { get; set; } = null!;
    
    [ForeignKey("Service")]
    public Guid ServiceId { get; set; }
    public virtual Service Service { get; set; } = null!;
    
    [ForeignKey("Room")]
    public Guid RoomId { get; set; }
    public virtual Room Room { get; set; } = null!;

    public DateTime UsageDate { get; set; } = DateTime.UtcNow;
    
    public int Quantity { get; set; } = 1;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal PricePerUnit { get; set; } // Giá tại thời điểm sử dụng
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    
    [ForeignKey("RecordedBy")]
    public Guid RecordedById { get; set; }
    public virtual User RecordedBy { get; set; } = null!; // Nhân viên ghi nhận
    
    public string? Notes { get; set; }
    
    public bool IsCharged { get; set; } = false; // Đã tính vào hóa đơn chưa
}