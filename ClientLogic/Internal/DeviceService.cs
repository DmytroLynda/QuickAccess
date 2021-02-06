using ClientLogic.ExternalInterfaces;
using DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLogic.Internal
{
    internal class DeviceService : IDeviceService
    {
        private readonly ITrackerContext _trackerContext;

        public DeviceService(ITrackerContext trackerContext)
        {
            _trackerContext = trackerContext;
        }

        public async Task<List<Device>> GetDevicesAsync(User user, Device device)
        {
            return await _trackerContext.GetDevicesAsync(user, device);
        }
    }
}
