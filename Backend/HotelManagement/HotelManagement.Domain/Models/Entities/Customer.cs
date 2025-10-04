using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Domain.Models.Entities;

public class Customer : BaseEntity
{
    [Required]
    public string FullName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }
    
    public string? Address { get; set; }

    public string? HistorySummary { get; set; }

    public bool IsGroupCustomer { get; set; }

    public DateTime? LastInteractionDate { get; set; }

    public virtual ICollection<Booking>? Bookings { get; set; }
    public virtual ICollection<Contract>? Contracts { get; set; }
}