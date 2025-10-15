using HotelManagement.Application.Models.Dtos.RequestDtos;
using HotelManagement.Application.Models.Dtos.ResponseDtos;
using HotelManagement.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace HotelManagement.Application.AspModelService
{
    public interface IUserService
    {
        Task<BaseResponseDto<UserDto>> GetByIdAsync(Guid id);
        Task<BaseResponseDto<List<UserDto>>> GetAllAsync();
        Task<BaseResponseDto<bool>> AddAsync(UserDto model);
        Task<BaseResponseDto<bool>> UpdateAsync(UserDto model);
        Task<BaseResponseDto<bool>> DeleteAsync(Guid id);
        Task<BaseResponseDto<int>> GetCountAsync();
        Task<BaseResponseDto<List<UserDto>>> GetManagerAsync();
        Task<BaseResponseDto<string>> ChangeAvatarAsync(Guid guid, IFormFile file);
        Task<BaseResponseDto<bool>> UpdateUserInfoAsync(UpdateUserInfoRequestDto dto);
    }
}
