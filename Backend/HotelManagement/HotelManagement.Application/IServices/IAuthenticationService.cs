using HotelManagement.Domain.RequestDtos;
using HotelManagement.Domain.ResponseDtos;

namespace HotelManagement.Application.IServices
{
    public interface IAuthenticationService
    {
        Task<BaseResponseDto<LoginResponseDto>> AdminLogin(LoginRequestDto loginDto);
        Task<BaseResponseDto<bool>> ConfirmEmail(ConfirmEmailRequestDto request);
        Task<BaseResponseDto<LoginResponseDto>> Login(LoginRequestDto request);
        Task<BaseResponseDto<RefreshTokenResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task<BaseResponseDto<bool>> Register(RegisterRequestDto model);
        Task<BaseResponseDto<bool>> SendConfirmationCodeAsync(string email);
    }
}
