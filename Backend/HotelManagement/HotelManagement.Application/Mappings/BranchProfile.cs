using HotelManagement.Application.Models.Dtos.All;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Mappings;

public class BranchProfile : PaginationProfile
{
    public BranchProfile()
    {
        CreateMap<Branch, BranchDto>().ReverseMap();
    }
}