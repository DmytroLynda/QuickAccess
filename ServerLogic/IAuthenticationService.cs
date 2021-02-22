using System.Net;
using System.Threading.Tasks;

namespace ServerLogic
{
    public interface IAuthenticationService
    {
        Task AuthenticateAsync(string address, int port);
    }
}
