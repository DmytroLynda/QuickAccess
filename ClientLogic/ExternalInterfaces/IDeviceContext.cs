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
        Task<List<DirectoryPath>> OpenFolderAsync(DirectoryPath folder);
        Task<File> DownloadFileAsync(FilePath file);
    }
}
