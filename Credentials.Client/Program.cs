using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Credentials.Client
{
    class Program
    {
        static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        private static async Task MainAsync(string[] args)
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:6001");
            if (disco.IsError) throw new Exception(disco.Error);

            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                ClientId = "Client1",
                ClientSecret = "mySecrets",
                Scope = "Api1"
            });
            if (response.IsError)
            {
                Console.WriteLine(response.HttpStatusCode);
            }
            else
            {
                var strResponseData = response.Json;
                Console.WriteLine(strResponseData);
            }

            client.SetBearerToken(response.AccessToken);
            var responseApi = await client.GetAsync("https://localhost:5001/values");
            //var responseApi = await client.GetAsync("https://localhost:5001/identity");
            if (responseApi.IsSuccessStatusCode)
            {
                var data = await responseApi.Content.ReadAsStringAsync();
                Console.WriteLine(data);
            }
            else
            {
                Console.WriteLine(responseApi.StatusCode);
            }

        }
    }
}
