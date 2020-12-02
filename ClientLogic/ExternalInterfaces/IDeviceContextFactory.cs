using DomainEntities;

namespace ClientLogic.ExternalInterfaces
{
    public interface IDeviceContextFactory
    {
        IDeviceContext GetDeviceContext(Device device);
    }
}
