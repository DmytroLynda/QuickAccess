using System;

namespace Data.Internal.Exceptions
{
    internal class AuthenticationException : Exception
    {
        public AuthenticationException(string message) : base(message)
        { }
    }
}
