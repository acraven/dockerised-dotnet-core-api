using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Banshee;
using Bivouac.Abstractions;
using Bivouac.Events;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DotnetCoreApiReference.Tests.PingScenarios
{
    public class HappyPath
    {
        private HttpResponseMessage _response;

        [OneTimeSetUp]
        public void SetupScenario()
        {
            var startup = new Startup();

            void ConfigureServices(IServiceCollection services)
            {
                startup.ConfigureServices(services);
                services.AddTransient<IHttpServerEventCallback, NoOpHttpServerEventCallback>();
            }

            var testHost = new LightweightWebApiHost(ConfigureServices, startup.Configure);

            _response = testHost.Get("/ping");
        }

        [Test]
        public void should_return_status_code_200()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task should_return_content_pong()
        {
            var content = await _response.Content.ReadAsStringAsync();

            Assert.That(content, Is.EqualTo("Pong!"));
        }
    }
}