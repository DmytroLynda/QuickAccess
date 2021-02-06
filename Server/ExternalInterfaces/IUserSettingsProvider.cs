using DomainEntities;
using System.Threading.Tasks;

namespace ServerInterface.ExternalInterfaces
{
    public interface IUserSettingsProvider
    {
        Task<UserSettings> GetUserSettingsAsync();
    }
}
