using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface IServer
    {
        Task StartAsync();
    }
}
