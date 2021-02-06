using ClientLogic.ExternalInterfaces;
using DomainEntities;
using Microsoft.Extensions.Logging;

namespace ClientLogic.Internal
{
    internal class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> _logger;
        private readonly ITrackerContext _tracker;

        public AuthenticationService(ILogger<AuthenticationService> logger, ITrackerContext tracker)
        {
            _logger = logger;
            _tracker = tracker;
        }

        public bool LogIn(User user, Device device)
        {
            return _tracker.LogIn(user, device);
        }

        public bool Register(User user, Device device)
        {
            return _tracker.Register(user, device);
        }
    }
}
