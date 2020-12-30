using System;

namespace Data.Internal
{
    internal class ErrorResponseMessageException : Exception
    {
        public ErrorResponseMessageException(Exception innerException) : base(string.Empty, innerException)
        { }
    }
}
