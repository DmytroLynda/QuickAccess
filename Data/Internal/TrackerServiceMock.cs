using ClientLogic.ExternalInterfaces;
using Data.Internal.Interfaces;
using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data
{
    internal class TrackerServiceMock : ITrackerService, ITrackerContext
    {
        private readonly List<Device> _devices = new List<Device>();
        private readonly Dictionary<Device, Uri> _uris = new Dictionary<Device, Uri>();

        public TrackerServiceMock()
        {
            _devices.Add(new Device { Id = 1, Name = "Dima" });
            _devices.Add(new Device { Id = 2, Name = "Julia" });

            var dimaUri = new UriBuilder("http", "127.0.0.1", 65432);
            var juliaUri = new UriBuilder("http", "192.168.0.220", 8080);

            _uris.Add(_devices.First(), dimaUri.Uri);
            _uris.Add(_devices.Skip(1).First(), juliaUri.Uri);
        }

        public async Task<Uri> GetDeviceUriAsync(Device device)
        {
            return await Task.FromResult(_uris[device]);
        }

        public async Task<List<Device>> GetDevicesAsync()
        {
            return await Task.FromResult(_devices);
        }
    }
}
