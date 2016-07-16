namespace DotnetApiReference.Api.Tests.PingScenarios
{
   using System.Net;
   using System.Net.Http;
   using Xunit;

   public class ping : ApiBase
   {
      private readonly HttpResponseMessage _response;

      public ping()
      {
         _response = Get("/ping");
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
