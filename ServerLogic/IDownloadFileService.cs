using DomainEntities;
using System.Threading.Tasks;

namespace ServerLogic
{
    public interface IDownloadFileService
    {
        Task<FileChunk> DownloadFileAsync(FileRequest part, int chunkSizeInMegabytes);
    }
}