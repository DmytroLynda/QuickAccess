using System.Threading.Tasks;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Interfaces
{
    internal interface ILoginWindowController
    {
        bool LogIn(UserViewModel user, DeviceViewModel currentDevice);
        bool Register(UserViewModel newUser, DeviceViewModel currentDevice);
        Task<UserSettingsViewModel> GetUserSettingsAsync();
    }
}
