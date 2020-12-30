using ClientLogic.ExternalInterfaces;
using Data.Internal.Interfaces;
using DomainEntities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Data.Internal
{
    internal class DeviceContextFactory : IDeviceContextFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DeviceContextFactory> _logger;

        public DeviceContextFactory(ILogger<DeviceContextFactory> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<IDeviceContext> GetDeviceContext(Device device)
        {
            var logger = _serviceProvider.GetRequiredService<ILogger<HttpDeviceContext>>();

            var tracker = _serviceProvider.GetRequiredService<ITrackerService>();
            var deviceAddress = await tracker.GetDeviceUriAsync(device);

            var preprocessorFactory = _serviceProvider.GetRequiredService<IOperationPreprocessorFactory>();

            return new HttpDeviceContext(logger, deviceAddress, device, preprocessorFactory);
        }

        //TODO: Add configuration file and get in LocalDeviceContext IConfigurationProvider. Arter get save path from it.
        public ILocalDeviceContext GetLocalDevice()
        {
            return new LocalDeviceContext(new DirectoryPath(@"C:\Users\Dmytro\Desktop\Dyplom\"));
        }
    }
}
