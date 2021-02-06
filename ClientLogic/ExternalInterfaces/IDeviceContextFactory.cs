using DomainEntities;
using System.Threading.Tasks;

namespace ClientLogic.ExternalInterfaces
{
    public interface IDeviceContextFactory
    {
        Task<IDeviceContext> GetDeviceContext(Device device);
    }
}
