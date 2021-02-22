using Microsoft.Extensions.DependencyInjection;
using ServerLogic.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic
{
    public static class ServiceCollectionForServerLogic
    {
        public static void ConfigureForServerLogic(this IServiceCollection services)
        {
            services.AddTransient<FileService, FileService>();

            services.AddTransient<IDownloadFileService>(service => service.GetRequiredService<FileService>());
            services.AddTransient<IOpenDirectoryService>(service => service.GetRequiredService<FileService>());
            services.AddTransient<IFileInfoService>(services => services.GetRequiredService<FileService>());

            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }
    }
}
