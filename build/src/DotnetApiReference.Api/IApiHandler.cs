namespace DotnetApiReference.Api
{
   using System.Threading.Tasks;
   using Microsoft.AspNetCore.Http;

   public interface IApiHandler
   {
      Task Handle(HttpContext context);
   }
}