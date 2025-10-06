using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Application.Models.Dtos.RequestDtos
{
    public class RegisterRequestDto
    {
        public required string UserName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }
        
        public string? Address { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; }
    }
}
