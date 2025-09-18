using Duende.IdentityServer.Models;
using HotelManagement.Services.Email;

namespace HotelManagement.Services.Config
{
    public interface IConfig
    {
        IEnumerable<IdentityResource> GetIdentityResources();
        IEnumerable<ApiScope> GetApiScopes();
        IEnumerable<Client> GetClients();
        EmailConfiguration GetEmailConfiguration();
    }
}
