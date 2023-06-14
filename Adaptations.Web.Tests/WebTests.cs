using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Adaptations.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace AspNetCoreTemplate.Web.Tests
{
    public class WebTests
    {
        private readonly TestServer server;
        private readonly HttpClient client;

        public WebTests()
        {
            this.server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            this.client = this.server.CreateClient();
        }

        [Fact(Skip = "Example test. Disabled for CI.")]
        public async Task IndexPageShouldReturnStatusCode200WithTitle()
        {
            var response = await this.client.GetAsync("/");
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.Contains("<title>", responseContent);
        }

        [Fact(Skip = "Example test. Disabled for CI.")]
        public async Task AccountManagePageRequiresAuthorization()
        {
            var response = await this.client.GetAsync("Identity/Account/Manage");
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        }
    }
}