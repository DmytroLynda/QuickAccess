using DomainEntities;
using System;
using System.Threading.Tasks;

namespace Data.Internal.Interfaces
{
    internal interface ITrackerService
    {
        Task<Uri> GetDeviceUriAsync(Device device);
    }
}
