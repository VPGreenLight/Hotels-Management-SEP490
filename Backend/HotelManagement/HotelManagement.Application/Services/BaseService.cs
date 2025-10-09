using AutoMapper;
using HotelManagement.Application.IServices;
using HotelManagement.Application.Models.Dtos.ResponseDtos;
using HotelManagement.Domain.Dtos;
using HotelManagement.Domain.Models.Entities;
using HotelManagement.Infrastructure.Repository;

namespace HotelManagement.Application.Services
{
    public class BaseService<TModel, TDto>(IRepository<TModel> repository, IMapper mapper)
        : IBaseService<TModel, TDto>
        where TModel : BaseEntity
        where TDto : BaseModelDto
    {
        public async Task<BaseResponseDto<TDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await repository.GetByIdAsync(id);
                if (entity == null)
                {
                    return new BaseResponseDto<TDto>
                    {
                        Status = 404,
                        Message = "Entity not found.",
                        ResponseData = null
                    };
                }

                var dto = mapper.Map<TDto>(entity);
                return new BaseResponseDto<TDto>
                {
                    Status = 200,
                    Message = "Success",
                    ResponseData = dto
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseDto<TDto>
                {
                    Status = 500,
                    Message = ex.Message,
                    ResponseData = null
                };
            }
        }

        public async Task<BaseResponseDto<bool>> DeleteByIdAsync(Guid id)
        {
            try
            {
                var entity = await repository.GetByIdAsync(id);
                if (entity == null)
                {
                    return new BaseResponseDto<bool>
                    {
                        Status = 404,
                        Message = "Entity not found.",
                        ResponseData = false
                    };
                }

                await repository.DeleteAsync(entity);
                return new BaseResponseDto<bool>
                {
                    Status = 200,
                    Message = "Delete successful",
                    ResponseData = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseDto<bool>
                {
                    Status = 500,
                    Message = ex.Message,
                    ResponseData = false
                };
            }
        }
    }
}
