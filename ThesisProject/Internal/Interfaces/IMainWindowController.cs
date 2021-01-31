using System.Collections.Generic;
using System.Threading.Tasks;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Interfaces
{
    internal interface IMainWindowController
    {
        Task<List<DeviceViewModel>> GetDevicesAsync(UserViewModel userViewModel, DeviceViewModel deviceViewModel);
        Task<List<PathViewModel>> GetDirectoryAsync(DirectoryPathViewModel directory, DeviceViewModel device);
        Task DownloadFileAsync(DeviceViewModel deviceViewModel, FilePathViewModel filePath);
        Task<FileInfoViewModel> GetFileInfoAsync(FilePathViewModel filePath);
    }
}
