using System.Threading.Tasks;

namespace ServerInterface
{
    public interface IServer
    {
        Task StartAsync();
        void Start();
    }
}
