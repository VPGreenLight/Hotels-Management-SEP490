using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace HotelManagement.Application.Utilities;

public static class DiExtensions
{
    public static IServiceCollection RegisterScopedServices(this IServiceCollection services)
    {
        services.RegisterAssemblyPublicNonGenericClasses()
            .Where(c => c.Name.EndsWith("Service"))
            .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
        return services;
    }
}