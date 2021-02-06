using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Interfaces
{
    internal interface IConfigurationWindowController
    {
        Task<UserSettingsViewModel> GetUserSettingsAsync();
        Task SetUserSettingsAsync(UserSettingsViewModel userSettings);
    }
}
