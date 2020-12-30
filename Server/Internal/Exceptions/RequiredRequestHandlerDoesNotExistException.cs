using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Internal.Exceptions
{
    internal class RequiredRequestHandlerDoesNotExistException : Exception
    {
        public RequiredRequestHandlerDoesNotExistException(string message) : base(message)
        {
        }
    }
}
