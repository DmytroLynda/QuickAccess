using Server.Enums;

namespace Server.DTOs
{
    public class ResponseDTO
    {
        public ResponseType Type { get; set; }
        public byte[] Data { get; set; }
    }
}
