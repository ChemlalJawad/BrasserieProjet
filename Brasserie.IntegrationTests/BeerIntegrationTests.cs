using Brasserie.Service.Beers;
using Brasserie.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Brasserie.IntegrationTests
{
    public class BeerIntegrationTests
    {
        private readonly HttpClient _client;
        private readonly TestServer _server;

        public BeerIntegrationTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Theory]
        [InlineData("GET")]
        public async Task TestBeerAsync(string method) 
        {
            var request = new HttpRequestMessage(new HttpMethod(method), "api/beers");
            
            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Theory]
        [InlineData("GET")]
        public async Task TestOneBeerAsync(string method, int? id = null) 
        {
            var request = new HttpRequestMessage(new HttpMethod(method), $"api/beers/{id}");
            
            var response = await _client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

        [Fact]
        public async Task TestAddBeerAsync() 
        {           
            var response = await _client.PostAsync("/api/beers", 
                new StringContent(JsonConvert.SerializeObject(new CreateBeerCommand()
                {
                    AlcoholPercentage = 1.00,
                    Name = "Jaja",
                    Price = 70,
                    BrewerId = 1
                }),
                 Encoding.UTF8,
                "application/json"));

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}
