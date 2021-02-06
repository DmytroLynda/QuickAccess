using ClientLogic.ExternalInterfaces;
using Data.Internal.Contexts;
using Data.Internal.Interfaces;
using DomainEntities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Data.Internal.Factories
{
    internal class DeviceContextFactory : IDeviceContextFactory
    {
        private readonly ILogger<DeviceContextFactory> _logger;
        private readonly ITrackerService _tracker;
        private readonly IServiceProvider _serviceProvider;

        public DeviceContextFactory(ILogger<DeviceContextFactory> logger, ITrackerService tracker, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _tracker = tracker;
            _serviceProvider = serviceProvider;
        }

        public async Task<IDeviceContext> GetDeviceContext(Device device)
        {
            var deviceAddress = await _tracker.GetDeviceUriAsync(device);

            var context = _serviceProvider.GetRequiredService<HttpDeviceContext>();
            context.DeviceAddress = deviceAddress;

            return context;
        }
    }
}
