using Server.Internal.Exceptions;
using System;

namespace Server.DTOs.ResponseTypes
{
    public class ErrorDTO
    {
        public ServerException Exception { get; set; }
    }
}
