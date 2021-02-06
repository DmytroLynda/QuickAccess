using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLogic.ExternalInterfaces
{
    public interface IUserSettingsContext
    {
        Task<UserSettings> GetUserSettingsAsync();
        Task SetUserSettingsAsync(UserSettings userSettings);
    }
}
