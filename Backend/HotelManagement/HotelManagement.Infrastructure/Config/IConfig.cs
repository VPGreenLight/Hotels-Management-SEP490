using Duende.IdentityServer.Models;
using HotelManagement.Infrastructure.Email;

namespace HotelManagement.Infrastructure.Config
{
    public interface IConfig
    {
        IEnumerable<IdentityResource> GetIdentityResources();
        IEnumerable<ApiScope> GetApiScopes();
        IEnumerable<Client> GetClients();
        EmailConfiguration GetEmailConfiguration();
    }
}
