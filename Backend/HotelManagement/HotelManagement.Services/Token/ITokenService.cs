namespace HotelManagement.Services.Token
{
    public interface ITokenService
    {
        Task<string> CreateAccessTokenAsync(User user);
        Task<string> CreateRefreshTokenAsync(User user);
        Task<string> RefreshAccessTokenAsync(string refreshToken);
    }
}
