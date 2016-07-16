namespace DotnetApiReference.Api.Tests.VersionScenarios
{
   using System.Net;
   using System.Net.Http;
   using Xunit;

   public class version : ApiBase
   {
      private readonly HttpResponseMessage _response;

      public version()
      {
         _response = Get("/version");
      }

      [Fact]
      public void should_return_status_code_200()
      {
         Assert.Equal(_response.StatusCode, HttpStatusCode.OK);
      }

      [Fact]
      public void should_return_content_1_0_0()
      {
         var content = _response.Content.ReadAsStringAsync().Result;

         Assert.Equal(content, "1.0.0");
      }
   }
}
