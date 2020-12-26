using System;

namespace Data.Internal
{
    internal class ErrorResponseMessageException : Exception
    {
        public ErrorResponseMessageException(string message) : base(message)
        {
        }
    }
}
