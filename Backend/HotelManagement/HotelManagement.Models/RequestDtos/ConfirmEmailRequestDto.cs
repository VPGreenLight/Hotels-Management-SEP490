namespace HotelManagement.Models.RequestDtos
{
    public class ConfirmEmailRequestDto
    {
        [Required]
        public string ConfirmEmail { get; set; }
        [Required]
        public string ConfirmCode { get; set; }
    }
}
