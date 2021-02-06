using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ExternalInterfaces
{
    public interface IUserSettingsProvider
    {
        Task<UserSettings> GetUserSettingsAsync();
    }
}
