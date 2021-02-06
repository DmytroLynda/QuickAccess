﻿using ServerInterface.Enums;

namespace ServerInterface.DTOs
{
    public class RequestDTO
    {
        public Query Query { get; set; }
        public byte[] Data { get; set; }
    }
}
