using System;

namespace ServerInterface.Internal.Exceptions
{
    internal class RequestHandlerDoesNotExistException : Exception
    {
        public RequestHandlerDoesNotExistException(string message) : base(message)
        {
        }
    }
}
