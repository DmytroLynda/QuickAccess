using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackerWebApi.Entities;
using TrackerWebApi.Model;

namespace TrackerWebApi.Services
{
    public interface IUserService
    {
        AuthenticationResponse Authenticate(AuthenticationRequest request);
        RegistrationResponse Register(RegistrationRequest request);
        IEnumerable<User> GetAll();
        User GetById(int userId);
    }
}
