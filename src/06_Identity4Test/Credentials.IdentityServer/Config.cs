using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Credentials.IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource( "Api1" ,"My Protected Api" )
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId ="Client1",
                    AllowedGrantTypes=GrantTypes.ClientCredentials,

                    ClientName="My Client1",
                    ClientSecrets = { new Secret("mySecrets".Sha256())},
                    AllowedScopes={ "Api1"}
                }
            };
        }
    }
}
