﻿using Server.Enums;

namespace Server.DTOs
{
    public class RequestDTO
    {
        public Query Query { get; set; }
        public byte[] Data { get; set; }
    }
}
