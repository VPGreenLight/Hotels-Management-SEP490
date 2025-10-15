using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Domain.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string? Code { get; set; }
    }
}
