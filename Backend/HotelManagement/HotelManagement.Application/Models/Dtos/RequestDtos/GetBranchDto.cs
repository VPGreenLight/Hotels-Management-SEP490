namespace HotelManagement.Application.Models.Dtos.RequestDtos;

public class GetBranchDto
{
    public string Query { get; set; } = string.Empty;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}