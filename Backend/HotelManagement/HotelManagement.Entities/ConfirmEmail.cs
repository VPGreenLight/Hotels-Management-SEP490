using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Entities
{
    public class ConfirmEmail : BaseModel
    {
        [Required]
        public Guid? UserId { get; set; }

        [Required]
        public DateTime RequestedAt { get; set; } 

        [Required]
        public DateTime ExpiresAt { get; set; } 

        [Required]
        [MaxLength(8)] 
        public string? ConfirmCode { get; set; }

        public bool IsConfirmed { get; set; }

        public virtual User? User { get; set; }
    }
}
