using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Domain.RequestDtos
{
    public class ConfirmEmailRequestDto
    {
        [Required]
        public string ConfirmEmail { get; set; }
        [Required]
        public string ConfirmCode { get; set; }
    }
}
