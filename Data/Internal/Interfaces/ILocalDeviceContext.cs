using Data.Internal.DataTypes;
using DomainEntities;
using System.Threading.Tasks;

namespace Data.Internal.Interfaces
{
    internal interface ILocalDeviceContext
    {
        Task SaveNewFileChunk(File file);
        Task SaveNextFileChunk(File file);
    }
}