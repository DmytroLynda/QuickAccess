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
        private readonly ILoggerFactory _loggerFactory;
        private readonly ITrackerService _tracker;
        private readonly IOperationPreprocessorFactory _preprocessorFactory;

        public DeviceContextFactory(ILoggerFactory loggerFactory, ITrackerService tracker, IOperationPreprocessorFactory preprocessorFactory)
        {
            _logger = loggerFactory.CreateLogger<DeviceContextFactory>();
            _loggerFactory = loggerFactory;
            _tracker = tracker;
            _preprocessorFactory = preprocessorFactory;
        }

        public async Task<IDeviceContext> GetDeviceContext(Device device)
        {
            var deviceAddress = await _tracker.GetDeviceUriAsync(device);

            return new HttpDeviceContext(_loggerFactory.CreateLogger<HttpDeviceContext>(), deviceAddress, device, _preprocessorFactory);
        }
    }
}
