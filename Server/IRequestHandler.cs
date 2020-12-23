using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public interface IRequestHandler
    {
        byte[] Handle(byte[] data);
    }
}
