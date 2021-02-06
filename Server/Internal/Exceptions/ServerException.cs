using System;
using System.Runtime.Serialization;

namespace Server.Internal.Exceptions
{
    public class ServerException : Exception
    {
        public ServerException(Exception innerException) : base(innerException.Message, innerException)
        { }
    }
}
