using HotelManagement.Application.Models.Dtos.ResponseDtos;

namespace HotelManagement.Application.IServices
{
    public interface IBaseService<TModel, TDto>
        where TModel : class
        where TDto : class
    {
        Task<TDto?> GetByIdAsync(Guid id);
        Task<bool> DeleteByIdAsync(Guid id);
    }
}
