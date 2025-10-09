using HotelManagement.Application.Models.Dtos.ResponseDtos;

namespace HotelManagement.Application.IServices
{
    public interface IBaseService<TModel, TDto>
        where TModel : class
        where TDto : class
    {
        Task<BaseResponseDto<TDto>> GetByIdAsync(Guid id);
        Task<BaseResponseDto<bool>> DeleteByIdAsync(Guid id);
    }
}
