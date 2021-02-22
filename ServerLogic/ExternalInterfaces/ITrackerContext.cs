using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.ExternalInterfaces
{
    public interface ITrackerContext
    {
        Task<bool> IsValid(string address, int port);
    }
}
