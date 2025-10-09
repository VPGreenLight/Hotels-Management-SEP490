using System.ComponentModel.DataAnnotations;
using NpgsqlTypes;

namespace HotelManagement.Domain.Entities;

public class Customer : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = null!;

    [Phone] // Thêm validation
    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [EmailAddress] // Thêm validation
    [MaxLength(100)]
    public string? Email { get; set; }
    
    [MaxLength(500)]
    public string? Address { get; set; }

    [MaxLength(2000)]
    public string? HistorySummary { get; set; }

    public bool IsGroupCustomer { get; set; } = false;

    public DateTime? LastInteractionDate { get; set; }
    
    public NpgsqlTsVector SearchVector { get; set; }
    
    public virtual ICollection<Booking>? Bookings { get; set; }
    public virtual ICollection<Contract>? Contracts { get; set; }

}