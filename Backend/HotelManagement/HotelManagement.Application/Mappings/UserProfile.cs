using HotelManagement.Domain.Dtos;
using HotelManagement.Domain.Entities;

namespace HotelManagement.Application.Mappings;

public class UserProfile : PaginationProfile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
    }
}