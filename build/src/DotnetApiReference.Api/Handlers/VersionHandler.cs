namespace DotnetApiReference.Api.Handlers
{
   using System.Threading.Tasks;
   using Microsoft.AspNetCore.Http;

   public class VersionHandler : IApiHandler
   {
      public async Task Handle(HttpContext context)
      {
         await context.Response.WriteAsync("1.0.0");
      }
   }
}