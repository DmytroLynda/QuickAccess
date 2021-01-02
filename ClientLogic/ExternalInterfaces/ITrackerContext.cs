using DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLogic.ExternalInterfaces
{
    public interface ITrackerContext
    {
        Task<List<Device>> GetDevicesAsync();
    }
}
