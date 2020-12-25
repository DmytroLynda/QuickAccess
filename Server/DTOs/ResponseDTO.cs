using Server.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DTOs
{
    public class ResponseDTO
    {
        public ResponseType Type { get; set; }
        public byte[] Data { get; set; }
    }
}
