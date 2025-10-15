using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Entities;

public class RoomInventoryCheckDetail : BaseEntity
{
    [ForeignKey("RoomInventoryCheck")]
    public Guid CheckId { get; set; }
    public virtual RoomInventoryCheck RoomInventoryCheck { get; set; } = null!;
    
    [ForeignKey("RoomInventoryTemplate")]
    public Guid? TemplateItemId { get; set; }
    public virtual RoomInventoryTemplate? TemplateItem { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string ItemName { get; set; } = null!; // Tên đồ vật
    
    public int ExpectedQuantity { get; set; } // Số lượng chuẩn
    
    public int ActualQuantity { get; set; } // Số lượng thực tế
    
    public int VarianceQuantity { get; set; } // Chênh lệch (Actual - Expected)
    
    public ItemCondition Condition { get; set; } = ItemCondition.Good;
    
    public bool IsMissing { get; set; } = false;
    
    public bool IsDamaged { get; set; } = false;
    
    public bool IsExtra { get; set; } = false; // Thừa so với chuẩn
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? DamageCost { get; set; } // Chi phí sửa chữa/thay thế
    
    public string? Notes { get; set; }
    
    public string? PhotoUrl { get; set; } // Ảnh chụp nếu có vấn đề
    
    public bool RequiresAction { get; set; } = false; // Cần xử lý
    
    [ForeignKey("ActionJob")]
    public Guid? ActionJobId { get; set; }
    public virtual Job? ActionJob { get; set; } // Job được tạo để xử lý (sửa, mua mới)
}