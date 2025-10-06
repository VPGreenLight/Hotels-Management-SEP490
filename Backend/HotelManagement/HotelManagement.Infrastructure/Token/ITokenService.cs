using HotelManagement.Domain.Models;
using HotelManagement.Domain.Models.Entities;

namespace HotelManagement.Infrastructure.Token
{
    public interface ITokenService
    {
        Task<string> CreateAccessTokenAsync(User user);
        Task<string> CreateRefreshTokenAsync(User user);
        Task<string> RefreshAccessTokenAsync(string refreshToken);
    }
}
