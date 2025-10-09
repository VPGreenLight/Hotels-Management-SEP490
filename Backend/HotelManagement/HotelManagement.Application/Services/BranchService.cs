using System.Linq.Expressions;
using AutoMapper;
using HotelManagement.Application.IServices;
using HotelManagement.Application.Models.Dtos.All;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Exceptions;
using HotelManagement.Domain.Models.Pagination;
using HotelManagement.Infrastructure.Repository;

namespace HotelManagement.Application.Services;

public class BranchService(IRepository<Branch> branchRepo, IRepository<User> userRepo, IMapper mapper) : IBranchService
{
    public async Task AddAsync(BranchDto dto)
    {
        var branch = mapper.Map<Branch>(dto);
        await branchRepo.AddAsync(branch);
    }

    public async Task UpdateAsync(BranchDto dto)
    {
        var branch = await branchRepo.GetByIdAsync(dto.Id);
        if (branch == null)
            throw new NotFoundException("Branch not found");

        if (branch.ManagerId != null)
        {
            var manager = await userRepo.GetByIdAsync(branch.ManagerId);
            if (manager == null)
                throw new NotFoundException("Manager not found");

            branch.ManagerId = manager.Id;
        }

        branch.Name = dto.Name;
        branch.Address = dto.Address;
        branch.TotalRooms = dto.TotalRooms;
        branch.ParkingSlotsCount = dto.ParkingSlotsCount;

        await branchRepo.UpdateAsync(branch);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var branch = await branchRepo.GetByIdAsync(id);
        if (branch == null)
            throw new NotFoundException("Branch not found");
        await branchRepo.DeleteAsync(branch);
    }

    public async Task<Pagination<BranchDto>> GetAllAsync(string query, int pageNumber, int pageSize)
    {
        Expression<Func<Branch, bool>>? predicate = null;

        if (!string.IsNullOrEmpty(query))
            predicate = b =>
                b.Name.Contains(query) || (b.Address != null && b.Address.Contains(query));
        var branches =
            await branchRepo.GetPaginationAsync(filter: predicate, pageNumber: pageNumber, pageSize: pageSize);
        
        return mapper.Map<Pagination<BranchDto>>(branches);
    }

    public async Task<BranchDto> GetByIdAsync(Guid id)
    {
        var branch = await branchRepo.GetByIdAsync(id);
        if (branch == null)
            throw new NotFoundException("Branch not found");
        
        return mapper.Map<BranchDto>(branch);
    }
}