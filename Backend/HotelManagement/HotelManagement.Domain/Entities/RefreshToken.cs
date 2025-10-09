using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        [MaxLength(1000)]
        public string? Token { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime ExpiredTime { get; set; }

        public Guid UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
