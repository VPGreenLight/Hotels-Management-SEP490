using HotelManagement.Domain.Dtos;
using HotelManagement.Domain.Models.Enums;

namespace HotelManagement.Application.Models.Dtos.All;

public class BranchDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;

    public string? Address { get; set; }
    
    public Guid? ManagerId { get; set; }
    // public virtual UserDto? Manager { get; set; }

    public BranchStatus Status { get; set; }

    public int TotalRooms { get; set; }

    public int ParkingSlotsCount { get; set; }
}