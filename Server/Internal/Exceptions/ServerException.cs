using System;

namespace Server.Internal.Exceptions
{
    internal class ServerException : Exception
    {
        public ServerException(Exception innerException) : base(string.Empty, innerException)
        { }
    }
}
