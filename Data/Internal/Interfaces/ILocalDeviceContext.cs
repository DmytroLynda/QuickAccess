using DomainEntities;
using Server.DTOs.ResponseTypes;
using System.Threading.Tasks;

namespace Data.Internal.Interfaces
{
    public interface ILocalDeviceContext
    {
        Task SaveFileAsync(FileDTO file);
    }
}