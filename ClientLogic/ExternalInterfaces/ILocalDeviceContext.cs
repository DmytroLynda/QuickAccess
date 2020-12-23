using DomainEntities;

namespace ClientLogic.ExternalInterfaces
{
    public interface ILocalDeviceContext
    {
        void SaveFile(byte[] file);
    }
}