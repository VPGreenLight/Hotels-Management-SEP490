using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Application.Models.Dtos.RequestDtos
{
    public class UpdateUserInfoRequestDto
    {
        public Guid UserId { get;set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DataOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
    }
}
