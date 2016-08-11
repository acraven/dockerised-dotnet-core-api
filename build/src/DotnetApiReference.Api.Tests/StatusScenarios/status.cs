namespace DotnetApiReference.Api.Tests.StatusScenarios
{
   using System;
   using System.Net;
   using System.Net.Http;
   using Newtonsoft.Json;
   using Banshee;
   using Bivouac.Abstractions;
   using Bivouac.Model;
   using Bivouac.Services;
   using Microsoft.Extensions.DependencyInjection;
   using Xunit;

   public class status
   {
      private readonly HttpResponseMessage _response;

      public status()
      {
         Action<IServiceCollection> configureServices = services =>
         {
            Startup.ConfigureServices(services);
            services.AddSingleton<IServerLoggingService, NoOpServerLoggingService>();
         };
         var testHost = new WebApiTestHost(configureServices, Startup.Configure);

         _response = testHost.Get("/status");
      }

      [Fact]
      public void should_return_status_code_200()
      {
         Assert.Equal(_response.StatusCode, HttpStatusCode.OK);
      }

      [Fact]
      public void should_return_status_content()
      {
         var content = _response.Content.ReadAsStringAsync().Result;
         var status = JsonConvert.DeserializeObject<Status>(content);

         Assert.Equal("dotnet-api-reference", status.Name);
         Assert.Equal(Availability.Available, status.Availability);
      }
   }
}
