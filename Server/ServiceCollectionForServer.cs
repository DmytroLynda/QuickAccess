using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Internal;
using Server.Internal.Interfaces;
using Server.Internal.Options;
using Server.Internal.RequestHandlers;
using Server.Internal.Services;
using System.Net;

namespace Server
{
    public static class ServiceCollectionForServer
    {
        public static void ConfigureForServer(this IServiceCollection services, IConfigurationSection configuration)
        {
            services.Configure<HttpServerOptions>(configuration);
            services.AddSingleton<IServer, HttpServer>();

            services.AddSingleton<IAuthenticationService, AuthenticationService>();

            services.AddTransient<IRequestHandlerFactory, RequestHandlerFactory>();

            services.Configure<DownloadFileRequestHandlerOptions>(configuration);
            services.AddTransient<IRequestHandler, DownloadFileRequestHandler>();

            services.AddTransient<IRequestHandler, OpenFolderRequestHandler>();
            
            services.AddTransient<HttpListener>();
        }
    }
}
