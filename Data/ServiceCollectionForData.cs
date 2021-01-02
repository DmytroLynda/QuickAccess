using ClientLogic.ExternalInterfaces;
using Data.Internal;
using Data.Internal.Interfaces;
using Data.Internal.Preprocessors;
using DomainEntities;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class ServiceCollectionForData
    {
        public static void ConfigureForData(this IServiceCollection services)
        {
            services.AddSingleton<ITrackerService, TrackerServiceMock>();
            services.AddSingleton<ITrackerContext, TrackerServiceMock>();

            services.AddTransient<IDeviceContextFactory, DeviceContextFactory>();
            services.AddTransient<IOperationPreprocessorFactory, OperationPreprocessorFactory>();

            services.AddScoped<IOperationPreprocessor<FilePath, DomainEntities.File>, DownloadFileOperationPreprocessor>();
        }
    }
}
