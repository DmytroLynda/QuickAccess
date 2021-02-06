using DomainEntities;
using System.Threading.Tasks;

namespace ClientLogic
{
    public interface IUserSettingsService
    {
        Task<UserSettings> GetUserSettingsAsync();
        Task SetUserSettingsAsync(UserSettings userSettings);
    }
}
