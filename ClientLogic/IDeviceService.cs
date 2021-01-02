using DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLogic
{
    public interface IDeviceService
    {
        Task<List<Device>> GetDevicesAsync();
    }
}
