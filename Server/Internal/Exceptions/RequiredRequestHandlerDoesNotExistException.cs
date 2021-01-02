using System;

namespace Server.Internal.Exceptions
{
    internal class RequiredRequestHandlerDoesNotExistException : Exception
    {
        public RequiredRequestHandlerDoesNotExistException(string message) : base(message)
        {
        }
    }
}
