namespace DotnetApiReference.Api
{
   using System;
   using DotnetApiReference.Api.Handlers;
   using Microsoft.AspNetCore.Builder;
   using Microsoft.Extensions.DependencyInjection;

   public static class Startup
   {
      public static void ConfigureServices(IServiceCollection services)
      {
      }

      public static void ConfigureApp(IApplicationBuilder app)
      {
         if (app == null) throw new ArgumentNullException(nameof(app));

         app.HandleGet<PingHandler>("/ping");
         app.HandleGet<VersionHandler>("/version");
      }
   }
}
