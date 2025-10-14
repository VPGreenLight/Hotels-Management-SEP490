using HotelManagement.Application.Models.Dtos.All;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Models.Pagination;

namespace HotelManagement.Application.IServices;

public interface IBranchService
{
    Task AddAsync(BranchDto dto);
    Task UpdateAsync(BranchDto dto);
    Task DeleteByIdAsync(Guid id);
    Task<Pagination<BranchDto>> GetAllAsync(string query, int page, int pageSize);
    Task<BranchDto> GetByIdAsync(Guid id);
}