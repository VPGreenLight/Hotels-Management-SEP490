namespace HotelManagement.Application.Models.Dtos.ResponseDtos
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
