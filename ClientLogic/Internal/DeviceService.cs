using AutoMapper;
using ClientLogic.DataTypes;
using ClientLogic.ExternalInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLogic.Internal
{
    internal class DeviceService : IDeviceService
    {
        private readonly ITrackerContext _trackerContext;
        private readonly IMapper _mapper;

        public DeviceService(ITrackerContext trackerContext, IMapper mapper)
        {
            _trackerContext = trackerContext;
            _mapper = mapper;
        }

        public async Task<List<DeviceDTO>> GetDevices()
        {
            var devices = await _trackerContext.GetDevicesAsync();
            return _mapper.Map<List<DeviceDTO>>(devices);
        }
    }
}
