using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
    public class ConfiguracionIdentity
    {
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {                
                new ApiScope("ApiServicio", "Api de apoyo a la principal")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client
                {
                    ClientId = "Api_Principal",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("1234567890asdfghjkl1234567890asdfghjkl".Sha256())
                    },
                    AllowedScopes =
                    {
                        "ApiServicio"
                    }
                }
            };
        }
    }
}
