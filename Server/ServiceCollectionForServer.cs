using Microsoft.Extensions.DependencyInjection;
using Server.Internal;
using Server.Internal.Interfaces;

namespace Server
{
    public static class ServiceCollectionForServer
    {
        public static void ConfigureForServer(this IServiceCollection services)
        {
            services.AddSingleton<IRequestHandlerFactory, RequestHandlerFactory>();
            services.AddSingleton<IServer, HttpServer>();
        }
    }
}
