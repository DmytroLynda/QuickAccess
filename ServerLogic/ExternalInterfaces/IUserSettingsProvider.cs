using DomainEntities;
using System.Threading.Tasks;

namespace ServerLogic.ExternalInterfaces
{
    public interface IUserSettingsProvider
    {
        Task<UserSettings> GetUserSettingsAsync();
    }
}
