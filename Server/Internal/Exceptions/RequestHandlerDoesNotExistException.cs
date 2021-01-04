using System;

namespace Server.Internal.Exceptions
{
    internal class RequestHandlerDoesNotExistException : Exception
    {
        public RequestHandlerDoesNotExistException(string message) : base(message)
        {
        }
    }
}
