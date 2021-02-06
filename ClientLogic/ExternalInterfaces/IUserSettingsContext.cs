using DomainEntities;
using System.Threading.Tasks;

namespace ClientLogic.ExternalInterfaces
{
    public interface IUserSettingsContext
    {
        Task<UserSettings> GetUserSettingsAsync();
        Task SetUserSettingsAsync(UserSettings userSettings);
    }
}
