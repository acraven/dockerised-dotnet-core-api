namespace DotnetApiReference.Api.Tests.StatusScenarios
{
   using System;
   using System.Net;
   using System.Net.Http;
   using Banshee;
   using Bivouac.Abstractions;
   using Bivouac.Services;
   using Microsoft.Extensions.DependencyInjection;
   using Xunit;

   public class ping
   {
      private readonly HttpResponseMessage _response;

      public ping()
      {
         Action<IServiceCollection> configureServices = services =>
         {
            Startup.ConfigureServices(services);
            services.AddSingleton<IServerLoggingService, NoOpServerLoggingService>();
         };
         var testHost = new WebApiTestHost(configureServices, Startup.Configure);

         _response = testHost.Get("/ping");
      }

      [Fact]
      public void should_return_status_code_200()
      {
         Assert.Equal(_response.StatusCode, HttpStatusCode.OK);
      }

      [Fact]
      public void should_return_content_pong()
      {
         var content = _response.Content.ReadAsStringAsync().Result;

         Assert.Equal(content, "Pong!");
      }
   }
}
