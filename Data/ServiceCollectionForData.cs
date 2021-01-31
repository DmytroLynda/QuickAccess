using ClientLogic.ExternalInterfaces;
using Data.Internal.Contexts;
using Data.Internal.DataTypes;
using Data.Internal.Factories;
using Data.Internal.Interfaces;
using Data.Internal.Options;
using Data.Internal.Preprocessors;
using DomainEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Data
{
    public static class ServiceCollectionForData
    {
        public static void ConfigureForData(this IServiceCollection services, IConfigurationSection configuration)
        {
            services.AddSingleton<TrackerContextMock>();
            services.AddSingleton<ITrackerService>(serviceProvider => serviceProvider.GetRequiredService<TrackerContextMock>());
            services.AddSingleton<ITrackerContext>(serviceProvider => serviceProvider.GetRequiredService<TrackerContextMock>());

            services.Configure<LocalDeviceOptions>(configuration);
            services.AddSingleton<ILocalDeviceContext, LocalDeviceContext>();

            services.AddTransient<IDeviceContextFactory, DeviceContextFactory>();
            services.AddTransient<IOperationPreprocessorFactory, OperationPreprocessorFactory>();

            services.AddScoped<IOperationPreprocessor<FileRequest, FileChunk>, DownloadFileOperationPreprocessor>();
            services.AddScoped<IOperationPreprocessor<DirectoryPath, List<Path>>, OpenFolderOperationPreprocessor>();

            services.AddScoped<HttpDeviceContext>();
        }
    }
}
