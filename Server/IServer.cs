using System.Threading.Tasks;

namespace Server
{
    public interface IServer
    {
        Task StartAsync(string serverUri);
    }
}
