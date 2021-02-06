using ServerInterface.Enums;

namespace ServerInterface.DTOs
{
    public class ResponseDTO
    {
        public ResponseType Type { get; set; }
        public byte[] Data { get; set; }
    }
}
