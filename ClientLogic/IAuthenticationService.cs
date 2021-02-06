using DomainEntities;

namespace ClientLogic
{
    public interface IAuthenticationService
    {
        bool LogIn(User user, Device device);
        bool Register(User user, Device device);
    }
}
