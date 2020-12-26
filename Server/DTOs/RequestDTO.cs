using Server.Enums;

namespace Server.DTOs
{
    public class RequestDTO
    {
        public Query Query { get; set; }
        public byte[] Request { get; set; }
    }
}
