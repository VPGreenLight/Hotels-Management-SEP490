namespace HotelManagement.Application.Models.Dtos.RequestDtos;

public class BaseRequestDto<T>(T data) where T : class
{
    public T Data { get; set; } = data;
    public long Timestamp { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
}