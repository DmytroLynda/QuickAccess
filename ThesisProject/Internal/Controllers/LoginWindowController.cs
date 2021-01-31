using AutoMapper;
using ClientLogic;
using DomainEntities;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Controllers
{
    internal class LoginWindowController : ILoginWindowController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;

        public LoginWindowController(IAuthenticationService authenticationService, IMapper mapper)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
        }

        public bool LogIn(UserViewModel userViewModel, DeviceViewModel currentDeviceViewModel)
        {
            var user = _mapper.Map<User>(userViewModel);
            var currentDevice = _mapper.Map<Device>(currentDeviceViewModel);

            return _authenticationService.LogIn(user, currentDevice);
        }
    }
}
