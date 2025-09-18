namespace HotelManagement.Models.Dtos
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
        public UserStatus UserStatus { get; set; }
    }
}