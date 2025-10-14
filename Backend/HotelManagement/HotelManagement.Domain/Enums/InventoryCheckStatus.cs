namespace HotelManagement.Domain.Models.Enums;

public enum InventoryCheckStatus
{
    InProgress,
    PendingApproval,
    Approved,
    RequiresAction, // Cần xử lý (mất, hỏng)
    Completed
}