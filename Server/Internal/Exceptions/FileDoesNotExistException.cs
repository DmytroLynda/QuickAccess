using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Internal.Exceptions
{
    internal class FileDoesNotExistException : Exception
    {
        public FileDoesNotExistException(string message) : base(message)
        {
        }
    }
}
