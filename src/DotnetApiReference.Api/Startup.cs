namespace DotnetApiReference.Api
{
   using System;
   using System.Net.Http;
   using Bivouac.Middleware;
   using Microsoft.AspNetCore.Builder;
   using Microsoft.Extensions.DependencyInjection;

   public static class Startup
   {
      public static void ConfigureServices(IServiceCollection services)
      {
         if (services == null) throw new ArgumentNullException(nameof(services));

         services.AddStatusEndpointServices("dotnet-api-reference");
         services.AddServerLoggingServices();

         services.AddSingleton<HttpMessageHandler, LoggingHttpClientHandler>();

         services.AddMvc();
      }

      public static void Configure(IApplicationBuilder app)
      {
         if (app == null) throw new ArgumentNullException(nameof(app));

         app.UseServerLoggingMiddleware();
         app.UseStatusEndpointMiddleware();

         app.UseMvc();
      }
   }
}
