using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerMVC
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API"),
                //new ApiResource
                //{
                //    Name = "Complicated_API",
                //    DisplayName = "Complicated API",
                //    UserClaims  = { "name", "email" },
                //    Scopes =
                //    {
                //        new Scope("full_access")
                //        {
                //            UserClaims = { "role"}
                //        },
                //        new Scope("read_only")
                //    }
                //}
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientName = "Console App",
                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                new Client
                {
                    ClientId = "mvc",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireConsent = false,
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    // where to redirect to after login
                    RedirectUris = { "http://localhost:5002/signin-oidc" },
                    //RedirectUris = { "http://localhost:5002/Home/Contact" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                    AllowOfflineAccess = true
                }
            };
        }

        //public static List<TestUser> GetUsers()
        //{
        //    return new List<TestUser>
        //    {
        //        new TestUser
        //        {
        //            SubjectId = "1",
        //            Username = "alice",
        //            Password = "password",
        //            Claims =
        //            {
        //                new System.Security.Claims.Claim("name","Alice in Wonderland"),
        //                new System.Security.Claims.Claim("email","alice@wonderland"),
        //                new System.Security.Claims.Claim("location","USA")
        //            }
        //        },
        //        new TestUser
        //        {
        //            SubjectId = "2",
        //            Username = "bob",
        //            Password = "password",
        //            Claims = new []
        //            {
        //                new System.Security.Claims.Claim("name", "Bob"),
        //                new System.Security.Claims.Claim("website", "https://bob.com")
        //            }
        //        }
        //    };
        //}

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }
    }
}
