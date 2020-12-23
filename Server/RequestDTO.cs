using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    public class RequestDTO
    {
        public Query Query { get; set; }
        public byte[] Data { get; set; }
    }
}
