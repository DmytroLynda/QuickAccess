using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public interface IRequestHandlerFactory
    {
        IRequestHandler Create(Query query);
    }
}
