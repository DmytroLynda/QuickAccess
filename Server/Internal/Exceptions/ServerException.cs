using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Internal.Exceptions
{
    internal class ServerException : Exception
    {
        public ServerException(Exception innerException) : base(string.Empty, innerException)
        { }
    }
}
