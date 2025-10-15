using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Domain.Dtos
{
    public class UserDto : BaseModelDto
    {
        public string? FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
        public List<string> Roles { get; set; } = new();
        public UserStatus UserStatus { get; set; }
    }
}