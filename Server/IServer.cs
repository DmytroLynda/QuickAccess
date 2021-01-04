using System.Threading.Tasks;

namespace Server
{
    public interface IServer
    {
        Task StartAsync();
        void Start();
    }
}
