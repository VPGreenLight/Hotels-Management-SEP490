using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace HotelManagement.Infrastructure.Utilities;

public static class DiExtensions
{
    public static IServiceCollection RegisterScopedRepositories(this IServiceCollection services)
    {
        services.RegisterAssemblyPublicNonGenericClasses()
            .Where(c => c.Name.EndsWith("Repository"))
            .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);
        return services;
    }
}