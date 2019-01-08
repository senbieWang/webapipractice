using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AuthClientTest
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();        

        private static async Task MainAsync()
        {
            // discover endpoints from the metadata by calling Auth server hosted on 5000 port
            var discoveryClient = await DiscoveryClient.GetAsync("http://localhost:5000"); //retrieve the OpenID Connect discovery document
            if (discoveryClient.IsError)
            {
                Console.WriteLine(discoveryClient.Error);
                return;
            }

            //客户端模式
            //var tokenClient = new TokenClient(discoveryClient.TokenEndpoint, "client", "secret");
            //var response = await tokenClient.RequestClientCredentialsAsync("api1");  //get token


            //密码模式
            var tokenClient = new TokenClient(discoveryClient.TokenEndpoint, "Client_id_1", "secret");
            var response = await tokenClient.RequestResourceOwnerPasswordAsync("xishuai","123", "api_name1");  //get token

            if (response.IsError)
            {
                Console.WriteLine(response.Error);
                return;
            }

            Console.WriteLine(response.Json);

            //https://neelbhatt.com/2018/03/08/web-api-security-with-identityserver4-identityserver4-with-net-core-part-iii/
            //call api
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);
            var webApiResponse = await client.GetAsync(@"http://localhost:8080/bims/v1.1/iot");
            if (!webApiResponse.IsSuccessStatusCode)
            {
                Console.WriteLine(webApiResponse.StatusCode);
            }
            else
            {
                var content = await webApiResponse.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }

        }
    }
}
