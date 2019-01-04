using IdentityModel.Client;
using System;
using System.Net.Http;

namespace ResourceOwnerPasswords.Client
{
    class Program
    {
        static void Main(string[] args) => AnsycMain();

        public static async void AnsycMain()
        {
            var client = new HttpClient();

            try
            {

                var disco = await client.GetDiscoveryDocumentAsync("https://localhost:6001");
                if (disco.IsError) throw new Exception(disco.Error);


                var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disco.TokenEndpoint,
                    ClientId = "Client1",
                    ClientSecret = "clientSecrets",
                    Scope = "Api1",
                    UserName = "user1",
                    Password = "pwdUser1"
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
                var responseApi = await client.GetAsync("https://localhost:5001/api");
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
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
