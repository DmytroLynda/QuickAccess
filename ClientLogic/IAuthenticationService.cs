using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLogic
{
    public interface IAuthenticationService
    {
        bool LogIn(User user, Device device);
        bool Register(User user, Device device);
    }
}
