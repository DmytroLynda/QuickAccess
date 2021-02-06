using System.Net;

namespace Server.Internal.Interfaces
{
    public interface IAuthenticationService
    {
        void Authenticate(EndPoint authenticated);
    }
}
