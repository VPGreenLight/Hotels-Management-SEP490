using HotelManagement.Application.IAspModelService;
using HotelManagement.Application.IServices;
using HotelManagement.Infrastructure.Converter;
using HotelManagement.Infrastructure.Email;
using HotelManagement.Infrastructure.R2Storage;
using HotelManagement.Infrastructure.Repository;
using HotelManagement.Infrastructure.Services;
using HotelManagement.Infrastructure.Token;
using HotelManagement.Services.AspModelService;
using HotelManagement.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HotelManagement.Application
{
    public static class _Imports
    {
        public static void AddServiceCollections(this IServiceCollection service)
        {
            // Import các service vào hệ thống theo cấu trúc đã cho
            service.AddScoped(typeof(IConverter<,>), typeof(Infrastructure.Converter.Converter<,>));
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

