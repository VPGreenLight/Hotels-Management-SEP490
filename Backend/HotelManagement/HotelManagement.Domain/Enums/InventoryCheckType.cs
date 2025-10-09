namespace HotelManagement.Domain.Models.Enums;

public enum InventoryCheckType
{
    CheckOut,    // Kiểm kê sau khi khách trả phòng
    Periodic,    // Kiểm kê định kỳ
    Incident,    // Kiểm kê do sự cố
    Maintenance  // Kiểm kê trước/sau bảo trì
}