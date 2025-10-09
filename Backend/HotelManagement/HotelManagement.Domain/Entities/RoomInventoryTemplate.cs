using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Entities;

public class RoomInventoryTemplate : BaseEntity
{
    [ForeignKey("RoomCategory")]
    public Guid RoomCategoryId { get; set; }
    public virtual RoomCategory RoomCategory { get; set; } = null!;
    
    [Required]
    [MaxLength(200)]
    public string ItemName { get; set; } = null!;
    
    public string? Description { get; set; }
    
    public int StandardQuantity { get; set; } // Số lượng chuẩn
    
    public InventoryItemCategory Category { get; set; } // Furniture, Linen, Electronics, Amenities
    
    public bool IsConsumable { get; set; } = false; // Đồ tiêu hao (khăn, xà phòng...)
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal? ReplacementCost { get; set; } // Chi phí thay thế nếu mất
    
    public bool IsActive { get; set; } = true;
    
    public string? ImageUrl { get; set; }
}