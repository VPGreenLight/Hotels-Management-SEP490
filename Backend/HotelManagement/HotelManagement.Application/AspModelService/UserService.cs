﻿using HotelManagement.Infrastructure.Repository;
using HotelManagement.Infrastructure.Converter;
using HotelManagement.Application.IAspModelService;
using HotelManagement.Infrastructure.R2Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using HotelManagement.Domain.Models;
using HotelManagement.Domain.Dtos;
using HotelManagement.Domain.ResponseDtos;
using HotelManagement.Domain.RequestDtos;

namespace HotelManagement.Services.AspModelService
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly UserManager<User> _userManager;
        private readonly IConverter<User, UserDto> _converter;
        private readonly IConfiguration _configuration;
        private readonly IR2StorageService _r2StorageService;

        public UserService(
            IRepository<User> repository,
            UserManager<User> userManager,
            IConverter<User, UserDto> converter,
            IConfiguration configuration,
            IR2StorageService r2StorageService)
        {
            _repository = repository;
            _userManager = userManager;
            _converter = converter;
            _configuration = configuration;
            _r2StorageService = r2StorageService;
        }

        public async Task<BaseResponseDto<UserDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _repository.GetOneAsyncUntracked<User>(f => f.Id == id);

                if (entity == null)
                {
                    return new BaseResponseDto<UserDto>
                    {
                        Status = 404,
                        Message = "Entity not found.",
                        ResponseData = null
                    };
                }

                var dto = _converter.ToDTO(entity);

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
                var entities = await _repository.GetListAsyncUntracked<User>();
                var dtos = _converter.ToListDTO(entities);

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
                var model = _converter.ToModel(dto);
                await _repository.AddAsync(model);

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
                var entity = await _repository.GetOneAsync(filter: f => f.Id == dto.Id);
                if (entity == null)
                {
                    return new BaseResponseDto<bool>
                    {
                        Status = 404,
                        Message = "User not found",
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

                await _repository.UpdateAsync(entity);

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
                var user = await _repository.GetOneAsync(filter: f => f.Id == dto.UserId);
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

                await _repository.UpdateAsync(user);

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
                var entity = await _repository.GetOneAsyncUntracked<User>(f => f.Id == id);

                if (entity == null)
                {
                    return new BaseResponseDto<bool>
                    {
                        Status = 404,
                        Message = "Entity not found.",
                        ResponseData = false
                    };
                }

                await _repository.DeleteAsync(entity);

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
                var count = await _repository.GetCount();

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
                var managers = await _userManager.GetUsersInRoleAsync("Manager");

                var dtos = _converter.ToListDTO(managers);

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
                var user = await _repository.GetByIdAsync(userId);
                if (user == null)
                {
                    return new BaseResponseDto<string>
                    {
                        Status = 404,
                        Message = "Người dùng không tồn tại",
                        ResponseData = null
                    };
                }

                var fileUrl = await _r2StorageService.UploadFileAsync(file, "avatars", userId.ToString());

                user.AvatarUrl = fileUrl;
                await _repository.UpdateAsync(user);

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