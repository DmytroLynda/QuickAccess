using ServerInterface.Internal.Exceptions;

namespace ServerInterface.DTOs.ResponseTypes
{
    public class ErrorDTO
    {
        public ServerException Exception { get; set; }
    }
}
