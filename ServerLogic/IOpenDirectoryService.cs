using DomainEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerLogic
{
    public interface IOpenDirectoryService
    {
        Task<List<Path>> OpenDirectoryAsync(DirectoryPath directory);
    }
}