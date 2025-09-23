using HotelManagement.Common.Enum;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Domain.Models
{
    public class User : IdentityUser<Guid>
    {
        public string? FullName { get; set; }

        public string? Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string? AvatarUrl { get; set; }

        public UserStatus UserStatus { get; set; }

        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<ConfirmEmail>? ConfirmEmails { get; set; }
    }
}
