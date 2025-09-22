using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Domain.Models
{
    public class Role : IdentityRole<Guid>
    {
        public string? Code { get; set; }
    }
}
