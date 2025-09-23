using Microsoft.AspNetCore.Http;

namespace HotelManagement.Infrastructure.R2Storage
{
    public interface IR2StorageService
    {
        Task<string> UploadFileAsync(IFormFile file, string keyPrefix, string fileName);
    }
}
