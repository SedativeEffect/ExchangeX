using System.Collections.Generic;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer
{
    public static class ConfigurationIS4
    {
        public static IEnumerable<Client> GetClients() =>
            new List<Client>()
            {
                new Client {
                    ClientName = "vuejs_code_client",
                    ClientId = "vuejs_code_client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequireConsent = false,

                    RedirectUris = new List<string>
                    {
                        "https://localhost:44357/signin-callback",
                    },
                    PostLogoutRedirectUris = new List<string> {
                        "https://localhost:44357",
                    },
                    AllowedCorsOrigins = new List<string>{
                        "https://localhost:44357"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "profile",
                        "email",
                        "TestAPI"
                    }
                }
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource{
                    Name = "TestAPI",
                    Scopes = new List<string> {"TestAPI"},
                }
            };

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope {
                    Name = "TestAPI",
                }
            };
    }
}
