using Microsoft.AspNetCore.Hosting;

namespace DotnetCoreApiReference
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:8080/")
                .UseStartup<Startup>()
                .Build();
            
            host.Run();
        }
    }
}