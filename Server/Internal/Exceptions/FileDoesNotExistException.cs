using System;

namespace ServerInterface.Internal.Exceptions
{
    internal class FileDoesNotExistException : Exception
    {
        public FileDoesNotExistException(string message) : base(message)
        {
        }
    }
}
