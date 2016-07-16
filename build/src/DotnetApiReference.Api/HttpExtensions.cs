namespace DotnetApiReference.Api
{
   using System;
   using System.Linq;
   using System.Reflection;
   using System.Threading.Tasks;
   using Microsoft.AspNetCore.Builder;
   using Microsoft.AspNetCore.Http;

   public static class HttpExtensions
   {
      private const string GET = "GET";
      private const string POST = "POST";

      public static void Map(this IApplicationBuilder app, string path, string method, Func<HttpContext, Task> handler)
      {
         app.MapWhen(
            context => context.Request.Path.Value == path && context.Request.Method == method,
            builder =>
            {
               RequestDelegate rd = context => handler(context); 
               builder.Run(rd);
            });
      }

      public static void HandleGet<TApiHandler>(this IApplicationBuilder app, string path)
         where TApiHandler : IApiHandler
      {
         Func<HttpContext, Task> handler = async context =>
         {
            var c = typeof(TApiHandler).GetConstructors().Single();
            var parameters = c.GetParameters();
            var args = new object[parameters.Length];

            //app.ApplicationServices.GetService<TParam>();

            var apiHandler = (TApiHandler)c.Invoke(args);

            await apiHandler.Handle(context);
         };

         Map(app, path, GET, handler);
      }
   }
}