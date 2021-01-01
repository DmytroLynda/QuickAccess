using ClientLogic.DataTypes;
using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLogic
{
    public interface IDeviceService
    {
        Task<List<DeviceDTO>> GetDevices();
    }
}
