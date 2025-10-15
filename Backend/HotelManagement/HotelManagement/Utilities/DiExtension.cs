using HotelManagement.Application.Utilities;
using HotelManagement.Infrastructure.Email;
using HotelManagement.Infrastructure.R2Storage;
using HotelManagement.Infrastructure.Services;
using HotelManagement.Infrastructure.Token;
using HotelManagement.Infrastructure.Utilities;

namespace HotelManagement.API.Utilities;

public static class DiExtension
{
    public static IServiceCollection AddDiServices(this IServiceCollection services)
    {
        services.RegisterScopedRepositories();
        services.RegisterScopedServices();

        return services;
    }
}