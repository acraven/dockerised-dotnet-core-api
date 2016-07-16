namespace DotnetApiReference.Api.Tests
{
   using System;
   using System.Net.Http;
   using Microsoft.AspNetCore.Hosting;
   using Microsoft.AspNetCore.TestHost;
   using Microsoft.Extensions.DependencyInjection;

   public abstract class ApiBase
   {
      public Action<IServiceCollection> ConfigureServices { get; set; }

      protected HttpResponseMessage Get(string uri)
      {
         using (var server = CreateTestServer())
         using (var httpClient = server.CreateClient())
         {
            return httpClient.GetAsync(uri).Result;
         }
      }

      protected HttpResponseMessage Post(string uri, string body)
      {
         using (var server = CreateTestServer())
         using (var httpClient = server.CreateClient())
         {
            return httpClient.PostAsync(uri, new StringContent(body)).Result;
         }
      }

      private TestServer CreateTestServer()
      {
         var builder = new WebHostBuilder()
            .ConfigureServices(services =>
            {
               ConfigureServices?.Invoke(services);
               Startup.ConfigureServices(services);
            })
            .Configure(Startup.ConfigureApp);

         var testServer = new TestServer(builder);
         return testServer;
      }
   }
}
