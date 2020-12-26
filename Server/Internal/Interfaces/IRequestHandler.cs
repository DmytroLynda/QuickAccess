using System.Threading.Tasks;

namespace Server
{
    internal interface IRequestHandler
    {
        Task<byte[]> HandleAsync(byte[] data);
    }
}
