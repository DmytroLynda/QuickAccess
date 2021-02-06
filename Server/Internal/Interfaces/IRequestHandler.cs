using System.Threading.Tasks;

namespace ServerInterface.Internal.Interfaces
{
    internal interface IRequestHandler
    {
        Task<byte[]> HandleAsync(byte[] data);
    }
}
