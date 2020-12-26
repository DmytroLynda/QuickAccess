using DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLogic.ExternalInterfaces
{
    public interface IDeviceContext
    {
        Task<List<Path>> OpenFolderAsync(DirectoryPath folder);
        Task<File> DownloadFileAsync(FilePath file);
        Task<FileInfo> GetFileInfoAsync(FilePath file);
    }
}
