using ClientLogic.ExternalInterfaces;
using Data.Internal.Interfaces;
using DomainEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Data.Internal
{
    internal class DeviceContextFactory : IDeviceContextFactory
    {
        private readonly ILogger<IDeviceContextFactory> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ITrackerService _tracker;
        private readonly IOperationPreprocessorFactory _preprocessorFactory;

        public DeviceContextFactory(ILoggerFactory loggerFactory, ITrackerService tracker, IOperationPreprocessorFactory preprocessorFactory)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _tracker = tracker ?? throw new ArgumentNullException(nameof(tracker));
            _preprocessorFactory = preprocessorFactory ?? throw new ArgumentNullException(nameof(preprocessorFactory));
            _logger = _loggerFactory.CreateLogger<IDeviceContextFactory>();
        }

        public async Task<IDeviceContext> GetDeviceContext(Device device)
        {
            var deviceContextLogger = _loggerFactory.CreateLogger<IDeviceContext>();
            var deviceAddress = await _tracker.GetDeviceUriAsync(device);

            return new HttpDeviceContext(deviceContextLogger, deviceAddress, device, _preprocessorFactory);
        }

        public ILocalDeviceContext GetLocalDevice()
        {
            throw new NotImplementedException();
        }
    }
}
