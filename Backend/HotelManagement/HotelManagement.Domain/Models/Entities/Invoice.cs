using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Models.Entities;

public class Invoice : BaseEntity
{
    [ForeignKey("Booking")]
    public Guid BookingId { get; set; }
    public virtual Booking Booking { get; set; } = null!;

    public InvoiceType InvoiceType { get; set; }

    public DateTime IssueDate { get; set; } = DateTime.UtcNow;

    [Column(TypeName = "decimal(18,2)")]
    public decimal RoomCharges { get; set; } = 0; // Tiền phòng
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal ServiceCharges { get; set; } = 0; // Tiền dịch vụ
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal DamageCharges { get; set; } = 0; // Tiền bồi thường hư hỏng
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal OtherCharges { get; set; } = 0; // Phí khác
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Discount { get; set; } = 0;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal TaxAmount { get; set; } = 0;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; } // Tổng cuối cùng
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal PaidAmount { get; set; } = 0; // Đã thanh toán
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal RemainingAmount { get; set; } // Còn lại

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;

    public string? PaymentMethod { get; set; }
    
    public DateTime? PaidDate { get; set; }

    [ForeignKey("Contract")]
    public Guid? ContractId { get; set; }
    public virtual Contract? Contract { get; set; }
    
    public string? Notes { get; set; }
}