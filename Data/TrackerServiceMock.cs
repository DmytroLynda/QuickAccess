using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TrackerServiceMock : ITrackerService
    {
        private readonly Dictionary<Device, Uri> _devices = new Dictionary<Device, Uri>();

        public TrackerServiceMock()
        {
            var dimaUri = new UriBuilder("http", "192.168.0.213", 8080);
            var juliaUri = new UriBuilder("http", "192.168.0.220", 8080);

            _devices.Add(new Device { Id = 1, Name = "Dima" }, dimaUri.Uri);
            _devices.Add(new Device { Id = 2, Name = "Julia" }, juliaUri.Uri);
        }

        public async Task<Uri> GetDeviceUriAsync(Device device)
        {
            return _devices[device];
        }
    }
}
