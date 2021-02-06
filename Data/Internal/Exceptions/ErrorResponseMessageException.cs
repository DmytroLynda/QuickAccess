using System;

namespace Data.Internal.Exceptions
{
    internal class ErrorResponseMessageException : Exception
    {
        public ErrorResponseMessageException(Exception innerException) : base(string.Empty, innerException)
        { }
    }
}
