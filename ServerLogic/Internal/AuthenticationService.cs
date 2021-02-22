using ServerLogic.Exceptions;
using ServerLogic.ExternalInterfaces;
using System.Net;
using System.Threading.Tasks;

namespace ServerLogic.Internal
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly ITrackerContext _tracker;

        public AuthenticationService(ITrackerContext tracker)
        {
            _tracker = tracker;
        }

        public async Task AuthenticateAsync(string address, int port)
        {
            var isValid = await _tracker.IsValid(address, port);

            if (!isValid)
            {
                throw new AuthenticationException();
            }
        }
    }
}
