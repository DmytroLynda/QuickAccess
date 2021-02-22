using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.ExternalInterfaces
{
    public interface IFileContext
    {
        Task<FileChunk> DownloadFileAsync(FileRequest part, int chunkSizeInMegabytes);
        Task<FileInfo> GetFileInfoAsync(FilePath path);
        Task<List<Path>> OpenDirectoryAsync(DirectoryPath path);
    }
}
