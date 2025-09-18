using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string? Code { get; set; }
    }
}
