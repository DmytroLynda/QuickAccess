using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public interface IRequestHandler
    {
        Task<byte[]> HandleAsync(byte[] data);
    }
}
