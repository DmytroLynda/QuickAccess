using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServerInterface.Internal;
using ServerInterface.Internal.Interfaces;
using ServerInterface.Internal.Options;
using ServerInterface.Internal.RequestHandlers;
using ServerInterface.Internal.Services;
using System.Net;

namespace ServerInterface
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
            services.AddTransient<IRequestHandler, GetFileInfoRequestHandler>();

            services.AddTransient<HttpListener>();
        }
    }
}
