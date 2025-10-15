using HotelManagement.Application.IServices;
using HotelManagement.Application.Models.Dtos.All;
using HotelManagement.Application.Models.Dtos.RequestDtos;
using HotelManagement.Application.Models.Dtos.ResponseDtos;
using HotelManagement.Domain.Exceptions;
using HotelManagement.Domain.Models.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers;

// [Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class BranchController(IBranchService branchService) : BaseController
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var branch = await branchService.GetByIdAsync(id);

            return Ok(new BaseResponseDto<BranchDto>
            {
                Status = 200,
                ResponseData = branch,
                Message = $"Get branch {id} successfully"
            });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new BaseResponseDto<string>
            {
                Message = ex.Message,
                Status = 404
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new BaseResponseDto<string>
            {
                Message = ex.Message,
                Status = 500
            });
        }
    }

    [HttpPost("filter")]
    public async Task<IActionResult> GetByFilter([FromBody] BaseRequestDto<GetBranchDto> requestDto)
    {
        if (requestDto.Data.PageNumber <= 0)
            return BadRequest(new BaseResponseDto<string>
            {
                Status = 400,
                ResponseData = "PageNumber cannot be less or equal than zero",
            });

        if (requestDto.Data.PageSize <= 0 && requestDto.Data.PageSize > 10000)
        {
            return BadRequest(new BaseResponseDto<string>
            {
                Status = 400,
                ResponseData = "Invalid PageSize",
            });
        }
        
        var result = await branchService.GetAllAsync(requestDto.Data.Query, requestDto.Data.PageNumber, requestDto.Data.PageSize);
        return Ok(new BaseResponseDto<Pagination<BranchDto>>
        {
            Status = 200,
            ResponseData = result,
            Message = "Get branches successfully!"
        });
    }

    [HttpPost]
    public async Task<IActionResult> AddBranch([FromBody] BranchDto branch)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(branch.Name))
            {
                return BadRequest(new BaseResponseDto<string>
                {
                    Status = 400,
                    Message = "Name is required"
                });
            }

            await branchService.AddAsync(branch);

            return Ok(new BaseResponseDto<string>
            {
                Status = 201,
                Message = "Branch created successfully!"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new BaseResponseDto<string>
            {
                Message = ex.Message,
                Status = 500
            });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBranch([FromBody] BranchDto branch)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(branch.Name))
                return BadRequest(new BaseResponseDto<string>
                {
                    Status = 400,
                    Message = "Name is required"
                });

            await branchService.UpdateAsync(branch);

            return Ok(new BaseResponseDto<string>
            {
                Status = 200,
                Message = "Branch updated successfully!"
            });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new BaseResponseDto<string>
            {
                Status = 404,
                Message = ex.Message,
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new BaseResponseDto<string>
            {
                Status = 500,
                Message = ex.Message,
            });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteBranch(Guid id)
    {
        try
        {
            await branchService.DeleteByIdAsync(id);
            return Ok(new BaseResponseDto<string>
            {
                Status = 200,
                Message = "Branch deleted successfully!"
            });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new BaseResponseDto<string>
            {
                Status = 404,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new BaseResponseDto<string>
            {
                Status = 500,
                Message = ex.Message
            });
        }
    }
}