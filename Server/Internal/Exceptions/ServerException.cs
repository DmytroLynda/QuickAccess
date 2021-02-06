using System;

namespace ServerInterface.Internal.Exceptions
{
    public class ServerException : Exception
    {
        public ServerException(Exception innerException) : base(innerException.Message, innerException)
        { }
    }
}
