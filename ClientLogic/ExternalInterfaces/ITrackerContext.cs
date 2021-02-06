using DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLogic.ExternalInterfaces
{
    public interface ITrackerContext
    {
        Task<List<Device>> GetDevicesAsync(User user, Device device);
        bool LogIn(User user, Device device);
        bool Register(User user, Device device);
    }
}
