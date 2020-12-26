using ClientLogic.ExternalInterfaces;
using Data.Internal;
using Data.Internal.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class ServiceCollectionForData
    {
        public static void ConfigureForData(this IServiceCollection services)
        {
            services.AddSingleton<ITrackerService, TrackerServiceMock>();
            services.AddSingleton<IDeviceContextFactory, DeviceContextFactory>();
            services.AddSingleton<IOperationPreprocessorFactory, OperationPreprocessorFactory>();
        }
    }
}
