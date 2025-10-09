using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        public DateTime? DeletedAt { get; set; }
        
        public bool IsDeleted { get; set; } = false;
        
        public Guid? CreatedById { get; set; }
        
        public Guid? UpdatedById { get; set; }
        
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
