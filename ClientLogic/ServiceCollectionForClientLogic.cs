using ClientLogic.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ClientLogic
{
    public static class ServiceCollectionForClientLogic
    {
        public static void ConfigureForClientLogic(this IServiceCollection services)
        {
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<IDeviceService, DeviceService>();
        }
    }
}
