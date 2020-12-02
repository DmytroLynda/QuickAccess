using DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClientLogic
{
    public interface IFileService
    {
        public void DownloadFile(Device from, Path file);
        public string GetFileInfo(Device from, Path file);
        public Task<List<Path>> ShowDirectoryAsync(Device from, Path directory);
    }
}
