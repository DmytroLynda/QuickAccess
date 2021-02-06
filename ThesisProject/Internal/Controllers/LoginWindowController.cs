using AutoMapper;
using ClientLogic;
using DomainEntities;
using System.Threading.Tasks;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Controllers
{
    internal class LoginWindowController : ILoginWindowController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        private readonly IUserSettingsController _userSettingsController;

        public LoginWindowController(IAuthenticationService authenticationService, IMapper mapper, IUserSettingsController userSettingsController)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _userSettingsController = userSettingsController;
        }

        public async Task<UserSettingsViewModel> GetUserSettingsAsync()
        {
            return await _userSettingsController.GetUserSettingsAsync();
        }

        public bool LogIn(UserViewModel userViewModel, DeviceViewModel currentDeviceViewModel)
        {
            var user = _mapper.Map<User>(userViewModel);
            var currentDevice = _mapper.Map<Device>(currentDeviceViewModel);

            return _authenticationService.LogIn(user, currentDevice);
        }

        public bool Register(UserViewModel newUserViewModel, DeviceViewModel currentDeviceViewModel)
        {
            var newUser = _mapper.Map<User>(newUserViewModel);
            var currentDevice = _mapper.Map<Device>(currentDeviceViewModel);

            return _authenticationService.Register(newUser, currentDevice);
        }
    }
}
