using Server.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.DTOs
{
    public class RequestDTO
    {
        public Query Query { get; set; }
        public byte[] Request { get; set; }
    }
}
