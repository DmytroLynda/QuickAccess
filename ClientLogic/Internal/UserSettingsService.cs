using ClientLogic.ExternalInterfaces;
using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLogic.Internal
{
    internal class UserSettingsService : IUserSettingsService
    {
        private readonly IUserSettingsContext _settingsContext;

        public UserSettingsService(IUserSettingsContext settingsContext)
        {
            _settingsContext = settingsContext;
        }

        public async Task<UserSettings> GetUserSettingsAsync()
        {
            return await _settingsContext.GetUserSettingsAsync();
        }

        public async Task SetUserSettingsAsync(UserSettings userSettings)
        {
            await _settingsContext.SetUserSettingsAsync(userSettings);
        }
    }
}
