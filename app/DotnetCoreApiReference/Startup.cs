using System;
using Bivouac.Abstractions;
using Bivouac.Events;
using Bivouac.Middleware;
using Burble.Abstractions;
using Burble.EventCallbacks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetCoreApiReference
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddStatusEndpointServices("dotnet-api-reference");
            services.AddServerLoggingServices();

            services.AddTransient<IHttpServerEventCallback>(CreateHttpServerEventCallback);
            services.AddTransient<IHttpClientEventCallback>(CreateHttpClientEventCallback);

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseServerLoggingMiddleware();
            app.UseStatusEndpointMiddleware();

            app.UseMvc();
        }

        private IHttpServerEventCallback CreateHttpServerEventCallback(IServiceProvider serviceProvider)
        {
            var requestIdGetter = serviceProvider.GetService<IGetRequestId>();
            var correlationIdGetter = serviceProvider.GetService<IGetCorrelationId>();
            var assemblyVersionGetter = serviceProvider.GetService<IGetAssemblyVersion>();

            return new IdentifyingHttpServerEventCallback(
                requestIdGetter,
                correlationIdGetter,
                assemblyVersionGetter,
                new JsonHttpServerEventCallback(Console.WriteLine));
        }

        private IHttpClientEventCallback CreateHttpClientEventCallback(IServiceProvider serviceProvider)
        {
           return new JsonHttpClientEventCallback(Console.WriteLine);
        }
    }
}
