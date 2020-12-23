using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLogic.ExternalInterfaces
{
    public interface IDeviceContext
    {
        Task<List<Path>> OpenFolderAsync(DirectoryPath folder);
        Task<byte[]> DownloadFileAsync(FilePath file);
        Task<FileInfo> GetFileInfoAsync(FilePath file);
    }
}
