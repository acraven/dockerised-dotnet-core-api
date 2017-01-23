namespace DotnetApiReference
{
   using Microsoft.AspNetCore.Hosting;

   public class Program
   {
      public static void Main(string[] args)
      {
         var host = new WebHostBuilder()
             .UseKestrel()
             .UseUrls("http://*:9000/")
             .ConfigureServices(Startup.ConfigureServices)
             .Configure(Startup.Configure)
             .Build();

         host.Run();
      }
   }
}
