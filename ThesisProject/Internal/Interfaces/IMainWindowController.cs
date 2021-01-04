using System.Collections.Generic;
using System.Threading.Tasks;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Interfaces
{
    internal interface IMainWindowController
    {
        Task<List<DeviceViewModel>> GetDevicesAsync();
        Task<List<PathViewModel>> GetDirectoryAsync(DirectoryPathViewModel directory, DeviceViewModel device);
        Task DownloadFileAsync(FilePathViewModel filePath);
        Task<FileInfoViewModel> GetFileInfoAsync(FilePathViewModel filePath);
    }
}
