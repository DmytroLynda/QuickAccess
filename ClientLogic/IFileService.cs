using DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLogic
{
    public interface IFileService
    {
        public string GetFileInfo(Device from, FilePath file);
        public Task DownloadFileAsync(Device from, FilePath file);
        public Task<List<DirectoryPath>> ShowDirectoryAsync(Device from, DirectoryPath directory);
    }
}
