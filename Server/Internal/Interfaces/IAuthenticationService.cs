using System.Net;

namespace ServerInterface.Internal.Interfaces
{
    public interface IAuthenticationService
    {
        void Authenticate(EndPoint authenticated);
    }
}
