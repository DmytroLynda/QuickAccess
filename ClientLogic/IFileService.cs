using DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLogic
{
    public interface IFileService
    {
        public Task<FileInfo> GetFileInfoAsync(Device from, FilePath file);
        public Task DownloadFileAsync(Device from, FilePath file);
        public Task<List<Path>> ShowDirectoryAsync(Device from, DirectoryPath directory);
    }
}
