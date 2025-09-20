global using HotelManagement.Common.Constant;
global using HotelManagement.Common.Enum;
global using HotelManagement.Entities;
global using HotelManagement.Models.Dtos;
global using HotelManagement.Models.RequestDtos;
global using HotelManagement.Models.ResponseDtos;
global using HotelManagement.Services.IServices;
using HotelManagement.EntityFramework.Repository;
using HotelManagement.Services.AspModelService;
using HotelManagement.Services.Converter;
using HotelManagement.Services.Email;
using HotelManagement.Services.IAspModelService;
using HotelManagement.Services.R2Storage;
using HotelManagement.Services.Services;
using HotelManagement.Services.Token;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.Services
{
    public static class _Imports
    {
        public static void AddServiceCollections(this IServiceCollection service)
        {
            // Import các service vào hệ thống theo cấu trúc đã cho
            service.AddScoped(typeof(IConverter<,>), typeof(Converter.Converter<,>));
            service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            service.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
            service.AddScoped<ITokenService, TokenService>();
            service.AddScoped<IR2StorageService, R2StorageService>();
            service.AddScoped<IEmailService, EmailService>();
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAuthenticationService, AuthenticationService>();
        }
    }
}

