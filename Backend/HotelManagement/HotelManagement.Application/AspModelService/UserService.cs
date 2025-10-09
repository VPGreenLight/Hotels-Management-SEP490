using AutoMapper;
using HotelManagement.Application.Models.Dtos.RequestDtos;
using HotelManagement.Application.Models.Dtos.ResponseDtos;
using HotelManagement.Domain.Dtos;
using HotelManagement.Domain.Entities;
using HotelManagement.Infrastructure.R2Storage;
using HotelManagement.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace HotelManagement.Application.AspModelService
{
    public class UserService(
        IRepository<User> repository,
        UserManager<User> userManager,
        IMapper mapper,
        IConfiguration configuration,
        IR2StorageService r2StorageService) : IUserService
    {
        public async Task<BaseResponseDto<UserDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await repository.GetByIdAsync(id);

                if (entity == null)
                {
                    return new BaseResponseDto<UserDto>
                    {
                        Status = 404,
                        Message = "Entity not found.",
                        ResponseData = null
                    };
                }

                var dto = mapper.Map<UserDto>(entity);

                return new BaseResponseDto<UserDto>
                {
                    Status = 200,
                    Message = "Success",
                    ResponseData = dto
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseDto<UserDto>
                {
                    Status = 500,
                    Message = ex.Message,
                    ResponseData = null
                };
            }
        }

        public async Task<BaseResponseDto<IEnumerable<UserDto>>> GetAllAsync()
        {
            try
            {
                var entities = await repository.GetListAsync();
                var dtos = mapper.Map<IEnumerable<UserDto>>(entities);

                return new BaseResponseDto<IEnumerable<UserDto>>
                {
                    Status = 200,
                    Message = "Success",
                    ResponseData = dtos
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseDto<IEnumerable<UserDto>>
                {
                    Status = 500,
                    Message = ex.Message,
                    ResponseData = null
                };
            }
        }

        public async Task<BaseResponseDto<bool>> AddAsync(UserDto dto)
        {
            try
            {
                var model = mapper.Map<User>(dto);
                await repository.AddAsync(model);

                return new BaseResponseDto<bool>
                {
                    Status = 201,
                    Message = "Add successful",
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

        public async Task<BaseResponseDto<bool>> UpdateAsync(UserDto dto)
        {
            try
            {
                var entity = await repository.GetOneAsync(filter: f => f.Id == dto.Id);
                if (entity == null)
                {
                    return new BaseResponseDto<bool>
                    {
                        Status = 404,
                        Message = "User not found!",
                        ResponseData = false
                    };
                }

                var dtoProps = dto.GetType().GetProperties();
                var entityProps = entity.GetType().GetProperties();

                foreach (var dtoProp in dtoProps)
                {
                    var entityProp = entityProps.FirstOrDefault(p => p.Name == dtoProp.Name && p.CanWrite);
                    if (entityProp != null)
                    {
                        var dtoValue = dtoProp.GetValue(dto);
                        entityProp.SetValue(entity, dtoValue);
                    }
                }

                await repository.UpdateAsync(entity);

                return new BaseResponseDto<bool>
                {
                    Status = 200,
                    Message = "Update successful",
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

        public async Task<BaseResponseDto<bool>> UpdateUserInfoAsync(UpdateUserInfoRequestDto dto)
        {
            try
            {
                var user = await repository.GetOneAsync(filter: f => f.Id == dto.UserId);
                if (user == null)
                {
                    return new BaseResponseDto<bool>
                    {
                        Status = 404,
                        Message = "User not found",
                        ResponseData = false
                    };
                }

                var dtoProps = dto.GetType().GetProperties();
                var entityProps = user.GetType().GetProperties();

                foreach (var dtoProp in dtoProps)
                {
                    var entityProp = entityProps.FirstOrDefault(p => p.Name == dtoProp.Name && p.CanWrite);
                    if (entityProp != null)
                    {
                        var dtoValue = dtoProp.GetValue(dto);
                        entityProp.SetValue(user, dtoValue);
                    }
                }

                await repository.UpdateAsync(user);

                return new BaseResponseDto<bool>
                {
                    Status = 200,
                    Message = "Update successful",
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

        public async Task<BaseResponseDto<bool>> DeleteAsync(Guid id)
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

        public async Task<BaseResponseDto<int>> GetCountAsync()
        {
            try
            {
                var count = await repository.GetCount();

                return new BaseResponseDto<int>
                {
                    Status = 200,
                    Message = "Success",
                    ResponseData = count
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseDto<int>
                {
                    Status = 500,
                    Message = ex.Message,
                    ResponseData = 0
                };
            }
        }

        public async Task<BaseResponseDto<IEnumerable<UserDto>>> GetManagerAsync()
        {
            try
            {
                var managers = await userManager.GetUsersInRoleAsync("Manager");

                var dtos = mapper.Map<IEnumerable<UserDto>>(managers);

                return new BaseResponseDto<IEnumerable<UserDto>>
                {
                    Status = 200,
                    Message = "Success",
                    ResponseData = dtos
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseDto<IEnumerable<UserDto>>
                {
                    Status = 500,
                    Message = ex.Message,
                    ResponseData = null
                };
            }
        }

        public async Task<BaseResponseDto<string>> ChangeAvatarAsync(Guid userId, IFormFile file)
        {
            try
            {
                var user = await repository.GetByIdAsync(userId);
                if (user == null)
                {
                    return new BaseResponseDto<string>
                    {
                        Status = 404,
                        Message = "User not found!",
                        ResponseData = null
                    };
                }

                var fileUrl = await r2StorageService.UploadFileAsync(file, "avatars", userId.ToString());

                user.AvatarUrl = fileUrl;
                await repository.UpdateAsync(user);

                return new BaseResponseDto<string>
                {
                    Status = 200,
                    Message = "Tải ảnh đại diện thành công",
                    ResponseData = fileUrl + "?v=" + DateTime.Now.Microsecond.ToString()
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseDto<string>
                {
                    Status = 500,
                    Message = ex.Message,
                    ResponseData = null
                };
            }
        }
    }
}