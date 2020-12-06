using ClientLogic.ExternalInterfaces;
using DomainEntities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DeviceContextFactory : IDeviceContextFactory
    {
        private readonly ILogger<IDeviceContextFactory> _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ITrackerService _tracker;

        public DeviceContextFactory(ILoggerFactory loggerFactory, ITrackerService tracker)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _tracker = tracker ?? throw new ArgumentNullException(nameof(tracker));

            _logger = _loggerFactory.CreateLogger<IDeviceContextFactory>();
        }

        public IDeviceContext GetDeviceContext(Device device)
        {
            var deviceContextLogger = _loggerFactory.CreateLogger<IDeviceContext>();
            var deviceAddress = _tracker.GetDevice(device);
            var httpClient = new HttpClient();

            return new DeviceContext(deviceContextLogger, httpClient, deviceAddress);
        }

        public ILocalDeviceContext GetLocalDevice()
        {
            throw new NotImplementedException();
        }
    }
}
