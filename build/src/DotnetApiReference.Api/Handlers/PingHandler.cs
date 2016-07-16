namespace DotnetApiReference.Api.Handlers
{
   using System.Threading.Tasks;
   using Microsoft.AspNetCore.Http;

   public class PingHandler : IApiHandler
   {
      public async Task Handle(HttpContext context)
      {
         await context.Response.WriteAsync("Pong!");
      }
   }
}
