using Microsoft.Extensions.DependencyInjection;
using Server.Internal;
using Server.Internal.Interfaces;
using Server.Internal.RequestHandlers;

namespace Server
{
    public static class ServiceCollectionForServer
    {
        public static void ConfigureForServer(this IServiceCollection services)
        {
            services.AddTransient<IRequestHandlerFactory, RequestHandlerFactory>();
            services.AddSingleton<IServer, HttpServer>();

            services.AddScoped<IRequestHandler, DownloadFileRequestHandler>();
        }
    }
}
