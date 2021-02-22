using DomainEntities;
using System.Threading.Tasks;

namespace ServerLogic
{
    public interface IFileInfoService
    {
        Task<FileInfo> GetFileInfoAsync(FilePath file);
    }
}