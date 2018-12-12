using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        const string authority = "http://localhost:5000";
        const string clientId = "client";
        const string clientSecret = "secret";
        const string scope = "api1";
        const string apiUrl = "http://localhost:5001/api/values";

        static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        public static async Task MainAsync()
        {
            Console.Title = "Console Client";

            var token = await GetTokenAsync();
            Console.WriteLine();

            await CallApiAsync(token);
        }

        private static async Task<string> GetTokenAsync()
        {
            var disco = new DiscoveryClient(authority);
            var discoResponse = await disco.GetAsync();
            if (discoResponse.IsError)
            {
                Console.WriteLine("Disco Error: {0}", discoResponse.Error);
                return null;
            }

            var tokenClient = new TokenClient(discoResponse.TokenEndpoint, clientId, clientSecret);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync(scope);
            if (tokenResponse.IsError)
            {
                Console.WriteLine("Token Endpoint error: {0}", tokenResponse.Error);
                return null;
            }

            Console.WriteLine("Success obtaining an access token");
            return tokenResponse.AccessToken;
        }

        private static async Task CallApiAsync(string token)
        {
            using (var client = new HttpClient())
            {
                //setup client
                //client.BaseAddress = new Uri(authority);
                //client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                //make request
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                var responseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseString);
            }
        }
    }
}
