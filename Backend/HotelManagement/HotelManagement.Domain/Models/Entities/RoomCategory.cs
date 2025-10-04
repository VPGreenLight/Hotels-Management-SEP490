using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Domain.Models.Entities;

public class RoomCategory : BaseEntity
{
    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal BasePrice { get; set; }
    
    public string? Amenities { get; set; }

    public int Capacity { get; set; }
}