using System.Threading.Tasks;

namespace ClientLogic.ExternalInterfaces
{
    public interface ILocalDeviceContext
    {
        Task SaveFileAsync(byte[] file, string name);
    }
}