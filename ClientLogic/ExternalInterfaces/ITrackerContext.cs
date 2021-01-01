using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLogic.ExternalInterfaces
{
    public interface ITrackerContext
    {
        Task<List<Device>> GetDevicesAsync();
    }
}
