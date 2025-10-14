using AutoMapper;
using HotelManagement.Application.IServices;
using HotelManagement.Domain.Dtos;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.Repository;

namespace HotelManagement.Application.Services
{
    public class BaseService<TModel, TDto>(IRepository<TModel> repository, IMapper mapper)
        : IBaseService<TModel, TDto>
        where TModel : BaseEntity
        where TDto : BaseModelDto
    {
        public async Task<TDto?> GetByIdAsync(Guid id)
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }

            var dto = mapper.Map<TDto>(entity);
            return dto;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            await repository.DeleteAsync(entity);
            return true;
        }
    }
}