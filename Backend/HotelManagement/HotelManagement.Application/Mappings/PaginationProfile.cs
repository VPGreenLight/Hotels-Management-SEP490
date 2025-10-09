using HotelManagement.Domain.Models.Pagination;

namespace HotelManagement.Application.Mappings;

public class PaginationProfile : MapProfile
{
    public PaginationProfile()
    {
        CreateMap(typeof(Pagination<>), typeof(Pagination<>));
    }
}