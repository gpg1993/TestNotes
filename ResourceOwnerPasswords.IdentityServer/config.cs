using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace ResourceOwnerPasswords.IdentityServer
{
    public class config
    {
        public static IEnumerable<ApiResource> GetResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("Api1", "my api1")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client{
                    ClientId ="Client1",
                    ClientName ="my client1",
                    ClientSecrets=
                    {
                        new Secret("clientSecrets".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes={"Api1" }
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser{ Username="user1", SubjectId ="subject1", Password = "pwdUser1" }
            };
        }
    }
}
