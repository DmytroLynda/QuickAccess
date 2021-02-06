using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Controllers
{
    internal class ConfigurationWindowController : IConfigurationWindowController
    {
        private readonly IUserSettingsController _userSettingsController;

        public ConfigurationWindowController(IUserSettingsController userSettingsController)
        {
            _userSettingsController = userSettingsController;
        }

        public async Task<UserSettingsViewModel> GetUserSettingsAsync()
        {
            return await _userSettingsController.GetUserSettingsAsync();
        }

        public async Task SetUserSettingsAsync(UserSettingsViewModel userSettings)
        {
            await _userSettingsController.SetUserSettingsAsync(userSettings);
        }
    }
}
