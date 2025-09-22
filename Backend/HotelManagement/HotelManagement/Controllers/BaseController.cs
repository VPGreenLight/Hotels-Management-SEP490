﻿using HotelManagement.Application.IServices;
using HotelManagement.Domain.ResponseDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController<TModel, TDto> : ControllerBase
        where TModel : class
        where TDto : class
    {
        protected readonly IBaseService<TModel, TDto> _baseService;

        public BaseController(IBaseService<TModel, TDto> baseService)
        {
            _baseService = baseService;
        }

        [HttpGet("get-all")]
        public virtual async Task<BaseResponseDto<IEnumerable<TDto>>> GetAll()
        {
            var result = await _baseService.GetAllAsync();
            return (result);
        }

        [HttpGet("get-by-id")]
        public virtual async Task<BaseResponseDto<TDto>> GetById([FromQuery] Guid id)
        {
            var result = await _baseService.GetByIdAsync(id);
            return (result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public virtual async Task<BaseResponseDto<bool>> Add([FromBody] TDto dto)
        {
            var result = await _baseService.AddAsync(dto);
            return (result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("update")]
        public virtual async Task<BaseResponseDto<bool>> Update([FromBody] TDto dto)
        {
            var result = await _baseService.UpdateAsync(dto);
            return (result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("delete")]
        public virtual async Task<BaseResponseDto<bool>> Delete([FromBody] Guid id)
        {
            var result = await _baseService.DeleteAsync(id);
            return (result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-count")]
        public virtual async Task<BaseResponseDto<int>> GetCount()
        {
            var result = await _baseService.GetCountAsync();
            return (result);
        }
    }
}
