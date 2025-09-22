using HotelManagement.Domain.Dtos;
using HotelManagement.Domain.RequestDtos;
using HotelManagement.Domain.ResponseDtos;
using Microsoft.AspNetCore.Http;

namespace HotelManagement.Application.IAspModelService
{
    public interface IUserService
    {
        Task<BaseResponseDto<UserDto>> GetByIdAsync(Guid id);
        Task<BaseResponseDto<IEnumerable<UserDto>>> GetAllAsync();
        Task<BaseResponseDto<bool>> AddAsync(UserDto model);
        Task<BaseResponseDto<bool>> UpdateAsync(UserDto model);
        Task<BaseResponseDto<bool>> DeleteAsync(Guid id);
        Task<BaseResponseDto<int>> GetCountAsync();
        Task<BaseResponseDto<IEnumerable<UserDto>>> GetManagerAsync();
        Task<BaseResponseDto<string>> ChangeAvatarAsync(Guid guid, IFormFile file);
        Task<BaseResponseDto<bool>> UpdateUserInfoAsync(UpdateUserInfoRequestDto dto);
    }
}
